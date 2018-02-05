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

    //public List<ReorderTrendView> getReport(List<string> itemList, String supplierSelect, DateTime startDate, DateTime endDate)
    //{
    //    var reportListItem = new List<ReorderTrendView>();

    //    //filter items to get list
    //    for(int i = 0; i < itemList.Count; i++)
    //    {
    //        var itemName = itemList[i];
    //        var item = context.ReorderTrendViews
    //.Where(x => x.DateReviewed >= startDate && x.DateReviewed <= endDate)
    //.Where(x => x.SupplierName == supplierSelect)
    //.Where(x => x.Description == itemName)
    //.ToList();

    //        //add items in list[i] into a bigger list to bind to gridview
    //        for(int x= 0; x <item.Count; x++)
    //        {
    //            reportListItem.Add(item[x]);
    //        }
    //    }

    //    //remove null items
    //for (int y = 0; y <reportListItem.Count; y++)
    //    {
    //        if(reportListItem[y] == null)
    //        {
    //            reportListItem.Remove(reportListItem[y]);
    //        }
    //    }
    //    reportListItem.OrderBy(x => x.DateReviewed);
    //    return reportListItem;
    //}

    //private void addItemSeriesToChart(Chart chart, string itemList, String supplierSelect, DateTime startDate, DateTime endDate)
    //{
    //    List<ReorderTrendView> checkForEmpty = new List<ReorderTrendView>();
    //    //start to filter items
    //    var listOfPO = context.ReorderTrendViews
    //        .Where(x => x.DateReviewed >= startDate && x.DateReviewed <= endDate)
    //        .Where(x => x.SupplierName == supplierSelect)
    //        .Where(x => x.Description == itemList)
    //        .OrderBy(x => x.DateReviewed)
    //        .ToList();

    //    for(int q =0; q < listOfPO.Count; q++)
    //    {
    //        checkForEmpty.Add(listOfPO[q]);
    //    }

    //    //another filter to group the first list by Month
    //    var listOfPO2 = listOfPO
    //        .GroupBy(x => x.DateReviewed.Value.Month)
    //        .Select(g => new ReorderTrendItem()
    //        {
    //            Key = g.Key,
    //            ItemNo = g.First().ItemNo,
    //            Qty = Convert.ToInt32(g.Sum(s => s.Qty)),
    //            Date = g.First().DateReviewed.Value.Month + "/" + g.First().DateReviewed.Value.Year,
    //            StoredDate = new DateTime(g.First().DateReviewed.Value.Year, g.First().DateReviewed.Value.Month, 1)
    //        })
    //        .ToList();

    //    // Fill in the missing Months starting from startDate till first date in list
    //    {
    //        var firstRecord = listOfPO2.FirstOrDefault();
    //        if (firstRecord != null)
    //        {
    //            ReorderTrendItem curr = new ReorderTrendItem()
    //            {
    //                Key = firstRecord.Key,
    //                ItemNo = firstRecord.ItemNo,
    //                Qty = 0,
    //                StoredDate = new DateTime(startDate.Year, startDate.Month, 1),
    //                Date = startDate.Month + "/" + startDate.Year,
    //            };

    //            int pos = 0;
    //            while (!curr.StoredDate.Equals(firstRecord.StoredDate))
    //            {
    //                listOfPO2.Insert(pos, curr);
    //                pos++;
    //                curr = new ReorderTrendItem()
    //                {
    //                    Key = firstRecord.Key,
    //                    ItemNo = firstRecord.ItemNo,
    //                    Qty = 0,
    //                    StoredDate = curr.StoredDate.AddMonths(1),
    //                    Date = curr.StoredDate.AddMonths(1).Month + "/" + curr.StoredDate.AddMonths(1).Year,
    //                };
    //            }
    //        }
    //    }

    //    // Fill in the missing Months in between
    //    for (int i = 0; i < listOfPO2.Count - 1; i++)
    //    {
    //        var curr = listOfPO2.ElementAtOrDefault(i);
    //        var next = listOfPO2.ElementAtOrDefault(i + 1);

    //        if (curr != null && next != null)
    //        {
    //            while (!curr.StoredDate.AddMonths(1).Equals(next.StoredDate))
    //            {
    //                listOfPO2.Insert(i + 1, new ReorderTrendItem()
    //                {
    //                    Key = curr.Key,
    //                    ItemNo = curr.ItemNo,
    //                    Qty = 0,
    //                    StoredDate = curr.StoredDate.AddMonths(1),
    //                    Date = curr.StoredDate.AddMonths(1).Month + "/" + curr.StoredDate.AddMonths(1).Year,
    //                });

    //                i = i + 1;
    //                curr = listOfPO2.ElementAtOrDefault(i);
    //            }
    //        }
    //    }

    //    // Fill in the missing Months starting from last in list till endDate
    //    {
    //        var lastRecord = listOfPO2.LastOrDefault();
    //        if (lastRecord != null)
    //        {
    //            ReorderTrendItem curr = new ReorderTrendItem()
    //            {
    //                Key = lastRecord.Key,
    //                ItemNo = lastRecord.ItemNo,
    //                Qty = 0,
    //                StoredDate = lastRecord.StoredDate.AddMonths(1),
    //                Date = lastRecord.StoredDate.AddMonths(1).Month + "/" + lastRecord.StoredDate.AddMonths(1).Year,
    //            };

    //            DateTime finalDate = new DateTime(endDate.Year, endDate.Month, 1);
    //            while (DateTime.Compare(curr.StoredDate, finalDate) < 0) // !curr.StoredDate.Equals(finalDate))
    //            {
    //                listOfPO2.Add(curr);
    //                curr = new ReorderTrendItem()
    //                {
    //                    Key = lastRecord.Key,
    //                    ItemNo = lastRecord.ItemNo,
    //                    Qty = 0,
    //                    StoredDate = curr.StoredDate.AddMonths(1),
    //                    Date = curr.StoredDate.AddMonths(1).Month + "/" + curr.StoredDate.AddMonths(1).Year,
    //                };
    //            }
    //        }
    //    }

    //    //create series for chart + add points
    //    Chart1.Width = 1000;
    //    Chart1.Height = 450;

    //    string seriesName = itemList;
    //    Series newSeries = new Series(itemList);
    //    Chart1.Series.Add(seriesName);

    //    //series parameters
    //    Chart1.Series[seriesName].ChartType = SeriesChartType.Column;
    //    Chart1.Series[seriesName].IsValueShownAsLabel = true;
    //    chart.Series[seriesName].LabelFormat = "##0;(); ";

    //    //Legend parameters
    //    Chart1.Legends.Add(new Legend());
    //    Chart1.Legends["Legend1"].Docking = Docking.Bottom;

    //    //chartarea parameters
    //    Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
    //    Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
    //    Chart1.ChartAreas["ChartArea1"].AxisX.Title = "Date";
    //    Chart1.ChartAreas["ChartArea1"].AxisY.Title = "Quantity";
    //    Chart1.ChartAreas["ChartArea1"].AxisX.IntervalType = DateTimeIntervalType.Auto;
    //    Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;

    //    for (int y = 0; y < listOfPO2.Count(); y++)
    //    {
    //        Chart1.Series[seriesName].Points.AddXY(listOfPO2.ElementAt(y).Date, listOfPO2.ElementAt(y).Qty);
    //    }

    //    //remove legend for 0 datapoints
    //    foreach (Series s in Chart1.Series)
    //    {
    //        bool isAllValuesZero = true;
    //        foreach (DataPoint p in s.Points)
    //        {
    //            for (int i = 0; i < p.YValues.Length; i++)
    //            {
    //                if (p.YValues[i] == 0)
    //                    p.Label = "";
    //                else
    //                    isAllValuesZero = false;
    //            }
    //        }
    //        if (isAllValuesZero) // If all the values are zero for a series then hiding the legend..
    //            s.IsVisibleInLegend = false;
    //    }
    //    if (checkForEmpty.Count == 0)
    //    {
    //        Label4.Text = "There is no available data.";
    //    }
    //    else
    //    {
    //        Label4.Text = "";
    //    }
    //}
}
