using LUSSIS_Backend;
using Email_Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Store_Supervisor_ApprovePODetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LussisEntities context = new LussisEntities();

        int poNO = Convert.ToInt32(Request["PO"]);
        poNumberLabel.Text = Convert.ToString(poNO);
        var toGetSupplier = context.PurchaseOrders.Where(x => x.PONo == poNO).FirstOrDefault();
        var supplierSelected = toGetSupplier.SupplierCode;
        var supplierName = toGetSupplier.Supplier.SupplierName;
        suppliernameLabel.Text = supplierName;
        var getPODetails = ApprovePurchaseOrderController.getPurchaseOrderDetails(poNO, supplierSelected);

        ApprovePODetailsGridView.DataSource = getPODetails;
        ApprovePODetailsGridView.DataBind();
    }

    //approvebutton
    protected void approveButton_Click(object sender, EventArgs e)
    {
        int poNO = Convert.ToInt32(Request["PO"]);
        var empNo = Profile.EmpNo;
        var remarks = approvePORemarksTB.Text;
        
        ApprovePurchaseOrderController.setStatusApprove(poNO);
        ApprovePurchaseOrderController.updateApproveBy(poNO, empNo);
        ApprovePurchaseOrderController.updateDateReviewed(poNO);
        ApprovePurchaseOrderController.updateRemarks(poNO, remarks);

        //send Email, will need to put in Controller
        LussisEntities entity = new LussisEntities();
        PurchaseOrder currentPO = entity.PurchaseOrders.Where(x => x.PONo == poNO).FirstOrDefault();
        Employee recpt = entity.Employees.Where(x => x.EmpNo == currentPO.OrderedBy).FirstOrDefault();
        Employee approver = entity.Employees.Where(x => x.EmpNo == empNo).FirstOrDefault();

        EmailBackend.sendEmailStep(recpt.Email,
            EmailTemplate.GeneratePOStatusChangedEmailSubject(poNO.ToString(), currentPO.Status),
            EmailTemplate.GeneratePOStatusChangedEmail(recpt.EmpName, poNO.ToString(), approver.EmpName, currentPO.Status, currentPO.Remarks));
        
        Response.Redirect("~/Store/Supervisor/ApprovePOList.aspx");
    }

    //reject button
    protected void rejectButton_Click(object sender, EventArgs e)
    {
        int poNO = Convert.ToInt32(Request["PO"]);
        var empNo = Profile.EmpNo;
        var remarks = approvePORemarksTB.Text;
        ApprovePurchaseOrderController.setStatusReject(poNO);
        ApprovePurchaseOrderController.updateApproveBy(poNO, empNo);
        ApprovePurchaseOrderController.updateDateReviewed(poNO);
        ApprovePurchaseOrderController.updateRemarks(poNO, remarks);

        //send email, will need to put in controller
        LussisEntities entity = new LussisEntities();
        PurchaseOrder currentPO = entity.PurchaseOrders.Where(x => x.PONo == poNO).FirstOrDefault();
        Employee recpt = entity.Employees.Where(x => x.EmpNo == currentPO.OrderedBy).FirstOrDefault();
        Employee approver = entity.Employees.Where(x => x.EmpNo == empNo).FirstOrDefault();

        EmailBackend.sendEmailStep(recpt.Email,
            EmailTemplate.GeneratePOStatusChangedEmailSubject(poNO.ToString(), currentPO.Status),
            EmailTemplate.GeneratePOStatusChangedEmail(recpt.EmpName, poNO.ToString(), approver.EmpName, currentPO.Status, currentPO.Remarks));

        Response.Redirect("~/Store/Supervisor/ApprovePOList.aspx");
    }

    protected void approvePOBackBut_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Store/Supervisor/ApprovePOList.aspx");
    }
}