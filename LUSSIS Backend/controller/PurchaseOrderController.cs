using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LUSSIS_Backend.controller
{
    public class PurchaseOrderController
    {
        public static List<PurchaseOrderDetail> GetPurchaseOrderDetailsOf(int purchaseOrderNumber)
        {
            List<PurchaseOrderDetail> poDetails = new List<PurchaseOrderDetail>();
            LussisEntities context = new LussisEntities();
            poDetails = context.PurchaseOrderDetails.Where(po => po.PONo == purchaseOrderNumber).ToList();
            return poDetails;
        }
    }
}
