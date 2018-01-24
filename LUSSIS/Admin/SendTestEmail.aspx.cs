using Email_Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_NewAccount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void SendEmailButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (EmailBackend.sendEmailStep(
                Email.Text,
                Subject.Text,
                Message.Text
                ))
            {
                StatusText.Text = "[Message Sent Successfully]";
            }
            else
            {
                StatusText.Text = "[There was a problem sending that email]";
            }
        }
        catch (Exception ex)
        {
            StatusText.Text = "[An Exception has occured :] <br/>" + ex.Message;
        }
    }
}