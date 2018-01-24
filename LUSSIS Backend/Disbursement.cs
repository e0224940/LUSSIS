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
    
    public partial class Disbursement
    {
        public Disbursement()
        {
            this.DisbursementDetails = new HashSet<DisbursementDetail>();
        }
    
        public int DisbursementNo { get; set; }
        public string DeptCode { get; set; }
        public Nullable<System.DateTime> DisbursementDate { get; set; }
        public Nullable<int> RepEmpNo { get; set; }
        public Nullable<int> CollectionPointNo { get; set; }
        public Nullable<decimal> Pin { get; set; }
        public string Status { get; set; }
    
        public virtual CollectionPoint CollectionPoint { get; set; }
        public virtual Department Department { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<DisbursementDetail> DisbursementDetails { get; set; }
    }
}