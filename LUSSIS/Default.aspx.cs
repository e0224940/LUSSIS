using LUSSIS_Backend;
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
        // Populate Database with default logins if there are none
        if (Membership.GetAllUsers().Count == 0)
        {
            FirstTimeSetup();
        }

        // Nothing to show on this page, go straight to Login Page
        Response.Redirect(Page.ResolveUrl("~/Login.aspx"));
    }

    protected void FirstTimeSetup()
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

        // Create Roles
        foreach (String role in Enum.GetNames(typeof(RoleController.LUSSISRoles)))
        {
            if (!Roles.GetAllRoles().Contains(role))
            {
                Roles.CreateRole(role);
            }
        }

        bool result = true;
        for (int i = 0; i < usernames.Count(); i++)
        {
            // Create Login
            MembershipCreateStatus userCreateResult = GenerateAccount(usernames[i], passwords[i], employeeIDs[i]);
            bool accountCreationStatus = userCreateResult == MembershipCreateStatus.Success;
            result = result && accountCreationStatus;
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
        }

        return result;
    }
}