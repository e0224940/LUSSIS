using Email_Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LUSSIS_Backend
{
    public class ApproveAuthorityController
    {

        public static Employee getDeputyHeadOfDepartment(String deptCode)
        {
            using (LussisEntities context = new LussisEntities())
            {
                int w = (int)context.Departments.Where(x => x.DeptCode.Equals(deptCode)).First().DeputyEmpNo;

                Employee ww = context.Employees.Where(x => x.EmpNo.Equals(w)).First();

                return ww;
            }
        }

        public static string getDepartmentNoFromProfile(int profileEmpNo)
        {
            using (LussisEntities context = new LussisEntities())
            {
                return context.Employees.Where(x => x.EmpNo.Equals(profileEmpNo)).First().DeptCode;
            }
        }

        public static List<String> getEmployeesNameInDepartment(int profileEmpNo)
        {
            string deptNo = getDepartmentNoFromProfile(profileEmpNo);
            List<string> list_names;

            using (LussisEntities context = new LussisEntities())
            {
                List<Employee> list = context.Employees.Where(x => x.DeptCode.Equals(deptNo)).ToList();
                list_names = new List<string>();

                for (int i = 0; i < list.Count(); i++)
                {
                    list_names.Add(list[i].EmpName);
                }
            }

            return list_names;
        }

        public static int getEmpNoFromEmpName(string empName)
        {
            using (LussisEntities context = new LussisEntities())
            {
                Employee dep = context.Employees.Where(x => x.EmpName.Equals(empName)).First();
                return dep.EmpNo;
            }

        }

        public static string removeAuthority(int profileEmpNo, string deptCode, int outgoingEmpNo)
        {

            string recipientEmail;
            string recipientName;
            string deptName;
            string newDeputyName;
            using (LussisEntities context = new LussisEntities())
            {
                Deputy dep = context.Deputies.Where(x => x.DeptCode.Equals(deptCode)).First();
                context.Deputies.Remove(dep);

                Department dept = context.Departments.Where(x => x.DeptCode.Equals(deptCode)).First();
                dept.DeputyEmpNo = profileEmpNo;
                deptName = dept.DeptName;

                context.SaveChanges();

                RoleController.removeRoleFromEmployee(context, outgoingEmpNo, RoleController.LUSSISRoles.DepartmentDeputy);

                Employee newDeputy = context.Employees.Where(x => x.EmpNo.Equals(dept.EmployeeDeputy.EmpNo)).First();
                //txtBox_appAuth_currentHead.Text = newDeputy.EmpName;
                newDeputyName = newDeputy.EmpName;

            }

            using (LussisEntities context = new LussisEntities())
            {
                recipientEmail = context.Employees.Where(x => x.EmpNo.Equals(outgoingEmpNo)).First().Email;
                recipientName = context.Employees.Where(x => x.EmpNo.Equals(outgoingEmpNo)).First().EmpName;
            }

            EmailBackend.sendEmailStep(recipientEmail,
                EmailTemplate.GenerateOldDeputyAuthorityRemovedSubject(),
                EmailTemplate.GenerateOldDeputyAuthorityRemovedEmail(recipientName, deptName));

            return newDeputyName; //for display in view
        }

        public static string addAuthority(string deptCode, int empNo, DateTime start, DateTime end)
        {
            Deputy d = new Deputy();
            d.DeptCode = deptCode;
            d.DeputyEmpNo = empNo;
            d.FromDate = start;
            d.ToDate = end;

            string empName;
            string deptName;
            string recipientEmail;

            using (LussisEntities context = new LussisEntities())
            {
                context.Deputies.Add(d);
                Department dept = context.Departments.Where(x => x.DeptCode.Equals(deptCode)).First();
                dept.DeputyEmpNo = empNo;
                deptName = dept.DeptName;
                context.SaveChanges();
                RoleController.addRoleToEmployee(context, empNo, RoleController.LUSSISRoles.DepartmentDeputy);

                Employee newDeputy = context.Employees.Where(x => x.EmpNo.Equals(empNo)).First();
                empName = newDeputy.EmpName;
                recipientEmail = newDeputy.Email;
                //txtBox_appAuth_currentHead.Text = empName;

            }

            EmailBackend.sendEmailStep(recipientEmail,
            EmailTemplate.GenerateNewDeputyAuthoritySubject(),
            EmailTemplate.GenerateNewDeputyAuthorityEmail(empName, deptName, start.ToString(), end.ToString()));

            return empName;     //for display in view
        }
    }
}
