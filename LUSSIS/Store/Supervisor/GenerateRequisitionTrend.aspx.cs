using LUSSIS_Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

public partial class Store_Supervisor_GenerateRequisitionTrend : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {  
            //populate DeptListBox
            LussisEntities context = new LussisEntities();
            var populateDeptListBox = context.Departments.Select(x => x.DeptName).ToList();
            DeptListBox.DataSource = populateDeptListBox;
            DeptListBox.DataBind();

            //populate ItemsDDL
            List<String> itemsList = new List<String>();
            itemsList = context.StationeryCatalogues.Select(x => x.Description).ToList();
            ItemsDDL.DataSource = itemsList;
            ItemsDDL.DataBind();

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

    //button to send item over to Listbox2 from Listbox
    protected void rqToLB2_Click(object sender, EventArgs e)
    {
        if (DeptListBox.SelectedItem != null)
        {
            ListItem item = DeptListBox.SelectedItem;
            SelectedDeptListBox.Items.Add(item);
            DeptListBox.Items.Remove(item);

            DeptListBox.SelectedIndex = -1;
            SelectedDeptListBox.SelectedIndex = -1;

            GridView3.DataSource = null;
            GridView3.DataBind();
        }
    }

    protected void rqToLB1_Click(object sender, EventArgs e)
    {
        if (SelectedDeptListBox.SelectedItem != null)
        {
            ListItem item = SelectedDeptListBox.SelectedItem;
            DeptListBox.Items.Add(item);
            SelectedDeptListBox.Items.Remove(item);

            DeptListBox.SelectedIndex = -1;
            SelectedDeptListBox.SelectedIndex = -1;

            GridView3.DataSource = null;
            GridView3.DataBind();
        }
    }

    protected void generateReportBut_Click(object sender, EventArgs e)
    {
        if (FromDate.Text != "" && EndDate.Text != "")
        {
            //get selected item
            var itemSelect = ItemsDDL.SelectedValue;
            //get listbox dept count
            var deptSelectCount = SelectedDeptListBox.Items.Count;
            //get FromDate
            var startDate = DateTime.Parse(FromDate.Text.ToString());
            //get EndDate
            var endDate = DateTime.Parse(EndDate.Text.ToString());

            //loop through dept in selected dept list box, add to deptList
            var deptList = new List<String>();
            for (int i = 0; i < deptSelectCount; i++)
            {
                var item = SelectedDeptListBox.Items[i].ToString();
                deptList.Add(item);
            }
            for (int p = 0; p < deptList.Count; p++)
            {
                addItemSeriesToChart(Chart2, deptList[p], itemSelect, startDate, endDate);
            }

            var getReportData = getReport(deptList, itemSelect, startDate, endDate);
            GridView3.DataSource = getReportData;
            GridView3.DataBind();
        }
    }

    private List<RequisitionTrendView> getReport(List<String> deptList, String itemSelect, DateTime startDate, DateTime endDate)
    {
        LussisEntities context = new LussisEntities();
        var reportListItemDept = new List<RequisitionTrendView>();

        for (var i = 0; i < deptList.Count(); i++)
        {
            var deptName = deptList[i];
            var dept = context.RequisitionTrendViews
            .Where(x => x.DateReviewed >= startDate && x.DateReviewed <= endDate)
            .Where(x => x.Description == itemSelect)
            .Where(x => x.DeptName == deptName)
            .OrderBy(x => x.DateReviewed)
            .ToList();

            //add items in list[i] into a bigger list to bind to gridview
            for (int x = 0; x < dept.Count; x++)
            {
                reportListItemDept.Add(dept[x]);
            }
        }

        //remove null items
        for (int y = 0; y < reportListItemDept.Count; y++)
        {
            if (reportListItemDept[y] == null)
            {
                reportListItemDept.Remove(reportListItemDept[y]);
            }
        }
        return reportListItemDept;
        //start to filter items
    }

    private void addItemSeriesToChart(Chart Chart1, string deptList, String itemSelect, DateTime startDate, DateTime endDate)
    {
        LussisEntities context = new LussisEntities();
        List<RequisitionTrendView> checkIfEmpty = new List<RequisitionTrendView>();

            //start to filter items
            var listOfPO = context.RequisitionTrendViews
                .Where(x => x.DateReviewed >= startDate && x.DateReviewed <= endDate)
                .Where(x => x.Description == itemSelect)
                .Where(x => x.DeptName == deptList)
                .OrderBy(x => x.DateReviewed)
                .ToList();

        for(int q = 0; q < listOfPO.Count; q++)
        {
            checkIfEmpty.Add(listOfPO[q]);
        }

            //another filter to group the first list by Month
            var listOfPO2 = listOfPO
                .GroupBy(x => x.DateReviewed.Value.Month)
                .Select(g => new RequisitionTrendItem()
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
            var firstRecord = listOfPO2.FirstOrDefault();
            if(firstRecord != null)
            {
                RequisitionTrendItem curr = new RequisitionTrendItem()
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
                    curr = new RequisitionTrendItem()
                    {
                        Key = firstRecord.Key,
                        ItemNo = firstRecord.ItemNo,
                        Qty = 0,
                        StoredDate = curr.StoredDate.AddMonths(1),
                        Date = curr.StoredDate.AddMonths(1).Month + "/" + curr.StoredDate.AddMonths(1).Year,
                    };
                }
            }
        }

        // Fill in the missing Months
        for (int i = 0; i < listOfPO2.Count - 1; i++)
            {
                var curr = listOfPO2.ElementAtOrDefault(i);
                var next = listOfPO2.ElementAtOrDefault(i + 1);

                if (curr != null && next != null)
                {
                    while (!curr.StoredDate.AddMonths(1).Equals(next.StoredDate))
                    {
                        listOfPO2.Insert(i + 1, new RequisitionTrendItem()
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
            var lastRecord = listOfPO2.LastOrDefault();
            if(lastRecord != null){
                RequisitionTrendItem curr = new RequisitionTrendItem()
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
                    curr = new RequisitionTrendItem()
                    {
                        Key = lastRecord.Key,
                        ItemNo = lastRecord.ItemNo,
                        Qty = 0,
                        StoredDate = curr.StoredDate.AddMonths(1),
                        Date = curr.StoredDate.AddMonths(1).Month + "/" + curr.StoredDate.AddMonths(1).Year,
                    };
                }
            }
        }

        //create series for chart + add points
        Chart2.Width = 1000;
            Chart2.Height = 450;

            string seriesName = deptList;
            Chart2.Series.Add(seriesName);
            //series parameters
            Chart2.Series[seriesName].ChartType = SeriesChartType.Column;
            Chart2.Series[seriesName].IsValueShownAsLabel = true;
            Chart2.Series[seriesName].LabelFormat = "##0;(); ";
            //Legend parameters
            Chart2.Legends.Add(new Legend());
            Chart2.Legends["Legend1"].Docking = Docking.Bottom;
            //chartarea parameters
            Chart2.ChartAreas["ChartArea2"].AxisX.MajorGrid.Enabled = false;
            Chart2.ChartAreas["ChartArea2"].AxisY.MajorGrid.Enabled = false;
            Chart2.ChartAreas["ChartArea2"].AxisX.Title = "Date";
            Chart2.ChartAreas["ChartArea2"].AxisY.Title = "Quantity";
            Chart1.ChartAreas["ChartArea2"].AxisX.IntervalType = DateTimeIntervalType.Auto;
            Chart1.ChartAreas["ChartArea2"].AxisX.Interval = 1;

            for (int y = 0; y < listOfPO2.Count(); y++)
            {
                Chart2.Series[seriesName].Points.AddXY(listOfPO2.ElementAt(y).Date, listOfPO2.ElementAt(y).Qty);
            }
        //remove legend for 0 datapoints
        foreach (Series s in Chart1.Series)
        {
            bool isAllValuesZero = true;
            foreach (DataPoint p in s.Points)
            {
                for (int i = 0; i < p.YValues.Length; i++)
                {
                    if (p.YValues[i] == 0)
                        p.Label = "";
                    else
                        isAllValuesZero = false;
                }
            }
            if (isAllValuesZero) // If all the values are zero for a series then hiding the legend..
                s.IsVisibleInLegend = false;
        }

        if (checkIfEmpty.Count == 0)
        {
            noDataLabel.Text = "There is no available data.";
        }
        else
        {
            noDataLabel.Text = "";
        }
    }
    }

public class RequisitionTrendItem
{
    public int Key { get; set; }
    public string ItemNo { get; set; }
    public int Qty { get; set; }
    public DateTime StoredDate { get; set; }
    public string Date { get; set; }
    public string DeptName { get; set; }
    public DateTime DateReviewed { get; set;}
}
