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
     * If you want mnore functionality from the backend, contact backend developpers
     * and have them add it to the 
     */ 
    class LussisFactory
    {
        private static LussisImpl backend = null;
        static ILussis GetBackend()
        {
            if(backend == null)
            {
                backend = new LussisImpl();
            }

            return backend;
        }
    }
}
