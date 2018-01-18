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

    }

    protected void LoginForm_LoggedIn(object sender, EventArgs e)
    {
        redirectLoggedInUser();
    }

    void redirectLoggedInUser()
    {
        String[] departmentRoles =
        {
            "DepartmentHead", "DepartmentDeputy", "DepartmentEmployee", "DepartmentRepresentative"
        };

        String[] storeRoles =
        {
            "StoreSupervisor", "StoreManager", "StoreClerk"
        };

        String username = LoginForm.UserName;
        String[] userRoles = Roles.GetRolesForUser(username);

        // Redirect to Store if There is a Store Role
        foreach (String role in storeRoles)
        {
            foreach(String userRole in userRoles)
            {
                if (role == userRole)
                {
                    Response.Redirect("./Store/" + role.Remove(0, "Store".Length) + "/Default.aspx");
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
                    Response.Redirect("./Department/" + role.Remove(0, "Department".Length) + "/Default.aspx");
                    return;
                }
            }           
        }

        // TODO : Handle Error case when no role is assigned
    }
}