using Email_Backend;
using LUSSIS_Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Department_Head_ApproveAuthority : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            //get current Acting Head and print on webpage
            Employee deputyHead = ApproveAuthorityController.getDeputyHeadOfDepartment(ApproveAuthorityController.getDepartmentNoFromProfile(Profile.EmpNo));
            Employee me = ApproveAuthorityController.getHeadOfDepartment(ApproveAuthorityController.getDepartmentNoFromProfile(Profile.EmpNo));
            txtBox_appAuth_currentHead.Text = deputyHead.EmpName;

            //getDepartmentEmployeeList
            ddl_appAuth_deptEmps.DataSource = ApproveAuthorityController
                .getEmployeesNameInDepartment(Profile.EmpNo)
                .Where(x=> !x.Equals(me.EmpName));
            ddl_appAuth_deptEmps.DataBind();

            //set date so that dates past are not allowed.
            txtbox_dateStart.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");
            txtbox_dateEnd.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");

            Employee head = ApproveAuthorityController.getHeadOfDepartment(ApproveAuthorityController.getDepartmentNoFromProfile(Profile.EmpNo));

            if(head.EmpNo!=deputyHead.EmpNo)
            {
            Deputy d = ApproveAuthorityController.getDeputyDetails(ApproveAuthorityController.getDepartmentNoFromProfile(Profile.EmpNo).ToString());

                txtbox_dateStart.Text = Convert.ToDateTime(d.FromDate).ToString("yyyy-MM-dd");
                txtbox_dateEnd.Text = Convert.ToDateTime(d.ToDate).ToString("yyyy-MM-dd");
            }
            else
            {
                txtbox_dateStart.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtbox_dateEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }

            button_appAuth_remove.Enabled = false;
            lbl_currentFutureAppts.Visible = false;

        }
        //populate gridview
        gridView_AppAuthCurrFutAppts.DataSource = ApproveAuthorityController.getDeputyDetailsForDept(ApproveAuthorityController.getDepartmentNoFromProfile(Profile.EmpNo).ToString());
        if (ApproveAuthorityController.getDeputyDetailsForDept(ApproveAuthorityController.getDepartmentNoFromProfile(Profile.EmpNo).ToString()).Count!=0)
        {
            lbl_currentFutureAppts.Visible = true;
            button_appAuth_remove.Enabled = true;
        }
        BindGrid();

    }

    protected void button_appAuth_appoint_Click(object sender, EventArgs e)
    {
        string deptCode = ApproveAuthorityController.getDepartmentNoFromProfile(Profile.EmpNo);

        if (ApproveAuthorityController.getDeputyDetailsForDept(ApproveAuthorityController.getDepartmentNoFromProfile(Profile.EmpNo)).Count!=0)
        {
            ApproveAuthorityController.removeAuthority(Profile.EmpNo, deptCode, ApproveAuthorityController.getDeputyHeadOfDepartment(deptCode).EmpNo);
        }
        string empName = ddl_appAuth_deptEmps.SelectedItem.Value;
        int empNo = ApproveAuthorityController.getEmpNoFromEmpName(empName);
        DateTime dateStart = Convert.ToDateTime(txtbox_dateStart.Text).Date;
        DateTime dateEnd = Convert.ToDateTime(txtbox_dateEnd.Text).Date;

        ApproveAuthorityController.addAuthority(deptCode, empNo, dateStart, dateEnd);

        //checking if the employee is taking over today or later
        if (dateStart.CompareTo(DateTime.Today) == 0)
        {
            Deputy d = ApproveAuthorityController.getDeputyDetails(ApproveAuthorityController.getDepartmentNoFromProfile(Profile.EmpNo));

            txtBox_appAuth_currentHead.Text = d.Employee.EmpName;
        }
        button_appAuth_remove.Enabled = true;

        BindGrid();
    }

    protected void button_appAuth_remove_Click(object sender, EventArgs e)
    {
        int outgoingDeputyHeadCode = ApproveAuthorityController.getDeputyHeadOfDepartment(ApproveAuthorityController.getDepartmentNoFromProfile(Profile.EmpNo)).EmpNo;

        ApproveAuthorityController.removeAuthority(Profile.EmpNo, ApproveAuthorityController.getDepartmentNoFromProfile(Profile.EmpNo), outgoingDeputyHeadCode);

        Employee me = ApproveAuthorityController.getHeadOfDepartment(ApproveAuthorityController.getDepartmentNoFromProfile(Profile.EmpNo));

        txtBox_appAuth_currentHead.Text = me.EmpName; //getting head from method

        button_appAuth_remove.Enabled = false;

        BindGrid();
    }

    protected void txtbox_dateStart_TextChanged(object sender, EventArgs e)
    {
        txtbox_dateEnd.Text = txtbox_dateStart.Text;
        txtbox_dateEnd.Attributes["min"] = txtbox_dateStart.Text;
    }

   
    private void BindGrid()
    {
        // Get PODetails
        List<Deputy> deputy = ApproveAuthorityController.getDeputyDetailsForDept(ApproveAuthorityController.getDepartmentNoFromProfile(Profile.EmpNo));

        if(deputy.Count!=0)
        {
            // Set gridview
            gridView_AppAuthCurrFutAppts.DataSource = deputy.Select(
                dep => new
                {
                    DeptCode = dep.DeptCode,
                    DeputyEmpName = dep.Employee.EmpName,
                    FromDate = dep.FromDate.Value.Date.ToShortDateString(),
                    ToDate = dep.ToDate.Value.Date.ToShortDateString()
                });
            lbl_currentFutureAppts.Visible = true;
            gridView_AppAuthCurrFutAppts.Visible = true;
            gridView_AppAuthCurrFutAppts.DataBind();
        }
        else
        {
            lbl_currentFutureAppts.Visible = false;
            gridView_AppAuthCurrFutAppts.Visible = false;
        }
    }
}