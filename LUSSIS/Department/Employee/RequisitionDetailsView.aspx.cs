using LUSSIS_Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Department_Employee_RequisitionDetailsView : System.Web.UI.Page
{
   

    protected void Page_Load(object sender, EventArgs e)
    {
        int empNo = Profile.EmpNo;

        if (!IsPostBack)
        {          
            string empName = EmployeeController.GetName(empNo);
            empNameshow.Text = empName;
        }
    }
}