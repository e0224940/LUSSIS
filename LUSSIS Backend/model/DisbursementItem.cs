using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LUSSIS_Backend.model
{
    [Serializable]
    public class DisbursementItem
    {
        // Properties
        public string ItemNo { get; set; }
        public string ItemDescription { get; set; }
        public int? Qty { get; set; }
        public int? Delivered { get; set; }
    }
}
