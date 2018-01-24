using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LUSSIS_Backend.model;
using LUSSIS_Backend.dao;

namespace LUSSIS_Backend.controller
{
    public static class DisbursementController
    {
        // ATTRIBUTES

        static LussisEntities context = new LussisEntities();

        // METHODS

        public static Disbursement GetDisbursement(int disbursementNo)
        {
            return StoreClerkDao.GetDisbursement(context, disbursementNo);
        }

        public static List<DisbursementItem> GetDisbursementItemList(int disbursementNo)
        {
            List<DisbursementItem> dItemList = new List<DisbursementItem>();

            // Get DisbursementDetail List
            List<DisbursementDetail> dDetailList = StoreClerkDao.GetDisbursementDetailList(context, disbursementNo);

            // For each DisbursementDetail
            for (int i = 0; i < dDetailList.Count; i++)
            {
                // Create DisbursementItem
                DisbursementItem dItem = new DisbursementItem();
                dItem.ItemNo = dDetailList[i].ItemNo;
                dItem.ItemDescription = dDetailList[i].StationeryCatalogue.Description;
                dItem.Qty = dDetailList[i].Promised;
                dItem.Delivered = dDetailList[i].Received;
                dItemList.Add(dItem);
            }

            return dItemList;
        }

        public static bool CompleteDisbursement(int disbursementNo, decimal? pin, List<string> itemNoList, List<int> receivedQtyList)
        {
            // Validate Pin
            bool isValidPin = StoreClerkDao.ValidatePin(context, disbursementNo, pin);

            if (isValidPin)
            {
                using (var dbcxtransaction = context.Database.Connection.BeginTransaction())
                {
                    try
                    {
                        // Update Disbursement (Status)
                        StoreClerkDao.UpdateDisbursementStatus(context, disbursementNo);

                        // Update DisbursementDetails (Received Qty)
                        StoreClerkDao.UpdateDisbursementDetails(context, disbursementNo, itemNoList, receivedQtyList);

                        // Create Backlogs
                        StoreClerkDao.CreateBacklogs(context, disbursementNo);

                        // Send Email
                        SendEmail();
                        dbcxtransaction.Commit();
                        return true;
                    }
                    catch (Exception e)
                    {
                        dbcxtransaction.Rollback();
                        throw e;
                    }
                }
            }
            else
            {
                return false;
            }
        }

        private static void SendEmail()
        {

        }
    }
}
