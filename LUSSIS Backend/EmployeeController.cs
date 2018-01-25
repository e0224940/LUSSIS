using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Email_Backend;

namespace LUSSIS_Backend
{
    public class EmployeeController
    {
        public static List<StationeryCatalogue> ViewItem()
        {
            LussisEntities entity = new LussisEntities();
            return entity.StationeryCatalogues.ToList();
        }

        public static string GetName(int empNo)
        {
            LussisEntities entity = new LussisEntities();
            Employee currrentEmp = entity.Employees.Where(emp => emp.EmpNo == empNo).First();
            return currrentEmp.EmpName;
        }
        public static List<StationeryCatalogue> SearchDes(string value)
        {
            using (LussisEntities entities = new LussisEntities())
            {
                return entities.StationeryCatalogues.Where(p => p.Description.Contains(value.Trim())).ToList();
            }
        }
        public static List<RequisitionDetail> SearchReqNo(string value)
        {
            using (LussisEntities entities = new LussisEntities())
            {
                return entities.RequisitionDetails.Where(p => p.ReqNo.ToString().Contains(value.Trim())).ToList();
            }
        }


        //Add data into database: requisitionDetails
        public static void RaisedRequisitionDetails(string ItemNum, int qty)
        {
            using (LussisEntities entities = new LussisEntities())
            {

            }

        }
        //Add data into database: requisition
        public static void RaisedRequisition(int issueBy, DateTime dateIssue, string status, List<RequisitionDetail> r)
        {
            using (LussisEntities entities = new LussisEntities())
            {
                Requisition req = new Requisition
                {
                    IssuedBy = issueBy,
                    DateIssued = dateIssue,
                    Status = status
                };

                foreach (RequisitionDetail l in r)
                {
                    RequisitionDetail rl = new RequisitionDetail();
                    rl.ReqNo = req.ReqNo;
                    rl.ItemNo = l.ItemNo;
                    rl.Qty = l.Qty;
                    entities.RequisitionDetails.Add(l);
                }
                entities.Requisitions.Add(req);
                entities.SaveChanges();

                LussisEntities entity = new LussisEntities();
                Employee currEmployee = entity.Employees.Where(emp => emp.EmpNo == issueBy).First();
                Department currDepartment = entity.Departments.Where(dep => dep.DeptCode.Equals(currEmployee.DeptCode)).First();
                // Send Email
                EmailBackend.sendEmailStep(
                    currDepartment.EmployeeHead.Email,
                    EmailTemplate.GeneratePendingRequisitionSubject(currEmployee.ToString()),
                    EmailTemplate.GenerateCollectionPointStatusChangedEmail(currDepartment.EmployeeHead.ToString(), currEmployee.ToString()));
                
            }
        }

        public static List<Requisition> ViewRequisition()
        {
            LussisEntities entity = new LussisEntities();
            return entity.Requisitions.ToList();
        }

        //Delete row for Requisition History
        public static void DeleteReqHistory(int reqId)
        {
            using (LussisEntities entities = new LussisEntities())
            {
                List<RequisitionDetail> rd = entities.RequisitionDetails.Where(u => u.ReqNo == reqId).ToList<RequisitionDetail>();

                foreach (RequisitionDetail ff in rd)
                {
                    //delete from database
                    entities.RequisitionDetails.Remove(ff);

                }

                Requisition req = entities.Requisitions.Where(p => p.ReqNo == reqId).FirstOrDefault<Requisition>();
                entities.Requisitions.Remove(req);
                entities.SaveChanges();
            }
        }
    }
}


