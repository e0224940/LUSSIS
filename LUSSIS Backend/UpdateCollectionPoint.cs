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
            // bool result = false;
            Employee currEmployee = entity.Employees.Where(emp => emp.EmpNo == empNo).First();
            Department currDepartment = entity.Departments.Where(dep => dep.DeptCode.Equals(currEmployee.DeptCode)).First();
            return currDepartment.CollectionPoint.CollectionPointDetails;
        }

        public static void AddCollectionPoint(string newPoint)
        {
            using (LussisEntities entities = new LussisEntities())
            {
                CollectionPoint point = new CollectionPoint
                {
                    CollectionPointDetails = newPoint,
                };
                entities.CollectionPoints.Add(point);
                entities.SaveChanges();
            }
        }

        public static void UpdatePoint(int empNo, int collectionNewPoint)
        {
           
            string dayofweek = DateTime.Today.DayOfWeek.ToString();
            //string hour = DateTime.Today.Hour.ToString();
            //string minute = DateTime.Now.Minute.ToString();


            if (dayofweek != "Saturday" || dayofweek != "Sunday")
            {
                using (LussisEntities entities = new LussisEntities())
                {
                    Employee currEmploy = entities.Employees.Where(emp => emp.EmpNo == empNo).First();
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
                    EmailTemplate.GenerateUpdateCollectionPointEmailSubject(
                        currDepartment.CollectionPoint.ToString(),
                        currDepartment.EmployeeHead.ToString()),
                    EmailTemplate.GenerateCollectionPointStatusChangedEmail(
                        currDepartment.EmployeeHead.ToString(),
                        currDepartment.CollectionPoint.ToString())
                    );
            }
        }

    }
}