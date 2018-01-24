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
    }
}
