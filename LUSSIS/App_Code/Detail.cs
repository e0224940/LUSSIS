using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Detail
/// </summary>
public class Detail
{
    public int reqNo { get; set; }
    public string itemNo { get; set; }
    public string description { get; set; }
    public int? quantity { get; set; }

    public bool isEditable { get; set; }


    public Detail()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}