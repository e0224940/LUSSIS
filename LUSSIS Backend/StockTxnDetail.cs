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
    
    public partial class StockTxnDetail
    {
        public string ItemNo { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> AdjustQty { get; set; }
        public Nullable<int> RecordedQty { get; set; }
        public string Remarks { get; set; }
        public int StockTxnNo { get; set; }
    
        public virtual StationeryCatalogue StationeryCatalogue { get; set; }
    }
}
