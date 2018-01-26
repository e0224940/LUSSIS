using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LUSSIS_Backend;

public partial class Store_Clerk_ViewPurchaseOrderHistory : System.Web.UI.Page
{
    static List<PurchaseOrder> purchaseOrder;

    protected void Page_Load(object sender, EventArgs e)
    {
        int empNo = Profile.EmpNo;
        if(!IsPostBack)
        {
            string empName = EmployeeController.GetName(empNo);
            PurchaseOrderGridView.Visible = true;
            purchaseOrder = new List<PurchaseOrder>();
            PurchaseOrderGridView.DataSource = EmployeeController.ViewPurchaseOrder(empNo);
            PurchaseOrderGridView.DataBind();
        }
    }

    protected void PurchaseOrderGridView_Delete(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void PurchaseOrderGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int poNo;

        if (e.CommandName == "PODetails")
        {
            poNo = Convert.ToInt32(e.CommandArgument);
            Session["PONo"] = poNo;
            Response.Redirect("PurchaseOrderDetailsView.aspx");
        }
    }
}