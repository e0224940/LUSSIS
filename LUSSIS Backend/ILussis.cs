using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LUSSIS_Backend
{
    public interface ILussis
    {
        // ANDROID - Generate a session id
        int GenerateAndroidSession(int employeeID);

        // ANDROID - Check session id
        bool CheckAndroidSession(int employeeID, int sessionID);

        // TODO : Add all required functionality required from Sequence Diagrams Here.
    }
}
