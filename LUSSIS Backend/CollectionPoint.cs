
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
    
public partial class CollectionPoint
{

    public CollectionPoint()
    {

        this.Departments = new HashSet<Department>();

        this.Disbursements = new HashSet<Disbursement>();

    }


    public int CollectionPointNo { get; set; }

    public string CollectionPointDetails { get; set; }



    public virtual ICollection<Department> Departments { get; set; }

    public virtual ICollection<Disbursement> Disbursements { get; set; }

}

}
