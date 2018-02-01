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
            //for (int i = 0; i < DetailGridView.Rows.Count; i++)
            //{
            //    string check = DetailGridView.Rows[i].Cells[3].Text.ToString();
            //    if (check.Equals("Approved"))
            //    {
            //        DetailGridView.Rows[i].Cells[4].Visible = false;
            //    }
            //}
            for (int i = 0; i < DetailGridView.Rows.Count; i++)
            {
                string check = DetailGridView.Rows[i].Cells[3].Text.ToString();
                if (check.Equals("Approved"))
                {
                    DetailGridView.Rows[i].Cells[1].Visible = false;
                    //DetailGridView.Rows[i]..Visible = false;
                }
            }

            string empName = EmployeeController.GetName(empNo);
            name.Text = empName;  
            DetailGridView.Visible = true;
            requsition = new List<Requisition>();
            reqhistory = new List<ReqHistory>();
           
            DetailGridView.DataSource = EmployeeController.ViewRequisition();
            DetailGridView.DataBind();      
               
        }
    }

    //Delete Requisition History
    protected void detailGridView_Delete(object sender, GridViewDeleteEventArgs e)
    {
        
        int reqId = Convert.ToInt32(DetailGridView.DataKeys[e.RowIndex].Values[0]);
        Session["sessionID"] = reqId;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Are you sure want to delete the History?')</script>");
        EmployeeController.DeleteReqHistory(reqId);
        DetailGridView.DataSource = EmployeeController.ViewRequisition();
        DetailGridView.DataBind();
    }


    //Go to detail Page
    protected void DetailGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        int ReqNo = (int)DetailGridView.SelectedDataKey.Values[0];
        Response.Redirect("RequisitionDetailsView.aspx?ReqNo=" + ReqNo);
    }


    protected void DeleteButton_Click(object sender, EventArgs e)
    {
        Button b = (Button)sender;
        int reqId = Convert.ToInt32(b.CommandArgument);
        Session["sessionID"] = reqId;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Are you sure want to delete the History?')</script>");
        EmployeeController.DeleteReqHistory(reqId);
        DetailGridView.DataSource = EmployeeController.ViewRequisition();
        DetailGridView.DataBind();
    }
}