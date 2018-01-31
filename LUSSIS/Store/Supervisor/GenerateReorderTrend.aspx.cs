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
        {         //populate ItemsInCatalogueListBox
            var populateItemsInCatalogueListBox = context.StationeryCatalogues.Select(x => x.Description).ToList();
            ItemsInCatalogueListBox.DataSource = populateItemsInCatalogueListBox;
            ItemsInCatalogueListBox.DataBind();

            //populate SupplierDDL
            List<String> supList = new List<String>();
            supList = context.Suppliers.Select(x => x.SupplierName).ToList();
            SupplierDDL.DataSource = supList;
            SupplierDDL.DataBind();
        }
}

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
        }
    }

    //button to send item from Listbox2 to Listbox1
    protected void toLB1_Click(object sender, EventArgs e)
    {
        if (SelectedItemsListBox.SelectedItem != null)
        {
            ListItem item = SelectedItemsListBox.SelectedItem;
            //List<ListItem> abc = new List<ListItem>();
            //for(int i = 0; i < SelectedItemsListBox.Items.Count; i++)
            //{
            //    abc.Add(ItemsInCatalogueListBox.Items[i]);
            //}
            //var key = context.StationeryCatalogues.Where(x => x.Description == Convert.ToString(item)).Select(x => x.ItemNo);
            //abc.Add(item);
            //abc.OrderBy(item.Value);
            //ItemsInCatalogueListBox.DataSource = abc;
            //ItemsInCatalogueListBox.DataBind();
            ItemsInCatalogueListBox.Items.Add(item);
            SelectedItemsListBox.Items.Remove(item);

            ItemsInCatalogueListBox.SelectedIndex = -1;
            SelectedItemsListBox.SelectedIndex = -1;
        }
    }

    protected void generateReportBut_Click(object sender, EventArgs e)
    {
        if(FromDate.Text !="" && EndDate.Text !="")
        {
            //get selected supplier
            var supplierSelect = SupplierDDL.SelectedValue;
            //get listbox items count
            var itemSelectCount = SelectedItemsListBox.Items.Count;
            //get FromDate
            var startDate = DateTime.Parse(FromDate.Text.ToString());
            //get EndDate
            var endDate = DateTime.Parse(EndDate.Text.ToString());
            //get selected Item
            var itemSelected = SelectedItemsListBox.SelectedValue;

            //loop through item in selected list box, add to itemList
            var itemList = new List<String>();
            for (int i = 0; i < itemSelectCount; i++)
            {
                var item = SelectedItemsListBox.Items[i].ToString();
                itemList.Add(item);
            }

            for (int p = 0; p < itemList.Count; p++)
            {
                addItemSeriesToChart(Chart1, itemList[p], supplierSelect, startDate, endDate);
            }
        }

    }

    private void addItemSeriesToChart(Chart chart, string itemList, String supplierSelect, DateTime startDate, DateTime endDate)
    {
        //start to filter items
        var listOfPO = context.ReorderTrendViews
            .Where(x => x.DateReviewed >= startDate && x.DateReviewed <= endDate)
            .Where(x => x.SupplierName == supplierSelect)
            .Where(x => x.Description == itemList)
            .OrderBy(x => x.DateReviewed)
            .ToList();

        //another filter to group the first list by Month
        var listOfPO2 = listOfPO
            .GroupBy(x => x.DateReviewed.Value.Month)
            .Select(g => new ReorderTrendItem()
            {
                Key = g.Key,
                ItemNo = g.First().ItemNo,
                Qty = Convert.ToInt32(g.Sum(s => s.Qty)),
                Date = g.First().DateReviewed.Value.Month + "/" + g.First().DateReviewed.Value.Year,
                StoredDate = new DateTime(g.First().DateReviewed.Value.Year, g.First().DateReviewed.Value.Month, 1)
            })
            .ToList();

        // Fill in the missing Months starting from startDate till first date in list
        {
            var firstRecord = listOfPO2.First();
            ReorderTrendItem curr = new ReorderTrendItem()
            {
                Key = firstRecord.Key,
                ItemNo = firstRecord.ItemNo,
                Qty = 0,
                StoredDate = new DateTime(startDate.Year, startDate.Month, 1),
                Date = startDate.Month + "/" + startDate.Year,
            };

            int pos = 0;
            while (!curr.StoredDate.Equals(firstRecord.StoredDate))
            {
                listOfPO2.Insert(pos, curr);
                pos++;
                curr = new ReorderTrendItem()
                {
                    Key = firstRecord.Key,
                    ItemNo = firstRecord.ItemNo,
                    Qty = 0,
                    StoredDate = curr.StoredDate.AddMonths(1),
                    Date = curr.StoredDate.AddMonths(1).Month + "/" + curr.StoredDate.AddMonths(1).Year,
                };
            }
        }

        // Fill in the missing Months in between
        for (int i = 0; i < listOfPO2.Count - 1; i++)
        {
            var curr = listOfPO2.ElementAtOrDefault(i);
            var next = listOfPO2.ElementAtOrDefault(i + 1);

            if (curr != null && next != null)
            {
                while (!curr.StoredDate.AddMonths(1).Equals(next.StoredDate))
                {
                    listOfPO2.Insert(i + 1, new ReorderTrendItem()
                    {
                        Key = curr.Key,
                        ItemNo = curr.ItemNo,
                        Qty = 0,
                        StoredDate = curr.StoredDate.AddMonths(1),
                        Date = curr.StoredDate.AddMonths(1).Month + "/" + curr.StoredDate.AddMonths(1).Year,
                    });

                    i = i + 1;
                    curr = listOfPO2.ElementAtOrDefault(i);
                }
            }
        }

        // Fill in the missing Months starting from last in list till endDate
        {
            var lastRecord = listOfPO2.Last();
            ReorderTrendItem curr = new ReorderTrendItem()
            {
                Key = lastRecord.Key,
                ItemNo = lastRecord.ItemNo,
                Qty = 0,
                StoredDate = lastRecord.StoredDate.AddMonths(1),
                Date = lastRecord.StoredDate.AddMonths(1).Month + "/" + lastRecord.StoredDate.AddMonths(1).Year,
            };

            DateTime finalDate = new DateTime(endDate.Year, endDate.Month, 1);           
            while (DateTime.Compare(curr.StoredDate, finalDate) < 0) // !curr.StoredDate.Equals(finalDate))
            {
                listOfPO2.Add(curr);
                curr = new ReorderTrendItem()
                {
                    Key = lastRecord.Key,
                    ItemNo = lastRecord.ItemNo,
                    Qty = 0,
                    StoredDate = curr.StoredDate.AddMonths(1),
                    Date = curr.StoredDate.AddMonths(1).Month + "/" + curr.StoredDate.AddMonths(1).Year,
                };
            }

        }

        //create series for chart + add points
        Chart1.Width = 1000;
        Chart1.Height = 300;

        string seriesName = itemList;
        Series newSeries = new Series(itemList);
        Chart1.Series.Add(seriesName);
        //series parameters
        Chart1.Series[seriesName].ChartType = SeriesChartType.Line;
        Chart1.Series[seriesName].IsValueShownAsLabel = true;
        //Legend parameters
        Chart1.Legends.Add(new Legend());
        Chart1.Legends["Legend1"].Docking = Docking.Bottom;
        //chartarea parameters
        Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
        Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
        Chart1.ChartAreas["ChartArea1"].AxisX.Title = "Date";
        Chart1.ChartAreas["ChartArea1"].AxisY.Title = "Quantity";
        Chart1.Series[seriesName].BorderWidth = 5;

        for (int y = 0; y < listOfPO2.Count(); y++)
        {
            Chart1.Series[seriesName].Points.AddXY(listOfPO2.ElementAt(y).Date, listOfPO2.ElementAt(y).Qty);
        }

        //to TEST if the code / logic is working
        GridView1.DataSource = listOfPO;
        GridView1.DataBind();
        GridView2.DataSource = listOfPO2;
        GridView2.DataBind();
    }


}

public class ReorderTrendItem
{
    public int Key { get; set; }
    public string ItemNo { get; set; }
    public int Qty { get; set; }
    public DateTime StoredDate { get; set; }
    public string Date { get; set; }
}
