using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LUSSIS_Backend
{
    // Stub class to send some data back to the user
    public class StubImpl : ILussis
    {
        public int GenerateAndroidSession(int employeeID)
        {
            return 9999;
        }

        public bool CheckAndroidSession(int employeeID, int sessionID)
        {
            return true;
        }
    }
}
