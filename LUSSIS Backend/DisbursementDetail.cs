
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
    
public partial class DisbursementDetail
{

    public int DisbursementNo { get; set; }

    public string ItemNo { get; set; }

    public Nullable<int> Needed { get; set; }

    public Nullable<int> Promised { get; set; }

    public Nullable<int> Received { get; set; }



    public virtual Disbursement Disbursement { get; set; }

    public virtual StationeryCatalogue StationeryCatalogue { get; set; }

}

}
