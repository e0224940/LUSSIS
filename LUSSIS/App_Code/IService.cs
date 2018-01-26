using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
[ServiceContract]
public interface IService
{
    // Test Function for android apps to check the connection to server.
    // Always returns the string "test"
    [OperationContract]
    [WebGet(UriTemplate = "/test", ResponseFormat = WebMessageFormat.Json)]
    string Test();

    [OperationContract]
    [WebInvoke(UriTemplate = "/login", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
    WCFSessionID AuthenticateUser(String user, String pass);

    // Check if Session id is okay
    [OperationContract]
    [WebInvoke(UriTemplate = "/checkSession", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
    bool TestSessionId(int sessionID);

    [OperationContract]
    [WebInvoke(UriTemplate = "/CollectionPoint", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
    String GetCollectionPoint(int sessionID, int collectionPointNo);

    [OperationContract]
    [WebInvoke(UriTemplate = "/Department", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
    WCFDepartment GetDepartment(int sessionID, String deptCode);

    [OperationContract]
    [WebInvoke(UriTemplate = "/LatestDisbursement", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
    WCFDisbursement GetCurrentDisbursementForDepartment(int sessionID, String deptCode);

    [OperationContract]
    [WebInvoke(UriTemplate = "/Employee", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
    WCFEmployee GetEmployeeById(int sessionID, int empNo);



}

public class WCFEmployee
{
    public String EmpNo;
    public String EmpName;
    public String DeptCode;
    public String Email;
    public String SessionNo;
    public String SessionExpiry;
}

public class WCFDisbursement
{
    public String DisbursementNo;
    public String DeptCode;
    public String DisbursementDate;
    public String RepEmpNo;
    public String CollectionPointNo;
    public String Pin;
    public String Status;
}

public class WCFSessionID
{
    public String SessionID;
}
public class WCFDepartment
{
    public String DeptCode;
    public String DeptName;
    public String ContactName;
    public String PhoneNo;
    public String FaxNo;
    public int HeadEmpNo;
    public int CollectionPointNo;
    public int RepEmpNo;
    public int DeputyEmpNo;
}

