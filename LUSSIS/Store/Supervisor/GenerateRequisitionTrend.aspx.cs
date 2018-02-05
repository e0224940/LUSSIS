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
            //get string if empty text 
            string emptyOrNot;

            //loop through dept in selected dept list box, add to deptList
            var deptList = new List<String>();
            for (int i = 0; i < deptSelectCount; i++)
            {
                var item = SelectedDeptListBox.Items[i].ToString();
                deptList.Add(item);
            }
            for (int p = 0; p < deptList.Count; p++)
            {
                ChartController.addItemSeriesToChartRequisitionTrend(Chart2, deptList[p], itemSelect, startDate, endDate);
            }

            var getReportData = ChartController.getReportRequisitionTrend(deptList, itemSelect, startDate, endDate);
            GridView3.DataSource = getReportData;
            GridView3.DataBind();
        }
    }
}
