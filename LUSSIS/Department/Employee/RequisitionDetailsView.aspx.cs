using LUSSIS_Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Department_Employee_RequisitionDetailsView : System.Web.UI.Page
{
    static List<Requisition> requsition;
    static List<Detail> reqhistory;
    static List<RequisitionDetail> reqDetail;


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

            //reqNo = (int)Session["sessionvalue"];
            //ReqId.Text = reqNo.ToString();
            //requsition = new List<Requisition>();
            //reqDetail = new List<RequisitionDetail>();
            reqhistory = new List<Detail>();

            //if(Session["sessionID"] != null) {

            
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

           // }

        }
    }
    protected void cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewRequisitionHistory.aspx");
    }

    protected void detailGrid_Delete(object sender, GridViewDeleteEventArgs e)
    {
        int requisitionNo = 42;
        string item = GridViewForDetail.DataKeys[e.RowIndex].ToString();
        //int reqId = Convert.ToInt32(detailGrid.DataKeys[e.RowIndex].Values[0]);
        //Session["sessionID"] = reqId;
        EmployeeController.DeleteForDetail(requisitionNo, item);
        GridViewForDetail.DataSource = EmployeeController.ViewRequisitionDetail(requisitionNo);
        GridViewForDetail.DataBind();

    }

    protected void detailGrid_Edit(object sender, GridViewEditEventArgs e)
    {
        
    }
}
