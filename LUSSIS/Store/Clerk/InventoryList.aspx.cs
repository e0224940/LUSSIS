using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LUSSIS_Backend;


public partial class Store_Clerk_InventoryList : System.Web.UI.Page
{
    LUSSIS_Backend.LussisEntities context = new LUSSIS_Backend.LussisEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            DropDownList1.DataTextField = "Category";
            DropDownList1.DataValueField = "Category";
            DropDownList1.DataSource = context.StationeryCatalogues.Select(item => new { Category = item.Category }).Distinct().ToList();
            DropDownList1.DataBind();

            List<StationeryCatalogue> b = context.StationeryCatalogues.ToList();
            GridView1.DataSource = b;
            GridView1.DataBind();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string searchResult = TextBox1.Text;
        List<StationeryCatalogue> a = context.StationeryCatalogues.Where(x => x.Description.Contains(searchResult)).ToList();
        GridView1.DataSource = a;
        GridView1.DataBind();
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string c = DropDownList1.SelectedValue;
        List<StationeryCatalogue> selected = context.StationeryCatalogues.Where(x => x.Category == c).ToList();
        GridView1.DataSource = selected;
        GridView1.DataBind();
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Click")
        {

            Response.Redirect("RetrivalListUI.aspx?key=");
        }
    }
}