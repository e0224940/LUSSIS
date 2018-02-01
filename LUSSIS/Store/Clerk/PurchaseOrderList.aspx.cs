using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LUSSIS_Backend;
using LUSSIS_Backend.controller;

public partial class Store_Clerk_PurchaseOrderList : System.Web.UI.Page
{
    int empNo;
    List<PurchaseOrder> pendingPOs;

    protected void Page_Load(object sender, EventArgs e)
    {
        empNo = Profile.EmpNo;

        if (!IsPostBack)
        {
            // Set gridview
            BindGrid();
        }
    }

    protected void PendingPurchaseOrderGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "PODetails")
        {
            int pONo = Convert.ToInt32(e.CommandArgument);
            Session["PONo"] = pONo;
            Response.Redirect("PurchaseOrderDetails.aspx");
        }
    }

    protected void PendingPurchaseOrderGridView_Delete(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int pONo = Int32.Parse(PendingPurchaseOrderGridView.Rows[e.RowIndex].Cells[0].Text);
            POController.DeletePO(pONo);
            BindGrid();
            Session["POProcessed"] = pONo;
        }
        catch (Exception exception)
        {
            Session["Error"] = "An Error Has Occured: " + exception.Message;
        }
    }

    private void BindGrid()
    {
        // Get Pending Purchase Orders belonging to this User
        pendingPOs = POController.GetPendingPOsByOrderedEmp(empNo);

        // Set GridView
        PendingPurchaseOrderGridView.DataSource = pendingPOs.Select(
            pO => new
            {
                PONo = pO.PONo,
                DateIssued = pO.DateIssued,
                Supplier = pO.Supplier.SupplierName,
                Status = pO.Status
            });
        PendingPurchaseOrderGridView.DataBind();
    }
}