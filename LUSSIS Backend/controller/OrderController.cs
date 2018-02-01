﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Email_Backend;

namespace LUSSIS_Backend.controller
{
    public static class OrderController
    {
        public static List<OrderItem> GetAutoGeneratedOrderList()
        {
            // Create empty OrderList
            List<OrderItem> orderList = new List<OrderItem>();

            // Get all Stocks
            List<StationeryCatalogue> allStockList = GetAllStocks();

            // Get Pending Vouchers
            List<AdjustmentVoucher> aVList = new LussisEntities().AdjustmentVouchers.Where(x => x.Status.Equals("Pending")).ToList<AdjustmentVoucher>();

            // Get Pending PO
            List<PurchaseOrder> pOList = new LussisEntities().PurchaseOrders.Where(x => x.Status.Equals("Pending")).ToList<PurchaseOrder>();

            // For each Stock
            for (int i = 0; i < allStockList.Count; i++)
            {
                StationeryCatalogue stock = allStockList[i];

                // Get OrderQty
                int? orderQty = GetOrderQty(stock.ItemNo, aVList, pOList);

                // If OrderQty > 0, create OrderItem
                if (orderQty > 0)
                {
                    OrderItem orderItem = new OrderItem(stock);
                    orderItem.OrderQtyList[0] = orderQty;
                    orderList.Add(orderItem);
                }
            }

            return orderList;
        }

        public static List<StationeryCatalogue> GetUnselectedStocks(List<OrderItem> orderList)
        {
            // Create empty StockList
            List<StationeryCatalogue> stockList = new List<StationeryCatalogue>();

            // Get all Stocks
            List<StationeryCatalogue> allStockList = GetAllStocks();

            // For each Stock
            for (int i = 0; i < allStockList.Count; i++)
            {
                StationeryCatalogue stock = allStockList[i];
                string itemNo = stock.ItemNo;
                bool found = false;

                // Check in OrderList
                for (int j = 0; j < orderList.Count; j++)
                {
                    if (orderList[j].Stock.ItemNo.Equals(itemNo))
                    {
                        found = true;
                        break;
                    }
                }

                // Add to StockList if not found
                if (!found)
                {
                    stockList.Add(stock);
                }
            }

            return stockList;
        }

        public static List<StationeryCatalogue> GetAllStocks()
        {
            LussisEntities context = new LussisEntities();
            return context.StationeryCatalogues.ToList();
        }

        public static List<Supplier> GetAllSuppliers()
        {
            LussisEntities context = new LussisEntities();
            return context.Suppliers.ToList();
        }

        public static StationeryCatalogue GetStock(string itemNo)
        {
            LussisEntities context = new LussisEntities();
            return context.StationeryCatalogues.Where(x => x.ItemNo.Equals(itemNo)).FirstOrDefault();
        }

        public static Supplier GetSupplier(string supplierCode)
        {
            LussisEntities context = new LussisEntities();
            return context.Suppliers.Where(x => x.SupplierCode.Equals(supplierCode)).FirstOrDefault();
        }

        public static decimal? GetUnitPrice(string itemNo, string supplier1)
        {
            LussisEntities context = new LussisEntities();
            return context.SupplyTenders.Where(x => x.ItemNo.Equals(itemNo) && x.SupplierCode.Equals(supplier1)).FirstOrDefault().UnitPrice;
        }

        public static int GetStoreSupervisorEmpNo()
        {
            LussisEntities context = new LussisEntities();
            return context.StoreAssignments.Where(x => x.Role.Equals("Supervisor")).FirstOrDefault().EmpNo;
        }

        // PRIVATE METHODS

        private static int? GetOrderQty(string itemNo, List<AdjustmentVoucher> aVList, List<PurchaseOrder> pOList)
        {
            // Find Stock
            StationeryCatalogue stock = GetStock(itemNo);

            // Find ReorderLevel
            int? reorderLevel = stock.ReorderLevel;

            // Find CurrentQty
            int? currentQty = stock.CurrentQty;

            // Find TotalAdjustmentQty
            int? adjustmentQty = GetTotalPendingAdjustmentQtyForStock(itemNo, aVList);

            // Find TotalPurchaseOrderQty
            int? purchaseQty = GetTotalPendingPurchaseQtyForStock(itemNo, pOList);

            // Find OrderQty
            int? orderQty = (reorderLevel - currentQty - adjustmentQty - purchaseQty);
            if (orderQty > 0 && orderQty < stock.ReorderQty)
            {
                orderQty = stock.ReorderQty;
            }

            return orderQty;
        }

        private static int? GetTotalPendingPurchaseQtyForStock(string itemNo, List<PurchaseOrder> pOList)
        {
            int? qty = 0;
            LussisEntities context = new LussisEntities();

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

        private static int? GetTotalPendingAdjustmentQtyForStock(string itemNo, List<AdjustmentVoucher> aVList)
        {
            int? qty = 0;
            LussisEntities context = new LussisEntities();

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
                        AdjustmentVoucherDetail aVD = aVDList[j];
                        if (aVD.ItemNo.Equals(itemNo))
                        {
                            qty += aVD.Qty;
                        }
                    }
                }
                return qty;
            }
        }

        private static PurchaseOrder CreatePurchaseOrder(string supplierCode, int orderedBy, DateTime dateIssued, List<PurchaseOrder> pOList)
        {
            LussisEntities context = new LussisEntities();

            // Check if SupplierCode inside pOList
            PurchaseOrder pO = pOList.Where(x => x.SupplierCode.Equals(supplierCode)).FirstOrDefault();

            if (pO == null)
            {
                // Create PurchaseOrder
                pO = new PurchaseOrder();
                pO.SupplierCode = supplierCode;
                pO.OrderedBy = orderedBy;
                pO.DateIssued = dateIssued;
                pO.Status = "Pending";
                context.PurchaseOrders.Add(pO);
                context.SaveChanges();
                pOList.Add(pO);
            }

            return pO;
        }

        private static PurchaseOrderDetail CreatePurchaseOrderDetail(int pONo, string itemNo, int? qty)
        {
            LussisEntities context = new LussisEntities();

            // Create PurchaseOrderDetail
            PurchaseOrderDetail pOD = new PurchaseOrderDetail();
            pOD.PONo = pONo;
            pOD.ItemNo = itemNo;
            pOD.Qty = qty;
            context.PurchaseOrderDetails.Add(pOD);
            context.SaveChanges();
            return pOD;
        }

        public static void AddOrder(List<OrderItem> orderList, string itemNo)
        {
            // Get Stock
            StationeryCatalogue stock = GetStock(itemNo);

            // Get Pending Vouchers
            List<AdjustmentVoucher> aVList = new LussisEntities().AdjustmentVouchers.Where(x => x.Status.Equals("Pending")).ToList<AdjustmentVoucher>();

            // Get Pending PO
            List<PurchaseOrder> pOList = new LussisEntities().PurchaseOrders.Where(x => x.Status.Equals("Pending")).ToList<PurchaseOrder>();

            // Create orderItem
            OrderItem orderItem = new OrderItem(stock);
            int? orderQty = GetOrderQty(itemNo, aVList, pOList);
            if (orderQty > 0)
            {
                orderItem.OrderQtyList[0] = orderQty;
            }
            else
            {
                orderItem.OrderQtyList[0] = stock.ReorderQty;
            }

            // Add to orderList
            orderList.Add(orderItem);
        }

        public static void DeleteOrder(List<OrderItem> orderList, string itemNo)
        {
            // Find Stock in orderList
            for (int i = 0; i < orderList.Count; i++)
            {
                // Delete Stock if found
                OrderItem orderItem = orderList[i];
                if (orderItem.Stock.ItemNo.Equals(itemNo))
                {
                    orderList.Remove(orderItem);
                }
            }
        }

        public static void SubmitOrder(List<OrderItem> orderList, int orderedBy, DateTime dateIssued)
        {
            // Create empty POList
            List<PurchaseOrder> pOList = new List<PurchaseOrder>();

            using (var txn = new TransactionScope())
            {
                // For each OrderItem
                for (int i = 0; i < orderList.Count; i++)
                {
                    OrderItem orderItem = orderList[i];
                    string itemNo = orderItem.Stock.ItemNo;

                    // Qty > 0?
                    for (int j = 0; j < orderItem.OrderQtyList.Count; j++)
                    {
                        string supplierCode = orderItem.SupplierCodeList[j];
                        int? qty = orderItem.OrderQtyList[j];
                        if (qty > 0)
                        {
                            // Create or get PO
                            PurchaseOrder pO = CreatePurchaseOrder(supplierCode, orderedBy, dateIssued, pOList);
                            int pONo = pO.PONo;

                            // Create POD
                            PurchaseOrderDetail pOD = CreatePurchaseOrderDetail(pONo, itemNo, qty);
                        }
                    }
                }

                txn.Complete();
            }

            // Send Email
            LussisEntities context = new LussisEntities();
            var supervisorName = context.StoreAssignments.Where(x => x.Role == "Supervisor").FirstOrDefault().Employee.EmpName;
            List<string> pOEmailEntries = new List<string>();
            foreach (PurchaseOrder pO in pOList)
            {
                var pONo = pO.PONo;
                var supplier = context.Suppliers.Where(x => x.SupplierCode.Equals(pO.SupplierCode)).FirstOrDefault().SupplierName;
                var pOEmailEntry = pONo + " " + supplier;
                pOEmailEntries.Add(pOEmailEntry);
            }
            var recipientEmail = context.StoreAssignments.Where(x => x.Role == "Supervisor").FirstOrDefault().Employee.Email;
            var emailSubject = EmailTemplate.GenerateOrderFormEmailSubject();
            var emailContent = EmailTemplate.GenerateOrderFormEmail(supervisorName, pOEmailEntries);
            EmailBackend.sendEmailStep(recipientEmail, emailSubject, emailContent);
        }
    }

    public class OrderItem
    {
        // ATTRIBUTES
        StationeryCatalogue stock;
        List<string> supplierCodeList;
        List<int?> orderQtyList;

        public OrderItem(StationeryCatalogue stock)
        {
            this.stock = stock;
            supplierCodeList = new List<string> { stock.Supplier.SupplierCode, stock.Supplier4.SupplierCode, stock.Supplier5.SupplierCode };
            orderQtyList = new List<int?> { 0, 0, 0 };
        }

        public StationeryCatalogue Stock { get { return stock; } set { stock = value; } }

        public string ItemNo { get { return stock.ItemNo; } }

        public string Description { get { return stock.Description; } }

        public int? QtyOnHand { get { return stock.CurrentQty; } }

        public int? ReorderLevel { get { return stock.ReorderLevel; } }

        public int? ReorderQty { get { return stock.ReorderQty; } }

        public List<string> SupplierCodeList { get { return supplierCodeList; } }

        public string Supplier1 { get { return stock.Supplier1; } }

        public string Supplier2 { get { return stock.Supplier2; } }

        public string Supplier3 { get { return stock.Supplier3; } }

        public List<int?> OrderQtyList { get { return orderQtyList; } }

        public int? Qty1 { get { return orderQtyList[0]; } set { orderQtyList[0] = value; } }

        public int? Qty2 { get { return orderQtyList[1]; } set { orderQtyList[1] = value; } }

        public int? Qty3 { get { return orderQtyList[2]; } set { orderQtyList[2] = value; } }
    }
}
