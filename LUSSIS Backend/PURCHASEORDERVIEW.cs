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
    
    public partial class PURCHASEORDERVIEW
    {
        public int PONo { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public Nullable<int> OrderedBy { get; set; }
        public Nullable<System.DateTime> DateIssued { get; set; }
        public Nullable<int> ApprovedBy { get; set; }
        public Nullable<System.DateTime> DateReviewed { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public string ItemNo { get; set; }
        public Nullable<int> Qty { get; set; }
        public string Description { get; set; }
        public Nullable<int> ReorderLevel { get; set; }
        public Nullable<int> ReorderQty { get; set; }
        public Nullable<int> CurrentQty { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
    }
}
