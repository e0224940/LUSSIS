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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService
    {
        // Test Function for android apps to check the connection to server.
        // Always returns the string "test"
        [OperationContract]
        [WebGet(UriTemplate = "/test", ResponseFormat = WebMessageFormat.Json)]
        string Test();

        // Check if Session id is okay
        [OperationContract]
        [WebInvoke(UriTemplate = "/checkSession", Method="POST", ResponseFormat = WebMessageFormat.Json, RequestFormat =WebMessageFormat.Json)]
        bool TestSessionId(int sessionID);
    }
}
