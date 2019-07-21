using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class RightOfAccesViewModel
    {
      
        public int TransferId { get; set; }
        public int TransferDetailsId { get; set; }
        public int TransferOrderId { get; set; }
        public int RightOfAccesId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public decimal ItemQuantity { get; set; }
        public int SubStoreId { get; set; }
        public string SubStoreName { get; set; }
        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }
        public int SubSubStoreId { get; set; }
        public string SubSubStoreName { get; set; }
        public int SID { get; set; }
        public string Name { get; set; }
        public int SubSubSubStoreId { get; set; }
        public string SubSubSubSubName { get; set; }

       
        public int SubSubSubSubStoreId { get; set; }
        public string SubSubSubSubStoreName { get; set; }

        [DataType(DataType.MultilineText)]
        public string Reason { get; set; }
        public string Reference { get; set; }
        public string Acknowledgement { get; set; }

        public bool IsChecked { get; set; }
        public string IsNull { get; set; }
        public Nullable<decimal>  ReceiveItemQuantity { get; set; }
        public Nullable<decimal> PendingItemQuantity { get; set; }

        public int TempId { get; set; }
        public int Inv_HD_ID { get; set; }
        public int FromStoreId { get; set; }
        public string FromStoreName { get; set; }
        public Nullable<int> ToStoreId { get; set; }
        public string  ToStoreName { get; set; }
        public decimal PO_Price { get; set; }
        public Nullable<int> UnitId { get; set; }
        public string UnitName { get; set; }
        public Nullable<int> ProductId { get; set; }
        public string ProductName { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }

     public List<RightOfAccesViewModel> rightOfAccesViewModelslist { get; set; }

    }
}
