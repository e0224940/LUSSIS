using LUSSIS_Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Store_Supervisor_ReorderReportList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var reorderReport = ViewReorderReportController.showReorderReport();
            reorderReportListGridView.DataSource = reorderReport;
            reorderReportListGridView.DataBind();
        }
    }
}