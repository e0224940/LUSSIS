using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Department_Employee_RaisedRequisition : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Name.Text = 
    }

    protected void AddItem_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddItemPage.aspx");
    }
}