using Email_Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LUSSIS_Backend
{

    public class UpdateCollectionPoint
    {
        public static string SearchLocation(int empNo)
        {
            LussisEntities entity = new LussisEntities();
            Employee currEmployee = entity.Employees.Where(emp => emp.EmpNo == empNo).First();
            Department currDepartment = entity.Departments.Where(dep => dep.DeptCode.Equals(currEmployee.DeptCode)).First();
            return currDepartment.CollectionPoint.CollectionPointDetails;
        }

        public static void UpdatePoint(int empNo, int collectionNewPoint)
        {
           
            string dayofweek = DateTime.Today.DayOfWeek.ToString();

            if (dayofweek != "Saturday" || dayofweek != "Sunday")
            {
                using (LussisEntities entities = new LussisEntities())
                {
                    Employee currEmploy = entities.Employees.Where(emp => emp.EmpNo == empNo).FirstOrDefault    ();
                    Department currDept = currEmploy.Department;
                    currDept.CollectionPointNo = collectionNewPoint;
                    entities.SaveChanges();
                }

                LussisEntities entity = new LussisEntities();
                Employee currEmployee = entity.Employees.Where(emp => emp.EmpNo == empNo).First();
                Department currDepartment = entity.Departments.Where(dep => dep.DeptCode.Equals(currEmployee.DeptCode)).First();
                
                // Send Email
                EmailBackend.sendEmailStep(
                    currDepartment.EmployeeHead.Email,
                    EmailTemplate.GenerateUpdateCollectionPointEmailSubject(),
                    EmailTemplate.GenerateCollectionPointStatusChangedEmail(
                        currDepartment.EmployeeHead.EmpName.ToString(),
                        currDepartment.CollectionPoint.CollectionPointDetails.ToString())
                    );
            }
        }
    }
}