using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LUSSIS_Backend
{
    public class ApproveInventoryAdjustmentController
    {
        //functions for ApproveInventoryAdjustmentList
        public static List<AdjustmentVoucher> getInvAdjList()
        {
            LussisEntities context = new LussisEntities();
            var result = context.AdjustmentVouchers.Where(x => x.Status == "Pending").ToList();
            return result;
        }

        public static List<AdjustmentVoucherDetail> getInvAdjDetails(int iAV)
        {
            LussisEntities context = new LussisEntities();

            var result = context.AdjustmentVoucherDetails.Where(x => x.AvNo == iAV).ToList();
            return result;

        }

        //Button(Approve) function for ApproveInventoryAdjustmentDetails
        public static void setStatusApprove(int iAV)
        {
            LussisEntities context = new LussisEntities();
            var result = context.AdjustmentVouchers.Where(x => x.AvNo == iAV).FirstOrDefault();
            result.Status = "Approved";
            context.SaveChanges();

        }

        //Button(Reject) function
        public static void setStatusReject(int iAV)
        {
            LussisEntities context = new LussisEntities();
            var result = context.AdjustmentVouchers.Where(x => x.AvNo == iAV).FirstOrDefault();
            result.Status = "Rejected";
            context.SaveChanges();
        }

        //insert new stock transaction details into stocktxndetails
        public static void updateStockTransactioninDB(StockTxnDetail stkdetails)
        {
            LussisEntities context = new LussisEntities();
            context.StockTxnDetails.Add(stkdetails);
            context.SaveChanges();
        }

        //update currentqty in stationery catalogue after adjustments
        public static void updateStationeryCatalogue(string itemNoText, int qtyAmt)
        {
            LussisEntities context = new LussisEntities();
            var stockRequireUpdating = context.StationeryCatalogues.SingleOrDefault
                (x => x.ItemNo == itemNoText);
            if (stockRequireUpdating != null)
            {
                stockRequireUpdating.CurrentQty = qtyAmt;
                context.SaveChanges();
            }
        }

        //Update ApproveEmpNo in Adjustment Voucher
        public static void setApprovedBy(int iAV, int empNo)
        {
            LussisEntities context = new LussisEntities();
            var avd = context.AdjustmentVouchers.Where(x => x.AvNo == iAV).FirstOrDefault();
                avd.ApproveEmpNo = empNo;
                context.SaveChanges();
        }

    }
}
