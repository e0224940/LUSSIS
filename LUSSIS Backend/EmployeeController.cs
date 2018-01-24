using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //using (LussisEntities entities = new LussisEntities())
            //{
            //    RequisitionDetail reqDe = new RequisitionDetail
            //    {
            //        ItemNo = ItemNum,
            //        Qty = qty

            //    };
            //    entities.RequisitionDetails.Add(reqDe);
            //    entities.SaveChanges();
            //}

            
           

        }
        //Add data into database: requisition
        public static void RaisedRequisition(int issueBy,DateTime dateIssue,string status)
        {
            using (LussisEntities entities = new LussisEntities())
            {
                Requisition req = new Requisition
                {
                    IssuedBy = issueBy ,
                    DateIssued = dateIssue,
                    Status = status
                };
                entities.Requisitions.Add(req);
                entities.SaveChanges();
            }
        }

       

    }
}


