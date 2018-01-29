using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LUSSIS_Backend.model
{
    public class OrderItem
    {
        // ATTRIBUTES
        StationeryCatalogue stock;
        List<string> supplierCodeList;
        List<int?> orderQtyList;

        // CONSTRUCTOR

        public OrderItem(StationeryCatalogue stock)
        {
            this.stock = stock;
            supplierCodeList = new List<string> { stock.Supplier.SupplierCode, stock.Supplier4.SupplierCode, stock.Supplier5.SupplierCode };
            orderQtyList = new List<int?> { 0, 0, 0 };
        }

        // PROPERTIES

        public StationeryCatalogue Stock { get { return stock; } set { stock = value; } }

        public string ItemNo { get { return stock.ItemNo; } }

        public string Description { get { return stock.Description; } }

        public int? QtyOnHand { get { return stock.CurrentQty; } }

        public int? ReorderLevel { get { return stock.ReorderLevel; } }

        public int? ReorderQty { get { return stock.ReorderQty; } }

        public List<string> SupplierCodeList { get { return supplierCodeList; } }

        public string Supplier1 { get { return stock.Supplier1; } }

        public string Supplier2 { get { return stock.Supplier2; } }

        public string Supplier3 { get { return stock.Supplier3; } }

        public List<int?> OrderQtyList { get { return orderQtyList; } }

        public int? Qty1 { get { return orderQtyList[0]; } set { orderQtyList[0] = value; } }

        public int? Qty2 { get { return orderQtyList[1]; } set { orderQtyList[1] = value; } }

        public int? Qty3 { get { return orderQtyList[2]; } set { orderQtyList[2] = value; } }
    }
}
