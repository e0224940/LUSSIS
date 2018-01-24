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

        public static void removeRoleFromEmployee(LussisEntities context, int employeeNo, LUSSISRoles roleToRemove)
        {
            if(context == null)
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

                foreach(aspnet_Profile profile in context.aspnet_Profile)
                {
                    if(profile.PropertyValuesString.Equals(empNoToFind))
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
    }
}
