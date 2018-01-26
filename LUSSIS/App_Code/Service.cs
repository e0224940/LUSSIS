using LUSSIS_Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Security;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class Service : IService
{
    public string Test()
    {
        return "test";
    }

    public bool TestSessionId(int sessionID)
    {
        return AndroidAuthenticationController.IsValidSessionId(sessionID);
    }

    public string GetCollectionPoint(int sessionID, int collectionPointNo)
    {
        String result = "";

        if (AndroidAuthenticationController.IsValidSessionId(sessionID))
        {
            result = AndroidController.GetCollectionPointOf(collectionPointNo);
        }

        return result;
    }

    public WCFDepartment GetDepartment(int sessionID, String deptCode)
    {
        Department department = null;
        WCFDepartment result = null;

        if (AndroidAuthenticationController.IsValidSessionId(sessionID))
        {
            department = AndroidController.GetDepartment(deptCode);
            if (department != null)
            {
                result = new WCFDepartment()
                {
                    CollectionPointNo = department.CollectionPointNo ?? -1,
                    ContactName = department.ContactName,
                    DeptCode = department.DeptCode,
                    DeptName = department.DeptName,
                    DeputyEmpNo = department.DeputyEmpNo ?? -1,
                    FaxNo = department.FaxNo.ToString(),
                    HeadEmpNo = department.HeadEmpNo ?? -1,
                    PhoneNo = department.PhoneNo.ToString(),
                    RepEmpNo = department.RepEmpNo ?? -1
                };
            }
        }

        return result;
    }

    public WCFSessionID AuthenticateUser(String username, String password)
    {
        ProfileCommon loginProfile;
        WCFSessionID result = new WCFSessionID() { SessionID = "0" };

        if (Membership.ValidateUser(username, password))
        {
            // Get the Profile of the User
            loginProfile = (ProfileCommon)ProfileCommon.Create(username, true);

            result.SessionID = AndroidAuthenticationController.GenerateAndroidSessionNumber(loginProfile.EmpNo).ToString();
        }

        return result;
    }
}
