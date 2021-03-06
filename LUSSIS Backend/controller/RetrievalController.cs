﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace LUSSIS_Backend.controller
{
    public static class RetrievalController
    {
        static LussisEntities context;

        public static int CreateWeeklyRetrieval()
        {
            context = new LussisEntities();
            int retrievalNo = -1;
            // Creating Database Transaction
            using (var txn = new TransactionScope())
            {
                try
                {
                    //Add Retrieval
                    Retrieval retrieval = AddRetrieval(context, new Retrieval() { Date = DateTime.Today });

                    // Get RetrievalNo
                    retrievalNo = retrieval.RetrievalNo;

                    // Add RetrievalDetails (from RequisitionDetails)
                    ProcessRequisitions(context, retrievalNo);

                    // Add RetrievalDetails (from Backlogs)
                    ProcessBacklogs(context, retrievalNo);

                    // Clear Backlog
                    Clear(context);

                    // Commit Transaction
                    txn.Complete();
                }
                catch (Exception)
                {
                    // Rollback Transaction
                    //dbcxtransaction.Rollback();
                }
            }

            return retrievalNo;
        }

        private static void ProcessRequisitions(LussisEntities context, int retrievalNo)
        {
            // Get Approved Requisitions
            List<Requisition> requisitionList = GetApprovedRequisitions(context);

            if(requisitionList.Count == 0)
            {
                throw new Exception("No Requisitions to Process");
            }

            // For each Requisition
            for (int i = 0; i < requisitionList.Count; i++)
            {
                // Get RequisitionDetails
                List<RequisitionDetail> requisitionDetailList = requisitionList[i].RequisitionDetails.ToList<RequisitionDetail>();

                // For each RequisitionDetail
                for (int j = 0; j < requisitionDetailList.Count; j++)
                {
                    // Get deptCode & itemNo
                    string deptCode = requisitionDetailList[j].RequisitionInfo.EmployeeWhoIssued.DeptCode;
                    string itemNo = requisitionDetailList[j].ItemNo;

                    // Get RetrievalDetail
                    RetrievalDetail retrievalDetail = GetRetrievalDetail(context, retrievalNo, deptCode, itemNo);

                    if (retrievalDetail != null)
                    {
                        // Update RetrievalDetail
                        retrievalDetail.Needed += requisitionDetailList[j].Qty;
                    }
                    else
                    {
                        // Create RetrievalDetail
                        retrievalDetail = new RetrievalDetail();
                        retrievalDetail.RetrievalNo = retrievalNo;
                        retrievalDetail.DeptCode = deptCode;
                        retrievalDetail.ItemNo = requisitionDetailList[j].ItemNo;
                        retrievalDetail.Needed = requisitionDetailList[j].Qty;
                        retrievalDetail.BackLogQty = 0;
                        retrievalDetail.Actual = 0;
                        retrievalDetail = AddRetrievalDetail(context, retrievalDetail);
                        context.SaveChanges();
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
            DateTime today = DateTime.Today;
            int daysUntilMonday = ((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7;
            DateTime disbursementDate = today.Date.AddDays(daysUntilMonday);

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
                Disbursement disbursement = GetPendingDisbursement(context, deptCode, disbursementDate);
                if (disbursement == null)
                {
                    disbursement = new Disbursement();
                    disbursement.DisbursementNo = int.MaxValue;
                    disbursement.DeptCode = deptCode;
                    disbursement.DisbursementDate = disbursementDate;
                    disbursement.RepEmpNo = GetDepartment(context, deptCode).RepEmpNo;
                    disbursement.CollectionPointNo = GetDepartment(context, deptCode).CollectionPointNo;
                    disbursement.Pin = new Random().Next(10000, 99999);
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
            context.SaveChanges();
        }
        public static List<Requisition> GetApprovedRequisitions(LussisEntities context)
        {
            return context.Requisitions.Where(x => x.Status == "Approved").ToList<Requisition>();
        }
        public static RetrievalDetail GetRetrievalDetail(LussisEntities context, int retrievalNo, string deptCode, string itemNo)
        {
            return context.RetrievalDetails.Where(x => x.RetrievalNo == retrievalNo && x.DeptCode.Equals(deptCode) && x.ItemNo.Equals(itemNo)).FirstOrDefault();
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
            return context.StationeryCatalogues.Where(x => x.ItemNo.Equals(itemNo)).FirstOrDefault();
        }
        public static Department GetDepartment(LussisEntities context, string deptCode)
        {
            return context.Departments.Where(x => x.DeptCode.Equals(deptCode)).FirstOrDefault();
        }
        public static Disbursement GetPendingDisbursement(LussisEntities context, string deptCode, DateTime disbursementDate)
        {
            return context.Disbursements.Where(x => x.DeptCode.Equals(deptCode) && x.DisbursementDate == disbursementDate && x.Status.Equals("Pending")).FirstOrDefault();
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
            // TODO : Put a filter for what kind of disbursement to edit
            DisbursementDetail detail = context.DisbursementDetails
                .Where(det => det.DisbursementNo.Equals(disbursementDetail.DisbursementNo) && det.ItemNo.Equals(disbursementDetail.ItemNo))
                .FirstOrDefault();

            if(detail != null)
            {
                // Append to existing if it already exists
                detail.Needed += disbursementDetail.Needed;
                detail.Promised += disbursementDetail.Promised;
                detail.Received += disbursementDetail.Received;
            }
            else
            {
                // Add new detail
                context.DisbursementDetails.Add(disbursementDetail);
            }           
            
            context.SaveChanges();
            return disbursementDetail;
        }
    }
}
