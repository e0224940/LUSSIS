using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LUSSIS_Backend;

public partial class Store_Manager_StoreManagerCreateSupplier : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string code = TextBox1.Text;
        string name = TextBox2.Text;
        string gst = TextBox3.Text;
        string contactName = TextBox4.Text;
        int phoneNo = Convert.ToInt32(TextBox5.Text);
        int faxNo = Convert.ToInt32(TextBox6.Text);
        string address = TextBox7.Text;

        Supplier supplierObj = new Supplier();
        supplierObj.SupplierCode = code;
        supplierObj.SupplierName = name;
        supplierObj.GstNo = gst;
        supplierObj.ContactName = contactName;
        supplierObj.PhoneNo = phoneNo;
        supplierObj.FaxNo = faxNo;
        supplierObj.Address = address;

        LUSSIS_Backend.LussisEntities databaseObject = new LUSSIS_Backend.LussisEntities();
        databaseObject.Suppliers.Add(supplierObj);
        databaseObject.SaveChanges();

        //Response.Write("<script type=\"text/javascript\">alert('New supplier added successfully!');</script>");
        Session["SupplierProcessed"] = code;
        Response.Redirect("StoreManagerUpdate.aspx");

    }
}