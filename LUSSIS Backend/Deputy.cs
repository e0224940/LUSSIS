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
    
    public partial class Deputy
    {
        public string DeptCode { get; set; }
        public Nullable<int> DeputyEmpNo { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
    
        public virtual Department Department { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
