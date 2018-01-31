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
        {   //populate DeptListBox
            LussisEntities context = new LussisEntities();
            var populateDeptListBox = context.Departments.Select(x => x.DeptName).ToList();
            DeptListBox.DataSource = populateDeptListBox;
            DeptListBox.DataBind();

            //populate ItemsDDL
            List<String> itemsList = new List<String>();
            itemsList = context.StationeryCatalogues.Select(x => x.Description).ToList();
            ItemsDDL.DataSource = itemsList;
            ItemsDDL.DataBind();
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
        }
    }

    protected void rqToLB1_Click(object sender, EventArgs e)
    {
        if (SelectedDeptListBox.SelectedItem != null)
        {
            ListItem item = SelectedDeptListBox.SelectedItem;
            DeptListBox.Items.Add(item);
            SelectedDeptListBox.Items.Remove(item);
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
        }
    }

    private void addItemSeriesToChart(Chart Chart1, string deptList, String itemSelect, DateTime startDate, DateTime endDate)
    {
        LussisEntities context = new LussisEntities();
        //for (int p = 0; p < deptList.Count; p++)
        //{
        //    var abc = Convert.ToString(SelectedDeptListBox.Items[p]);

            //start to filter items
            var listOfPO = context.RequisitionTrendViews
                .Where(x => x.DateReviewed >= startDate && x.DateReviewed <= endDate)
                .Where(x => x.Description == itemSelect)
                .Where(x => x.DeptName == deptList)
                .OrderBy(x => x.DateReviewed)
                .ToList();

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

            //create series for chart + add points
            Chart2.Width = 1000;
            Chart2.Height = 300;

            string seriesName = deptList;
            Chart2.Series.Add(seriesName);
            //series parameters
            Chart2.Series[seriesName].ChartType = SeriesChartType.Line;
            Chart2.Series[seriesName].IsValueShownAsLabel = true;
            //Legend parameters
            Chart2.Legends.Add(new Legend());
            Chart2.Legends["Legend1"].Docking = Docking.Bottom;
            //chartarea parameters
            Chart2.ChartAreas["ChartArea2"].AxisX.MajorGrid.Enabled = false;
            Chart2.ChartAreas["ChartArea2"].AxisY.MajorGrid.Enabled = false;
            Chart2.ChartAreas["ChartArea2"].AxisX.Title = "Date";
            Chart2.ChartAreas["ChartArea2"].AxisY.Title = "Quantity";
            Chart2.Series[seriesName].BorderWidth = 5;

            for (int y = 0; y < listOfPO2.Count(); y++)
            {
                Chart2.Series[seriesName].Points.AddXY(listOfPO2.ElementAt(y).Date, listOfPO2.ElementAt(y).Qty);
            }

            //to TEST if the code/logic is working
            GridView3.DataSource = listOfPO;
            GridView3.DataBind();
            GridView4.DataSource = listOfPO2;
            GridView4.DataBind();
        }
    }

//public class ReorderTrendItem
//{
//    private int Key { get; set; }
//    private string ItemNo { get; set; }
//    private int Qty { get; set; }
//    private DateTime StoredDate { get; set; }
//    private string Date { get; set; }

public class RequisitionTrendItem
{
    public int Key { get; set; }
    public string ItemNo { get; set; }
    public int Qty { get; set; }
    public DateTime StoredDate { get; set; }
    public string Date { get; set; }
}
