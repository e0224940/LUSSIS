using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LUSSIS_Backend;

public partial class Store_Clerk_RetrivalListUI : System.Web.UI.Page
{
    LUSSIS_Backend.LussisEntities context = new LUSSIS_Backend.LussisEntities();
    protected void Page_Load(object sender, EventArgs e)
    {

        Retrieval retrivalInfo = new Retrieval();
        List<Retrieval> display = context.Retrievals.Where(x => x.Date == null).ToList();
        GridView1.DataSource = display;
        GridView1.DataBind();

        List<Retrieval> sortDate = context.Retrievals.Where(x => x.Date != null).ToList();
        sortDate.Sort(delegate (Retrieval x, Retrieval y) {
            return y.Date.Value.CompareTo(x.Date);
        });
        GridView2.DataSource = sortDate;
        GridView2.DataBind();


    }
}