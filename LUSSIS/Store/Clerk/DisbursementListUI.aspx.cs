using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LUSSIS_Backend;

public partial class Store_Clerk_DisbursementListUI : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LUSSIS_Backend.LussisEntities context = new LUSSIS_Backend.LussisEntities();
        List<Disbursement> displayPending = context.Disbursements.Where(x => x.Status == "pending").ToList();
        GridView1.DataSource = displayPending;
        GridView1.DataBind();

        List<Disbursement> displayCompleted = context.Disbursements.Where(x => x.Status == "completed").ToList();
        GridView2.DataSource = displayCompleted;
        GridView2.DataBind();

    }
}