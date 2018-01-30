using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LUSSIS_Backend;
using LUSSIS_Backend.controller;

public partial class Store_Clerk_AdjustmentVoucherList : System.Web.UI.Page
{
    List<AdjustmentVoucher> pendingAdjustmentVouchers;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Load GridView
            BindGrid();
        }
    }

    protected void PendingAdjustmentVoucherGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int aVNo;

        if (e.CommandName == "Details")
        {
            aVNo = Convert.ToInt32(e.CommandArgument);
            Session["AVNo"] = aVNo;
            Response.Redirect("AdjustmentVoucherDetails.aspx");
        }
    }

    protected void PendingAdjustmentVoucherGridView_Delete(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int aVNo = Int32.Parse(PendingAdjustmentVoucherGridView.Rows[e.RowIndex].Cells[0].Text);
            AVController.DeleteAV(aVNo);
            BindGrid();
            Session["AVProcessed"] = aVNo;
        }
        catch (Exception exception)
        {
            Session["Error"] = " " + exception.Message;
        }
    }

    private void BindGrid()
    {
        // Get Pending Adjustment Vouchers belonging to this User
        pendingAdjustmentVouchers = AVController.GetPendingAdjustmentVouchersByIssueEmp(Profile.EmpNo);

        // Bind GridView
        PendingAdjustmentVoucherGridView.DataSource = pendingAdjustmentVouchers.Select(aV => new
        {
            AvNo = aV.AvNo,
            DateIssued = aV.DateIssued,
            ApproveEmpName = aV.Employee1.EmpName,
            Status = aV.Status
        });
        PendingAdjustmentVoucherGridView.DataBind();
    }
}