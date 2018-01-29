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
            //GridViewRow row = DetailGridView.Rows[DetailGridView.SelectedIndex];
           // string check = (row.FindControl("Status") as Label).Text;\
           
           
            string empName = EmployeeController.GetName(empNo);
            name.Text = empName;  
            DetailGridView.Visible = true;
            requsition = new List<Requisition>();
            reqhistory = new List<ReqHistory>();
            DetailGridView.DataSource = EmployeeController.ViewRequisition();
            DetailGridView.DataBind();         
        }
        //if()

        ////Zeng Rui' code
        //GridViewRow row = DetailGridView.SelectedRow;
        //string Rnum = DetailGridView.Rows[row.RowIndex].Cells[2].Text.ToString();
        //if (Rnum == "Approved")
        //{
        //    row.Cells[3].Visible = false;
        //    row.Cells[4].Visible = false;
        //}
        //DetailGridView.DataSource = EmployeeController.ViewRequisition();
        //DetailGridView.DataBind();

    }

    //Zeng Rui'code 
    //protected void detailGridView_Delete(object sender, GridViewDeleteEventArgs e)
    //{
    //    int reqId = Convert.ToInt32(DetailGridView.DataKeys[e.RowIndex].Values[0]);
    //    EmployeeController.DeleteReqHistory(reqId);
    //    DetailGridView.DataSource = EmployeeController.viewRequisition();
    //    DetailGridView.DataBind();
    //}


    protected void detailGridView_Delete(object sender, GridViewDeleteEventArgs e)
    {
        int reqId = Convert.ToInt32(DetailGridView.DataKeys[e.RowIndex].Values[0]);
        Session["sessionID"] = reqId;
        EmployeeController.DeleteReqHistory(reqId);
        DetailGridView.DataSource = EmployeeController.ViewRequisition();
        DetailGridView.DataBind();
    }

    protected void DetailGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GridViewRow row = DetailGridView.Rows.ToStrin;
        string check = DetailGridView.SelectedDataKey.Values[3].ToString();
        //if (check.Equals("Approved"))
        //{
            
            ////GridViewRow row = DetailGridView.Rows[DetailGridView.SelectedIndex];
            //DetailGridView.Columns[4].Visible = false;
        //}
        //else
        //{ 
            int ReqNo = (int)DetailGridView.SelectedDataKey.Values[0];
            Response.Redirect("RequisitionDetailsView.aspx?ReqNo="+ReqNo);
        //}
    }

    //protected void view_Click(object sender, EventArgs e)
    //{
       
    //}
}