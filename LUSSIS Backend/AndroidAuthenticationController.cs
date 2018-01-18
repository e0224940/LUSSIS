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

            if(employee != null)
            {
                // Generate the session number
                result = new Random().Next(10000, 99999);

                // Set the session number and expiry (today)
                employee.SessionNo = result;
                employee.SessionExpiry = DateTime.Now.Date;

                // Update the database
                context.SaveChanges();
            }

            // Return the session number
            return result;
        }
    }
}
