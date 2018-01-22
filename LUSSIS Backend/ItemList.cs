using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LUSSIS_Backend
{
    public class ItemList
    {
        public static List<StationeryCatalogue> GetDescription()
        {
            LussisEntities context = new LussisEntities();
            return context.StationeryCatalogues.ToList();
        }
    }
}
