using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LUSSIS_Backend;

public partial class Department_Employee_ViewRequisitionHistory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int empNo = Profile.EmpNo;

        if (!IsPostBack)
        {
            string empName = EmployeeController.GetName(empNo);
            name.Text = empName;
        }

    }
}