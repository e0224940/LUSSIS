using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LUSSIS_Backend
{
    public class LoginController
    {
        public static String[] setupRolesAfterAuthentication(string username)
        {
            LussisEntities context = new LussisEntities();
            String[] result = null;
             
            int empNo = getEmployeeNumberFromUserName(username);            

            if(RoleController.updateRolesOfEmployeeNo(context,empNo))
            {
                result = RoleController.getRolesOfEmployee(context, empNo);
            }

            return result;
        }

        private static int getEmployeeNumberFromUserName(string username)
        {
            int result = -1;

            try
            {
                LussisEntities context = new LussisEntities();
                aspnet_Users userDetail = context.aspnet_Users.Where(user => user.UserName.Equals(username)).FirstOrDefault();

                if (userDetail != null)
                {
                    result = Convert.ToInt32(userDetail.aspnet_Profile.PropertyValuesString);
                }
            }
            catch(Exception)
            {
                result = -2;
            }

            return result;
        }
    }
}
