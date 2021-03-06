﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Email_Backend;

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

        public static void SubmitAdjustmentVoucher(string itemNo, DateTime dateIssued, int qty, string reason, int issueEmpNo)
        {
            AdjustmentVoucher aV;
            AdjustmentVoucherDetail aVD;

            LussisEntities context = new LussisEntities();
            using (var txn = new TransactionScope())
            {
                // Create AdjustmentVoucher
                aV = new AdjustmentVoucher();
                aV.DateIssued = dateIssued;
                aV.IssueEmpNo = issueEmpNo;
                aV.Status = "Pending";
                context.AdjustmentVouchers.Add(aV);
                context.SaveChanges();

                // Create AdjustmentVoucherDetails
                aVD = new AdjustmentVoucherDetail();
                aVD.AvNo = aV.AvNo;
                aVD.ItemNo = itemNo;
                aVD.Qty = qty;
                aVD.Reason = reason;
                context.AdjustmentVoucherDetails.Add(aVD);
                context.SaveChanges();

                txn.Complete();
            }

            // Send Email
            var supplier1Code = context.StationeryCatalogues.Where(x => x.ItemNo.Equals(itemNo)).FirstOrDefault().Supplier1;
            var unitPrice = context.SupplyTenders.Where(x => x.ItemNo.Equals(itemNo) && x.SupplierCode.Equals(supplier1Code)).FirstOrDefault().UnitPrice;
            var price = qty * unitPrice;

            Employee recipient;
            if (price >= 250)
            {
                recipient = context.StoreAssignments.Where(x => x.Role.Equals("Manager")).FirstOrDefault().Employee;
            }
            else
            {
                recipient = context.StoreAssignments.Where(x => x.Role.Equals("Supervisor")).FirstOrDefault().Employee;
            }

            string recipientEmail = recipient.Email;
            string emailSubject = EmailTemplate.GenerateAdjustmentVoucherSubject();
            string emailContent = EmailTemplate.GenerateAdjustmentVoucherEmail(recipient.EmpName, aV.AvNo);

            EmailBackend.sendEmailStep(recipientEmail, emailSubject, emailContent);
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

        public static void IncreaseStockFromSupplier(String itemNo, int quantity, String supplierCode)
        {
            LussisEntities context = new LussisEntities();

            // Get Entities
            StationeryCatalogue stationeryItem = context.StationeryCatalogues.Where(item => item.ItemNo.Equals(itemNo)).FirstOrDefault();
            Supplier supplier = context.Suppliers.Where(supp => supp.SupplierCode.Equals(supplierCode)).FirstOrDefault();
            StockTxnDetail detail = new StockTxnDetail()
            {
                AdjustQty = quantity,
                Date = DateTime.Today,
                ItemNo = itemNo,
                Remarks = "Supplied by " + supplierCode,
            };

            // If entities are found, update database
            if (stationeryItem != null && supplier != null)
            {
                detail.RecordedQty = stationeryItem.CurrentQty + detail.AdjustQty;
                stationeryItem.CurrentQty = detail.RecordedQty;
                context.StockTxnDetails.Add(detail);

                context.SaveChanges();
            }
        }
    }
}
