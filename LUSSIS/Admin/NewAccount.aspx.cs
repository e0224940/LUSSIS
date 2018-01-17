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

    protected void NewUserButton_Click(object sender, EventArgs e)
    {
        try
        {
            // TODO : Read Inputs And Create User
            String username = Username.Text;
            String password = Password.Text;
            String employeeID = EmployeeID.Text;
            
            // Create Login
            MembershipCreateStatus result;
            MembershipUser newUser = Membership.CreateUser(username, password, "", "?", "!", true, out result);

            // Get the Profile of the newly created User
            ProfileCommon p = (ProfileCommon)ProfileCommon.Create(username, true);

            // TODO : CHECK IF EMPLOYEE NUMBER EXISTS
            p.EmpNo = employeeID;

            // Update The Profile
            p.Save();

            // Add The User Roles Assigned
            foreach(ListItem item in AssignedRoles.Items)
            {
                if (item.Selected)
                {
                    Roles.AddUserToRole(username, item.Value);
                }
            }

            // Update Account Creation Status
            if (result == MembershipCreateStatus.Success)
            {
                StatusText.Text = "[Account Created Sucessfully]";
            }
            else
            {
                StatusText.Text = "[Account Creation Failed]" + "<br/>" + result.ToString();
            }
        }
        catch (Exception exception)
        {
            StatusText.Text = "[Account Creation Failure]:" + "<br/><br/>" + exception.Message.Replace("\n", "<br/>");
        }
    }
}