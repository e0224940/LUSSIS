using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LUSSIS_Backend
{
    public class AndroidAuthenticationController
    {
        public static int GenerateAndroidSessionNumber(int employeeID)
        {
            // If there's a problem, defaults to -1
            int result = -1;
            LussisEntities context = new LussisEntities();

            // Retrieve the employee
            Employee employee = context.Employees.Where(emp => emp.EmpNo == employeeID).FirstOrDefault();

            if (employee != null)
            {
                // Generate the session number
                result = new Random().Next(10000, 99999);
                while (context.Employees.Where(emp => emp.SessionNo == result).FirstOrDefault() != null)
                {
                    // Keep Generating Another Session number until its a unique one
                    // TODO: Find a better way to do this
                    result += 1;

                    while (result > 99999)
                    {
                        result = 10000;
                    }
                }

                // Set the session number and expiry (today)
                employee.SessionNo = result;
                employee.SessionExpiry = DateTime.Now.Date;

                // Update the database
                context.SaveChanges();
            }

            // Return the session number
            return result;
        }

        public static bool IsValidSessionId(int sessionID)
        {
            bool result = false;
            LussisEntities context = new LussisEntities();

            // Retrieve the employee
            Employee employee = context.Employees.Where(emp => emp.SessionNo == sessionID).FirstOrDefault();

            if (employee != null)
            {
                DateTime expiry = employee.SessionExpiry ?? DateTime.MinValue;
                if (DateTime.Compare(DateTime.Today, expiry) >= 0)
                {
                    result = true;
                }
            }

            return result;
        }

        public static int GetEmployeeIdFromSessionId(int sessionID)
        {
            int result = -1;
            LussisEntities context = new LussisEntities();

            if (IsValidSessionId(sessionID))
            {
                // Retrieve the employee
                Employee employee = context.Employees.Where(emp => emp.SessionNo == sessionID).FirstOrDefault();

                // Get the session number
                result = (int)employee.SessionNo;
            }

            return result;
        }

        public static Employee GetDetailsOfEmployee(int sessionID)
        {
            Employee result = null;
            LussisEntities context = new LussisEntities();

            if (IsValidSessionId(sessionID))
            {
                // Retrieve the employee
                result = context.Employees.Where(emp => emp.SessionNo == sessionID).FirstOrDefault();
            }

            return result;
        }

        public String[] GetRolesOf(int sessionID)
        {
            String[] result = new String[] { };
            LussisEntities context = new LussisEntities();

            if (IsValidSessionId(sessionID))
            {
                // Retrieve the employee
                Employee employee = context.Employees.Where(emp => emp.SessionNo == sessionID).FirstOrDefault();

                result = RoleController.getRolesOfEmployee(context, employee.EmpNo);
            }

            return result;
        }
    }
}
