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
        ProfileCommon loginProfile;
        String result = "0"; // Return 0 if authentication failure

        // Arguments can be passed either by get or by post
        String username = Request["user"];
        String password = Request["pass"];

        // If valid, get the employee number and generate the random number
        if (Membership.ValidateUser(username, password))
        {
            // Get the Profile of the User
            loginProfile = (ProfileCommon)ProfileCommon.Create(username, true);

            result = loginProfile.EmpNo
                    + ":"
                    + AndroidAuthenticationController.GenerateAndroidSessionNumber(loginProfile.EmpNo);
        }

        Response.Write(result);
    }
}