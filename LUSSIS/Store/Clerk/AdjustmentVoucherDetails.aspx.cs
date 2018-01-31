using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LUSSIS_Backend;
using LUSSIS_Backend.controller;

public partial class Store_Clerk_AdjustmentVoucherDetails : System.Web.UI.Page
{
    protected int aVNo;
    protected AdjustmentVoucher aV;
    protected List<AdjustmentVoucherDetail> aVDetails;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // If no AVNo is passed
            if (Session["AVNo"] == null)
            {
                // Go back to AdjustmentVoucherListPage
                GoToAdjustmentVoucherListPage();
            }
        }

        // Set page attributes
        SetPageAttributes();

        if (!IsPostBack)
        {
            // Bind gridview
            BindGrid();
        }
    }

    protected void Button_Click(object sender, EventArgs e)
    {
        try
        {
            Button buttonPressed = (Button)sender;

            if (buttonPressed.CommandArgument == "Delete")
            {
                // Delete AV
                AVController.DeleteAV(aVNo);
                Session["AVProcessed"] = (int)Session["AVNo"];
                GoToAdjustmentVoucherListPage();
            }
        }
        catch (Exception exception)
        {
            // Alert user of error
            Session["Error"] = "An Error Has Occured: " + exception.Message;
        }
    }

    protected void OnRowEditing(object sender, GridViewEditEventArgs e)
    {
        AVDetailsGridView.EditIndex = e.NewEditIndex;
        BindGrid();
    }

    protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            // Get selected row
            GridViewRow row = AVDetailsGridView.Rows[e.RowIndex];

            // Get page data
            string itemNo = (row.FindControl("LabelItemNo") as Label).Text;
            int qty = Int32.Parse((string)e.NewValues["Qty"]);
            string reason = (string)e.NewValues["Reason"];

            // Update AVD (qty & reason)
            AVController.UpdateAVDetailQty(aVNo, itemNo, qty, reason);

            // Update page
            AVDetailsGridView.EditIndex = -1;
            SetPageAttributes();
            BindGrid();
        }
        catch (Exception exception)
        {
            // Alert user of error
            Session["Error"] = "An Error Has Occured: " + exception.Message;
        }
    }

    protected void OnRowCancelingEdit(object sender, EventArgs e)
    {
        AVDetailsGridView.EditIndex = -1;
        BindGrid();
    }

    protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            // Get selected row
            GridViewRow row = AVDetailsGridView.Rows[e.RowIndex];

            // Get page data
            string itemNo = (row.FindControl("LabelItemNo") as Label).Text;

            // Delete AVD
            AVController.DeleteAVD(aVNo, itemNo);

            // UpdatePage
            SetPageAttributes();
            BindGrid();
        }
        catch (Exception exception)
        {
            // Alert user of error
            Session["Error"] = "An Error Has Occured: " + exception.Message;
        }
    }

    private void GoToAdjustmentVoucherListPage()
    {
        Response.Redirect("AdjustmentVoucherList.aspx");
    }

    private void SetPageAttributes()
    {
        aVNo = (int)Session["AVNo"];
        aV = AVController.GetAdjustmentVoucher(aVNo);
        aVDetails = AVController.GetAVDetails(aVNo);
    }

    private void BindGrid()
    {
        // Set DataGrid
        AVDetailsGridView.DataSource = aVDetails.Select(
            aVD => new
            {
                ItemNo = aVD.ItemNo,
                ItemDescription = aVD.StationeryCatalogue.Description,
                Qty = aVD.Qty,
                UnitPrice = AVController.GetUnitPrice(aVD.ItemNo, aVD.StationeryCatalogue.Supplier1),
                AdjustmentAmount = AVController.GetUnitPrice(aVD.ItemNo, aVD.StationeryCatalogue.Supplier1) * aVD.Qty,
                Reason = aVD.Reason
            });

        AVDetailsGridView.DataBind();
    }
}