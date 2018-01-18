using LUSSIS_Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int requisitionNumber;

        if (!IsPostBack)
        {
            // If No Requisition Number is passed, go back to all pending requisitions
            if (Session["ReqNo"] == null)
            {
                GotoAllRequisitionsPage();
                return;
            }

            requisitionNumber = (int)Session["ReqNo"];

            // Get Requisition Details
            var requisitionDetails = ViewRequisitionDetailsController.GetRequisitionDetailsOf(requisitionNumber);

            // Set the Data Grid
            RequisitionDetailsGridView.DataSource = requisitionDetails.Select(
                req => new
                {
                    ItemCode = req.ItemNo,
                    ItemDescription = req.StationeryItem.Description,
                    Qty = req.Qty
                });
            RequisitionDetailsGridView.DataBind();
        }
    }

    protected void Button_Click(object sender, EventArgs e)
    {
        Button buttonPressed = (Button)sender;
        int requisitionNumber = (int)Session["ReqNo"];
        String remarks = TextBoxReason.Text;

        Session["RequisitionProcessed"] = null;
        Session["Error"] = null;

        // Accept Button
        if (buttonPressed.CommandArgument == "Approve")
        {
            if (AcceptRequisitionController.AcceptRequisition(Profile.EmpNo, requisitionNumber, remarks))
            {
                Session["RequisitionProcessed"] = Session["ReqNo"].ToString();
            }
            else
            {
                Session["Error"] = "Failed";
            }
        }
        // Reject Button
        else if (buttonPressed.CommandArgument == "Reject")
        {
            if (RejectRequisitionController.RejectRequisition(Profile.EmpNo, requisitionNumber, remarks))
            {
                Session["RequisitionProcessed"] =  Session["ReqNo"].ToString();
            }
            else
            {
                Session["Error"] = "Failed";
            }
        }
        // For Cancel Button, do nothing, just go back

        GotoAllRequisitionsPage();
    }

    private void GotoAllRequisitionsPage()
    {
        Response.Redirect("ViewAllPendingRequisitions.aspx");
    }
}