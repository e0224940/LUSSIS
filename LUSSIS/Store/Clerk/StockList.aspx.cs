using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LUSSIS_Backend;
using LUSSIS_Backend.controller;

public partial class Store_Clerk_StockList : System.Web.UI.Page
{
    protected List<string> binList = StockController.GetBinList();
    protected List<StationeryCatalogue> stockList;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Set StockList
            stockList = StockController.GetAllStocks();

            // Load BinList
            LoadBinList(binList);

            // Load StockList
            LoadStockList(stockList);
        }
    }

    protected void BinDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedBin = BinDropDownList.SelectedItem.Text;

        // Set StockList
        if (selectedBin != "All")
        {
            stockList = StockController.GetStockListByBin(selectedBin);
        }
        else
        {
            stockList = StockController.GetAllStocks();
        }

        // Load StockList
        LoadStockList(stockList);
    }

    protected void SearchButton_Click(object sender, EventArgs e)
    {
        // Set StockList
        string searchString = SearchTextBox.Text;
        stockList = StockController.GetStockListByDescriptionContain(searchString);

        // Load StockList
        LoadStockList(stockList);
    }

    protected void DetailsButton_Click(object sender, EventArgs e)
    {
        string itemNo = ((Button)sender).CommandArgument;
        Session["StockNo"] = itemNo;
        Response.Redirect("StockDetails.aspx");
    }

    protected void AdjustmentVoucherButton_Click(object sender, EventArgs e)
    {
        // Show Repeater Controls
        Button b = (Button)sender;
        RepeaterItem rI = (RepeaterItem)b.Parent;
        (rI.FindControl("AdjustmentVoucherButton") as Button).Visible = false;
        (rI.FindControl("NewQtyTextBox") as TextBox).Visible = true;
        (rI.FindControl("RemarksTextBox") as TextBox).Visible = true;
        (rI.FindControl("SubmitAdjustmentVoucherButton") as Button).Visible = true;
        (rI.FindControl("CancelAdjustmentVoucherButton") as Button).Visible = true;
    }

    protected void SubmitAdjustmentVoucherButton_Click(object sender, EventArgs e)
    {
        Label qtyOnHandLabel = ((Button)sender).FindControl("QtyOnHandLabel") as Label;
        TextBox newQtyTextBox = ((Button)sender).FindControl("NewQtyTextBox") as TextBox;
        TextBox remarksTextBox = ((Button)sender).FindControl("RemarksTextBox") as TextBox;

        // Get AdjustmentVoucher Data
        string itemNo = ((Button)sender).CommandArgument;
        DateTime dateIssued = DateTime.Today;
        int qty = Int32.Parse(newQtyTextBox.Text) - Int32.Parse(qtyOnHandLabel.Text);
        string reason = remarksTextBox.Text;
        int issueEmpNo = Profile.EmpNo;
        StationeryCatalogue stock = StockController.GetStock(itemNo);
        decimal? amount = OrderController.GetUnitPrice(itemNo, stock.Supplier1) * qty;

        try
        {
            // Submit AdjustmentVoucher
            StockController.SubmitAdjustmentVoucher(itemNo, dateIssued, qty, reason, issueEmpNo);

            // Toggle Control Visibility
            Button b = (Button)sender;
            RepeaterItem rI = (RepeaterItem)b.Parent;
            (rI.FindControl("AdjustmentVoucherButton") as Button).Visible = true;
            (rI.FindControl("NewQtyTextBox") as TextBox).Visible = false;
            (rI.FindControl("RemarksTextBox") as TextBox).Visible = false;
            (rI.FindControl("SubmitAdjustmentVoucherButton") as Button).Visible = false;
            (rI.FindControl("CancelAdjustmentVoucherButton") as Button).Visible = false;

            // Show Success Message
            Session["VoucherProcessed"] = itemNo;
        }
        catch (Exception exception)
        {
            // Show Error Message
            Session["Error"] = "An Error Has Occured: " + exception.Message;
        }
    }

    protected void CancelAdjustmentVoucherButton_Click(object sender, EventArgs e)
    {
        // Toggle Control Visibility
        Button b = (Button)sender;
        RepeaterItem rI = (RepeaterItem)b.Parent;
        (rI.FindControl("AdjustmentVoucherButton") as Button).Visible = true;
        (rI.FindControl("NewQtyTextBox") as TextBox).Visible = false;
        (rI.FindControl("RemarksTextBox") as TextBox).Visible = false;
        (rI.FindControl("SubmitAdjustmentVoucherButton") as Button).Visible = false;
        (rI.FindControl("CancelAdjustmentVoucherButton") as Button).Visible = false;
    }

    private void LoadBinList(List<string> binList)
    {
        binList.Insert(0, "All");
        BinDropDownList.DataSource = binList;
        BinDropDownList.DataBind();
    }

    private void LoadStockList(List<StationeryCatalogue> stockList)
    {
        Repeater1.DataSource = stockList;
        Repeater1.DataBind();
    }
}

public class DisplayableStationeryCatalogue : StationeryCatalogue
{
    public DisplayableStationeryCatalogue(StationeryCatalogue cat)
    {
        this.AdjustmentVoucherDetails = cat.AdjustmentVoucherDetails;

    }

    public bool displayAlert;
}