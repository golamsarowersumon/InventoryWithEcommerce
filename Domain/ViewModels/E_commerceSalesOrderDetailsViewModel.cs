using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    class E_commerceSalesOrderDetailsViewModel
    {

        public int E_SalerOrderDetailsId { get; set; }

        public bool IsShipping { get; set; }
        public decimal E_ShippingCharge { get; set; }
        public int CountryId { get; set; }
        public int DistrictId { get; set; }
        public Nullable<int> ItemId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<int> StoreId { get; set; }
        public Nullable<int> SubStoreId { get; set; }
        public Nullable<int> SubSubStoreId { get; set; }
        public Nullable<int> SubSubSubStoreId { get; set; }
        public Nullable<int> SubSubSubSubStoreId { get; set; }
        public decimal E_OrderQuantity { get; set; }
        public decimal E_SO_Price { get; set; }
        public decimal E_PO_Price { get; set; }
        public decimal E_Item_Profit { get; set; }
        public int E_SalerOrderMasterId { get; set; }


    }
}
