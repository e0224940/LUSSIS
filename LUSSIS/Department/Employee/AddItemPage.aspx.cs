using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LUSSIS_Backend;


public partial class Department_Employee_AddItemPage : System.Web.UI.Page
{
    static List<StationeryCatalogue> itemList;
    static List<RaisedItem> cartitem;
    DateTime dateIssue;

    protected void Page_Load(object sender, EventArgs e)
    {
        int empNo = Profile.EmpNo;
        date.Text = DateTime.Today.ToString("dd MMM yyyy");
        if (!IsPostBack)
        {
            string empName = EmployeeController.GetName(empNo);
            NameLB.Text = empName;
            StationeryGridView.Visible = true;
            itemList = new List<StationeryCatalogue>();
            cartitem = new List<RaisedItem>();
            Session["session"] = cartitem;
            StationeryGridView.DataSource = EmployeeController.ViewItem();
            StationeryGridView.DataBind();
        }
    }

    //Select the selected item
    protected void StationeryGridView_SelectedIndexChanged(object sender, EventArgs e)
    { 
        GridViewRow row = StationeryGridView.SelectedRow;
        RaisedItem cart = new RaisedItem();
        cart.ItemNo = row.Cells[0].Text;
        cart.description = row.Cells[1].Text;
        if ((row.Cells[2].FindControl("Quantity") as TextBox).Text.ToString() == "" || Int32.Parse((row.Cells[2].FindControl("Quantity") as TextBox).Text) <= 0)
        {
            Msg.Text = "The input number has to be greater than 0.";
        }
        else
        {
            Session["Test"] = row;
            Msg.Text = "";
            cart.quantity = (row.Cells[2].FindControl("Quantity") as TextBox).Text;
            cartitem.Add(cart);
            Session["session"] = cartitem;
            Cart.DataSource = cartitem;
            Cart.DataBind();         
        }
    }

    //Create the requisition
    protected void Confirm_Click(object sender, EventArgs e)
    {
        cartitem = (List<RaisedItem>)Session["session"];
        int isissueBy = Profile.EmpNo;
        dateIssue = DateTime.Today;
        string status = "Pending";
        List<RequisitionDetail> detailList = new List<RequisitionDetail>();
        foreach (RaisedItem k in cartitem)
        {
            RequisitionDetail rd = new RequisitionDetail();
            rd.ItemNo = k.ItemNo;
            rd.Qty = Convert.ToInt32(k.quantity);
            detailList.Add(rd);
        }
        
        EmployeeController.RaisedRequisition(isissueBy, dateIssue, status, detailList);
        Session["success"] = dateIssue;
       
        Response.Redirect("RaisedRequisitionSuccessPage.aspx");
        Session.Remove("session");
        Msg.Text = "Success!";
    }

    //Search the item 
    protected void Search_Click(object sender, EventArgs e)
    {
        GridViewRow point = (GridViewRow)Session["Test"];
       
            string val = SearchItemText.Text;
            point.Visible = false;
            StationeryGridView.DataSource = EmployeeController.SearchDes(val);
        point.Visible = false;
        StationeryGridView.DataBind();
             
    }

    //Cancel the crate requisition
    protected void Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

    //Delete the selected item
    protected void Cart_GridViewDelete(object sender, GridViewDeleteEventArgs e)
    {
        string ItemNo = Cart.DataKeys[e.RowIndex].Values[0].ToString();
        RaisedItem selected = cartitem.Where(item => item.ItemNo == ItemNo).FirstOrDefault();
        cartitem.Remove(selected);
        Session["session"] = cartitem;
        Cart.DataSource = cartitem;
        Cart.DataBind();
        
    }


    //Cancel search button
    protected void CancelSearch_Click(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            StationeryGridView.DataSource = EmployeeController.ViewItem();
            StationeryGridView.DataBind();
            Session["session"] = cartitem;
        }
    }
}
