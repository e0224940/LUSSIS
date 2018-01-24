using LUSSIS_Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Department_Employee_RequisitionDetailsView : System.Web.UI.Page
{
    static List<RaisedItem> Reqitem;
    static List<ReqItem> item;

    protected void Page_Load(object sender, EventArgs e)
    {
        int empNo = Profile.EmpNo;

        if (!IsPostBack)
        {          
            string empName = EmployeeController.GetName(empNo);
            empNameshow.Text = empName;
        }
    }

    protected void cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewRequisitionHistory.aspx");
    }

    protected void detailGrid_Delete(object sender, GridViewDeleteEventArgs e)
    {
        string ItemNo = detailGrid.DataKeys[e.RowIndex].Values[0].ToString();
        RaisedItem selected = Reqitem.Where(item => item.ItemNo == ItemNo && item.ReqNo == Num.ToString()).FirstOrDefault();
        Reqitem.Remove(selected);
        detailGrid.DataSource = Reqitem;
        detailGrid.DataBind();

    }

    protected void detailGrid_Edit(object sender, GridViewEditEventArgs e)
    {

    }
}
