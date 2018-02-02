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
            // Read Inputs And Create User
            String username = Username.Text;
            String password = Password.Text;
            String employeeID = EmployeeID.Text;

            // Create Login
            MembershipCreateStatus result = GenerateAccount(username, password, employeeID);

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

    protected void ButtonGenerateAccounts_Click(object sender, EventArgs e)
    {
        try
        {
            String[] usernames = new String[] {
                "ezra", "pamela", "thet", // ENGL Department
                "wee", "fatt", "zrui", // CPSC Department
                "esther", "robin", "bruce" // STOR 
            };
            String[] passwords = new String[] {
                "ezraezraezra!", "pamelapamelapamela!", "thetthetthet!", // ENGL Department
                "weeweewee!", "fattfattfatt!", "zruizruizrui!", // CPSC Department
                "estherestheresther!", "robinrobinrobin!", "brucebrucebruce!" // STOR
            };
            String[] employeeIDs = new String[] {
                "1002", "1001", "1014", // ENGL Department
                "1004", "1003", "1015", // CPSC Department
                "1011", "1012", "1013", // STOR
            };

            bool result = true;
            for(int i = 0; i < usernames.Count(); i++)
            {
                // Create Login
                MembershipCreateStatus userCreateResult = GenerateAccount(usernames[i], passwords[i], employeeIDs[i]);
                bool accountCreationStatus = userCreateResult == MembershipCreateStatus.Success;
                result = result && accountCreationStatus;
            }


            // Update Account Creation Status
            if (result == true)
            {
                StatusText.Text = "[Multiple Accounts have been populated sucessfully]";
            }
            else
            {
                StatusText.Text = "[Multiple Accounts Creation Failed]";
            }

        }
        catch (Exception exception)
        {
            StatusText.Text = "[Multiple Accounts Creation Failure]:" + "<br/><br/>" + exception.Message.Replace("\n", "<br/>");
        }
    }

    private MembershipCreateStatus GenerateAccount(String username, String password, String employeeID)
    {
        // Create Login
        MembershipCreateStatus result;
        MembershipUser newUser = Membership.CreateUser(username, password, "", "?", "!", true, out result);

        if (result == MembershipCreateStatus.Success)
        {
            // Get the Profile of the newly created User
            ProfileCommon p = (ProfileCommon)ProfileCommon.Create(username, true);

            // TODO : CHECK IF EMPLOYEE NUMBER EXISTS
            p.EmpNo = Convert.ToInt32(employeeID);

            // Update The Profile
            p.Save();

            // Add The User Roles Assigned
            foreach (ListItem item in AssignedRoles.Items)
            {
                // TODO : SETUP ROLES IN DATABASE BY DEFAULT
                if (!Roles.GetAllRoles().Contains(item.Value))
                {
                    Roles.CreateRole(item.Value);
                }

                if (item.Selected)
                {
                    Roles.AddUserToRole(username, item.Value);
                }
            }
        }

        return result;
    }
}