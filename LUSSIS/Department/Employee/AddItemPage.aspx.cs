using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LUSSIS_Backend;
using System.Data.SqlClient;

public partial class Department_Employee_AddItemPage : System.Web.UI.Page
{
    static List<StationeryCatalogue> itemList;
    static List<RaisedItem> cartitem;
    static List<RaisedItem> searchitem;
    static List<SelectedItem> item;
    DateTime dateIssue;
    //string quantity;
    protected void Page_Load(object sender, EventArgs e)
    {
        int empNo = Profile.EmpNo;
        date.Text = DateTime.Today.ToString("d");
        if (!IsPostBack)
        {

            string empName = EmployeeController.GetName(empNo);
            NameLB.Text = empName;
            StationeryGridView.Visible = true;
            itemList = new List<StationeryCatalogue>();
            cartitem = new List<RaisedItem>();
            Session["raisedItem"] = cartitem;
            StationeryGridView.DataSource = EmployeeController.ViewItem();
            StationeryGridView.DataBind();
        }
    }

    protected void Confirm_Click(object sender, EventArgs e)
    {
        LussisEntities entity = new LussisEntities();
        List<RaisedItem> raisedItem = (List<RaisedItem>)Session["raisedItem"];

        foreach (RaisedItem selectItem in raisedItem)
        {
            entity.SaveChanges();
        }

        int isissueBy = Profile.EmpNo;
        dateIssue = DateTime.Now.Date;
        string status = "Not Approved yet.";

        EmployeeController.RaisedRequisition(isissueBy, dateIssue, status);

        Response.Redirect("Default.aspx");
    }

    

    protected void StationeryGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = StationeryGridView.SelectedRow;
        RaisedItem cart = new RaisedItem();
        cart.ItemNo = row.Cells[0].Text;
        cart.description = row.Cells[1].Text;
        if (Int32.Parse((row.Cells[2].FindControl("Quantity") as TextBox).Text) <= 0)
        {

        }
        else
        {
            cart.quantity = (row.Cells[2].FindControl("Quantity") as TextBox).Text;
        }

        cartitem.Add(cart);
        Cart.DataSource = cartitem;
        Cart.DataBind();
    }

    protected void Search_Click(object sender, EventArgs e)
    {
            string val = SearchItemText.Text;
            StationeryGridView.DataSource = EmployeeController.SearchDes(val);
            StationeryGridView.DataBind();
    }

    protected void Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

    protected void Cart_GridViewDelete(object sender, GridViewDeleteEventArgs e)
    {
        string ItemNo = Cart.DataKeys[e.RowIndex].Values[0].ToString();
        RaisedItem selected = cartitem.Where(item => item.ItemNo == ItemNo).FirstOrDefault();
        cartitem.Remove(selected);
        Cart.DataSource = cartitem;
        Cart.DataBind();
    }

    protected void CancelSearch_Click(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
          
            itemList = new List<StationeryCatalogue>();
            cartitem = new List<RaisedItem>();
            StationeryGridView.DataSource = EmployeeController.ViewItem();
            StationeryGridView.DataBind();
        }
    }
}
