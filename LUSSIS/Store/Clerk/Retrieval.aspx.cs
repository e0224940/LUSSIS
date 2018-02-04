using LUSSIS_Backend;
using LUSSIS_Backend.controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    List<BigRow> data = new List<BigRow>();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            updateDropDownList();
            Session["RetrievalNo"] = -1;
            Session["Editable"] = false;
        }
    }

    private void updateDropDownList()
    {
        // Set the dropdown list
        SelectRetrevialDropDownList.DataTextField = "Text";
        SelectRetrevialDropDownList.DataValueField = "Value";
        var items = RetrievalFormController
            .GetAllRetrievals()
            .Select(ret =>
               new
               {
                   Text = "#" + ret.RetrievalNo + " (" + String.Format("{0:dd/MMM/yyyy}", ret.Date) + ")",
                   Value = ret.RetrievalNo
               }).ToList();
        items.Insert(0, new
        {
            Text = "Select Retrieval",
            Value = -1
        });
        SelectRetrevialDropDownList.DataSource = items;
        SelectRetrevialDropDownList.DataBind();
    }

    private void displayRetreivalForm(int retrievalNo)
    {
        // Set Session Value
        Session["RetrievalNo"] = retrievalNo;

        // Set Editable
        Session["Editable"] = RetrievalFormController.IsRetrievalFormEditable(retrievalNo);

        // Get Retreival Details
        var retreivalDetails = RetrievalFormController.GetRetrievalDetailsOf(retrievalNo);

        data.Clear();
        // Create the Big and Small Row Objects to display
        {
            // Get the list of items
            var items = retreivalDetails.Select(retDet => retDet.StationeryCatalogue).Distinct().ToList();

            // To track CSS
            bool isAlternate = false;

            // For each item, create the big row and populate the values using retreival details
            foreach (var item in items)
            {
                BigRow temp = new BigRow()
                {
                    Bin = item.Bin,
                    ItemCode = item.ItemNo,
                    Description = item.Description,
                    Needed = 0,
                    Backlog = 0,
                    Reterived = 0,
                    Maximum = item.CurrentQty ?? 0,
                    Breakdown = new List<SmallRow>(),
                    DepartmentCount = 0,
                    CssClass = isAlternate ? "active" : "default",
                };

                var itemSpecificDetails = retreivalDetails.Where(retDet => retDet.ItemNo.Equals(item.ItemNo)).ToList();
                foreach (var detail in itemSpecificDetails)
                {
                    SmallRow smallTemp = new SmallRow()
                    {
                        Department = detail.DeptCode,
                        Needed = detail.Needed ?? 0,
                        Backlog = detail.BackLogQty ?? 0,
                        Actual = detail.Actual ?? 0,
                        CssClass = isAlternate ? "active" : "default",
                        ItemNo = item.ItemNo
                    };

                    temp.Needed += smallTemp.Needed;
                    temp.Backlog += smallTemp.Backlog;
                    temp.Breakdown.Add(smallTemp);
                }

                isAlternate = !isAlternate; // Toggle CSS
                temp.DepartmentCount = temp.Breakdown.Count;
                data.Add(temp);
            }
        }

        // Display the Data
        BigRepeater.DataSource = data;
        BigRepeater.DataBind();

        // Save the Data
        Session["DisplayedData"] = data;
    }

    protected void SelectRetrevialDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int retrievalNo = -1;

        if (int.TryParse(SelectRetrevialDropDownList.SelectedValue, out retrievalNo) && retrievalNo > 0)
        {
            displayRetreivalForm(retrievalNo);
        }
    }

    protected void SaveFormButton_Click(object sender, EventArgs e)
    {
        int retrievalNo = (int)Session["RetrievalNo"];
        List<String> deptCodes = new List<string>();
        List<String> itemNos = new List<string>();
        List<int> actualList = new List<int>();

        if (!IsValid)
        {
            // Page Validation failed somewhere, don't do anything
            return;
        }

        if (Session["RetrievalNo"] == null)
        {
            Session["Error"] = "No Retrieval detected, please select one first.";
            return;
        }

        // Save values to data
        int i = 0, j = 0;
        bool allZeroes = true;
        data = (List<BigRow>)Session["DisplayedData"];
        foreach (RepeaterItem item in BigRepeater.Items)
        {
            Repeater SmallRepeater = (Repeater)item.FindControl("SmallRepeater");
            BigRow bigRow = data[i];
            j = 0;
            foreach (RepeaterItem smallItem in SmallRepeater.Items)
            {
                TextBox inputTextBox = (TextBox)smallItem.FindControl("ActualTextBox");
                bigRow.Breakdown[j].Actual = Convert.ToInt32(inputTextBox.Text);
                allZeroes = allZeroes && (bigRow.Breakdown[j].Actual == 0);
                j++;
            }
            i++;
        }

        if (allZeroes)
        {
            Session["Error"] = "At least one item must be retrieved in order to submit.";
            return;
        }

        // Fill the arguments
        foreach (BigRow row in data)
        {

            foreach (SmallRow detail in row.Breakdown)
            {
                deptCodes.Add(detail.Department);
                itemNos.Add(row.ItemCode);
                actualList.Add(detail.Actual);
            }
        }

        // Submit the arguments to the controller
        if (RetrievalController.SubmitRetrieval(
            (int)Session["RetrievalNo"],
            deptCodes,
            itemNos,
            actualList
            ))
        {
            Session["RetrievalProcessed"] = "Retrieval #" + (int)Session["RetrievalNo"] + " Submitted.";
        }
        else
        {
            Session["Error"] = "An error has occured. Please try again after sometime.";
        }

        displayRetreivalForm((int)Session["RetrievalNo"]);
    }

    protected void CreateNewRetrievalButton_Click(object sender, EventArgs e)
    {
        // Create a new Retrieval
        int newRetrievalFormNumber = RetrievalFormController.CreateNewRetrieval();
        displayRetreivalForm(newRetrievalFormNumber);
        updateDropDownList();
    }

    protected void BigRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        // Display if empty
        if (BigRepeater.Items.Count < 1)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lblFooter = (Label)e.Item.FindControl("lblEmptyData");
                lblFooter.Visible = true;
            }
        }
    }

    protected void ActualTextBoxValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {
        String departmentCode;
        String itemNo;
        int value;
        bool result = false;
        int sum;
        CustomValidator customValidator = (CustomValidator)source;
        int i = customValidator.Controls.Count - 2;
        String infoString = customValidator.ErrorMessage;

        if (int.TryParse(args.Value, out value))
        {
            departmentCode = customValidator.ToolTip;
            itemNo = customValidator.ErrorMessage;

            // Get the Big row that this field belongs to
            foreach (RepeaterItem item in BigRepeater.Items)
            {
                HiddenField bigItemNoHiddenField = (HiddenField)item.FindControl("BigItemNoHiddenField");
                if (bigItemNoHiddenField.Value.Equals(itemNo))
                {
                    // Find the current sum of items retrieved
                    sum = 0;
                    Repeater SmallRepeater = (Repeater)item.FindControl("SmallRepeater");
                    foreach (RepeaterItem smallItem in SmallRepeater.Items)
                    {
                        TextBox inputTextBox = (TextBox)smallItem.FindControl("ActualTextBox");
                        if (int.TryParse(inputTextBox.Text, out value))
                        {
                            sum += value;
                        }
                    }

                    // Check if sum is greater than stock cart value
                    result = RetrievalFormController.IsThereEnoughItemsInStock(sum, itemNo);

                    break;
                }
            }

            // If an invalid sum was found, then display message
            if (!result)
            {
                customValidator.Text = "There are only " + RetrievalFormController.GetQuantityInStock(itemNo) + " in stock";
            }
            else
            {
                customValidator.Text = "";
            }
        }

        customValidator.ErrorMessage = infoString;
        args.IsValid = result;
    }    
}

public class BigRow
{
    public String Bin { get; set; }
    public String ItemCode { get; set; }
    public String Description { get; set; }
    public int Needed { get; set; }
    public int Backlog { get; set; }
    public int Reterived { get; set; }
    public int Maximum { get; set; }
    public List<SmallRow> Breakdown { get; set; }
    public int DepartmentCount { get; set; }
    public String CssClass { get; set; }
}

public class SmallRow
{
    public String Department { get; set; }
    public int Needed { get; set; }
    public int Backlog { get; set; }
    public int Actual { get; set; }
    public int TotalNeeded
    {
        get
        {
            return Backlog + Needed;
        }
    }
    public String CssClass { get; set; }
    public String Message { get; set; }
    public String ItemNo { get; set; }
}