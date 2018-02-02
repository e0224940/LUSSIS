using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LUSSIS_Backend;
using LUSSIS_Backend.controller;

public partial class Store_Clerk_StockDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["StockNo"] == null)
            {
                Response.Redirect("StockList.aspx");
            }
            else
            {
                // Set Stock
                string stockNo = (string)Session["StockNo"];
                StationeryCatalogue stock = StockController.GetStock(stockNo);

                // Load Stock
                ItemNoLabel.Text = stock.ItemNo;
                ItemDescriptionLabel.Text = stock.Description;
                BinNoLabel.Text = stock.Bin;
                UomLabel.Text = stock.Uom;
                FirstSupplierLabel.Text = stock.Supplier1;
                SecondSupplierLabel.Text = stock.Supplier2;
                ThirdSupplierLabel.Text = stock.Supplier3;
                QtyLabel.Text = stock.CurrentQty.ToString();

                // Set TxnList
                StockDetailsGridView.DataSource = stock.StockTxnDetails.Select(txn => new
                {
                    StockTxnNo = txn.StockTxnNo,
                    Date = String.Format("{0:dd/MMM/yyyy}", txn.Date),
                    DeptSupplier = txn.Remarks,
                    QtyRemarks = txn.AdjustQty < 0 ? ("-" + txn.AdjustQty) : txn.AdjustQty.ToString(),
                    Balance = txn.RecordedQty
                });

                // Load TxnList
                StockDetailsGridView.DataBind();

                // Fill Dropdown
                SupplierDropDownList.Items.Add(new ListItem(stock.Supplier1, stock.Supplier1));
                SupplierDropDownList.Items.Add(new ListItem(stock.Supplier2, stock.Supplier2));
                SupplierDropDownList.Items.Add(new ListItem(stock.Supplier3, stock.Supplier3));
            }
        }
    }

    protected void AddQuantityButton_Click(object sender, EventArgs e)
    {
        string stockNo = (string)Session["StockNo"];
        StockController.IncreaseStockFromSupplier(stockNo, Convert.ToInt32(QuantityTextBox.Text), SupplierDropDownList.SelectedValue);
        Response.Redirect(Request.RawUrl);
    }
}