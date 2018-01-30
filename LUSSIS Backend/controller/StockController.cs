﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace LUSSIS_Backend.controller
{
    public static class StockController
    {
        public static List<StationeryCatalogue> GetAllStocks()
        {
            LussisEntities context = new LussisEntities();
            return context.StationeryCatalogues.ToList();
        }

        public static List<StationeryCatalogue> GetStockListByBin(string selectedBin)
        {
            LussisEntities context = new LussisEntities();
            return context.StationeryCatalogues.Where(x => x.Bin.Equals(selectedBin)).ToList();
        }

        public static List<StationeryCatalogue> GetStockListByDescriptionContain(string searchString)
        {
            LussisEntities context = new LussisEntities();
            return context.StationeryCatalogues.Where(x => x.Description.ToLower().Contains(searchString.ToLower())).ToList();
        }

        public static List<StockTxnDetail> GetStockTxnList(string itemNo)
        {
            LussisEntities context = new LussisEntities();
            return context.StockTxnDetails.Where(x => x.ItemNo.Equals(itemNo)).ToList();
        }

        public static List<string> GetBinList()
        {
            LussisEntities context = new LussisEntities();
            return context.StationeryCatalogues.Select(x => x.Bin).Distinct().ToList();
        }

        public static StationeryCatalogue GetStock(string itemNo)
        {
            LussisEntities context = new LussisEntities();
            return context.StationeryCatalogues.Where(x => x.ItemNo.Equals(itemNo)).FirstOrDefault();
        }

        public static void SubmitAdjustmentVoucher(string itemNo, DateTime dateIssued, int qty, string reason, int issueEmpNo, int approveEmpNo)
        {
            LussisEntities context = new LussisEntities();
            using (var txn = new TransactionScope())
            {
                // Create AdjustmentVoucher
                AdjustmentVoucher aV = new AdjustmentVoucher();
                aV.DateIssued = dateIssued;
                aV.IssueEmpNo = issueEmpNo;
                aV.ApproveEmpNo = approveEmpNo;
                aV.Status = "Pending";
                context.AdjustmentVouchers.Add(aV);
                context.SaveChanges();

                // Create AdjustmentVoucherDetails
                AdjustmentVoucherDetail aVD = new AdjustmentVoucherDetail();
                aVD.AvNo = aV.AvNo;
                aVD.ItemNo = itemNo;
                aVD.Qty = qty;
                aVD.Reason = reason;
                context.AdjustmentVoucherDetails.Add(aVD);
                context.SaveChanges();

                txn.Complete();
            }
        }

        public static int GetStoreSupervisorEmpNo()
        {
            LussisEntities context = new LussisEntities();
            return context.StoreAssignments.Where(x => x.Role.Equals("Supervisor")).FirstOrDefault().EmpNo;
        }

        public static int GetStoreManagerEmpNo()
        {
            LussisEntities context = new LussisEntities();
            return context.StoreAssignments.Where(x => x.Role.Equals("Manager")).FirstOrDefault().EmpNo;
        }
    }
}
