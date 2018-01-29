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
    // ATTRIBUTES

    protected int aVNo;
    protected AdjustmentVoucher aV;

    // EVENTS

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // If no AVNo is passed, go back to List Page
            if (Session["AVNo"] == null)
            {
                GoToAdjustmentVoucherListPage();
            }
            else
            {
                // Set DataGrid
                BindGrid();
            }
        }

        // Set Everytime
        aVNo = (int)Session["AVNo"];
        aV = AVController.GetAdjustmentVoucher(aVNo);
    }

    protected void Button_Click(object sender, EventArgs e)
    {
        Button buttonPressed = (Button)sender;
        aVNo = (int)Session["AVNo"];

        // Delete Button
        if (buttonPressed.CommandArgument == "Delete")
        {
            // Delete Adjustment Voucher
            try
            {
                AVController.DeleteAV(aVNo);
                Session["AVProcessed"] = (int)Session["AVNo"];
            }
            catch (Exception exception)
            {
                Session["Error"] = "An Error Has Occured: " + exception.Message;
            }
            if (Session["Error"] == null)
            {
                GoToAdjustmentVoucherListPage();
            }
        }
    }

    protected void OnRowEditing(object sender, GridViewEditEventArgs e)
    {
        AVDetailsGridView.EditIndex = e.NewEditIndex;
        BindGrid();
    }

    protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        // Get Selected Row
        GridViewRow row = AVDetailsGridView.Rows[e.RowIndex];

        try
        {
            string itemNo = (row.FindControl("LabelItemNo") as Label).Text;
            int qty = Int32.Parse((string)e.NewValues["Qty"]);
            string reason = (string)e.NewValues["Reason"];
            AVController.UpdateAVDetailQty(aVNo, itemNo, qty, reason);
            AVDetailsGridView.EditIndex = -1;
            BindGrid();
        }
        catch (Exception exception)
        {
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
        GridViewRow row = AVDetailsGridView.Rows[e.RowIndex];
        string itemNo = (row.FindControl("LabelItemNo") as Label).Text;
        AVController.DeleteAVD(aVNo, itemNo);
        BindGrid();
    }

    // METHODS

    private void GoToAdjustmentVoucherListPage()
    {
        Response.Redirect("AdjustmentVoucherList.aspx");
    }

    private void BindGrid()
    {
        // Get AV Details
        int aVNo = (int)Session["AVNo"];
        List<AdjustmentVoucherDetail> aVDetails = AVController.GetAVDetails(aVNo);

        // Set DataGrid
        AVDetailsGridView.DataSource = aVDetails.Select(
            aVD => new
            {
                ItemNo = aVD.ItemNo,
                ItemDescription = aVD.StationeryCatalogue.Description,
                Qty = aVD.Qty,
                Reason = aVD.Reason
            });

        AVDetailsGridView.DataBind();
    }
}