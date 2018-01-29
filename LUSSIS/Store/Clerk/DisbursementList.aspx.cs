using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LUSSIS_Backend;
using LUSSIS_Backend.controller;

public partial class Store_Clerk_DisbursementList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<Disbursement> pendingDisbursements = DisbursementController.GetPendingDisbursements();
            PendingDisbursementGridView.DataSource = pendingDisbursements.Select(d => new
            {
                DisbursementNo = d.DisbursementNo,
                DepartmentName = d.Department.DeptName,
                Date = d.DisbursementDate,
                CollectionPoint = d.CollectionPoint.CollectionPointDetails,
                Status = d.Status
            });
            PendingDisbursementGridView.DataBind();
        }
    }

    protected void DisbursementGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int dNo;

        if (e.CommandName == "Details")
        {
            dNo = Convert.ToInt32(e.CommandArgument);
            Session["DNo"] = dNo;
            Response.Redirect("DisbursementDetails.aspx");
        }
    }
}