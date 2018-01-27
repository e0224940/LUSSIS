using LUSSIS_Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String username = LoginForm.UserName;
        String[] userRoles = Roles.GetRolesForUser(username);

        // If Already Logged in, redirect and don't show this page
        if (User.Identity.IsAuthenticated)
        {
            username = User.Identity.Name;
            userRoles = Roles.GetRolesForUser(username);
            redirectLoggedInUser(username, userRoles, Response);
        }
    }

    protected void LoginForm_LoggedIn(object sender, EventArgs e)
    {
        String username = LoginForm.UserName;
        String[] userRoles = LoginController.setupRolesAfterAuthentication(username);
        String[] actualRoles = Roles.GetRolesForUser(username);

        redirectLoggedInUser(username, userRoles, Response);
    }

    void redirectLoggedInUser(String username, String[] userRoles, HttpResponse response)
    {
        // NOTE : The order of the roles in these arrays is in decreasing order of "rank"
        String[] departmentRoles =
        {
            "DepartmentHead", "DepartmentDeputy", "DepartmentRepresentative", "DepartmentEmployee"
        };

        String[] storeRoles =
        {
            "StoreSupervisor", "StoreManager", "StoreClerk"
        };        

        // Redirect to Store if There is a Store Role
        foreach (String role in storeRoles)
        {
            foreach(String userRole in userRoles)
            {
                if (role == userRole)
                {
                    response.Redirect("./Store/" + role.Remove(0, "Store".Length) + "/Default.aspx");
                    return;
                }
            }            
        }

        // Redirect to Department if There is a Department Role
        foreach (String role in departmentRoles)
        {
            foreach (String userRole in userRoles)
            {
                if (role == userRole)
                {
                    response.Redirect("./Department/" + role.Remove(0, "Department".Length) + "/Default.aspx");
                    return;
                }
            }           
        }

        // TODO : Handle Error case when no role is assigned
    }
}