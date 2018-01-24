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

    }
}
