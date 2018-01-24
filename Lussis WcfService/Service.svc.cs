using LUSSIS_Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Lussis_WcfService
{
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
    }
}
