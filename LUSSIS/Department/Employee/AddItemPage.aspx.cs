using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LUSSIS_Backend;

public partial class Department_Employee_AddItemPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        StationeryGridView.DataSource = EmployeeController.ViewItem();
        StationeryGridView.DataBind();
    }

    protected void Confirm_Click(object sender, EventArgs e)
    {

    }

    protected void StationeryGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string Des = StationeryGridView.SelectedRow.Cells[0].Text;
        //int q = int.Parse(StationeryGridView.SelectedRow.Cells[1].Text);
        //Cart.

    }
}