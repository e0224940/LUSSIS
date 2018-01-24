﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LUSSIS_Backend
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LussisEntities : DbContext
    {
        public LussisEntities()
            : base("name=LussisEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<aspnet_Applications> aspnet_Applications { get; set; }
        public DbSet<aspnet_Membership> aspnet_Membership { get; set; }
        public DbSet<aspnet_Paths> aspnet_Paths { get; set; }
        public DbSet<aspnet_PersonalizationAllUsers> aspnet_PersonalizationAllUsers { get; set; }
        public DbSet<aspnet_PersonalizationPerUser> aspnet_PersonalizationPerUser { get; set; }
        public DbSet<aspnet_Profile> aspnet_Profile { get; set; }
        public DbSet<aspnet_Roles> aspnet_Roles { get; set; }
        public DbSet<aspnet_SchemaVersions> aspnet_SchemaVersions { get; set; }
        public DbSet<aspnet_Users> aspnet_Users { get; set; }
        public DbSet<aspnet_WebEvent_Events> aspnet_WebEvent_Events { get; set; }
        public DbSet<AdjustmentVoucher> AdjustmentVouchers { get; set; }
        public DbSet<AdjustmentVoucherDetail> AdjustmentVoucherDetails { get; set; }
        public DbSet<BackLog> BackLogs { get; set; }
        public DbSet<CollectionPoint> CollectionPoints { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Deputy> Deputies { get; set; }
        public DbSet<Disbursement> Disbursements { get; set; }
        public DbSet<DisbursementDetail> DisbursementDetails { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public DbSet<Requisition> Requisitions { get; set; }
        public DbSet<RequisitionDetail> RequisitionDetails { get; set; }
        public DbSet<Retrieval> Retrievals { get; set; }
        public DbSet<RetrievalDetail> RetrievalDetails { get; set; }
        public DbSet<StationeryCatalogue> StationeryCatalogues { get; set; }
        public DbSet<StockTxnDetail> StockTxnDetails { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplyTender> SupplyTenders { get; set; }
        public DbSet<ApprovePurchaseOrderView> ApprovePurchaseOrderViews { get; set; }
        public DbSet<PURCHASEORDERVIEW> PURCHASEORDERVIEWs { get; set; }
        public DbSet<ReorderTrendView> ReorderTrendViews { get; set; }
        public DbSet<StoreAssignment> StoreAssignments { get; set; }
    }
}
