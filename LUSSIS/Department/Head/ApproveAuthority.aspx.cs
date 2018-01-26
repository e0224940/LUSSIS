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
            //deputyHead = getDeputyHeadOfDepartment(getDepartmentNoFromProfile(Profile.EmpNo));
            Employee deputyHead = ApproveAuthorityController.getDeputyHeadOfDepartment(ApproveAuthorityController.getDepartmentNoFromProfile(Profile.EmpNo));

            txtBox_appAuth_currentHead.Text = deputyHead.EmpName;

            //getDepartmentEmployeeList
            ddl_appAuth_deptEmps.DataSource = ApproveAuthorityController.getEmployeesNameInDepartment(Profile.EmpNo);
            ddl_appAuth_deptEmps.DataBind();

            //set date so that dates past are not allowed.
            txtbox_dateStart.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");
            txtbox_dateEnd.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");

            txtbox_dateStart.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtbox_dateEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");

            if (deputyHead.EmpNo.Equals(Profile.EmpNo))
            {
                button_appAuth_remove.Enabled = false;
            }
            else
            {
                button_appAuth_remove.Enabled = true;
            }
        }


    }

    protected void button_appAuth_appoint_Click(object sender, EventArgs e)
    {
        //collecting data from view
        string empName = ddl_appAuth_deptEmps.SelectedItem.Value;
        int empNo = ApproveAuthorityController.getEmpNoFromEmpName(empName);
        DateTime dateStart = Convert.ToDateTime(txtbox_dateStart.Text);
        DateTime dateEnd = Convert.ToDateTime(txtbox_dateEnd.Text);
        string deptCode = ApproveAuthorityController.getDepartmentNoFromProfile(Profile.EmpNo);

        //remove authority 
        if (ApproveAuthorityController.getDeputyHeadOfDepartment(deptCode).EmpNo != Profile.EmpNo)
        {
            ApproveAuthorityController.removeAuthority(Profile.EmpNo,deptCode, ApproveAuthorityController.getDeputyHeadOfDepartment(deptCode).EmpNo);
        }

        //add authority
        string newAuthorityName = ApproveAuthorityController.addAuthority(deptCode, empNo, dateStart, dateEnd);    //getting new name from method
        txtBox_appAuth_currentHead.Text = newAuthorityName;

        button_appAuth_remove.Enabled = true;
    }

    protected void button_appAuth_remove_Click(object sender, EventArgs e)
    {
        int outgoingDeputyHeadCode = ApproveAuthorityController.getDeputyHeadOfDepartment(ApproveAuthorityController.getDepartmentNoFromProfile(Profile.EmpNo)).EmpNo;

        string newAuthorityName = ApproveAuthorityController.removeAuthority(Profile.EmpNo, ApproveAuthorityController.getDepartmentNoFromProfile(Profile.EmpNo), outgoingDeputyHeadCode);

        txtBox_appAuth_currentHead.Text = newAuthorityName; //getting new name from method

        button_appAuth_remove.Enabled = false;
    }

    protected void txtbox_dateStart_TextChanged(object sender, EventArgs e)
    {
        txtbox_dateEnd.Text = txtbox_dateStart.Text;
        txtbox_dateEnd.Attributes["min"] = txtbox_dateStart.Text;
    }

}