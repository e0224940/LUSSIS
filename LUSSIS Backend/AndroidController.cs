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
                    .Where( item =>
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
                errorList.Add(new StationeryCatalogue() { Description = e.Message.Substring(0,50) });
                return errorList;
            }
        }
    }
}
