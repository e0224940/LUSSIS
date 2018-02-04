using LUSSIS_Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Store_Supervisor_ReorderReportDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LussisEntities context = new LussisEntities();

            var reOrderReportDetails = ViewReorderReportController.showReorderReportDetails();

            Label2.Text = "Reorder Report for: ";
            Label3.Text = DateTime.Now.ToString("MMM yyyy");

        ReorderReportDetailsGridView.DataSource = reOrderReportDetails;
            ReorderReportDetailsGridView.DataBind();

    }

    protected void ReorderReportDetailsGridView_PreRender(object sender, EventArgs e)
    {
        //for (int rowIndex = ReorderReportDetailsGridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        //{
        //    GridViewRow row = ReorderReportDetailsGridView.Rows[rowIndex];
        //    GridViewRow previousRow = ReorderReportDetailsGridView.Rows[rowIndex + 1];

        //    if (row.Cells[3].Text == previousRow.Cells[3].Text)
        //    {
        //        row.Cells[3].RowSpan = previousRow.Cells[0].RowSpan < 2 ? 2 :
        //                               previousRow.Cells[3].RowSpan + 1;
        //        previousRow.Cells[3].Visible = false;
        //    }
        //}
    }
}

