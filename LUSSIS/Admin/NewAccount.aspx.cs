using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_NewAccount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // TODO : Fill the Department Dropdown List
            Department.Items.Add(new ListItem("English", "ENGL"));
            Department.Items.Add(new ListItem("Store", "STORE"));
        }
    }

    protected void NewUserButton_Click(object sender, EventArgs e)
    {
        // TODO : Read Inputs And Create User

        StatusText.Text = "[User XYZ Created]";
    }
}