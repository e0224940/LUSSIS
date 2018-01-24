using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LUSSIS_Backend.dao
{
    public static class StoreClerkDao
    {
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

        // PURCHASEORDER

        public static PurchaseOrder Add(LussisEntities context, PurchaseOrder pO)
        {
            context.PurchaseOrders.Add(pO);
            context.SaveChanges();
            return pO;
        }

        public static PurchaseOrderDetail Add(LussisEntities context, PurchaseOrderDetail pOD)
        {
            context.PurchaseOrderDetails.Add(pOD);
            context.SaveChanges();
            return pOD;
        }

        // STOCK

        public static StationeryCatalogue GetStock(LussisEntities context, string itemNo)
        {
            return context.StationeryCatalogues.Where(x => x.ItemNo.Equals(itemNo)).FirstOrDefault();
        }

        public static List<StationeryCatalogue> GetAllStocks(LussisEntities context)
        {
            return context.StationeryCatalogues.ToList<StationeryCatalogue>();
        }

        public static StockTxnDetail AddStockTxn(LussisEntities context, StockTxnDetail txn)
        {
            context.StockTxnDetails.Add(txn);
            context.SaveChanges();
            return txn;
        }

        public static int? GetTotalPendingAdjustmentQtyForStock(LussisEntities context, string itemNo)
        {
            int? qty = 0;

            // Get Pending Vouchers
            List<AdjustmentVoucher> aVList = context.AdjustmentVouchers.Where(x => x.Status.Equals("Pending")).ToList<AdjustmentVoucher>();

            // If no Pending Vouchers found
            if (aVList.Count == 0)
            {
                // Return 0
                return qty;
            }
            else
            {
                // For each Pending Voucher
                for (int i = 0; i < aVList.Count; i++)
                {
                    List<AdjustmentVoucherDetail> aVDList = aVList[i].AdjustmentVoucherDetails.ToList<AdjustmentVoucherDetail>();

                    // For each VoucherDetail
                    for (int j = 0; j < aVDList.Count; j++)
                    {
                        // Add to Qty if itemNo matches
                        AdjustmentVoucherDetail aVD = aVDList[i];
                        if (aVD.ItemNo.Equals(itemNo))
                        {
                            qty += aVD.Qty;
                        }
                    }
                }
                return qty;
            }
        }

        public static int? GetTotalPendingPurchaseQtyForStock(LussisEntities context, string itemNo)
        {
            int? qty = 0;

            // Get Pending PO
            List<PurchaseOrder> pOList = context.PurchaseOrders.Where(x => x.Status.Equals("Pending")).ToList<PurchaseOrder>();

            // If no Pending PO
            if (pOList.Count == 0)
            {
                // Return 0
                return qty;
            }
            else
            {
                // For each Pending PO
                for (int i = 0; i < pOList.Count; i++)
                {
                    List<PurchaseOrderDetail> pODList = pOList[i].PurchaseOrderDetails.ToList<PurchaseOrderDetail>();

                    // For each PODetail
                    for (int j = 0; j < pODList.Count; j++)
                    {
                        // Add to Qty if itemNo matches
                        PurchaseOrderDetail pOD = pODList[j];
                        if (pOD.ItemNo.Equals(itemNo))
                        {
                            qty += pOD.Qty;
                        }
                    }
                }
                return qty;
            }
        }

        // SUPPLIER

        public static List<Supplier> GetAllSuppliers(LussisEntities context)
        {
            return context.Suppliers.ToList<Supplier>();
        }

        public static Supplier GetSupplier(LussisEntities context, string supplierCode)
        {
            return context.Suppliers.Where(x => x.SupplierCode.Equals(supplierCode)).FirstOrDefault();
        }
    }
}
