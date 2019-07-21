using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class TransferViewModel
    {
        public int TransferId { get; set; }

        public Nullable<int> InventoryId { get; set; }
        public int TempTransferInfoId { get; set; }
        [Display(Name ="ToStore Name")]
        [Required]
        public Nullable<int> ToStoreId { get; set; }
        public string ToStoreName { get; set; }
        public Nullable<int> ToSubStoreId { get; set; }
        public string ToSubStoreName { get; set; }
        public Nullable<int> ToSubSubStoreId { get; set; }
        public string ToSubSubStoreName { get; set; }


        public Nullable<int> ToSubSubSubStoreId { get; set; }
        public string ToSubSubSubStoreName { get; set; }

        public Nullable<int> ToSubSubSubSubStoreId { get; set; }
        public string ToSubSubSubSubStoreName { get; set; }

        [Display(Name = "From Store Name")]
        [Required]
        public int FromStoreId { get; set; }

        [Display(Name = "From Store Name")]
        public string FromStoreName { get; set; }

        [Required(ErrorMessage ="Please Select Item Name")]
        public int ItemId { get; set; }
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        public int TransferDetailsId { get; set; }

        

        [Display(Name = "Condition Of Item")]
        public Nullable<int> ConditionOfItemId { get; set; }
        public string ConditionOfItemName{ get; set; }

      

        [Display(Name = "Item Quantity")]
        [Required(ErrorMessage = "Please Enter Quantity to Transfer")]
        public decimal TransactionQuantity { get; set; }

      

        public Nullable<int> UnitId { get; set; }
        public String UnitName { get; set; }

     
         public Nullable<int> AvailableQuantity { get; set; }
         public string Transferby { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime TransferDate { get; set; }

        public bool IsChecked { get; set; }
         public string IsNull { get; set; }
         public int PendingCount { get; set; }
        [Required(ErrorMessage = "Please Enter Transfer Order Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime TransferOrderDate { get; set; }
        [Display(Name = "Receive")]
        [Required(ErrorMessage = "Please Select Receive")]
        public string Recieve { get; set; }
        public string RecieveBy { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime RecieveDate { get; set; }
        public Nullable<int> TransferTypeId { get; set; }
        public string TransferTypeName { get; set; }
        public string TransferForStore { get; set; }
        public string TransferForProduction { get; set; }
        
        public int SID { get; set; }
        public string Name { get; set; }



        public int Inv_HD_ID { get; set; }


        public int Id { get; set; }
        
        public int Inv_Details_ID { get; set; }
     
        public int TransferDetailId { get; set; }
        public Nullable<int> SupplierCompanyId { get; set; }
        public Nullable<int> SubContractCompanyId { get; set; }
        public Nullable<int> StoreId { get; set; }
        public Nullable<int> SubStoreId { get; set; }
        public Nullable<int> SubSubStoreId { get; set; }
        public Nullable<int> SubSubSubStoreId { get; set; }
        public Nullable<int> SubSubSubSubStoreId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<DateTime> DateOfExpired { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<DateTime> DateOfNextMaintainance { get; set; }
        public String Barcode { get; set; }
        public String Item_Unique_Number { get; set; }
        public String Chesis_Number { get; set; }
        public String Engine_Number { get; set; }
      
        public Nullable<int> ProductId { get; set; }
        public Nullable<int> CountryId { get; set; }
        public Nullable<int> Principle_id { get; set; }
      
        public Nullable<int> WarrantyId { get; set; }
        public decimal TransactionQty { get; set; }
        public decimal AvailableQty { get; set; }
        public decimal PO_Price { get; set; }
        public decimal PO_SubTotal { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> SubCategoryId { get; set; }
        public Nullable<int> SubSubCategoryId { get; set; }
        public Nullable<int> SubSubSubCategoryId { get; set; }
        public Nullable<int> SubSubSubSubCategoryId { get; set; }
        public Nullable<int> BrandId { get; set; }
        public Nullable<int> ModelId { get; set; }
      
        public Nullable<int> MethodId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateOfActualTransfer { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateOfTransferOrder { get; set; }


        public int TransferOrderId { get; set; }
        public string TransferCriteria { get; set; }

        public string ItemCriteria { get; set; }
        public string ProductCriteria { get; set; }
        public string StoreCriteria { get; set; }
        public string TransTypeCriteria { get; set; }
        public string ConditionCriteria { get; set; }
        public string DateCriteria { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? FromDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ToDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DateOfReceive { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CreateDate { get; set; }
        public Nullable<decimal> ReceiveItemQuantity { get; set; }
        public Nullable<decimal> PendingItemQuantity { get; set; }

        public string CreateBy { get; set; }

        public List<TransferViewModel> TransferViewModelList { get; set; }
    }
}
