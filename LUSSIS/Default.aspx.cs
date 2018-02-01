using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Membership.GetAllUsers().Count == 0)
        {
            // Show setup Page if no account has been created
            Response.Redirect(Page.ResolveUrl("~/Admin/NewAccount.aspx"));
            return;
        }
        else
        {
            // Nothing to show on this page, go straight to Login Page
            Response.Redirect(Page.ResolveUrl("~/Login.aspx"));
            return;
        }        
    }
}