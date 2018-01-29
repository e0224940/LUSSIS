using LUSSIS_Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Department_Employee_RequisitionDetailsView : System.Web.UI.Page
{
    //static List<Requisition> requsition;
    static List<Detail> reqhistory;
    //static List<RequisitionDetail> reqDetail;


    protected void Page_Load(object sender, EventArgs e)
    {
        
        int empNo = Profile.EmpNo;
        
        if (!IsPostBack)
        {
           
            string empName = EmployeeController.GetName(empNo);
            empNameshow.Text = empName;

            string ss = Request.QueryString["ReqNo"];
            int reqNo = int.Parse(ss);
            ReqId.Text = ss; 
            reqhistory = new List<Detail>();
            List<RequisitionDetail> reqHistory = EmployeeController.ViewRequisitionDetail(reqNo);
            foreach(RequisitionDetail r in reqHistory)
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
    protected void cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewRequisitionHistory.aspx");
    }

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
        //else
        //{
        //    EmployeeController.DeleteReqHistory(requisitionNo);
            
        //}
    }

    protected void detailGrid_Edit(object sender, GridViewEditEventArgs e)
    {
        GridViewForDetail.EditIndex = e.NewEditIndex;
        GridViewForDetail.DataBind();
    }
}
