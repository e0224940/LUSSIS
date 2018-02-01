using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LUSSIS_Backend;
using LUSSIS_Backend.model;
using LUSSIS_Backend.controller;

public partial class Store_Clerk_DisbursementDetails : System.Web.UI.Page
{
    protected static int dNo;
    protected Disbursement d;
    protected List<DisbursementDetail> dDetails;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // If no DNo found
            if (Session["DNo"] == null)
            {
                // Redirect to list page
                Response.Redirect("DisbursementList.aspx");
            }
        }

        // Set page attributes
        dNo = (int)Session["DNo"];
        d = DisbursementController.GetDisbursement(dNo);

        if (!IsPostBack)
        {
            // Load gridview
            BindGrid();

            // Load controls
            RepTextBox.Text = d.Employee.EmpName;
            RepNoTextBox.Text = d.RepEmpNo.ToString();
        }
    }


    protected void OnRowEditing(object sender, GridViewEditEventArgs e)
    {
        DisbursementDetailsGridView.EditIndex = e.NewEditIndex;
        BindGrid();
    }

    protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            // Get page data
            GridViewRow row = DisbursementDetailsGridView.Rows[e.RowIndex];
            string itemNo = (string)DisbursementDetailsGridView.DataKeys[e.RowIndex].Values[0];
            int qty = Int32.Parse((row.FindControl("TextBoxDelivered") as TextBox).Text);

            // Update DDetail
            DisbursementController.UpdateReceivedQty(dNo, itemNo, qty);
            DisbursementDetailsGridView.EditIndex = -1;
            BindGrid();
        }
        catch (Exception exception)
        {
            Session["Error"] = "An Error Has Occured: " + exception.Message;
        }
    }

    protected void OnRowCancelingEdit(object sender, EventArgs e)
    {
        DisbursementDetailsGridView.EditIndex = -1;
        BindGrid();
    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        try
        {
            // Get page data
            decimal pin = Decimal.Parse(PinTextBox.Text);

            // Complete Disbursement
            DisbursementController.CompleteDisbursement(dNo, pin);

            // Show Success Page
            Session["DProcessed"] = dNo;
            Session["DNo"] = null;
        }
        catch (Exception exception)
        {
            Session["Error"] = "An Error Has Occured: " + exception.Message;
        }

        if (Session["DProcessed"] != null)
        {
            Response.Redirect("DisbursementList.aspx");
        }
    }

    private void BindGrid()
    {
        // Get DDetails
        dDetails = DisbursementController.GetDisbursementDetails(dNo);

        // Set gridview
        DisbursementDetailsGridView.DataSource = dDetails.Select(
            dD => new
            {
                ItemNo = dD.ItemNo,
                ItemDescription = dD.StationeryCatalogue.Description,
                Qty = dD.Promised,
                Delivered = dD.Received
            });

        DisbursementDetailsGridView.DataBind();
    }
}
