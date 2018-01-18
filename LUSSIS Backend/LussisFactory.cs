using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LUSSIS_Backend
{
    /* 
     * Anyone who wants access to the backend must use this class as follows:
     * 
     * ILussis backend = LussisFactory.GetBackend(); 
     * backend.doSomething();
     * 
     * If you want more functionality from the backend, contact backend developers
     * and have them add it to the ILussis Interface
     */ 
    public class LussisFactory
    {
        private static ILussis backend = null;
        public static ILussis GetBackend()
        {   
            if(backend == null)
            {
                // TODO : Change from Stub to Actual Implementation in the final stages of the project
                backend = new StubImpl();
            }

            return backend;
        }
    }
}
