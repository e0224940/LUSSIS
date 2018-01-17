using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ChangePasswordButton_Click(object sender, EventArgs e)
    {        
        MembershipUser currentUser = Membership.GetUser();

        // Reset Alerts
        SuccessLabel.Text = "";
        ErrorLabel.Text = "";

        if (Membership.ValidateUser(currentUser.UserName, OldPassword.Text))
        {
            if(currentUser.ChangePassword(OldPassword.Text, NewPassword.Text))
            {
                SuccessLabel.Text = "Password Changed Successfully";
            }
            else
            {
                ErrorLabel.Text = "Could not change Password : ";
            }
        } 
        else
        {
            ErrorLabel.Text = "Invalid Password Entered";
        }
    }
}