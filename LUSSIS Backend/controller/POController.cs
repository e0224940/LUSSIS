using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace LUSSIS_Backend.controller
{
    public class POController
    {
        public static List<PurchaseOrder> GetPendingPOsByOrderedEmp(int empNo)
        {
            LussisEntities context = new LussisEntities();
            return context.PurchaseOrders.Where(x => x.Status.Equals("Pending") && x.OrderedBy == empNo).ToList();
        }

        public static List<PurchaseOrderDetail> GetPODs(int pONo)
        {
            LussisEntities context = new LussisEntities();
            return context.PurchaseOrders.Where(x => x.PONo == pONo).FirstOrDefault().PurchaseOrderDetails.ToList();
        }

        public static PurchaseOrder GetPurchaseOrder(int pONo)
        {
            LussisEntities context = new LussisEntities();
            return context.PurchaseOrders.Where(x => x.PONo == pONo).FirstOrDefault();
        }

        public static void UpdatePODQty(int pONo, string itemNo, int qty)
        {
            LussisEntities context = new LussisEntities();
            PurchaseOrderDetail pOD = context.PurchaseOrderDetails.Where(x => x.PONo == pONo && x.ItemNo.Equals(itemNo)).FirstOrDefault();
            pOD.Qty = qty;
            context.SaveChanges();
        }

        public static void DeletePO(int pONo)
        {
            LussisEntities context = new LussisEntities();
            using (var txn = new TransactionScope())
            {
                // Remove PurchaseOrderDetails
                List<PurchaseOrderDetail> pODs = context.PurchaseOrders.Where(x => x.PONo == pONo).FirstOrDefault().PurchaseOrderDetails.ToList();
                foreach (PurchaseOrderDetail pOD in pODs)
                {
                    context.PurchaseOrderDetails.Remove(pOD);
                }

                // Remove PurchaseOrder
                PurchaseOrder pO = context.PurchaseOrders.Where(x => x.PONo == pONo).FirstOrDefault();
                context.PurchaseOrders.Remove(pO);

                context.SaveChanges();
                txn.Complete();
            }
        }

        public static void DeletePOD(int pONo, string itemNo)
        {
            LussisEntities context = new LussisEntities();
            PurchaseOrderDetail pOD = context.PurchaseOrderDetails.Where(x => x.PONo == pONo && x.ItemNo.Equals(itemNo)).FirstOrDefault();
            context.PurchaseOrderDetails.Remove(pOD);
            context.SaveChanges();
        }

        public static decimal? GetUnitPrice(string itemNo, string supplier1)
        {
            LussisEntities context = new LussisEntities();
            return context.SupplyTenders.Where(x => x.ItemNo.Equals(itemNo) && x.SupplierCode.Equals(supplier1)).FirstOrDefault().UnitPrice;
        }
    }
}
