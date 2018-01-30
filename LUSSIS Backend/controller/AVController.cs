using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace LUSSIS_Backend.controller
{
    public static class AVController
    {
        public static List<AdjustmentVoucher> GetPendingAdjustmentVouchersByIssueEmp(int empNo)
        {
            LussisEntities context = new LussisEntities();
            return context.AdjustmentVouchers.Where(x => x.IssueEmpNo == empNo && x.Status.Equals("Pending")).ToList<AdjustmentVoucher>();
        }

        public static List<AdjustmentVoucherDetail> GetAVDetails(int aVNo)
        {
            LussisEntities context = new LussisEntities();
            List<AdjustmentVoucherDetail> result = context.AdjustmentVoucherDetails.Where(x => x.AvNo == aVNo).ToList<AdjustmentVoucherDetail>();
            return result;
        }

        public static AdjustmentVoucher GetAdjustmentVoucher(int aVNo)
        {
            LussisEntities context = new LussisEntities();
            return context.AdjustmentVouchers.Where(x => x.AvNo == aVNo).FirstOrDefault();
        }

        public static decimal? GetUnitPrice(string itemNo, string supplier1)
        {
            LussisEntities context = new LussisEntities();
            return context.SupplyTenders.Where(x => x.ItemNo.Equals(itemNo) && x.SupplierCode.Equals(supplier1)).FirstOrDefault().UnitPrice;
        }

        public static void UpdateAVDetailQty(int aVNo, string itemNo, int qty, string reason)
        {
            LussisEntities context = new LussisEntities();
            AdjustmentVoucherDetail aVD = context.AdjustmentVoucherDetails.Where(x => x.AvNo == aVNo && x.ItemNo.Equals(itemNo)).FirstOrDefault();
            aVD.Qty = qty;
            aVD.Reason = reason;
            context.SaveChanges();
        }

        public static void DeleteAV(int aVNo)
        {
            LussisEntities context = new LussisEntities();
            using (var txn = new TransactionScope())
            {
                // Remove AdjustmentVoucherDetails
                List<AdjustmentVoucherDetail> result = context.AdjustmentVoucherDetails.Where(x => x.AvNo == aVNo).ToList<AdjustmentVoucherDetail>();
                foreach (AdjustmentVoucherDetail aVD in result)
                {
                    context.AdjustmentVoucherDetails.Remove(aVD);
                }

                // Remove AdjustmentVoucher
                AdjustmentVoucher aV = context.AdjustmentVouchers.Where(x => x.AvNo == aVNo).FirstOrDefault();
                context.AdjustmentVouchers.Remove(aV);

                context.SaveChanges();
                txn.Complete();
            }
        }

        public static void DeleteAVD(int aVNo, string itemNo)
        {
            LussisEntities context = new LussisEntities();
            AdjustmentVoucherDetail aVD = context.AdjustmentVoucherDetails.Where(x => x.AvNo == aVNo && x.ItemNo.Equals(itemNo)).FirstOrDefault();
            context.AdjustmentVoucherDetails.Remove(aVD);
            context.SaveChanges();
        }
    }
}
