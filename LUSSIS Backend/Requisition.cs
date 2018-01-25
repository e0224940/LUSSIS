
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
    
public partial class Requisition
{

    public Requisition()
    {

        this.RequisitionDetails = new HashSet<RequisitionDetail>();

    }


    public int ReqNo { get; set; }

    public Nullable<int> IssuedBy { get; set; }

    public Nullable<System.DateTime> DateIssued { get; set; }

    public Nullable<int> ApprovedBy { get; set; }

    public Nullable<System.DateTime> DateReviewed { get; set; }

    public string Status { get; set; }

    public string Remarks { get; set; }



    public virtual Employee EmployeeWhoIssued { get; set; }

    public virtual Employee EmployeeWhoApproved { get; set; }

    public virtual ICollection<RequisitionDetail> RequisitionDetails { get; set; }

}

}
