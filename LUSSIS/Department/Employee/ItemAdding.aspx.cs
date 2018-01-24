using LUSSIS_Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Department_Employee_ItemAdding : System.Web.UI.Page
{
    static List<StationeryCatalogue> catList;
    static List<RaisedItem> catitem;
  
    string quantity;
    protected void Page_Load(object sender, EventArgs e)
    {
        ItemList.GetDescription();
        if (!IsPostBack)
        {
            catList = new List<StationeryCatalogue>();
            catitem = new List<RaisedItem>();
            ItemList.GetDescription();
            GVAddItem.DataSource = ItemList.GetDescription();
            GVAddItem.DataBind();
            
        }
    }

    protected void GVAddItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        GridViewRow row = GVAddItem.SelectedRow;
        RaisedItem cat = new RaisedItem();
        
        cat.description = row.Cells[0].Text;
        cat.quantity = (row.Cells[1].FindControl("txtQty") as TextBox).Text;
       
        catitem.Add(cat);
      
        GVShowItem.DataSource = catitem;
        GVShowItem.DataBind();
        
    }

    //protected void RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        Label quantitylabel = (e.Row.FindControl("lblQuantity") as Label);
    //        if (quantitylabel != null)
    //            quantitylabel.Text = quantity;
    //    }
    //}

    protected void Button1_Click(object sender, EventArgs e)
    {
      
    }
}