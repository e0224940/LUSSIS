using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LUSSIS_Backend
{
    public class ViewPendingRequisitionsController
    {
        public static List<Requisition> GetPendingRequisitions(int employeeID)
        {
            List<Requisition> result = new List<Requisition>();
            LussisEntities context = new LussisEntities();

            // Get the Employee details
            Employee employee = context.Employees.Where(emp => emp.EmpNo == employeeID).FirstOrDefault();

            if (employee != null)
            {
                // Fetch Pending requisitions
                result = context.Requisitions.Where(
                    requisition =>
                    requisition.EmployeeWhoIssued.DeptCode == employee.DeptCode
                    && requisition.Status == "Pending"
                    ).ToList();
            }

            return result;
        }
    }
}
