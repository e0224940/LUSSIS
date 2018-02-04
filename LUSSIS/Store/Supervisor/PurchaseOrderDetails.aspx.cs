using LUSSIS_Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Store_Supervisor_PurchaseOrderDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LussisEntities context = new LussisEntities();
        int SNO = (Convert.ToInt16(Request["SNO"]));
        var query = context.PURCHASEORDERVIEWs
            .Where(x => x.PONo == SNO);

        var purchaseOrderDetails = ViewReorderReportController.showPurchaseOrderDetails(SNO);
        var abc = purchaseOrderDetails.Select(x => new
        {
            ItemNo = x.ItemNo,
            Description = x.Description,
            Qty = x.Qty,
            UnitPrice = x.UnitPrice
        });

        //purchaseOrderDetailsGridView.DataSource = purchaseOrderDetails;
        //purchaseOrderDetailsGridView.DataBind();
        GridViewTest.DataSource = abc;
        GridViewTest.DataBind();

        //filling up labels
        supplierNameLabel.Text = Convert.ToString(context.PurchaseOrders
                                .Where(p => p.PONo == SNO)
                                .FirstOrDefault().Supplier.SupplierName);
        poNumberLabel.Text = Convert.ToString(SNO);
        orderedByLabel.Text = Convert.ToString(context.PurchaseOrders
                        .Where(p => p.PONo == SNO)
                        .FirstOrDefault().Employee1.EmpName);
        datedLabel.Text = context.PurchaseOrders
            .Where(p => p.PONo == SNO)
            .FirstOrDefault().DateIssued.Value.ToString("dd MMM yyyy");
        int empNo = (int)context.PurchaseOrders.Where(p => p.PONo == SNO).FirstOrDefault().ApprovedBy;
        approvedByLabel.Text = context.Employees.Where(x => x.EmpNo == empNo).First().EmpName;
        dated2Label.Text = context.PurchaseOrders
            .Where(p => p.PONo == SNO)
            .FirstOrDefault().DateReviewed.Value.ToString("dd MMM yyyy");

    }

    protected void GridViewTest_DataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int unit = int.Parse(e.Row.Cells[2].Text.Trim());
            double price = Math.Round(double.Parse(e.Row.Cells[3].Text.Trim()));
            Label total = e.Row.FindControl("amt") as Label;
            total.Text = (price * unit).ToString();
        }
    }

    protected void purchaseOrderDetailBut_Click(object sender, EventArgs e)
    {
        int SNO = (Convert.ToInt16(Request["SNO"]));
        Response.Redirect("~/Store/Supervisor/ReorderReportDetails.aspx"+"?SNO="+SNO);
    }
}