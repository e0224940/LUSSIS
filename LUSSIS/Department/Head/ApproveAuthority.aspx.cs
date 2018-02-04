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

            //if (deputyHead.EmpNo.Equals(Profile.EmpNo))
            //{
            //    button_appAuth_remove.Enabled = false;
            //}
            //else
            //{
            //    button_appAuth_remove.Enabled = true;
            //}
            button_appAuth_remove.Enabled = false;
            lbl_currentFutureAppts.Visible = false;

        }
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

        if (dateStart.CompareTo(DateTime.Today) == 0)
        {
            Deputy d = ApproveAuthorityController.getDeputyDetails(ApproveAuthorityController.getDepartmentNoFromProfile(Profile.EmpNo));

            txtBox_appAuth_currentHead.Text = d.Employee.EmpName;
        }
        button_appAuth_remove.Enabled = true;

        BindGrid();

        //try
        //{
        //    if (ApproveAuthorityController.getDeputyDetailsForDept(ApproveAuthorityController.getDepartmentNoFromProfile(Profile.EmpNo)) != null)
        //    {
        //        //collecting data from view
        //        string empName = ddl_appAuth_deptEmps.SelectedItem.Value;
        //        int empNo = ApproveAuthorityController.getEmpNoFromEmpName(empName);
        //        DateTime dateStart = Convert.ToDateTime(txtbox_dateStart.Text);
        //        DateTime dateEnd = Convert.ToDateTime(txtbox_dateEnd.Text);
        ////        string deptCode = ApproveAuthorityController.getDepartmentNoFromProfile(Profile.EmpNo);

        //        //remove authority 
        //        if (ApproveAuthorityController.getDeputyHeadOfDepartment(deptCode).EmpNo != Profile.EmpNo)
        //        {
        //            ApproveAuthorityController.removeAuthority(Profile.EmpNo, deptCode, ApproveAuthorityController.getDeputyHeadOfDepartment(deptCode).EmpNo);
        //        }

        //        //add authority
        //        if (ApproveAuthorityController.getDeputyDetailsForDept(ApproveAuthorityController.getDepartmentNoFromProfile(Profile.EmpNo)).Count == 0)
        //        {
        //            string newAuthorityName = ApproveAuthorityController.addAuthority(deptCode, empNo, dateStart, dateEnd);    //getting new name from method
        //            Employee deputyHead = ApproveAuthorityController.getDeputyHeadOfDepartment(ApproveAuthorityController.getDepartmentNoFromProfile(Profile.EmpNo));
        //            txtBox_appAuth_currentHead.Text = deputyHead.EmpName;
        //            if (dateStart.Equals(DateTime.Today))
        //            {
        //                button_appAuth_remove.Enabled = true;
        //            }

        //            //lbl_currentFutureAppts.Visible = true;
        //        }
        //    }
        //}
        //catch (Exception exp)
        //{
        //    Session["Error"]="Only one delegation of authority is allowed. Please try again.";
        //}

        //BindGrid();
    }

    protected void button_appAuth_remove_Click(object sender, EventArgs e)
    {
        int outgoingDeputyHeadCode = ApproveAuthorityController.getDeputyHeadOfDepartment(ApproveAuthorityController.getDepartmentNoFromProfile(Profile.EmpNo)).EmpNo;

        ApproveAuthorityController.removeAuthority(Profile.EmpNo, ApproveAuthorityController.getDepartmentNoFromProfile(Profile.EmpNo), outgoingDeputyHeadCode);

        Employee me = ApproveAuthorityController.getHeadOfDepartment(ApproveAuthorityController.getDepartmentNoFromProfile(Profile.EmpNo));

        txtBox_appAuth_currentHead.Text = me.EmpName; //getting new name from method

        button_appAuth_remove.Enabled = false;

        BindGrid();
    }

    protected void txtbox_dateStart_TextChanged(object sender, EventArgs e)
    {
        txtbox_dateEnd.Text = txtbox_dateStart.Text;
        txtbox_dateEnd.Attributes["min"] = txtbox_dateStart.Text;
    }

    //protected void OnRowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    gridView_AppAuthCurrFutAppts.EditIndex = e.NewEditIndex;
    //    gridView_AppAuthCurrFutAppts.DataBind();
    //}

    //protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    // Get selected row
    //    GridViewRow row = gridView_AppAuthCurrFutAppts.Rows[e.RowIndex];

    //    try
    //    {
    //        Deputy d = (Deputy)row.DataItem;
    //        ApproveAuthorityController.UpdateDeputy(d);
    //        gridView_AppAuthCurrFutAppts.EditIndex = -1;
    //        gridView_AppAuthCurrFutAppts.DataBind();
    //    }
    //    catch (Exception exception)
    //    {
    //        Session["Error"] = "An Error Has Occured: " + exception.Message;
    //    }
    //}

    //protected void OnRowCancelingEdit(object sender, EventArgs e)
    //{
    //    gridView_AppAuthCurrFutAppts.EditIndex = -1;
    //    gridView_AppAuthCurrFutAppts.DataBind();
    //}


    //protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    GridViewRow row = gridView_AppAuthCurrFutAppts.Rows[e.RowIndex];
    //    string deptCode = (row.FindControl("LabelDeptCode") as Label).Text;
    //    int deputyEmpNo = Convert.ToInt32((row.FindControl("LabelDepEmpNo") as Label).Text);
    //    //ApproveAuthorityController.removeDeputy(deptCode,deputyEmpNo);
    //    ApproveAuthorityController.removeAuthority(Profile.EmpNo, deptCode, deputyEmpNo);
    //    button_appAuth_remove.Enabled = false;
    //    BindGrid();
    //}

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