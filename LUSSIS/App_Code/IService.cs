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

    // Get Roles Assigned from session
    [OperationContract]
    [WebInvoke(UriTemplate = "/GetRolesFromSession", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
    String[] GetRolesFromSession(int sessionID);

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
    [WebInvoke(UriTemplate = "/DeptListAll", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
    WCFDepartment[] GetAllDepartments(int sessionID);

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

    [OperationContract]
    [WebInvoke(UriTemplate = "/RemoveRequisitionDetail", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
    bool RemoveRequisitionDetail(int sessionID, WCFRequisitionDetail removeRequisitionDetail);

    [OperationContract]
    [WebInvoke(UriTemplate = "/UpdateRequisitionDetail", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
    bool UpdateRequisitionDetail(int sessionID, WCFRequisitionDetail updateRequisitionDetail);

    [OperationContract]
    [WebInvoke(UriTemplate = "/AddRequisition", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
    bool AddRequisition(int sessionID, WCFRequisition addedRequisition);

    [OperationContract]
    [WebInvoke(UriTemplate = "/AddRequisitionAndGetReqNo", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
    int AddRequisitionAndGetReqNo(int sessionID);

    [OperationContract]
    [WebInvoke(UriTemplate = "/PendingRequisitions", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
    WCFRequisition[] GetPendingRequisitions(int sessionID, int sessionEmpNo);

    [OperationContract]
    [WebInvoke(UriTemplate = "/GetRequisitionById", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
    WCFRequisition GetRequisitionById(int sessionID, int reqNo);

    [OperationContract]
    [WebInvoke(UriTemplate = "/UpdateRequisition", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
    bool UpdateRequisition(int sessionID, WCFRequisition updatedRequisition);

    [OperationContract]
    [WebInvoke(UriTemplate = "/RemoveRequisition", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
    bool RemoveRequisition(int sessionID, WCFRequisition removedRequisition);

    [OperationContract]
    [WebInvoke(UriTemplate = "/RequisitionDetails", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
    WCFRequisitionDetail[] GetRequisitionDetails(int sessionID, int ReqNo);

    [OperationContract]
    [WebInvoke(UriTemplate = "/LatestRetrieval", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
    WCFRetrieval GetLatestRetrieval(int sessionID);

    [OperationContract]
    [WebInvoke(UriTemplate = "/RetrievalDetails", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
    WCFRetrievalDetail[] GetRetrievalDetails(int sessionID, int retrievalNo);

    [OperationContract]
    [WebInvoke(UriTemplate = "/sendEmail", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
    bool SendEmail(int sessionID, String email, String subject, String message);

    [OperationContract]
    [WebInvoke(UriTemplate = "/CreateAdjustmentVoucher", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
    bool CreateAdjustmentVoucher(int sessionID, String ItemNo, int Qty, String Reason);

    [OperationContract]
    [WebInvoke(UriTemplate = "/GetCatalogueCatList", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
    String[] GetCatalogueCatList(int sessionID);

    [OperationContract]
    [WebInvoke(UriTemplate = "/GetCatalogueBinList", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
    String[] GetCatalogueBinList(int sessionID);

    [OperationContract]
    [WebInvoke(UriTemplate = "/MarkDisbursementAsCollected", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
    bool MarkDisbursementAsCollected(int sessionID, int DisbursementNo, String Pin);

    [OperationContract]
    [WebInvoke(UriTemplate = "/StockTxnDetails", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
    WCFTransactionDetail[] GetStockTxnDetails(int sessionID, String ItemNo);
}

public class WCFDisbursementDetail
{
    public String DisbursementNo;
    public String ItemNo;
    public String Description;
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
    public String ItemNo;
    public String Description;
    public int Qty;
}

public class WCFRequisition
{
    public String ReqNo;
    public String IssuedBy;
    public String DateIssued;
    public String ApprovedBy;
    public String DateReviewed;
    public String Status;
    public String Remarks;
}

public class WCFRetrieval
{
    public String RetrievalNo;
    public String Date;
}

public class WCFRetrievalDetail
{
    public int RetrievalNo;
    public String ItemNo;
    public String Description;
    public String Bin;
    public int Needed;
    public int BacklogQty;
    public int Actual;
}

public class WCFTransactionDetail
{
    public int StockTxnNo;
    public String ItemNo;
    public String Date;
    public int AdjustQty;
    public int RecordedQty;
    public String Remarks;
}

