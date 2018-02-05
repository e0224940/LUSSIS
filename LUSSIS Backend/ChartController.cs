using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.DataVisualization.Charting;

namespace LUSSIS_Backend
{
    public class ChartController
    {
        //===============================For Requisition Trend ===============================================
        //================================Get Report ======================================================
        public static List<RequisitionTrendView> getReportRequisitionTrend(List<String> deptList, String itemSelect, DateTime startDate, DateTime endDate)
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
        }

        //=====================================get Chart==================================================
        public static void addItemSeriesToChartRequisitionTrend(Chart Chart2, string deptList, String itemSelect, DateTime startDate, DateTime endDate)
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

            for (int q = 0; q < listOfPO.Count; q++)
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
                if (firstRecord != null)
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
                if (lastRecord != null)
                {
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
            Chart2.ChartAreas["ChartArea2"].AxisX.IntervalType = DateTimeIntervalType.Auto;
            Chart2.ChartAreas["ChartArea2"].AxisX.Interval = 1;

            for (int y = 0; y < listOfPO2.Count(); y++)
            {
                Chart2.Series[seriesName].Points.AddXY(listOfPO2.ElementAt(y).Date, listOfPO2.ElementAt(y).Qty);
            }
            //remove legend for 0 datapoints
            foreach (Series s in Chart2.Series)
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

        }
        //==============================End of for Requitision Trend============================================

        //===============================For Reorder Trend====================================================
        //================================Get Category Report=================================================
        public static List<ReorderTrendView> getReportCatReorderTrend(List<string> catList, String supplierSelect, DateTime startDate, DateTime endDate)
        {
            LussisEntities context = new LussisEntities();
            var reportListItemCat = new List<ReorderTrendView>();

            //filter items to get list
            for (int i = 0; i < catList.Count; i++)
            {
                var catName = catList[i];
                var cat = context.ReorderTrendViews
        .Where(x => x.DateReviewed >= startDate && x.DateReviewed <= endDate)
        .Where(x => x.SupplierName == supplierSelect)
        .Where(x => x.Category == catName)
        .ToList();

                //add items in list[i] into a bigger list to bind to gridview
                for (int x = 0; x < cat.Count; x++)
                {
                    reportListItemCat.Add(cat[x]);
                }
            }

            //remove null items
            for (int y = 0; y < reportListItemCat.Count; y++)
            {
                if (reportListItemCat[y] == null)
                {
                    reportListItemCat.Remove(reportListItemCat[y]);
                }
            }
            reportListItemCat.OrderBy(x => x.DateReviewed);
            return reportListItemCat;
        }

        //====================================get Category Chart===========================================
        public static void addItemSeriesToChartCat(Chart Chart1, string catList, String supplierSelect, DateTime startDate, DateTime endDate)
        {
            LussisEntities context = new LussisEntities();
            List<ReorderTrendView> checkForEmpty = new List<ReorderTrendView>();
            //start to filter items
            var listOfPO = context.ReorderTrendViews
                .Where(x => x.DateReviewed >= startDate && x.DateReviewed <= endDate)
                .Where(x => x.SupplierName == supplierSelect)
                .Where(x => x.Category == catList)
                .OrderBy(x => x.DateReviewed)
                .ToList();

            for (int q = 0; q < listOfPO.Count; q++)
            {
                checkForEmpty.Add(listOfPO[q]);
            }

            var listOfPO2 = listOfPO
        .GroupBy(x => x.DateReviewed.Value.Month)
        .Select(g => new ReorderTrendItem()
        {
            Key = g.Key,
            ItemNo = g.First().ItemNo,
            Category = g.First().Category,
            Qty = Convert.ToInt32(g.Sum(s => s.Qty)),
            Date = g.First().DateReviewed.Value.Month + "/" + g.First().DateReviewed.Value.Year,
            StoredDate = new DateTime(g.First().DateReviewed.Value.Year, g.First().DateReviewed.Value.Month, 1)
        })
        .ToList();

            // Fill in the missing Months starting from startDate till first date in list
            {
                var firstRecord = listOfPO2.FirstOrDefault();
                if (firstRecord != null)
                {
                    ReorderTrendItem curr = new ReorderTrendItem()
                    {
                        Key = firstRecord.Key,
                        ItemNo = firstRecord.ItemNo,
                        Qty = 0,
                        StoredDate = new DateTime(startDate.Year, startDate.Month, 1),
                        Date = startDate.Month + "/" + startDate.Year,
                        Category = firstRecord.Category,
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
                            Category = firstRecord.Category,
                        };
                    }
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
                            Category = curr.Category,
                        });

                        i = i + 1;
                        curr = listOfPO2.ElementAtOrDefault(i);
                    }
                }
            }

            // Fill in the missing Months starting from last in list till endDate
            {
                var lastRecord = listOfPO2.LastOrDefault();
                if (lastRecord != null)
                {
                    ReorderTrendItem curr = new ReorderTrendItem()
                    {
                        Key = lastRecord.Key,
                        ItemNo = lastRecord.ItemNo,
                        Qty = 0,
                        StoredDate = lastRecord.StoredDate.AddMonths(1),
                        Date = lastRecord.StoredDate.AddMonths(1).Month + "/" + lastRecord.StoredDate.AddMonths(1).Year,
                        Category = lastRecord.Category,
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
                            Category = curr.Category,
                        };
                    }
                }
            }

            //create series for chart + add points
            Chart1.Width = 1000;
            Chart1.Height = 450;

            string seriesName = catList;
            Series newSeries = new Series(catList);
            Chart1.Series.Add(seriesName);

            //series parameters
            Chart1.Series[seriesName].ChartType = SeriesChartType.Column;
            Chart1.Series[seriesName].IsValueShownAsLabel = true;
            Chart1.Series[seriesName].LabelFormat = "##0;(); ";

            //Legend parameters
            Chart1.Legends.Add(new Legend());
            Chart1.Legends["Legend1"].Docking = Docking.Bottom;

            //chartarea parameters
            Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
            Chart1.ChartAreas["ChartArea1"].AxisX.Title = "Date";
            Chart1.ChartAreas["ChartArea1"].AxisY.Title = "Quantity";
            Chart1.ChartAreas["ChartArea1"].AxisX.IntervalType = DateTimeIntervalType.Auto;
            Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;

            for (int y = 0; y < listOfPO2.Count(); y++)
            {
                Chart1.Series[seriesName].Points.AddXY(listOfPO2.ElementAt(y).Date, listOfPO2.ElementAt(y).Qty);
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
        }

        //===========================End of Category==================================================
        //============================Start of Item==================================================
        //===========================get Item Report===================================================

        public static List<ReorderTrendView> getReport(List<string> itemList, String supplierSelect, DateTime startDate, DateTime endDate)
        {
            LussisEntities context = new LussisEntities();
            var reportListItem = new List<ReorderTrendView>();

            //filter items to get list
            for (int i = 0; i < itemList.Count; i++)
            {
                var itemName = itemList[i];
                var item = context.ReorderTrendViews
        .Where(x => x.DateReviewed >= startDate && x.DateReviewed <= endDate)
        .Where(x => x.SupplierName == supplierSelect)
        .Where(x => x.Description == itemName)
        .ToList();

                //add items in list[i] into a bigger list to bind to gridview
                for (int x = 0; x < item.Count; x++)
                {
                    reportListItem.Add(item[x]);
                }
            }

            //remove null items
            for (int y = 0; y < reportListItem.Count; y++)
            {
                if (reportListItem[y] == null)
                {
                    reportListItem.Remove(reportListItem[y]);
                }
            }
            reportListItem.OrderBy(x => x.DateReviewed);
            return reportListItem;
        }

        //===========================get Chart item =====================================================

        public static void addItemSeriesToChart(Chart Chart1, string itemList, String supplierSelect, DateTime startDate, DateTime endDate)
        {
            LussisEntities context = new LussisEntities();
            List<ReorderTrendView> checkForEmpty = new List<ReorderTrendView>();
            //start to filter items
            var listOfPO = context.ReorderTrendViews
                .Where(x => x.DateReviewed >= startDate && x.DateReviewed <= endDate)
                .Where(x => x.SupplierName == supplierSelect)
                .Where(x => x.Description == itemList)
                .OrderBy(x => x.DateReviewed)
                .ToList();

            for (int q = 0; q < listOfPO.Count; q++)
            {
                checkForEmpty.Add(listOfPO[q]);
            }

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
                var firstRecord = listOfPO2.FirstOrDefault();
                if (firstRecord != null)
                {
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
                var lastRecord = listOfPO2.LastOrDefault();
                if (lastRecord != null)
                {
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
            }

            //create series for chart + add points
            Chart1.Width = 1000;
            Chart1.Height = 450;

            string seriesName = itemList;
            Series newSeries = new Series(itemList);
            Chart1.Series.Add(seriesName);

            //series parameters
            Chart1.Series[seriesName].ChartType = SeriesChartType.Column;
            Chart1.Series[seriesName].IsValueShownAsLabel = true;
            Chart1.Series[seriesName].LabelFormat = "##0;(); ";

            //Legend parameters
            Chart1.Legends.Add(new Legend());
            Chart1.Legends["Legend1"].Docking = Docking.Bottom;

            //chartarea parameters
            Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
            Chart1.ChartAreas["ChartArea1"].AxisX.Title = "Date";
            Chart1.ChartAreas["ChartArea1"].AxisY.Title = "Quantity";
            Chart1.ChartAreas["ChartArea1"].AxisX.IntervalType = DateTimeIntervalType.Auto;
            Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;

            for (int y = 0; y < listOfPO2.Count(); y++)
            {
                Chart1.Series[seriesName].Points.AddXY(listOfPO2.ElementAt(y).Date, listOfPO2.ElementAt(y).Qty);
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
        }

    }

    public class ReorderTrendItem
    {
        public int Key { get; set; }
        public string ItemNo { get; set; }
        public int Qty { get; set; }
        public DateTime StoredDate { get; set; }
        public string Date { get; set; }
        public string Category { get; set; }
    }



    public class RequisitionTrendItem
    {
        public int Key { get; set; }
        public string ItemNo { get; set; }
        public int Qty { get; set; }
        public DateTime StoredDate { get; set; }
        public string Date { get; set; }
        public string DeptName { get; set; }
        public DateTime DateReviewed { get; set; }
    }

}
