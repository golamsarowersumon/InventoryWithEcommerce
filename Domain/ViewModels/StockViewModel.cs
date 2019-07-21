using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
   public class StockViewModel
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string SubStoreName { get; set; }
        public string SubSubStoreName { get; set; }
        public string SubSubSubStoreName { get; set; }
        public string SubSubSubSubStoreName { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string UnitName { get; set; }
        public decimal PO_QTD { get; set; }
    }
}
