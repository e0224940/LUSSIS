using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LUSSIS_Backend;

public partial class Department_Employee_ViewRequisitionHistory : System.Web.UI.Page
{

    static List<Requisition> requsition;
    static List<ReqHistory> reqhistory;
    protected void Page_Load(object sender, EventArgs e)
    {
        int empNo = Profile.EmpNo;
        if (!IsPostBack)
        {
            string empName = EmployeeController.GetName(empNo);
            name.Text = empName;  
            DetailGridView.Visible = true;
            requsition = new List<Requisition>();
            reqhistory = new List<ReqHistory>();
            DetailGridView.DataSource = EmployeeController.viewRequisition();
            DetailGridView.DataBind();         
        }
        //if()

    }

    //protected void detailGridView_Delete(object sender, GridViewDeleteEventArgs e)
    //{
    //    string ReqNo = DetailGridView.DataKeys[e.RowIndex].Values[1].ToString();
    //    ReqHistory selected = reqhistory.Where(item => item.ReqNo == ReqNo).FirstOrDefault();
    //    reqhistory.Remove(selected);
    //    Session["session"] = reqhistory;
    //    DetailGridView.DataSource = reqhistory;
    //    DetailGridView.DataBind();
    //}

    protected void detailGridView_Delete(object sender, GridViewDeleteEventArgs e)
    {
        int reqId = Convert.ToInt32(DetailGridView.DataKeys[e.RowIndex].Values[0]);
        EmployeeController.DeleteReqHistory(reqId);
        DetailGridView.DataSource = EmployeeController.viewRequisition();
        DetailGridView.DataBind();
    }
}