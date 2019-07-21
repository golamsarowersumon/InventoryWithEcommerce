using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class DamagedItemViewModel
    {
        
        public int DamagedItemId { get; set; }
        public Nullable<int> ItemId { get; set; }
        public string DamagedItemType { get; set; }
        public decimal DamageQuantity { get; set; }
        public decimal AvailableQty { get; set; }
        public int Inv_Details_ID { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DamageDate { get; set; }
        public decimal PO_Price { get; set; }
        public String Item_Unique_Number { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> DateOfPurchase { get; set; }
        public Nullable<int> ProductId { get; set; }
        public string StoreName { get; set; }
        public Nullable<int> StoreId { get; set; }
        public Nullable<int> SubStoreId { get; set; }
        public Nullable<int> SubSubStoreId { get; set; }
        public Nullable<int> SubSubSubStoreId { get; set; }
        public Nullable<int> SubSubSubSubStoreId { get; set; }
        public string ItemName { get; set; }
        public string ProductName { get; set; }

    }
}
