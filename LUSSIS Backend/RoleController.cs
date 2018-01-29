using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LUSSIS_Backend
{
    public class RoleController
    {
        public enum LUSSISRoles
        {
            DepartmentHead,
            DepartmentDeputy,
            DepartmentEmployee,
            DepartmentRepresentative,
            StoreSupervisor,
            StoreManager,
            StoreClerk
        }

        private static String[] LUSSISRolesString =
        {
            "DepartmentHead",
            "DepartmentDeputy",
            "DepartmentEmployee",
            "DepartmentRepresentative",
            "StoreSupervisor",
            "StoreManager",
            "StoreClerk"
        };

        public static bool updateRolesOfEmployeeNo(LussisEntities context, int empNo)
        {
            bool result = false;
            try
            {

                String[] assignedRoles = getRolesOfEmployee(context, empNo);
                String[] expectedRoles = identifyRolesOfEmployee(context, empNo);

                // Remove Existing Roles
                foreach (LUSSISRoles role in Enum.GetValues(typeof(LUSSISRoles)))
                {
                    removeRoleFromEmployee(context, empNo, role);
                }

                // Add New Roles
                foreach (LUSSISRoles role in Enum.GetValues(typeof(LUSSISRoles)))
                {
                    if (expectedRoles.Contains(Enum.GetName(typeof(LUSSISRoles), role)))
                    {
                        addRoleToEmployee(context, empNo, role);
                    }
                }

                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public static void removeRoleFromEmployee(LussisEntities context, int employeeNo, LUSSISRoles roleToRemove)
        {
            if (context == null)
            {
                return;
            }

            String roleSelected = LUSSISRolesString[(int)roleToRemove];
            Employee employee = context.Employees
                .Where(emp => emp.EmpNo == employeeNo)
                .FirstOrDefault();
            aspnet_Roles selectedRole = context
                .aspnet_Roles
                .Where(role =>
                role
                    .RoleName
                    .Equals(roleSelected))
                .FirstOrDefault();

            if (employee != null)
            {
                String empNoToFind = employee.EmpNo.ToString();
                aspnet_Profile userProfile = null;

                foreach (aspnet_Profile profile in context.aspnet_Profile)
                {
                    if (profile.PropertyValuesString.Equals(empNoToFind))
                    {
                        userProfile = profile;
                        break;
                    }
                }

                if (userProfile != null && selectedRole != null)
                {
                    Guid userId = userProfile.UserId;
                    aspnet_Users userDetail = context.aspnet_Users.Where(user => user.UserId.Equals(userId)).FirstOrDefault();
                    userDetail.aspnet_Roles.Remove(selectedRole);
                    context.SaveChanges();
                }
            }
        }

        public static void addRoleToEmployee(LussisEntities context, int employeeNo, LUSSISRoles roleToAdd)
        {
            if (context == null)
            {
                return;
            }

            String roleSelected = LUSSISRolesString[(int)roleToAdd];
            Employee employee = context.Employees
                .Where(emp => emp.EmpNo == employeeNo)
                .FirstOrDefault();
            aspnet_Roles selectedRole = context
                .aspnet_Roles
                .Where(role =>
                role
                    .RoleName
                    .Equals(roleSelected))
                .FirstOrDefault();

            if (employee != null)
            {
                String empNoToFind = employee.EmpNo.ToString();
                aspnet_Profile userProfile = null;

                foreach (aspnet_Profile profile in context.aspnet_Profile)
                {
                    if (profile.PropertyValuesString.Equals(empNoToFind))
                    {
                        userProfile = profile;
                        break;
                    }
                }

                if (userProfile != null && selectedRole != null)
                {
                    Guid userId = userProfile.UserId;
                    aspnet_Users userDetail = context.aspnet_Users.Where(user => user.UserId.Equals(userId)).FirstOrDefault();
                    userDetail.aspnet_Roles.Add(selectedRole);
                    context.SaveChanges();
                }
            }
        }

        public static String[] identifyRolesOfEmployee(LussisEntities context, int employeeNo)
        {
            List<String> result = new List<string>();
            Employee employee = context.Employees.Where(emp => emp.EmpNo.Equals(employeeNo)).SingleOrDefault();

            if (employee != null && employee.Department != null)
            {
                // Assign Store Roles
                if(employee.DeptCode.Equals("STOR"))
                {
                    var managers = context.StoreAssignments.Where(assignment => assignment.Role.Equals("Manager"));
                    var supervisors = context.StoreAssignments.Where(assignment => assignment.Role.Equals("Supervisor"));

                    if (supervisors.Where(supervisor => supervisor.EmpNo.Equals(employee.EmpNo)).FirstOrDefault() != null)
                    {
                        result.Add(LUSSISRolesString[(int)LUSSISRoles.StoreSupervisor]);
                    }

                    if (managers.Where(manager => manager.EmpNo.Equals(employee.EmpNo)).FirstOrDefault() != null)
                    {
                        result.Add(LUSSISRolesString[(int)LUSSISRoles.StoreManager]);
                    }

                    if(!result.Contains(LUSSISRolesString[(int)LUSSISRoles.StoreSupervisor]) && !result.Contains(LUSSISRolesString[(int)LUSSISRoles.StoreManager]))
                    {
                        result.Add(LUSSISRolesString[(int)LUSSISRoles.StoreClerk]);
                    }
                }

                // Assign Department Roles
                {
                    if(employee.Department.HeadEmpNo.Equals(employee.EmpNo))
                    {
                        result.Add(LUSSISRolesString[(int)LUSSISRoles.DepartmentHead]);
                    }

                    if (employee.Department.DeputyEmpNo.Equals(employee.EmpNo))
                    {
                        result.Add(LUSSISRolesString[(int)LUSSISRoles.DepartmentDeputy]);
                    }

                    if (employee.Department.RepEmpNo.Equals(employee.EmpNo))
                    {
                        result.Add(LUSSISRolesString[(int)LUSSISRoles.DepartmentRepresentative]);
                    }

                    if (!result.Contains(LUSSISRolesString[(int)LUSSISRoles.DepartmentHead]) && !result.Contains(LUSSISRolesString[(int)LUSSISRoles.DepartmentDeputy]))
                    {
                        result.Add(LUSSISRolesString[(int)LUSSISRoles.DepartmentEmployee]);
                    }
                }
            }

            return result.ToArray();
        }

        public static String[] getRolesOfEmployee(LussisEntities context, int employeeNo)
        {
            String[] result = null;

            if (context == null)
            {
                return result;
            }

            Employee employee = context.Employees
                .Where(emp => emp.EmpNo == employeeNo)
                .FirstOrDefault();

            if (employee != null)
            {
                String empNoToFind = employee.EmpNo.ToString();
                aspnet_Profile userProfile = null;

                foreach (aspnet_Profile profile in context.aspnet_Profile)
                {
                    if (profile.PropertyValuesString.Equals(empNoToFind))
                    {
                        userProfile = profile;
                        break;
                    }
                }

                if (userProfile != null)
                {
                    Guid userId = userProfile.UserId;
                    result = context.aspnet_Users
                        .Where(user => user.UserId.Equals(userId))
                        .First()
                        .aspnet_Roles
                        .Select(role => role.RoleName)
                        .ToArray();
                }
            }

            return result;
        }
    }
}
