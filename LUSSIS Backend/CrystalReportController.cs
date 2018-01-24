using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LUSSIS_Backend
{
    public class CrystalReportController
    {
        public static IList populateCrystalReport(string supplierSelected)
        {
            LussisEntities context = new LussisEntities();

            var result = context.ReorderTrendViews
    .Where(x => x.SupplierName == supplierSelected)
        .GroupBy(x => x.Description)
        .Select( y => new
        {
            Description = y.Description,
            Qty = y.Sum(x => x.Qty)
        })
.ToList();


            //var result = context.ReorderTrendViews
            //    .Where(x => x.SupplierName == supplierSelected)
            //    .GroupBy(x => new { x.Description, x.Qty })
            //    .Select(x => new 
            //    {
            //        Description = x.Description,
            //        Qty = x.Qty ?? 0,
            //    })
            //.ToList();

            return result;
        }

    }
}
