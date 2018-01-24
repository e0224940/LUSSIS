using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LUSSIS_Backend
{
    public class ViewReorderReportController
    {

        public static List<PurchaseOrder> showReorderReport()
        {
            var minusOneMth = 0;
            var currentYear = DateTime.Now.Year;
            var minusOneYear = 0;
            int thirtySixMonthsRec = 36;
            DateTime thirtySixMonths = DateTime.Now.AddMonths(-thirtySixMonthsRec);
            List<PurchaseOrder> dtList = new List<PurchaseOrder>();

            for (int i = 1; i < 37; i++)
            {
                PurchaseOrder poObj = new PurchaseOrder();

                //format date to correct month
                DateTime month = DateTime.Now.AddMonths(-minusOneMth);
                var monthName = month.ToString("MMM");
                DateTime year = DateTime.Now.AddYears(-minusOneYear);
                var yearName = year.ToString("yyyy");

                poObj.DateReviewed = month;
                dtList.Add(poObj);

                minusOneMth++;
                if (monthName == "Jan")
                {
                    minusOneYear++;
                }
            }
            return dtList;
        }

        //public static List<PURCHASEORDERVIEW> showReorderReportDetails(int SNO)
        //{
        //    LussisEntities context = new LussisEntities();
        //    var currentYear = DateTime.Now.Year;
        //    var currentMonth = DateTime.Now.Month;
        //    int monthsToDeduct = SNO;
        //    DateTime selectedDate = DateTime.Now.AddMonths(-monthsToDeduct);
        //    int getSelectedMonth = selectedDate.Month;
        //    int getSelectedYear = selectedDate.Year;

        //    var result = context.PURCHASEORDERVIEWs.Where(p => p.DateReviewed.Value.Month == getSelectedMonth && p.DateReviewed.Value.Year == getSelectedYear).ToList();
        //    return result;
        //}

    }
}
