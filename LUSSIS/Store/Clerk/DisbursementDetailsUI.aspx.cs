using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LUSSIS_Backend;
using LUSSIS_Backend.model;
using LUSSIS_Backend.controller;

public partial class Store_Clerk_DisbursementDetailsUI : System.Web.UI.Page
{
    // ATTRIBUTES

    int disbursementNo;
    protected Disbursement disbursement;
    List<DisbursementItem> dItemList;

    decimal? pin;
    List<string> itemNoList;
    List<int> receivedQtyList;



    // EVENTS

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Set Page Data
            SetPageData();

            // Load Page
            if (disbursement != null)
            {
                if (disbursement.Status == "Pending")
                {
                    // Pending Page
                    LoadPendingPage();
                }
                else
                {
                    // Completed Page
                    LoadCompletedPage();
                }
            }

        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        // Get Page Data
        GetPageData();

        // Complete Disbursement
        bool completeSuccess = DisbursementController.CompleteDisbursement(disbursementNo, pin, itemNoList, receivedQtyList);

        // Load Success Page || Show Error Message
        if (completeSuccess)
        {
            // Load Success Page
            ShowSuccessLabel();
        }
        else
        {
            // Show Error Message
            MessageLabel.Text = "Error Processing Disbursement";
        }
    }



    // METHODS

    private void SetPageData()
    {
        if (Request.QueryString["DisbursementNo"] != null && Int32.TryParse(Request.QueryString["DisbursementNo"], out disbursementNo))
        {
            // Get Disbursement
            disbursement = DisbursementController.GetDisbursement(disbursementNo);

            if (disbursement != null)
            {
                // Get DItemList
                dItemList = DisbursementController.GetDisbursementItemList(disbursementNo);

                // Save DItemList to Viewstate
                ViewState["dItemList"] = dItemList;
            }
        }
    }

    private void GetPageData()
    {
        itemNoList = new List<string>();
        receivedQtyList = new List<int>();

        // Get Disbursement No
        Int32.TryParse(Request.QueryString["DisbursementNo"], out disbursementNo);

        // Get Disbursement
        disbursement = DisbursementController.GetDisbursement(disbursementNo);

        // Get pin
        pin = Convert.ToDecimal(PinTextBox.Text);

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            // Get itemNo
            string itemNo = (string)GridView1.DataKeys[i].Value;
            itemNoList.Add(itemNo);

            // Get receivedQty
            int receivedQty = Convert.ToInt32(((GridView1.Rows[i].FindControl("GridTextBox1") as TextBox).Text));
            receivedQtyList.Add(receivedQty);
        }
    }

    private void LoadPendingPage()
    {
        // GridView
        GridView1.DataSource = dItemList;
        GridView1.DataBind();

        // Labels
        DepartmentNameLabel.Text = disbursement.Department.DeptName;
        CollectionPointLabel.Text = disbursement.CollectionPoint.CollectionPointDetails;
        DisbursementDateLabel.Text = disbursement.DisbursementDate == null ? "" : ((DateTime)disbursement.DisbursementDate).ToString("dd MMM yyyy");
        StatusLabel.Text = disbursement.Status;

        // TexBoxs
        DeptRepNameTextBox.Text = disbursement.Employee.EmpName;
        DeptRepNoTextBox.Text = disbursement.Employee.EmpNo.ToString();
    }

    private void LoadCompletedPage()
    {
        // Load Pending Page
        LoadPendingPage();

        // Remove Some Controls
        Label7.Visible = false;
        PinTextBox.Visible = false;
        Button1.Visible = false;
    }

    private void ShowSuccessLabel()
    {
        LoadCompletedPage();
        MessageLabel.Text = "Delivery Completed";
    }
}
