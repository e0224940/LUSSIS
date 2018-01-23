using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LUSSIS_Backend
{
    public class UpdateRepresentativeController
    {
        public static Employee GetCurrentRepresentativeOf(int employeeNo)
        {
            LussisEntities context = new LussisEntities();
            Employee result = null;
            Employee currEmployee = context.Employees
                .Where(emp => emp.EmpNo == employeeNo)
                .FirstOrDefault();
            Department currDepartment = null;

            if (currEmployee != null)
            {
                currDepartment = context.Departments
                    .Where(dep => dep.DeptCode.Equals(currEmployee.DeptCode))
                    .FirstOrDefault();

                if (currDepartment != null)
                {
                    result = currDepartment.EmployeeRepresentative;
                }
            }

            return result;
        }

        public static List<Employee> GetAlternativeRepresentativesFor(int employeeNo)
        {
            LussisEntities context = new LussisEntities();
            List<Employee> result = new List<Employee>();
            Employee currEmployee = context.Employees
                .Where(emp => emp.EmpNo == employeeNo)
                .FirstOrDefault();
            Department currDepartment = null;

            if (currEmployee != null)
            {
                currDepartment = context.Departments
                    .Where(dep => dep.DeptCode.Equals(currEmployee.DeptCode))
                    .FirstOrDefault();

                if (currDepartment != null)
                {
                    result = currDepartment.Employees
                        .Where(emp => emp.EmpNo != currDepartment.EmployeeRepresentative.EmpNo)
                        .ToList();
                }
            }

            return result;
        }

        public static bool SetNewDepartmentRepresentativeTo(int employeeNo, int newRepresentativeNo)
        {
            LussisEntities context = new LussisEntities();
            bool result = false;
            Employee currEmployee = context.Employees
                .Where(emp => emp.EmpNo == employeeNo)
                .FirstOrDefault();
            Employee newRepresentative = context.Employees
                .Where(emp => emp.EmpNo == newRepresentativeNo)
                .FirstOrDefault();
            Department currDepartment = null;
            Employee oldRepresentative = null;
            aspnet_Roles representativeRole = context
                .aspnet_Roles
                .Where(role =>
                role
                    .RoleName
                    .Equals("DepartmentRepresentative"))
                .FirstOrDefault();

            try
            {
                if (currEmployee != null && newRepresentative != null)
                {
                    currDepartment = context.Departments
                        .Where(dep => dep.DeptCode.Equals(currEmployee.DeptCode))
                        .FirstOrDefault();

                    if (currDepartment != null)
                    {
                        // Remove the role from the old employee
                        oldRepresentative = currDepartment.EmployeeRepresentative;
                        if (oldRepresentative != null)
                        {
                            aspnet_Profile oldUserProfile = context
                                .aspnet_Profile
                                .Where(profile =>
                                    profile
                                        .PropertyValuesString
                                    .Equals(oldRepresentative.EmpNo.ToString()))
                                .FirstOrDefault();

                            if (oldUserProfile != null && representativeRole != null)
                            {
                                aspnet_Users oldUser = context.aspnet_Users.Where(user => user.UserId.Equals(oldUserProfile.UserId)).FirstOrDefault();
                                oldUser.aspnet_Roles.Remove(representativeRole);

                            }
                        }

                        // Add the role to the new employee
                        if (newRepresentative != null)
                        {
                            aspnet_Profile newUserProfile = context
                                .aspnet_Profile
                                .Where(profile =>
                                    profile
                                        .PropertyValuesString
                                    .Equals(newRepresentative.EmpNo.ToString()))
                                .FirstOrDefault();

                            if (newUserProfile != null && representativeRole != null)
                            {
                                aspnet_Users newUser = context.aspnet_Users.Where(user => user.UserId.Equals(newUserProfile.UserId)).FirstOrDefault();
                                newUser.aspnet_Roles.Add(representativeRole);
                            }
                        }

                        // Mark the new representative in the database
                        currDepartment.RepEmpNo = newRepresentative.EmpNo;
                        context.SaveChanges();
                        result = true;
                    }
                }
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }
    }
}
