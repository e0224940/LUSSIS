using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LUSSIS_Backend.dao
{
    public static class StoreClerkDao
    {
        // DISBURSEMENT

        public static Disbursement GetDisbursement(LussisEntities context, int disbursementNo)
        {
            return context.Disbursements.Where(x => x.DisbursementNo == disbursementNo).FirstOrDefault();
        }

        public static List<DisbursementDetail> GetDisbursementDetailList(LussisEntities context, int disbursementNo)
        {
            return context.DisbursementDetails.Where(x => x.DisbursementNo == disbursementNo).ToList<DisbursementDetail>();
        }

        public static bool ValidatePin(LussisEntities context, int disbursementNo, decimal? pin)
        {
            decimal? secretPin = context.Disbursements.Where(x => x.DisbursementNo == disbursementNo).FirstOrDefault().Pin;

            if (pin == secretPin)
            {
                return true;
            }
            return false;
        }

        public static Disbursement UpdateDisbursementStatus(LussisEntities context, int disbursementNo)
        {
            Disbursement disbursement = context.Disbursements.Where(x => x.DisbursementNo == disbursementNo).First();
            disbursement.Status = "Completed";
            context.SaveChanges();
            return disbursement;
        }

        public static List<DisbursementDetail> UpdateDisbursementDetails(LussisEntities context, int disbursementNo, List<string> itemNoList, List<int> receivedQtyList)
        {
            List<DisbursementDetail> disbursementDetailList = context.Disbursements.Where(x => x.DisbursementNo == disbursementNo).First().DisbursementDetails.ToList<DisbursementDetail>();

            // For each DisbursementDetail
            for (int i = 0; i < itemNoList.Count; i++)
            {
                // Update ReceivedQty
                DisbursementDetail disbursementDetail = disbursementDetailList.Where(x => x.DisbursementNo == disbursementNo && x.ItemNo.Equals(itemNoList[i])).FirstOrDefault();
                disbursementDetail.Received = receivedQtyList[i];
            }
            context.SaveChanges();
            return disbursementDetailList;
        }

        // BACKLOG

        public static List<BackLog> CreateBacklogs(LussisEntities context, int disbursementNo)
        {
            List<DisbursementDetail> disbursementDetailList = context.DisbursementDetails.Where(x => x.DisbursementNo == disbursementNo).ToList<DisbursementDetail>();
            List<BackLog> backlogList = new List<BackLog>();

            // For each DisbursementDetail
            for (int i = 0; i < disbursementDetailList.Count; i++)
            {
                // Create Backlog if Needed > Received
                DisbursementDetail disbursementDetail = disbursementDetailList[i];
                if (disbursementDetail.Needed - disbursementDetail.Received > 0)
                {
                    BackLog backlog = new BackLog();
                    backlog.DeptCode = disbursementDetail.Disbursement.DeptCode;
                    backlog.ItemNo = disbursementDetail.ItemNo;
                    backlog.BackLogQty = disbursementDetail.Needed - disbursementDetail.Received;
                    context.BackLogs.Add(backlog);
                    backlogList.Add(backlog);
                }
            }
            context.SaveChanges();
            return backlogList;
        }
    }
}
