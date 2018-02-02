using LUSSIS_Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Department_Employee_RequisitionDetailsView : System.Web.UI.Page
{
  
    static List<Detail> reqhistory;
    static int reqNo;

    protected void Page_Load(object sender, EventArgs e)
    {
        int empNo = Profile.EmpNo;
        string ss = Request.QueryString["ReqNo"];
        reqNo = int.Parse(ss);
        ReqId.Text = ss;
        if (!IsPostBack)
        {        
            string empName = EmployeeController.GetName(empNo);
            empNameshow.Text = empName;
            BindGrid();
        }        
    }


    //Data Bind Method for main page load
    private void BindGrid()
    {
        reqhistory = new List<Detail>();
        List<RequisitionDetail> reqHistory = EmployeeController.ViewRequisitionDetail(reqNo);
        foreach (RequisitionDetail r in reqHistory)
        {
            Detail d = new Detail();

            d.itemNo = r.ItemNo;
            d.reqNo = r.ReqNo;
            d.quantity = r.Qty;
            d.description = r.StationeryItem.Description;
            d.isEditable = r.RequisitionInfo.Status.Equals("Pending");
            reqhistory.Add(d);
        }
        GridViewForDetail.DataSource = reqhistory;
        GridViewForDetail.DataBind();
    
    }


    //Cancel 
    protected void cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewRequisitionHistory.aspx");
    }

   
    //Delte individual item by row deleting
    protected void detailGrid_Delete(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row = GridViewForDetail.Rows[e.RowIndex];
        string aa = Request.QueryString["ReqNo"];
        int requisitionNo = int.Parse(aa);
        string item = (row.FindControl("ItemNO") as Label).Text;
        
        if(GridViewForDetail.Rows.Count == 0)
        {

        }
       
       else {
            //Delete row
            EmployeeController.DeleteForDetail(requisitionNo, item);

            //show back gridview
            reqhistory = new List<Detail>();
            List<RequisitionDetail> reqHistory = EmployeeController.ViewRequisitionDetail(requisitionNo);
            foreach (RequisitionDetail r in reqHistory)
            {
                Detail d = new Detail();

                d.itemNo = r.ItemNo;
                d.reqNo = r.ReqNo;
                d.quantity = r.Qty;
                d.description = r.StationeryItem.Description;
                reqhistory.Add(d);
            }
            GridViewForDetail.DataSource = reqhistory;
            GridViewForDetail.DataBind();
        }
    }

    //Edit for quantity
    protected void detailGrid_Edit(object sender, GridViewEditEventArgs e)
    {
        GridViewForDetail.EditIndex = e.NewEditIndex;
        BindGrid();
    }


    //Update New Quantity
    protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        GridViewRow row = GridViewForDetail.Rows[e.RowIndex];
        //string itemNo = GridViewForDetail.DataKeys[e.RowIndex].Values[0].ToString();
        string itemNo = (row.FindControl("ItemNO") as Label).Text;
        int qty = Int32.Parse((row.FindControl("TextBox2") as TextBox).Text);

        EmployeeController.UpdateItem(reqNo, itemNo, qty);
        GridViewForDetail.EditIndex = -1;
        BindGrid();
    }

    //Cancel Editing
    protected void OnRowCancelingEdit(object sender, EventArgs e)
    {
        GridViewForDetail.EditIndex = -1;
        BindGrid();
    }



    protected void GridViewForDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
}
