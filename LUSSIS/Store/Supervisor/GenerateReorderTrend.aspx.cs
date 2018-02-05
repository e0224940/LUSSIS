using LUSSIS_Backend;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

public partial class Store_Supervisor_GenerateReorderTrend : System.Web.UI.Page
{
    LussisEntities context = new LussisEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {   
            //populate ItemsInCatalogueListBox
            var populateItemsInCatalogueListBox = context.StationeryCatalogues.Select(x => x.Description).ToList();
            ItemsInCatalogueListBox.DataSource = populateItemsInCatalogueListBox;
            ItemsInCatalogueListBox.DataBind();

            //populate SupplierDDL
            List<String> supList = new List<String>();
            supList = context.Suppliers.Select(x => x.SupplierName).ToList();
            SupplierDDL.DataSource = supList;
            SupplierDDL.DataBind();

             //populate CategoryListBox
                var populateCatInCatalogueListBox = context.StationeryCatalogues.Select(x => x.Category).Distinct().ToList();
                CategoryListBox.DataSource = populateCatInCatalogueListBox;
                CategoryListBox.DataBind();

                //allow user to choose the 1st day of the month even if 36month ago the cut
                // off was eg: 4th
                var minDate = DateTime.Now.AddMonths(-36);
            var firstDayOfMonth = new DateTime(minDate.Year, minDate.Month, 1);
            //set the textbox date 
            FromDate.Attributes["min"] = firstDayOfMonth.ToString("yyyy-MM-dd");
            FromDate.Attributes["max"] = DateTime.Now.ToString("yyyy-MM-dd");
            EndDate.Attributes["min"] = firstDayOfMonth.ToString("yyyy-MM-dd");
            EndDate.Attributes["max"] = DateTime.Now.ToString("yyyy-MM-dd");
            
        }
}

    //ITEMS listbox buttons function

    //button to send item from Listbox1 to Listbox2
protected void toLB2_Click(object sender, EventArgs e)
    {
        if(ItemsInCatalogueListBox.SelectedItem != null)
        {
            ListItem item = ItemsInCatalogueListBox.SelectedItem;
            SelectedItemsListBox.Items.Add(item);
            ItemsInCatalogueListBox.Items.Remove(item);

            ItemsInCatalogueListBox.SelectedIndex = -1;
            SelectedItemsListBox.SelectedIndex = -1;

            selectedCategoryListBox.Items.Clear();
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
    }

    //button to send item from Listbox2 to Listbox1
    protected void toLB1_Click(object sender, EventArgs e)
    {
        if (SelectedItemsListBox.SelectedItem != null)
        {
            ListItem item = SelectedItemsListBox.SelectedItem;
            ItemsInCatalogueListBox.Items.Add(item);
            SelectedItemsListBox.Items.Remove(item);

            ItemsInCatalogueListBox.SelectedIndex = -1;
            SelectedItemsListBox.SelectedIndex = -1;

            selectedCategoryListBox.Items.Clear();
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
    }

    //ITEMS listbos ALL button function
    //delete all itms from LB1 and add to LB2
    protected void addAll1_Click(object sender, EventArgs e)
    {
    }

    //delete all items from LB2 and repopulate LB1
    protected void deleteAll1_Click(object sender, EventArgs e)
    {
    }

    //CATEGORY listboxes button functions
    protected void toLB2C_Click(object sender, EventArgs e)
    {
        if (CategoryListBox.SelectedItem != null)
        {
            ListItem item = CategoryListBox.SelectedItem;
            selectedCategoryListBox.Items.Add(item);
            CategoryListBox.Items.Remove(item);

            CategoryListBox.SelectedIndex = -1;
            selectedCategoryListBox.SelectedIndex = -1;

            SelectedItemsListBox.Items.Clear();
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
    }

    protected void toLB1C_Click(object sender, EventArgs e)
    {
        if (selectedCategoryListBox.SelectedItem != null)
        {
            ListItem item = selectedCategoryListBox.SelectedItem;
            CategoryListBox.Items.Add(item);
            selectedCategoryListBox.Items.Remove(item);

            selectedCategoryListBox.SelectedIndex = -1;
            CategoryListBox.SelectedIndex = -1;

            SelectedItemsListBox.Items.Clear();
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
    }


    //main function generate report
    protected void generateReportBut_Click(object sender, EventArgs e)
    {
        if(FromDate.Text !="" && EndDate.Text !="")
        {
            //get selected supplier
            var supplierSelect = SupplierDDL.SelectedValue;
            //get listbox items count
            var itemSelectCount = SelectedItemsListBox.Items.Count;
            // get catlistbox count
            var catSelectCount = selectedCategoryListBox.Items.Count;
            //get FromDate
            var startDate = DateTime.Parse(FromDate.Text.ToString());
            //get EndDate
            var endDate = DateTime.Parse(EndDate.Text.ToString());
            //get selected Item
            var itemSelected = SelectedItemsListBox.SelectedValue;

            if(SelectedItemsListBox.Items.Count != 0)
            {
                //loop through item in selected list box, add to itemList
                var itemList = new List<String>();
                for (int i = 0; i < itemSelectCount; i++)
                {
                    var item = SelectedItemsListBox.Items[i].ToString();
                    itemList.Add(item);
                }

                for (int p = 0; p < itemList.Count; p++)
                {
                    ChartController.addItemSeriesToChart(Chart1, itemList[p], supplierSelect, startDate, endDate);
                }

                //generate report
                var reportList = ChartController.getReport(itemList, supplierSelect, startDate, endDate).OrderBy(x => x.DateReviewed);
                //generate the report
                GridView1.DataSource = reportList;
                GridView1.DataBind();
            } else
            {
                //loop through item in selected category box, add to catList
                var catList = new List<String>();
                for (int i = 0; i < catSelectCount; i++)
                {
                    var item = selectedCategoryListBox.Items[i].ToString();
                    catList.Add(item);
                }

                for (int p = 0; p < catList.Count; p++)
                {
                    ChartController.addItemSeriesToChartCat(Chart1, catList[p], supplierSelect, startDate, endDate);
                }

                //generate report
                var reportList = ChartController.getReportCatReorderTrend(catList, supplierSelect, startDate, endDate).OrderBy(x => x.DateReviewed);
                //generate the report
                GridView1.DataSource = reportList;
                GridView1.DataBind();
            }
        }
    }
}
