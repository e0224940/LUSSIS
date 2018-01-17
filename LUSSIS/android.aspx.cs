using LUSSIS_Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class android : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ProfileCommon p;
        ILussis lussisBackend = LussisFactory.GetBackend();
        String result = "0"; // Return 0 if authentication failure
        int employeeID;

        // Arguments can be passed either by get or by post
        String username = Request["user"];
        String password = Request["pass"];

        // If valid, get the employee number and generate the random number
        if (Membership.ValidateUser(username, password))
        {
            // Get the Profile of the User
            p = (ProfileCommon)ProfileCommon.Create(username, true);
            if (int.TryParse(p.EmpNo, out employeeID))
            {
                Response.Write(employeeID 
                    + ":" 
                    + lussisBackend.GenerateAndroidSession(employeeID));
            }
            else
            {
                // No employee record found, return -1
                result = "-1";
            }
        }

        Response.Write(result);
    }
}