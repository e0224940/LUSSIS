using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LUSSIS_Backend
{
    public class AndroidController
    {
        public static String GetCollectionPointOf(int collectionPointNo)
        {
            String result = "";
            LussisEntities context = new LussisEntities();
            CollectionPoint collectionPoint = context.CollectionPoints
                .Where(collPoint => collPoint.CollectionPointNo == collectionPointNo)
                .FirstOrDefault();

            if (collectionPoint != null)
            {
                result = collectionPoint.CollectionPointDetails;
            }

            return result;
        }

        public static Department GetDepartment(String deptCode)
        {
            LussisEntities context = new LussisEntities();
            return context.Departments.Where(dept => dept.DeptCode.Equals(deptCode)).FirstOrDefault();
        }

        public static List<Department> GetAllDepartments()
        {
            LussisEntities context = new LussisEntities();
            return context.Departments.ToList();
        }

        public static Disbursement GetCurrentDisbursementForDepartment(String deptCode)
        {
            // TODO : IS IT ASCENDING OR DESCENDING ORDER
            LussisEntities context = new LussisEntities();
            return context.Disbursements.Where(dis => dis.DeptCode.Equals(deptCode) && dis.Status.Equals("Pending")).OrderByDescending(dis => dis.DisbursementDate).FirstOrDefault();
        }

        public static Employee GetEmployee(int empNo)
        {
            LussisEntities context = new LussisEntities();
            return context.Employees.Where(emp => emp.EmpNo.Equals(empNo)).FirstOrDefault();
        }

        public static List<StationeryCatalogue> GetCatalogue()
        {
            LussisEntities context = new LussisEntities();
            return context.StationeryCatalogues.ToList();
        }

        public static List<StationeryCatalogue> SearchCatalogue(string itemNo, string category, string description, string bin)
        {
            LussisEntities context = new LussisEntities();

            try
            {
                return context.StationeryCatalogues
                    .Where(item =>
                       item.ItemNo.Contains(itemNo)
                       || item.Category.Contains(category)
                       || item.Description.Contains(description)
                       || item.Bin.Contains(bin)
                    )
                    .ToList();
            }
            catch (Exception e)
            {
                List<StationeryCatalogue> errorList = new List<StationeryCatalogue>();
                errorList.Add(new StationeryCatalogue() { Description = e.Message.Substring(0, 50) });
                return errorList;
            }
        }

        public static List<DisbursementDetail> GetDisbursementDetailsOf(int disbursementNo)
        {
            LussisEntities context = new LussisEntities();
            return context.DisbursementDetails.Where(dis => dis.DisbursementNo.Equals(disbursementNo)).ToList();
        }

        public static string GetDisbursementNoForCurrentDepartmentOf(int sessionID)
        {
            Employee employee = AndroidAuthenticationController.GetDetailsOfEmployee(sessionID);
            return employee
                .Department
                .Disbursements
                .Where(dis => dis.Status.Equals("Pending"))
                .OrderByDescending(dis => dis.DisbursementDate)
                .FirstOrDefault()
                .DisbursementNo
                .ToString();
        }

        public static bool UpdateDisbursement(DisbursementDetail updatedDisbursementDetail)
        {
            bool result = false;
            try
            {
                LussisEntities context = new LussisEntities();
                DisbursementDetail disbursementDetail = context.DisbursementDetails
                    .Where(dis => dis.DisbursementNo.Equals(updatedDisbursementDetail.DisbursementNo)
                    && dis.ItemNo.Equals(updatedDisbursementDetail.ItemNo))
                    .FirstOrDefault();

                if (disbursementDetail != null)
                {
                    disbursementDetail.Needed = updatedDisbursementDetail.Needed;
                    disbursementDetail.Promised = updatedDisbursementDetail.Promised;
                    disbursementDetail.Received = updatedDisbursementDetail.Received;

                    context.SaveChanges();

                    result = true;
                }
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public static bool AddRequisitionDetail(RequisitionDetail addRequisitionDetail)
        {
            bool result = false;
            try
            {
                LussisEntities context = new LussisEntities();
                /*RequisitionDetail requisitionDetail = context.RequisitionDetails
                    .Where(req => req.ReqNo.Equals(addRequisitionDetail.ReqNo))
                    .FirstOrDefault();

                if (requisitionDetail != null)
                {
                    requisitionDetail.ReqNo = addRequisitionDetail.ReqNo;
                    requisitionDetail.ItemNo = addRequisitionDetail.ItemNo;
                    requisitionDetail.Qty = addRequisitionDetail.Qty;*/

                RequisitionDetail requisitionDetail = new RequisitionDetail();
                requisitionDetail.ReqNo = addRequisitionDetail.ReqNo;
                requisitionDetail.ItemNo = addRequisitionDetail.ItemNo;
                requisitionDetail.Qty = addRequisitionDetail.Qty;

                context.RequisitionDetails.Add(requisitionDetail);
                context.SaveChanges();

                result = true;
            }

            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public static bool RemoveRequisitionDetail(RequisitionDetail removeRequisitionDetail)
        {
            bool result = false;
            try
            {
                LussisEntities context = new LussisEntities();
                RequisitionDetail requisitionDetail = context.RequisitionDetails
                    .Where(req => req.ReqNo.Equals(removeRequisitionDetail.ReqNo)
                    && req.ItemNo.Equals(removeRequisitionDetail.ItemNo)
                    && req.Qty.Equals(removeRequisitionDetail.Qty)).FirstOrDefault();

                if (requisitionDetail != null)
                {

                    context.RequisitionDetails.Remove(requisitionDetail);
                    context.SaveChanges();

                    result = true;
                }
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public static bool UpdateRequisitionDetail(RequisitionDetail updateRequisitionDetail)
        {
            bool result = false;
            try
            {
                LussisEntities context = new LussisEntities();
                RequisitionDetail requisitionDetail = context.RequisitionDetails
                    .Where(req => req.ReqNo.Equals(updateRequisitionDetail.ReqNo)
                    && req.ItemNo.Equals(updateRequisitionDetail.ItemNo)).FirstOrDefault();

                if (requisitionDetail != null)
                {
                    requisitionDetail.ReqNo = updateRequisitionDetail.ReqNo;
                    requisitionDetail.ItemNo = updateRequisitionDetail.ItemNo;
                    requisitionDetail.Qty = updateRequisitionDetail.Qty;

                    context.SaveChanges();

                    result = true;
                }
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public static int AddRequisition(Requisition requisition)
        {
            int result = -1;
            try
            {
                using (LussisEntities context = new LussisEntities())
                {
                    context.Requisitions.Add(requisition);
                    context.SaveChanges();
                    result = requisition.ReqNo;
                }
            }
            catch (Exception)
            {
                result = -1;
            }

            return result;

        }

        public static List<Requisition> GetPendingRequisitions(string employeeCode)
        {
            List<Requisition> list_result = new List<Requisition>();

            using (LussisEntities context = new LussisEntities())
            {
                string departmentCode = context.Departments.Where(x => x.HeadEmpNo.Equals(employeeCode)).First().DeptCode;
                List<Employee> list_e = context.Employees.Where(x => x.DeptCode.Equals(departmentCode)).ToList();
                List<Requisition> list_r = context.Requisitions.ToList();


                foreach (Employee e in list_e)
                {
                    foreach (Requisition rr in list_r)
                    {
                        if (e.EmpNo.Equals(rr.IssuedBy) && rr.Status.Equals("Pending"))        //to tally with the rest of the code if its pending
                        {
                            list_result.Add(rr);
                        }
                    }
                }
            }
            return list_result;
        }

        public static Requisition GetRequisitionById(int reqNo)
        {
            Requisition req = null;
            using (LussisEntities context = new LussisEntities())
            {
                req = context.Requisitions.Where(x => x.ReqNo.Equals(reqNo)).FirstOrDefault();
                return req;
            }
        }

        public static bool UpdateRequisition(Requisition requisition)
        {
            bool result = false;

            try
            {
                using (LussisEntities context = new LussisEntities())
                {
                    Requisition req = context.Requisitions.Where(x => x.ReqNo.Equals(requisition.ReqNo)).First();

                    if (req != null)
                    {
                        req.IssuedBy = requisition.IssuedBy;
                        req.DateIssued = requisition.DateIssued;
                        req.ApprovedBy = requisition.ApprovedBy;
                        req.DateReviewed = requisition.DateReviewed;
                        req.Status = requisition.Status;
                        req.Remarks = requisition.Remarks;
                    }
                    context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        public static bool RemoveRequisition(Requisition requisition)
        {
            bool result = false;
            try
            {
                using (LussisEntities context = new LussisEntities())
                {
                    context.Requisitions.Remove(requisition);
                    context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception)
            {
                result = false;
            }

            return result;

        }

        public static List<RequisitionDetail> GetRequisitionDetailsOf(int reqNo)
        {
            List<RequisitionDetail> result = new List<RequisitionDetail>();
            try
            {
                LussisEntities context = new LussisEntities();
                result = context.RequisitionDetails.Where(req => req.ReqNo.Equals(reqNo)).ToList();
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }

        public static Retrieval GetLatestRetrieval()
        {
            Retrieval result = null;
            try
            {
                using (LussisEntities context = new LussisEntities())
                {
                    result = context.Retrievals.Last();
                }
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }

        public static List<RetrievalDetail> GetRetrievalDetails(string retrievalNo)
        {
            List<RetrievalDetail> result = null;
            try
            {
                using (LussisEntities context = new LussisEntities())
                {
                    result = context.RetrievalDetails.Where(ret => ret.RetrievalNo.Equals(retrievalNo)).ToList();
                }
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }
    }
}
