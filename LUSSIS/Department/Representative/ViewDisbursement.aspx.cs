using LUSSIS_Backend;
using LUSSIS_Backend.controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected static int dNo;
    protected Disbursement d;
    protected List<DisbursementDetail> dDetails;

    protected void Page_Load(object sender, EventArgs e)
    {
        int empNo = Profile.EmpNo;
        String deptCode = EmployeeController.GetDept(empNo);
        
        d = DisbursementController.GetPendingDisbursementOfDepartment(deptCode);

        if (d != null)
        {
            Session["DNo"] = d.DisbursementNo;

            dNo = (int)Session["DNo"];
            d = DisbursementController.GetDisbursement(dNo);

            BindGrid();
        } else
        {
            Session["DNo"] = null;
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