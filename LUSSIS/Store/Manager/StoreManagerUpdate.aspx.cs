using LUSSIS_Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Store_Manager_StoreManagerUpdate : System.Web.UI.Page
{
    LussisEntities context = new LussisEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridView1.DataSource = context.Suppliers.ToList();
            GridView1.DataBind();
        }


    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.DataSource = context.Suppliers.ToList();
        GridView1.EditIndex = e.NewEditIndex;
        GridView1.DataBind();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string code = GridView1.DataKeys[e.RowIndex].Values["SupplierCode"].ToString();
        Supplier deleteSupplier = context.Suppliers.Where(x => x.SupplierCode == code).First();
        context.Suppliers.Remove(deleteSupplier);
        context.SaveChanges();

        GridView1.DataSource = context.Suppliers.ToList();
        GridView1.DataBind();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = GridView1.Rows[e.RowIndex];
        string code = GridView1.DataKeys[e.RowIndex].Values[0].ToString();

        Supplier editSupplier = context.Suppliers.Where(x => x.SupplierCode == code).First();
        editSupplier.SupplierCode = code;
        editSupplier.SupplierName = e.NewValues["SupplierName"].ToString();
        editSupplier.ContactName = e.NewValues["ContactName"].ToString();
        editSupplier.PhoneNo = Convert.ToInt32(e.NewValues["PhoneNo"].ToString());
        editSupplier.FaxNo = Convert.ToInt32(e.NewValues["FaxNo"].ToString());
        editSupplier.Address = e.NewValues["Address"].ToString();
        editSupplier.GstNo = e.NewValues["GstNo"].ToString();
        context.SaveChanges();
        GridView1.DataSource = context.Suppliers.ToList();
        GridView1.DataBind();
        Response.Redirect("StoreManagerUpdate.aspx");

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("StoreManagerCreateSupplier.aspx");
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        Response.Redirect("StoreManagerUpdate.aspx");
    }
}