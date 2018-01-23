using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LUSSIS_Backend.controller
{
    static class RetrievalController
    {
        static LussisEntities context;

        public static int CreateWeeklyRetrieval()
        {

            // Creating Database Transaction
            //using (var dbcxtransaction = context.Database.BeginTransaction())
            //{
            //    try
            //    {
                    // Add Retrieval
                    Retrieval retrieval = AddRetrieval(context, new Retrieval());

            // Get RetrievalNo
            int retrievalNo = retrieval.RetrievalNo;

            // Add RetrievalDetails (from RequisitionDetails)
            ProcessRequisitions(context, retrievalNo);

            // Add RetrievalDetails (from Backlogs)
            ProcessBacklogs(context, retrievalNo);

            // Clear Backlog
            Clear(context);

            //// Commit Transaction
            //dbcxtransaction.Commit();
            //}
            //catch
            //{
            //    // Rollback Transaction
            //    dbcxtransaction.Rollback();
            //}
            //}
            return retrieval.RetrievalNo;
        }

        private static void ProcessRequisitions(LussisEntities context, int retrievalNo)
        {
            // Get Approved Requisitions
            List<Requisition> requisitionList = GetApprovedRequisitions(context);

            // For each Requisition
            for (int i = 0; i < requisitionList.Count; i++)
            {
                // Get RequisitionDetails
                List<RequisitionDetail> requisitionDetailList = requisitionList[i].RequisitionDetails.ToList<RequisitionDetail>();

                // For each RequisitionDetail
                for (int j = 0; j < requisitionDetailList.Count; j++)
                {
                    // Get deptCode & itemNo
                    string deptCode = requisitionDetailList[i].RequisitionInfo.EmployeeWhoIssued.DeptCode;
                    string itemNo = requisitionDetailList[i].ItemNo;

                    // Get RetrievalDetail
                    RetrievalDetail retrievalDetail = GetRetrievalDetail(context, retrievalNo, deptCode, itemNo);

                    if (retrievalDetail != null)
                    {
                        // Update RetrievalDetail
                        retrievalDetail.Needed += requisitionDetailList[i].Qty;
                    }
                    else
                    {
                        // Create RetrievalDetail
                        retrievalDetail = new RetrievalDetail();
                        retrievalDetail.RetrievalNo = retrievalNo;
                        retrievalDetail.DeptCode = deptCode;
                        retrievalDetail.ItemNo = requisitionDetailList[i].ItemNo;
                        retrievalDetail.Needed = requisitionDetailList[i].Qty;
                        retrievalDetail.BackLogQty = 0;
                        retrievalDetail.Actual = 0;
                        retrievalDetail = AddRetrievalDetail(context, retrievalDetail);
                    }
                }

                // Update Requisition Status
                requisitionList[i].Status = "Processed";
            }

            context.SaveChanges();
        }

        private static void ProcessBacklogs(LussisEntities context, int retrievalNo)
        {
            // Get Backlogs
            List<BackLog> backlogList = context.BackLogs.ToList<BackLog>();

            // For each Backlog
            for (int i = 0; i < backlogList.Count; i++)
            {
                // Get deptCode & itemNo
                string deptCode = backlogList[i].DeptCode;
                string itemNo = backlogList[i].ItemNo;

                // Get RetrievalDetail
                RetrievalDetail retrievalDetail = GetRetrievalDetail(context, retrievalNo, deptCode, itemNo);

                if (retrievalDetail != null)
                {
                    // Update RetrievalDetail
                    retrievalDetail.BackLogQty += backlogList[i].BackLogQty;
                }
                else
                {
                    // Create RetrievalDetail
                    retrievalDetail = new RetrievalDetail();
                    retrievalDetail.RetrievalNo = retrievalNo;
                    retrievalDetail.DeptCode = deptCode;
                    retrievalDetail.ItemNo = itemNo;
                    retrievalDetail.Needed = 0;
                    retrievalDetail.BackLogQty = backlogList[i].BackLogQty;
                    retrievalDetail.Actual = 0;
                    retrievalDetail = AddRetrievalDetail(context, retrievalDetail);
                }
            }

            context.SaveChanges();
        }

        public static bool SubmitRetrieval(int retrievalNo, List<string> deptCodeList, List<string> itemNoList, List<int> actualList)
        {
            LussisEntities context = new LussisEntities();

            //// Creating Database Transaction
            //using (var dbcxtransaction = context.Database.BeginTransaction())
            //{
            //    try
            //    {
            // Get DisbursementDate
            DateTime today = DateTime.Today;
            int daysUntilMonday = ((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7;
            DateTime disbursementDate = today.AddDays(daysUntilMonday);

            // Update RetrievalDate
            Retrieval retrieval = GetRetrieval(context, retrievalNo);
            retrieval.Date = DateTime.Now;

            // For each RetrievalDetail
            for (int i = 0; i < actualList.Count; i++)
            {
                string deptCode = deptCodeList[i];
                string itemNo = itemNoList[i];
                int actual = actualList[i];

                // Update RetrievalDetail
                RetrievalDetail retrievalDetail = GetRetrievalDetail(context, retrievalNo, deptCode, itemNo);
                retrievalDetail.Actual = actual;

                // Update StationeryCatalogueCurrentyQty
                StationeryCatalogue stock = GetStock(context, itemNo);
                stock.CurrentQty -= actual;

                // Add StockTxnDetail
                StockTxnDetail txn = new StockTxnDetail();
                txn.ItemNo = itemNo;
                txn.Date = retrieval.Date;
                txn.AdjustQty = -actual;
                txn.RecordedQty = stock.CurrentQty;
                txn.Remarks = GetDepartment(context, deptCode).DeptName;
                txn = AddStockTxn(context, txn);

                // Add Disbursement
                Disbursement disbursement = GetDisbursement(context, deptCode, disbursementDate);
                if (disbursement == null)
                {
                    disbursement = new Disbursement();
                    disbursement.DeptCode = deptCode;
                    disbursement.DisbursementDate = disbursementDate;
                    disbursement.RepEmpNo = GetDepartment(context, deptCode).RepEmpNo;
                    disbursement.CollectionPointNo = GetDepartment(context, deptCode).CollectionPointNo;
                    disbursement.Pin = new Random().Next(0, 100000);
                    disbursement.Status = "Pending";
                    disbursement = AddDisbursement(context, disbursement);
                }

                // Add DisbursementDetail
                DisbursementDetail disbursementDetail = new DisbursementDetail();
                disbursementDetail.DisbursementNo = disbursement.DisbursementNo;
                disbursementDetail.ItemNo = itemNo;
                disbursementDetail.Needed = retrievalDetail.Needed + retrievalDetail.BackLogQty;
                disbursementDetail.Promised = actual;
                disbursementDetail.Received = actual;
                disbursementDetail = AddDisbursementDetail(context, disbursementDetail);
            }

            context.SaveChanges();
            //}
            //                catch
            //                {
            //                    // Rollback Transaction
            //                    dbcxtransaction.Rollback();
            //                    return false;
            //                }
            //}

            // Send Email

            return true;
        }










        public static Retrieval AddRetrieval(LussisEntities context, Retrieval retrieval)
        {
            context.Retrievals.Add(retrieval);
            context.SaveChanges();
            return retrieval;
        }
        public static void Clear(LussisEntities context)
        {
            //context.spClearBacklog();
            context.SaveChanges();
        }
        public static List<Requisition> GetApprovedRequisitions(LussisEntities context)
        {
            return context.Requisitions.Where(x => x.Status == "Approved").ToList<Requisition>();
        }
        public static RetrievalDetail GetRetrievalDetail(LussisEntities context, int retrievalNo, string deptCode, string itemNo)
        {
            return context.RetrievalDetails.Where(x => x.RetrievalNo == retrievalNo && x.DeptCode == deptCode && x.ItemNo == itemNo).FirstOrDefault();
        }
        public static RetrievalDetail AddRetrievalDetail(LussisEntities context, RetrievalDetail retrievalDetail)
        {
            context.RetrievalDetails.Add(retrievalDetail);
            return retrievalDetail;
        }
        public static Retrieval GetRetrieval(LussisEntities context, int retrievalNo)
        {
            return context.Retrievals.Where(x => x.RetrievalNo == retrievalNo).FirstOrDefault();
        }
        public static StationeryCatalogue GetStock(LussisEntities context, string itemNo)
        {
            return context.StationeryCatalogues.Where(x => x.ItemNo == itemNo).FirstOrDefault();
        }
        public static Department GetDepartment(LussisEntities context, string deptCode)
        {
            return context.Departments.Where(x => x.DeptCode == deptCode).FirstOrDefault();
        }
        public static Disbursement GetDisbursement(LussisEntities context, string deptCode, DateTime disbursementDate)
        {
            return context.Disbursements.Where(x => x.DeptCode == deptCode && x.DisbursementDate == disbursementDate).FirstOrDefault();
        }
        public static StockTxnDetail AddStockTxn(LussisEntities context, StockTxnDetail txn)
        {
            context.StockTxnDetails.Add(txn);
            context.SaveChanges();
            return txn;
        }
        public static Disbursement AddDisbursement(LussisEntities context, Disbursement disbursement)
        {
            context.Disbursements.Add(disbursement);
            context.SaveChanges();
            return disbursement;
        }
        public static DisbursementDetail AddDisbursementDetail(LussisEntities context, DisbursementDetail disbursementDetail)
        {
            context.DisbursementDetails.Add(disbursementDetail);
            context.SaveChanges();
            return disbursementDetail;
        }
    }
}
