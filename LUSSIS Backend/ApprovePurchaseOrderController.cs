﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LUSSIS_Backend
{
    public class ApprovePurchaseOrderController
    {
        //ApprovePOList functions
        public static List<PurchaseOrder> getPendingOrdersList()
        {
            LussisEntities context = new LussisEntities();
            var result = context.PurchaseOrders.Where(x => x.Status == "Pending").ToList();
            return result;
        }

        //ApprovePODetails functions
        public static List<PURCHASEORDERVIEW> getPurchaseOrderDetails(int poNO, string supplierSelected)
        {
            LussisEntities context = new LussisEntities();
            var result = context.PURCHASEORDERVIEWs.Where(x => x.PONo == poNO && x.SupplierCode == supplierSelected).ToList();
            return result;
        }

        //Button(Approve) function
        public static void setStatusApprove(int poNO)
        {
            LussisEntities context = new LussisEntities();
            var approvePurchaseOrderview = context.PurchaseOrders.Where(x => x.PONo == poNO).FirstOrDefault();
            approvePurchaseOrderview.Status = "Approved";
            context.SaveChanges();
        }

        //Button(Rejected) function
        public static void setStatusReject(int poNO)
        {
            LussisEntities context = new LussisEntities();
            var approvePurchaseOrderview = context.PurchaseOrders.Where(x => x.PONo == poNO).FirstOrDefault();
            approvePurchaseOrderview.Status = "Rejected";
            context.SaveChanges();
        }

        //set ApproveBy
        public static void updateApproveBy(int poNO, int empNo)
        {
            LussisEntities context = new LussisEntities();
        
            var query = context.PurchaseOrders.Where(x => x.PONo == poNO).SingleOrDefault();
            query.ApprovedBy = empNo;
            context.SaveChanges();
        }

        //set dateReviewed
        public static void updateDateReviewed(int poNO)
        {
            LussisEntities context = new LussisEntities();
            var query = context.PurchaseOrders.Where(x => x.PONo == poNO).SingleOrDefault();
            query.DateReviewed = DateTime.Now.Date;
            context.SaveChanges();
        }

        //set Remarks
        public static void updateRemarks(int poNO, string remarks)
        {
            LussisEntities context = new LussisEntities();
            var query = context.PurchaseOrders.Where(x => x.PONo == poNO).SingleOrDefault();
            query.Remarks = remarks;
            context.SaveChanges();
        }
    }
}
