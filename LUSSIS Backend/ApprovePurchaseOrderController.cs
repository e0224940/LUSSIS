using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LUSSIS_Backend
{
    public class ApprovePurchaseOrderController
    {
        //ApprovePOList functions
        public static List<PurchaseOrder> getPendingOrdersList()
        {
            LussisEntities context = new LussisEntities();
            var result = context.PurchaseOrders.Where(x => x.Status == "Pending").ToList();
            return result;
        }

        //ApprovePODetails functions
        public static List<ApprovePurchaseOrderView> getPurchaseOrderDetails(int poNO)
        {
            LussisEntities context = new LussisEntities();
            var result = context.ApprovePurchaseOrderViews.Where(x => x.PONo == poNO).ToList();
            return result;
        }

        //Button(Approve) function
        public static void setStatusApprove(int poNO)
        {
            LussisEntities context = new LussisEntities();
            var approvePurchaseOrderview = context.PurchaseOrders.Where(x => x.PONo == poNO).FirstOrDefault();
            approvePurchaseOrderview.Status = "Approved";
            context.SaveChanges();
        }

        //Button(Rejected) function
        public static void setStatusReject(int poNO)
        {
            LussisEntities context = new LussisEntities();
            var approvePurchaseOrderview = context.PurchaseOrders.Where(x => x.PONo == poNO).FirstOrDefault();
            approvePurchaseOrderview.Status = "Rejected";
            context.SaveChanges();
        }

    }
}
