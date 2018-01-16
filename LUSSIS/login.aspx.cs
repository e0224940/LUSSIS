using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void LoginForm_LoggedIn(object sender, EventArgs e)
    {
        String[] departmentRoles =
        {
            "DepartmentHead", "DepartmentDeputy", "DepartmentEmployee", "DepartmentRepresentative"
        };

        String[] storeRoles =
        {
            "StoreSupervisor", "StoreManager", "StoreClerk"
        };
        
        String username = User.Identity.Name;
        String employeeId = Profile.EmpNo;

        // Redirect to Store if There is a Store Role
        foreach(String role in storeRoles)
        {
            if(User.IsInRole(role))
            {
                Response.Redirect("./Store/" + role.Remove(0,"Store".Length) + "/Default.aspx");
                return;
            }
        }

        // Redirect to Department if There is a Department Role
        foreach (String role in departmentRoles)
        {
            if (User.IsInRole(role))
            {
                Response.Redirect("./Department/" + role.Remove(0, "Department".Length) + "/Default.aspx");
                return;
            }
        }

        // TODO : Handle Error case when no role is assigned
    }
}