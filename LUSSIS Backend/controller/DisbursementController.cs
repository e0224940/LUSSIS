using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LUSSIS_Backend.model;

namespace LUSSIS_Backend.controller
{
    public static class DisbursementController
    {
        public static List<Disbursement> GetPendingDisbursements()
        {
            LussisEntities context = new LussisEntities();
            return context.Disbursements.Where(x => x.Status.Equals("Pending")).ToList<Disbursement>();
        }

        public static Disbursement GetPendingDisbursementOfDepartment(String deptCode)
        {
            LussisEntities context = new LussisEntities();
            return context.Disbursements.Where(x => x.Status.Equals("Pending") && x.DeptCode.Equals(deptCode)).SingleOrDefault();
        }

        public static List<DisbursementDetail> GetDisbursementDetails(int dNo)
        {
            LussisEntities context = new LussisEntities();
            return context.DisbursementDetails.Where(x => x.DisbursementNo == dNo).ToList();
        }

        public static Disbursement GetDisbursement(int dNo)
        {
            LussisEntities context = new LussisEntities();
            return context.Disbursements.Where(x => x.DisbursementNo == dNo).FirstOrDefault();
        }

        public static void UpdateReceivedQty(int dNo, string itemNo, int qty)
        {
            LussisEntities context = new LussisEntities();
            DisbursementDetail dDetail = context.DisbursementDetails.Where(x => x.DisbursementNo == dNo && x.ItemNo.Equals(itemNo)).FirstOrDefault();
            dDetail.Received = qty;
            context.SaveChanges();
        }

        public static bool CompleteDisbursement(int dNo, decimal? pin)
        {
            bool result = false;
            LussisEntities context = new LussisEntities();
            Disbursement d = context.Disbursements.Where(x => x.DisbursementNo == dNo).FirstOrDefault();
            List<DisbursementDetail> dDetails = d.DisbursementDetails.ToList();

            // If Pin is Valid
            if (d.Pin == pin)
            {
                // Update Disbursement Status
                d.Status = "Completed";

                // Create Backlogs
                for (int i = 0; i < dDetails.Count; i++)
                {
                    if (dDetails[i].Needed - dDetails[i].Received > 0)
                    {
                        BackLog backlog = new BackLog();
                        backlog.DeptCode = dDetails[i].Disbursement.DeptCode;
                        backlog.ItemNo = dDetails[i].ItemNo;
                        backlog.BackLogQty = dDetails[i].Needed - dDetails[i].Received;
                        context.BackLogs.Add(backlog);
                    }
                }

                context.SaveChanges();

                result = true;

                // Send Email
            }
            else
            {
                throw new Exception("Invalid Pin");
                result = false;
            }

            return result;
        }
    }
}
