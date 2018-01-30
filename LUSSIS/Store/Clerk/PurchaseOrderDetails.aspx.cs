using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LUSSIS_Backend;
using LUSSIS_Backend.controller;

public partial class Store_Clerk_PurchaseOrderDetails : System.Web.UI.Page
{
    protected int pONo;
    protected PurchaseOrder pO;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // If no PONo is passed, go back to List Page
            if (Session["PONo"] == null)
            {
                GoToPurchaseOrderListPage();
            }
            else
            {
                // Set DataGrid
                pONo = (int)Session["PONo"];
                pO = POController.GetPurchaseOrder(pONo);
                BindGrid();
            }
        }
        pONo = (int)Session["PONo"];
        pO = POController.GetPurchaseOrder(pONo);
    }

    protected void Button_Click(object sender, EventArgs e)
    {
        Button buttonPressed = (Button)sender;
        pONo = (int)Session["PONo"];

        // Delete Button
        if (buttonPressed.CommandArgument == "Delete")
        {
            // Delete Purchase Order
            try
            {
                POController.DeletePO(pONo);
                Session["POProcessed"] = (int)Session["PONo"];
            }
            catch (Exception exception)
            {
                Session["Error"] = "An Error Has Occured: " + exception.Message;
            }
            if (Session["Error"] == null)
            {
                GoToPurchaseOrderListPage();
            }
        }
    }

    protected void OnRowEditing(object sender, GridViewEditEventArgs e)
    {
        PODetailsGridView.EditIndex = e.NewEditIndex;
        BindGrid();
    }

    protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        // Get Selected Row
        GridViewRow row = PODetailsGridView.Rows[e.RowIndex];

        try
        {
            string itemNo = (row.FindControl("LabelItemNo") as Label).Text;
            int qty = Int32.Parse((string)e.NewValues["Qty"]);
            POController.UpdatePODQty(pONo, itemNo, qty);
            PODetailsGridView.EditIndex = -1;
            BindGrid();
        }
        catch (Exception exception)
        {
            Session["Error"] = "An Error Has Occured: " + exception.Message;
        }
    }

    protected void OnRowCancelingEdit(object sender, EventArgs e)
    {
        PODetailsGridView.EditIndex = -1;
        BindGrid();
    }

    protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row = PODetailsGridView.Rows[e.RowIndex];
        string itemNo = (row.FindControl("LabelItemNo") as Label).Text;
        POController.DeletePOD(pONo, itemNo);
        BindGrid();
    }

    private void GoToPurchaseOrderListPage()
    {
        Response.Redirect("PurchaseOrderList.aspx");
    }

    private void BindGrid()
    {
        // Get PODetails
        List<PurchaseOrderDetail> pODs = POController.GetPODs(pONo);

        // Set DataGrid
        PODetailsGridView.DataSource = pODs.Select(
            pOD => new
            {
                ItemNo = pOD.ItemNo,
                ItemDescription = pOD.StationeryCatalogue.Description,
                UnitPrice = POController.GetUnitPrice(pOD.ItemNo, pO.SupplierCode),
                Qty = pOD.Qty,
                SubTotal = POController.GetUnitPrice(pOD.ItemNo, pO.SupplierCode) * pOD.Qty
            });

        PODetailsGridView.DataBind();
    }
}