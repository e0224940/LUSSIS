﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Department_Employee_RaisedRequisitionSuccessPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void view_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewRequisitionHistory.aspx");
    }
}