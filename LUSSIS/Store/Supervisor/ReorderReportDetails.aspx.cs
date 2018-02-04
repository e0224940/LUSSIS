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
        int SNO = (Convert.ToInt16(Request["SNO"]) - 1);
        int SNOforSupplier = SNO + 1;
        var query = ViewReorderReportController.checkForPO(SNO);

        if (query != null)
        {
            var dateQuery = query.DateIssued;
            Label2.Text = "Reorder Report as end of";
            Label3.Text = dateQuery.Value.ToString("MMM yyyy");
            var supplierSelected = query.SupplierCode;

            var reOrderReportDetails = ViewReorderReportController.showReorderReportDetails(SNO, supplierSelected);

            ReorderReportDetailsGridView.DataSource = reOrderReportDetails;
            ReorderReportDetailsGridView.DataBind();

        }
        else
        {
            DateTime currentMthYear = DateTime.Now.AddMonths(-SNO);
            Label2.Text = "There is no Reorder Report for the month of";
            Label3.Text = currentMthYear.ToString("MMM yyyy");

            //var reOrderReportDetails = ViewReorderReportController.showReorderReportDetails(SNO);
            ////LussisEntities context = new LussisEntities();
            ////var reOrderReportDetails = ViewReorderReportController.showReorderReportDetails(SNO, supplierSelected);

            ////    Label2.Text = "Reorder Report for: ";
            ////    Label3.Text = DateTime.Now.ToString("MMM yyyy");

            //ReorderReportDetailsGridView.DataSource = reOrderReportDetails;
            //ReorderReportDetailsGridView.DataBind();

        }
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Store/Supervisor/ReorderReportList.aspx");
    }
}

