using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LUSSIS_Backend
{
    public class ViewRequisitionDetailsController
    {
        public static List<RequisitionDetail> GetRequisitionDetailsOf(int requisitionNumber)
        {
            List<RequisitionDetail> result = new List<RequisitionDetail>();
            LussisEntities context = new LussisEntities();
            result = context.RequisitionDetails.Where(req => req.ReqNo == requisitionNumber).ToList();
            return result;
        }
    }
}
