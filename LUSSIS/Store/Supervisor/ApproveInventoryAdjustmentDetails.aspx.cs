using LUSSIS_Backend;
using Email_Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Store_Supervisor_ApproveInventoryAdjustmentDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LussisEntities context = new LussisEntities();

        int iAV = Convert.ToInt32(Request["IAV"]);
        invAdjLabel.Text = Convert.ToString(iAV);
        var getIAVDetails = ApproveInventoryAdjustmentController.getInvAdjDetails(iAV);
        var EmpName = context.AdjustmentVouchers.Where(x => x.AvNo == iAV).FirstOrDefault().Employee1.EmpName;
        invAdjClerkLabel.Text = EmpName;
        var dateRaised = context.AdjustmentVouchers.Where(x => x.AvNo == iAV).FirstOrDefault().DateIssued.Value;
        dateRaisedText.Text = dateRaised.ToString("dd-MMM-yyyy");
        ApproveInventoryAdjustmentDetailsGridView.DataSource = getIAVDetails;
        ApproveInventoryAdjustmentDetailsGridView.DataBind();
    }

    protected void approveAdjustmentButton_Click(object sender, EventArgs e)
    {
        int iAV = Convert.ToInt32(Request["IAV"]);
        int empNo = Profile.EmpNo;

        ApproveInventoryAdjustmentController.setStatusApprove(iAV);
        ApproveInventoryAdjustmentController.setApprovedBy(iAV, empNo);
        updateStockTxn();

        //send Email, will need to put in controller
        LussisEntities entity = new LussisEntities();
        AdjustmentVoucher currentAV = entity.AdjustmentVouchers.Where(x => x.AvNo == iAV).FirstOrDefault();
        Employee recpt = entity.Employees.Where(x => x.EmpNo == currentAV.IssueEmpNo).FirstOrDefault();
        Employee approver = entity.Employees.Where(x => x.EmpNo == empNo).FirstOrDefault();

        EmailBackend.sendEmailStep(recpt.Email,
            EmailTemplate.GenerateAdjVouchStatusChangedEmailSubject(iAV.ToString(), currentAV.Status),
            EmailTemplate.GenerateAdjVouchStatusChangedEmail(recpt.EmpName, iAV.ToString(), approver.EmpName, currentAV.Status));

        Response.Redirect("~/Store/Supervisor/ApproveInventoryAdjustmentList.aspx");

    }

    protected void rejectAdjustmentButton_Click(object sender, EventArgs e)
    {
        int iAV = Convert.ToInt32(Request["IAV"]);
        var empNo = Profile.EmpNo;
        ApproveInventoryAdjustmentController.setStatusReject(iAV);
        ApproveInventoryAdjustmentController.setApprovedBy(iAV, empNo);
        
        //send Email, will need to put in controller
        LussisEntities entity = new LussisEntities();
        AdjustmentVoucher currentAV = entity.AdjustmentVouchers.Where(x => x.AvNo == iAV).FirstOrDefault();
        Employee recpt = entity.Employees.Where(x => x.EmpNo == currentAV.IssueEmpNo).FirstOrDefault();
        Employee approver = entity.Employees.Where(x => x.EmpNo == empNo).FirstOrDefault();

        EmailBackend.sendEmailStep(recpt.Email,
            EmailTemplate.GenerateAdjVouchStatusChangedEmailSubject(iAV.ToString(), currentAV.Status),
            EmailTemplate.GenerateAdjVouchStatusChangedEmail(recpt.EmpName, iAV.ToString(), approver.EmpName, currentAV.Status));

        Response.Redirect("~/Store/Supervisor/ApproveInventoryAdjustmentList.aspx");
    }

    public void updateStationeryCatalogue(int IAV)
    {
        int iAV = Convert.ToInt32(Request["IAV"]);
        LussisEntities context = new LussisEntities();
        var getQty = context.AdjustmentVoucherDetails.Where(x => x.AvNo == iAV).FirstOrDefault().StationeryCatalogue.CurrentQty;
    }

    public void updateStockTxn()
    {
        //get no of Rows
        int rowCount = ApproveInventoryAdjustmentDetailsGridView.Rows.Count;

        for (int i = 0; i < rowCount; i++)
        {
            LussisEntities context = new LussisEntities();
            StockTxnDetail stkdetails = new StockTxnDetail();

            //get itemno to pass to controller to update stocktxndetails table
            Label itemNoLabel = (Label)ApproveInventoryAdjustmentDetailsGridView.Rows[i].FindControl("ItemCode");
            string itemNoText = itemNoLabel.Text;

            //get date
            DateTime getDate = DateTime.Now;

            //get adjusted qty
            Label amtAdjustedLabel = (Label)ApproveInventoryAdjustmentDetailsGridView.Rows[i].FindControl("qtyAdjustedAmt");
            int amtAdjustedText = Convert.ToInt32(amtAdjustedLabel.Text);

            //get Recordedqty
            int qtyAmt = (int)context.StationeryCatalogues.Where(x => x.ItemNo == itemNoText).Select(x => x.CurrentQty).First() + Convert.ToInt32(amtAdjustedText);

            //get Remarks
            Label remarksLabel = (Label)ApproveInventoryAdjustmentDetailsGridView.Rows[i].FindControl("Remarks");
            string remarksText = remarksLabel.Text;

            //get own Emp No
            var empNumber = Profile.EmpNo;

            stkdetails.ItemNo = itemNoText;
            stkdetails.Date = getDate;
            stkdetails.AdjustQty = amtAdjustedText;
            stkdetails.RecordedQty = qtyAmt;
            stkdetails.Remarks = remarksText;
            
            ApproveInventoryAdjustmentController.updateStockTransactioninDB(stkdetails);
            ApproveInventoryAdjustmentController.updateStationeryCatalogue(itemNoText, qtyAmt);
        }
    }

    protected void approveAVBackBut_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Store/Supervisor/ApproveInventoryAdjustmentList.aspx");
    }
}