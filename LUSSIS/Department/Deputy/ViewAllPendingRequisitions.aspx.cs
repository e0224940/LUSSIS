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
        if (!IsPostBack)
        {
            var pendingRequisitions = ViewPendingRequisitionsController.GetPendingRequisitions(Profile.EmpNo);
            PendingRequisitionGridView.DataSource = pendingRequisitions.Select(req => new
            {
                ReqNo = req.ReqNo,
                DateIssued = req.DateIssued,
                EmpName = req.EmployeeWhoIssued.EmpName,
                Email = req.EmployeeWhoIssued.Email,
            });
            PendingRequisitionGridView.DataBind();
        }
    }

    protected void PendingRequisitionGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int requisitionID;

        if (e.CommandName == "Details")
        {
            requisitionID = Convert.ToInt32(e.CommandArgument);
            Session["ReqNo"] = requisitionID;
            Response.Redirect("ViewRequisition.aspx");
        }
    }
}