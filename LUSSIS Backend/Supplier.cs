//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Supplier
    {
        public Supplier()
        {
            this.PurchaseOrders = new HashSet<PurchaseOrder>();
            this.StationeryCatalogues = new HashSet<StationeryCatalogue>();
            this.StationeryCatalogues1 = new HashSet<StationeryCatalogue>();
            this.StationeryCatalogues2 = new HashSet<StationeryCatalogue>();
            this.SupplyTenders = new HashSet<SupplyTender>();
        }
    
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string ContactName { get; set; }
        public Nullable<decimal> PhoneNo { get; set; }
        public Nullable<decimal> FaxNo { get; set; }
        public string Address { get; set; }
        public string GstNo { get; set; }
    
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual ICollection<StationeryCatalogue> StationeryCatalogues { get; set; }
        public virtual ICollection<StationeryCatalogue> StationeryCatalogues1 { get; set; }
        public virtual ICollection<StationeryCatalogue> StationeryCatalogues2 { get; set; }
        public virtual ICollection<SupplyTender> SupplyTenders { get; set; }
    }
}
