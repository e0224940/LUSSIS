using LUSSIS_Backend.controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LUSSIS_Backend
{
    public class RetrievalFormController
    {
        public static List<RetrievalDetail> GetRetrievalDetailsOf(int RetrievalNo)
        {
            LussisEntities context = new LussisEntities();
            List<RetrievalDetail> result = new List<RetrievalDetail>();
            Retrieval selectedRetrieval = context.Retrievals.Where(ret => ret.RetrievalNo == RetrievalNo).FirstOrDefault();

            if (selectedRetrieval != null)
            {
                result = context.RetrievalDetails.Where(retDetail => retDetail.RetrievalNo == selectedRetrieval.RetrievalNo).ToList();
            }

            return result;
        }

        public static List<Retrieval> GetAllRetrievals()
        {
            LussisEntities context = new LussisEntities();
            List<Retrieval> result = new List<Retrieval>();

            result = context.Retrievals.OrderBy(ret => ret.Date).ToList();

            return result;
        }

        public static bool IsRetrievalFormEditable(int RetrievalNo)
        {
            LussisEntities context = new LussisEntities();
            bool result = false;
            Retrieval selectedRetrieval = context.Retrievals.Where(ret => ret.RetrievalNo == RetrievalNo).FirstOrDefault();
            List<RetrievalDetail> details = null;

            if (selectedRetrieval != null)
            {
                details = context.RetrievalDetails.Where(retDetail => retDetail.RetrievalNo == selectedRetrieval.RetrievalNo).ToList();

                if(details != null)
                {
                    foreach(RetrievalDetail detail in details)
                    {
                        if(detail.Actual == 0)
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        public static bool IsThereEnoughItemsInStock(int qty, String ItemNo)
        {
            LussisEntities context = new LussisEntities();
            bool result = false;

            StationeryCatalogue item = context.StationeryCatalogues
                .Where(stationeryItem => stationeryItem.ItemNo.Equals(ItemNo))
                .FirstOrDefault();

            if(item != null)
            {
                result = item.CurrentQty >= qty;
            }


            return result;
        }

        public static int GetQuantityInStock(String ItemNo)
        {
            LussisEntities context = new LussisEntities();
            int result = 0;

            StationeryCatalogue item = context.StationeryCatalogues
                .Where(stationeryItem => stationeryItem.ItemNo.Equals(ItemNo))
                .FirstOrDefault();

            if (item != null)
            {
                result = item.CurrentQty.Value;
            }


            return result;
        }

        public static int CreateNewRetrieval()
        {
            return RetrievalController.CreateWeeklyRetrieval();
        }
    }
}
