using LUSSIS_Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace LussisScheduledTasks
{
    class Program
    {
        static DateTime previous=DateTime.Today.AddDays(-1);
        static DateTime current;

        static void Main(string[] args)
        {
            while(true)
            {
                current = DateTime.Today;
                if(DateTime.Compare(previous,current)!=0)
                {
                    previous = current;
                    {
                        ApproveAuthorityController.checkIfDeputyEndDateElapsed();
                        ApproveAuthorityController.checkIfDeputyStartDateElapsed();
                    }
                }
            }
        }
    }

   
}
