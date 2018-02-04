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

        public static Deputy getDeputyDetails(String deptCode)
        {
            LussisEntities context = new LussisEntities();

            return context.Deputies.Where(x => x.DeptCode.Equals(deptCode)).FirstOrDefault();
        }

        public static Employee getHeadOfDepartment(String deptCode)
        {
            LussisEntities context = new LussisEntities();
            Department d = context.Departments.Where(c => c.DeptCode.Equals(deptCode)).FirstOrDefault();
            int headEmpNo = Convert.ToInt32(d.HeadEmpNo);
            Employee emp = context.Employees.Where(x => x.EmpNo.Equals(headEmpNo)).FirstOrDefault();

            return emp;
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

        public static List<Deputy> getDeputyDetailsForDept(string deptCode)
        
        {
            LussisEntities context = new LussisEntities();
            return context.Deputies.Where(x => x.DeptCode.Equals(deptCode)).OrderBy(x => x.FromDate).ToList();
        }

        public static int getEmpNoFromEmpName(string empName)
        {
            using (LussisEntities context = new LussisEntities())
            {
                Employee dep = context.Employees.Where(x => x.EmpName.Equals(empName)).First();
                return dep.EmpNo;
            }

        }

        public static void removeAuthority(int profileEmpNo, string deptCode, int outgoingEmpNo)
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
                dept.DeputyEmpNo = profileEmpNo; //setting back to HOD
                deptName = dept.DeptName;
               
                context.SaveChanges();

                RoleController.removeRoleFromEmployee(context, outgoingEmpNo, RoleController.LUSSISRoles.DepartmentDeputy);

                Employee newDeputy = context.Employees.Where(x => x.EmpNo.Equals(dept.EmployeeDeputy.EmpNo)).First();
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

        }

        public static string addAuthority(string deptCode, int empNo, DateTime start, DateTime end)
        {
            Deputy d = new Deputy();
            d.DeptCode = deptCode;
            d.DeputyEmpNo = empNo;
            d.FromDate = start;
            d.ToDate = end;

            string empName="";
            string deptName;
            string recipientEmail="";

            using (LussisEntities context = new LussisEntities())
            {
                context.Deputies.Add(d);
                Department dept = context.Departments.Where(x => x.DeptCode.Equals(deptCode)).First();

                deptName = dept.DeptName;       //for email


                if (start.CompareTo(DateTime.Today)==0)
                {
                    dept.DeputyEmpNo = empNo;
                    RoleController.addRoleToEmployee(context, empNo, RoleController.LUSSISRoles.DepartmentDeputy);
                }
                    Employee newDeputy = context.Employees.Where(x => x.EmpNo.Equals(empNo)).First();
                    empName = newDeputy.EmpName;
                    recipientEmail = newDeputy.Email;

                context.SaveChanges();
            }

            EmailBackend.sendEmailStep(recipientEmail,
            EmailTemplate.GenerateNewDeputyAuthoritySubject(),
            EmailTemplate.GenerateNewDeputyAuthorityEmail(empName, deptName, start.ToString(), end.ToString()));

            return empName;     //for display in view
        }

        public static void UpdateDeputy(Deputy d)
        {
            LussisEntities context = new LussisEntities();
            Deputy updateDep = context.Deputies.Where(x => x.DeptCode.Equals(d.DeptCode))
                .Where(x => x.DeputyEmpNo.Equals(d.DeputyEmpNo)).First();
            updateDep.FromDate = d.FromDate;
            updateDep.ToDate = d.ToDate;
            context.SaveChanges();
        }

        public static void removeDeputy(string depCode, int depEmpNo)
        {
            LussisEntities context = new LussisEntities();

            //int depEmpNo = context.Employees.Where(x => x.EmpName.Equals(depEmpName)).First().EmpNo;
            Deputy removeDep = context.Deputies.Where(x => x.DeptCode.Equals(depCode)).First();
            context.Deputies.Remove(removeDep);
            context.SaveChanges();
        }

        public static void checkIfDeputyStartDateElapsed()
        {
            LussisEntities context = new LussisEntities();
            
                List<Deputy> listd = context.Deputies.ToList();

            if (listd.Count != 0)
            {
                for (int i = 0; i < listd.Count; i++)
                {
                    Deputy d = listd[i];
                    Department dept = context.Departments.Where(x => x.DeptCode.Equals(d.DeptCode)).First();
                    if (d.FromDate.Equals(DateTime.Today))
                    {
                        dept.DeputyEmpNo = d.DeputyEmpNo;
                        RoleController.addRoleToEmployee(context, (int)d.DeputyEmpNo, RoleController.LUSSISRoles.DepartmentDeputy);
                    }
                }
            }
            context.SaveChanges();
        }

        public static void checkIfDeputyEndDateElapsed()
        {
            using (LussisEntities context = new LussisEntities())
            {
                List<Deputy> listd = context.Deputies.ToList();
                if(listd.Count!=0)
                {
                    for(int i =0;i<listd.Count;i++)
                    {
                        Deputy d = listd[i];
                        Department dept = context.Departments.Where(x => x.DeputyEmpNo.Equals(d.DeputyEmpNo)).First();
                        if(d.ToDate.Equals(DateTime.Today))
                        {
                            context.Deputies.Remove(d);
                            dept.DeputyEmpNo = dept.HeadEmpNo;
                            RoleController.removeRoleFromEmployee(context, (int) d.DeputyEmpNo, RoleController.LUSSISRoles.DepartmentDeputy);
                        }
                    }
                    context.SaveChanges();
                }
                
            }
        }
    }
}
