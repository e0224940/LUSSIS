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

        if(!IsPostBack)
        {
            //get current Acting Head and print on webpage
            Employee deputyHead = getDeputyHeadOfDepartment(getDepartmentNoFromProfile());

            txtBox_appAuth_currentHead.Text = deputyHead.EmpName;

            //getDepartmentEmployeeList
            ddl_appAuth_deptEmps.DataSource = getEmployeesNameInDepartment();
            ddl_appAuth_deptEmps.DataBind();

            //set date so that dates past are not allowed.
            txtbox_dateStart.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");
            txtbox_dateEnd.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");

            txtbox_dateStart.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtbox_dateEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");

        }
    }

    protected void button_appAuth_appoint_Click(object sender, EventArgs e)
    {

        string empName = ddl_appAuth_deptEmps.SelectedItem.Value;
        int empNo = getEmpNoFromEmpName(empName);
        DateTime dateStart = Convert.ToDateTime(txtbox_dateStart.Text);
        DateTime dateEnd = Convert.ToDateTime(txtbox_dateEnd.Text);
        string deptCode = getDepartmentNoFromProfile();

        if (getDeputyHeadOfDepartment(deptCode).EmpNo != Profile.EmpNo)
        {
            removeAuthority(deptCode, getDeputyHeadOfDepartment(deptCode).EmpNo);
        }

        addAuthority(deptCode, empNo, dateStart, dateEnd);
    }

    protected void button_appAuth_remove_Click(object sender, EventArgs e)
    {
        int outgoingDeputyHeadCode = getDeputyHeadOfDepartment(getDepartmentNoFromProfile()).EmpNo;

        removeAuthority(getDepartmentNoFromProfile(), outgoingDeputyHeadCode);
    }



    //*****************************************************************************





    public Employee getDeputyHeadOfDepartment(String deptCode)
    {
        using (LussisEntities context = new LussisEntities())
        {
            int w = (int)context.Departments.Where(x => x.DeptCode.Equals(deptCode)).First().DeputyEmpNo;
          
            Employee ww = context.Employees.Where(x => x.EmpNo.Equals(w)).First();

            return ww;
        }
    }

    public string getDepartmentNoFromProfile()
    {
        int empNo = Profile.EmpNo;
        using (LussisEntities context = new LussisEntities())
        {
            return context.Employees.Where(x => x.EmpNo.Equals(empNo)).First().DeptCode;
        }
    }

    public List<String> getEmployeesNameInDepartment()
    {
        string deptNo = getDepartmentNoFromProfile();
        List<string> list_names;

        using (LussisEntities context = new LussisEntities())
        {
            List<Employee> list = context.Employees.Where(x => x.DeptCode.Equals(deptNo)).ToList();
            list_names = new List<string>();

            for(int i=0; i<list.Count();i++)
            {
                list_names.Add(list[i].EmpName);
            }
        }

        return list_names;
    }

    protected int getEmpNoFromEmpName (string empName)
    {
        using (LussisEntities context = new LussisEntities())
        {
            Employee dep = context.Employees.Where(x => x.EmpName.Equals(empName)).First();
            return dep.EmpNo;
        }

    }

    protected void removeAuthority(string deptCode,int outgoingEmpNo)
    {

        string recipientEmail;
        using (LussisEntities context = new LussisEntities())
        {
            Deputy dep = context.Deputies.Where(x => x.DeptCode.Equals(deptCode)).First();
            context.Deputies.Remove(dep);

            Department dept = context.Departments.Where(x => x.DeptCode.Equals(deptCode)).First();
            dept.DeputyEmpNo = Profile.EmpNo;

            context.SaveChanges();

            Employee newDeputy = context.Employees.Where(x => x.EmpNo.Equals(dept.EmployeeDeputy.EmpNo)).First();
            txtBox_appAuth_currentHead.Text = newDeputy.EmpName;


        }

        using (LussisEntities context = new LussisEntities())
        {
            recipientEmail = context.Employees.Where(x => x.EmpNo.Equals(outgoingEmpNo)).First().Email;
        }

        EmailBackend.sendEmailStep(recipientEmail,
            EmailTemplate.GenerateOldDeputyAuthorityRemovedSubject(), 
            EmailTemplate.GenerateOldDeputyAuthorityRemovedEmail());
        
    }

    protected void addAuthority(string deptCode, int empNo, DateTime start, DateTime end)
    {
        Deputy d = new Deputy();
        d.DeptCode = deptCode;
        d.DeputyEmpNo = empNo;
        d.FromDate = start;
        d.ToDate = end;

        using (LussisEntities context = new LussisEntities())
        {
            context.Deputies.Add(d);
            Department dept = context.Departments.Where(x => x.DeptCode.Equals(deptCode)).First();
            dept.DeputyEmpNo = empNo;
            context.SaveChanges();

            Employee newDeputy = context.Employees.Where(x => x.EmpNo.Equals(empNo)).First();
            txtBox_appAuth_currentHead.Text = newDeputy.EmpName;
        }

        string recipientEmail;

        using (LussisEntities context = new LussisEntities())
        {
            recipientEmail = context.Employees.Where(x => x.EmpNo.Equals(empNo)).First().Email;
        }

        EmailBackend.sendEmailStep(recipientEmail,
        EmailTemplate.GenerateNewDeputyAuthoritySubject(),
        EmailTemplate.GenerateNewDeputyAuthorityEmail());

    }

    protected void txtbox_dateStart_TextChanged(object sender, EventArgs e)
    {
        txtbox_dateEnd.Text = txtbox_dateStart.Text;
        txtbox_dateEnd.Attributes["min"] = txtbox_dateStart.Text;
    }


}