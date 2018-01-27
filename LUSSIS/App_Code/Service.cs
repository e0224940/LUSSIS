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
            // Update the roles of the logged in user
            LoginController.setupRolesAfterAuthentication(username);

            // Get the Profile of the User
            loginProfile = (ProfileCommon)ProfileCommon.Create(username, true);

            result.SessionID = AndroidAuthenticationController.GenerateAndroidSessionNumber(loginProfile.EmpNo).ToString();
        }

        return result;
    }

    public WCFDisbursement GetCurrentDisbursementForDepartment(int sessionID, string deptCode)
    {
        WCFDisbursement result = null;

        if (AndroidAuthenticationController.IsValidSessionId(sessionID))
        {
            Disbursement disbursement = AndroidController.GetCurrentDisbursementForDepartment(deptCode);

            if (disbursement != null)
            {
                result = new WCFDisbursement()
                {
                    CollectionPointNo = disbursement.CollectionPointNo.ToString(),
                    DeptCode = disbursement.DeptCode,
                    DisbursementDate = String.Format("{0:d/M/yyyy}", disbursement.DisbursementDate),
                    DisbursementNo = disbursement.DisbursementNo.ToString(),
                    Pin = disbursement.Pin.ToString(),
                    RepEmpNo = disbursement.RepEmpNo.ToString(),
                    Status = disbursement.Status
                };
            }
        }

        return result;
    }

    public WCFEmployee GetEmployeeById(int sessionID, int empNo)
    {
        WCFEmployee result = null;

        if (AndroidAuthenticationController.IsValidSessionId(sessionID))
        {
            Employee employee = AndroidController.GetEmployee(empNo);

            if (employee != null)
            {
                result = new WCFEmployee()
                {
                    DeptCode = employee.DeptCode,
                    Email = employee.Email,
                    EmpName = employee.EmpName,
                    EmpNo = employee.EmpNo.ToString(),
                    SessionExpiry = String.Format("{0:d/M/yyyy}", employee.SessionExpiry),
                    SessionNo = employee.SessionNo.ToString()
                };
            }
        }

        return result;
    }

    public WCFStationeryCatalogue[] GetCatalogue(int sessionID)
    {
        List<WCFStationeryCatalogue> result = new List<WCFStationeryCatalogue>();

        if (AndroidAuthenticationController.IsValidSessionId(sessionID))
        {
            List<StationeryCatalogue> items = AndroidController.GetCatalogue();

            if (items != null)
            {
                foreach (var item in items)
                {
                    result.Add(new WCFStationeryCatalogue()
                    {
                        Bin = item.Bin,
                        Category = item.Category,
                        CurrentQty = item.CurrentQty.ToString(),
                        Description = item.Description.ToString(),
                        ItemNo = item.ItemNo.ToString(),
                        ReorderLevel = item.ReorderLevel.ToString(),
                        ReorderQty = item.ReorderQty.ToString(),
                        Supplier1 = item.Supplier1,
                        Supplier2 = item.Supplier2,
                        Supplier3 = item.Supplier3,
                        Uom = item.Uom
                    });
                }
            }
        }

        return result.ToArray();
    }

    public WCFStationeryCatalogue[] CatalogueSearch(int sessionID, string ItemNo, string Category, string Description, string Bin)
    {
        List<WCFStationeryCatalogue> result = new List<WCFStationeryCatalogue>();

        if (AndroidAuthenticationController.IsValidSessionId(sessionID))
        {
            List<StationeryCatalogue> items = AndroidController.SearchCatalogue(ItemNo, Category, Description, Bin);

            if (items != null)
            {
                foreach (var item in items)
                {
                    result.Add(new WCFStationeryCatalogue()
                    {
                        Bin = item.Bin,
                        Category = item.Category,
                        CurrentQty = item.CurrentQty.ToString(),
                        Description = item.Description.ToString(),
                        ItemNo = item.ItemNo.ToString(),
                        ReorderLevel = item.ReorderLevel.ToString(),
                        ReorderQty = item.ReorderQty.ToString(),
                        Supplier1 = item.Supplier1,
                        Supplier2 = item.Supplier2,
                        Supplier3 = item.Supplier3,
                        Uom = item.Uom
                    });
                }
            }
        }

        return result.ToArray();
    }

    public WCFDisbursementDetail[] GetDisbursementDetailsOf(int sessionID, int DisbursementNo)
    {
        List<WCFDisbursementDetail> result = new List<WCFDisbursementDetail>();

        if (AndroidAuthenticationController.IsValidSessionId(sessionID))
        {
            List<DisbursementDetail> items = AndroidController.GetDisbursementDetailsOf(DisbursementNo);

            if (items != null)
            {
                foreach (var item in items)
                {
                    result.Add(new WCFDisbursementDetail()
                    {
                        DisbursementNo = item.DisbursementNo.ToString(),
                        ItemNo = item.ItemNo.ToString(),
                        Needed = item.Needed.ToString(),
                        Promised = item.Promised.ToString(),
                        Received = item.Received.ToString()
                    });
                }
            }
        }

        return result.ToArray();
    }

    public String GetLoggedInEmployeeNumber(int sessionID)
    {
        String result = "";

        if (AndroidAuthenticationController.IsValidSessionId(sessionID))
        {
            result = AndroidAuthenticationController.GetEmployeeIdFromSessionId(sessionID).ToString();
        }

        return result;
    }

    public string GetCurrentDisbursementNoForDepartment(int sessionID)
    {
        String result = "";

        if (AndroidAuthenticationController.IsValidSessionId(sessionID))
        {
            result = AndroidController.GetDisbursementNoForCurrentDepartmentOf(sessionID);
        }

        return result;
    }

    public bool UpdateDisbursementDetail(int sessionID, WCFDisbursementDetail updatedDisbursementDetail)
    {
        bool result = false;

        if (AndroidAuthenticationController.IsValidSessionId(sessionID))
        {
            DisbursementDetail disbursement = new DisbursementDetail()
            {
                DisbursementNo = int.Parse(updatedDisbursementDetail.DisbursementNo),
                ItemNo = updatedDisbursementDetail.ItemNo,
                Needed = int.Parse(updatedDisbursementDetail.Needed),
                Promised = int.Parse(updatedDisbursementDetail.Promised),
                Received = int.Parse(updatedDisbursementDetail.Received)
            };

            result = AndroidController.UpdateDisbursement(disbursement);
        }

        return result;
    }

    public bool AddRequisitionDetail(int sessionID, WCFRequisitionDetail addRequisitionDetail)
    {
        bool result = false;

        if (AndroidAuthenticationController.IsValidSessionId(sessionID))
        {
            RequisitionDetail requisition = new RequisitionDetail()
            {
                ReqNo = addRequisitionDetail.ReqNo,
                ItemNo = addRequisitionDetail.ItemNo,
                Qty = addRequisitionDetail.Qty
            };

            result = AndroidController.AddRequisitionDetail(requisition);
        }

        return result;
    }

    public bool RemoveRequisitionDetail(int sessionID, WCFRequisitionDetail removeRequisitionDetail)
    {
        bool result = false;

        if (AndroidAuthenticationController.IsValidSessionId(sessionID))
        {
            RequisitionDetail requisition = new RequisitionDetail()
            {
                ReqNo = removeRequisitionDetail.ReqNo,
                ItemNo = removeRequisitionDetail.ItemNo,
                Qty = removeRequisitionDetail.Qty
            };

            result = AndroidController.RemoveRequisitionDetail(requisition);
        }

        return result;
    }

    public bool UpdateRequisitionDetail(int sessionID, WCFRequisitionDetail removeRequisitionDetail)
    {
        bool result = false;

        if (AndroidAuthenticationController.IsValidSessionId(sessionID))
        {
            RequisitionDetail requisition = new RequisitionDetail()
            {
                ReqNo = removeRequisitionDetail.ReqNo,
                ItemNo = removeRequisitionDetail.ItemNo,
                Qty = removeRequisitionDetail.Qty
            };

            result = AndroidController.UpdateRequisitionDetail(requisition);
        }

        return result;
    }

    public bool AddRequisition(int sessionID, WCFRequisition addRequisition)
    {
        bool result = false;

        if (AndroidAuthenticationController.IsValidSessionId(sessionID))
        {
            Requisition requisition = new Requisition()
            {
                ReqNo = addRequisition.ReqNo,
                DateIssued = Convert.ToDateTime(addRequisition.DateIssued),
                ApprovedBy = addRequisition.ApprovedBy,
                DateReviewed = Convert.ToDateTime(addRequisition.DateReviewed),
                Status = addRequisition.Status,
                Remarks = addRequisition.Remarks
};

            result = AndroidController.AddRequisition(requisition);
        }

        return result;
    }

    public WCFRequisition[] GetPendingRequisitions(int sessionID, string empNo)
    {
        List<WCFRequisition> result = new List<WCFRequisition>();

        if (AndroidAuthenticationController.IsValidSessionId(sessionID))
        {
            List<Requisition> items = AndroidController.GetPendingRequisitions(empNo);

            if (items != null)
            {
                foreach (var item in items)
                {
                    result.Add(new WCFRequisition
                    {
                        ReqNo = item.ReqNo,
                        DateIssued = item.DateIssued.ToString(),
                        ApprovedBy = (int)item.ApprovedBy,
                        DateReviewed = item.DateReviewed.ToString(),
                        Status = item.Status,
                        Remarks = item.Remarks
                    });
                }
            }
        }

        return result.ToArray();
    }

    public WCFRequisition GetRequisitionById(int sessionID, int reqNo)
    {
       WCFRequisition result = null;

        if (AndroidAuthenticationController.IsValidSessionId(sessionID))
        {
            Requisition item = AndroidController.GetRequisitionById(reqNo);

            if (item != null)
            {
                    result = new WCFRequisition
                    {
                        ReqNo = item.ReqNo,
                        DateIssued = item.DateIssued.ToString(),
                        ApprovedBy = (int)item.ApprovedBy,
                        DateReviewed = item.DateReviewed.ToString(),
                        Status = item.Status,
                        Remarks = item.Remarks
                    };
            }
        }

        return result;
    }

    public bool UpdateRequisition(int sessionID, WCFRequisition updatedRequisition)
    {
        bool result = false;

        if (AndroidAuthenticationController.IsValidSessionId(sessionID))
        {
            Requisition requisition = new Requisition()
            {
                ReqNo = updatedRequisition.ReqNo,
                DateIssued = Convert.ToDateTime(updatedRequisition.DateIssued),
                ApprovedBy = updatedRequisition.ApprovedBy,
                DateReviewed = Convert.ToDateTime(updatedRequisition.DateReviewed),
                Status = updatedRequisition.Status,
                Remarks = updatedRequisition.Remarks
            };

            result = AndroidController.UpdateRequisition(requisition);
        }

        return result;
    }



    public bool RemoveRequisition(int sessionID, WCFRequisition removedRequisition)
    {
        bool result = false;

        if (AndroidAuthenticationController.IsValidSessionId(sessionID))
        {
            Requisition requisition = new Requisition()
            {
                ReqNo = removedRequisition.ReqNo,
                DateIssued = Convert.ToDateTime(removedRequisition.DateIssued),
                ApprovedBy = removedRequisition.ApprovedBy,
                DateReviewed = Convert.ToDateTime(removedRequisition.DateReviewed),
                Status = removedRequisition.Status,
                Remarks = removedRequisition.Remarks
            };

            result = AndroidController.RemoveRequisition(requisition);
        }

        return result;
    }
}