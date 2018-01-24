using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LUSSIS_Backend.model
{
    public class OrderItem
    {
        // CONSTRUCTOR

        public OrderItem(StationeryCatalogue stock)
        {
            Stock = stock;
            SupplierCodeList = new List<string> { stock.Supplier.SupplierCode, stock.Supplier4.SupplierCode, stock.Supplier5.SupplierCode };
            OrderQtyList = new List<int?> { 0, 0, 0 };

            ItemNo = Stock.ItemNo;
            Description = Stock.Description;
            QtyOnHand = Stock.CurrentQty;
            ReorderLevel = Stock.ReorderLevel;
            ReorderQty = Stock.ReorderQty;
            Supplier1 = Stock.Supplier.SupplierName;
            Supplier2 = Stock.Supplier4.SupplierName;
            Supplier3 = Stock.Supplier5.SupplierName;
        }

        // PROPERTIES

        public StationeryCatalogue Stock { get; }
        public List<string> SupplierCodeList { get; }
        public List<int?> OrderQtyList { get; }
        public string ItemNo { get; }
        public string Description { get; }
        public int? QtyOnHand { get; }
        public int? ReorderLevel { get; }
        public int? ReorderQty { get; }
        public string Supplier1 { get; }
        public string Supplier2 { get; }
        public string Supplier3 { get; }
        public int? Qty1 { get { return OrderQtyList[0]; } set { OrderQtyList[0] = value; } }
        public int? Qty2 { get { return OrderQtyList[1]; } set { OrderQtyList[1] = value; } }
        public int? Qty3 { get { return OrderQtyList[2]; } set { OrderQtyList[2] = value; } }
    }
}
