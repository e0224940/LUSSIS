﻿using System;
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

    // Get Employee id from session
    [OperationContract]
    [WebInvoke(UriTemplate = "/GetEmployeeIDFromSession", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
    String GetLoggedInEmployeeNumber(int sessionID);

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
    [WebInvoke(UriTemplate = "/CurrentDisbursementNo", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
    String GetCurrentDisbursementNoForDepartment(int sessionID);

    [OperationContract]
    [WebInvoke(UriTemplate = "/DisbursementDetails", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
    WCFDisbursementDetail[] GetDisbursementDetailsOf(int sessionID, int DisbursementNo);

    [OperationContract]
    [WebInvoke(UriTemplate = "/UpdateDisbursementDetail", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
    bool UpdateDisbursementDetail(int sessionID, WCFDisbursementDetail updatedDisbursementDetail);

    [OperationContract]
    [WebInvoke(UriTemplate = "/Employee", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
    WCFEmployee GetEmployeeById(int sessionID, int empNo);

    [OperationContract]
    [WebInvoke(UriTemplate = "/Catalogue", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
    WCFStationeryCatalogue[] GetCatalogue(int sessionID);

    [OperationContract]
    [WebInvoke(UriTemplate = "/CatalogueSearch", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
    WCFStationeryCatalogue[] CatalogueSearch(int sessionID, String ItemNo, String Category, String Description, String Bin);

    [OperationContract]
    [WebInvoke(UriTemplate = "/AddRequisitionDetail", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
    bool AddRequisitionDetail(int sessionID, WCFRequisitionDetail addRequisitionDetail);
}

public class WCFDisbursementDetail
{
    public String DisbursementNo;
    public String ItemNo;
    public String Needed;
    public String Promised;
    public String Received;
}

public class WCFStationeryCatalogue
{
    public String ItemNo;
    public String Description;
    public String Category;
    public String Uom;
    public String ReorderQty;
    public String ReorderLevel;
    public String CurrentQty;
    public String Supplier1;
    public String Supplier2;
    public String Supplier3;
    public String Bin;
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

public class WCFRequisitionDetail
{
    public int ReqNo;
    public string ItemNo;
    public int Qty;
}

