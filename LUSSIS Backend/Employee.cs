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
    
    public partial class Employee
    {
        public Employee()
        {
            this.AdjustmentVouchers = new HashSet<AdjustmentVoucher>();
            this.AdjustmentVouchers1 = new HashSet<AdjustmentVoucher>();
            this.Departments = new HashSet<Department>();
            this.Departments1 = new HashSet<Department>();
            this.Departments2 = new HashSet<Department>();
            this.Deputies = new HashSet<Deputy>();
            this.Disbursements = new HashSet<Disbursement>();
            this.PurchaseOrders = new HashSet<PurchaseOrder>();
            this.PurchaseOrders1 = new HashSet<PurchaseOrder>();
            this.Requisitions = new HashSet<Requisition>();
            this.Requisitions1 = new HashSet<Requisition>();
        }
    
        public int EmpNo { get; set; }
        public string EmpName { get; set; }
        public string DeptCode { get; set; }
        public string Email { get; set; }
        public Nullable<decimal> SessionNo { get; set; }
        public Nullable<System.DateTime> SessionExpiry { get; set; }
    
        public virtual ICollection<AdjustmentVoucher> AdjustmentVouchers { get; set; }
        public virtual ICollection<AdjustmentVoucher> AdjustmentVouchers1 { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Department> Departments1 { get; set; }
        public virtual ICollection<Department> Departments2 { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<Deputy> Deputies { get; set; }
        public virtual ICollection<Disbursement> Disbursements { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders1 { get; set; }
        public virtual ICollection<Requisition> Requisitions { get; set; }
        public virtual ICollection<Requisition> Requisitions1 { get; set; }
    }
}
