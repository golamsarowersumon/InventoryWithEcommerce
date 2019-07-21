using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
  public class ReportViewModel
    {

        public int PO_HD_ID { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfPurchase { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfReceive { get; set; }
        public decimal PO_GRAND_TOTAL { get; set; }
        public int ProcurementTypeId { get; set; }
        public int SupplierCompanyId { get; set; }
        public Nullable<int> SubContractCompanyId { get; set; }
        public Nullable<int> StoreId { get; set; }
        public Nullable<int> SubStoreId { get; set; }
        public Nullable<int> SubSubStoreId { get; set; }
        public Nullable<int> SubSubSubStoreId { get; set; }
        public Nullable<int> SubSubSubSubStoreId { get; set; }

        public Nullable<int> PurchasedBy_ProcureBy { get; set; }
        public Nullable<int> CreateBy { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreateDate { get; set; }
        public Nullable<int> UpdateBy { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime UpdateDate { get; set; }

      
        public string Product_Image { get; set; }
        public string Product_Image1 { get; set; }
        public string Product_Image2 { get; set; }
        public string ErrorMeassage { get; set; }



        public int PO_Details_ID { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfExpired { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfNextMaintainance { get; set; }
        public String Barcode { get; set; }
        public String Item_Unique_Number { get; set; }
        public String Chesis_Number { get; set; }
        public String Engine_Number { get; set; }
        public int ItemId { get; set; }
        public Nullable<int> CountryId { get; set; }
        public Nullable<int> Principle_id { get; set; }
        public Nullable<int> ConditionOfItemId { get; set; }
        public Nullable<int> WarrantyId { get; set; }
        public decimal PO_QTD { get; set; }
        public decimal PO_Price { get; set; }
        public decimal SO_Price { get; set; }
        public decimal PO_SubTotal { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> SubCategoryId { get; set; }
        public Nullable<int> SubSubCategoryId { get; set; }
        public Nullable<int> SubSubSubCategoryId { get; set; }
        public Nullable<int> SubSubSubSubCategoryId { get; set; }
        public Nullable<int> BrandId { get; set; }
        public Nullable<int> ModelId { get; set; }
        public Nullable<int> UnitId { get; set; }
        public int MethodId { get; set; }
        public string ModelName { get; set; }



        public string ItemName { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string SubSubCategoryName { get; set; }
        public string SubSubSubCategoryName { get; set; }
        public string SubSubSubSubCategoryName { get; set; }
        public string UnitName { get; set; }
        public Nullable<int> Height { get; set; }
        public Nullable<int> Width { get; set; }
        public Nullable<int> Weight { get; set; }
        public string CountryName { get; set; }
        public string SupplierCompanyName { get; set; }
        public string WarrantyName { get; set; }
        public string ProcurementTypeName { get; set; }
        public string ConditionOfItemName { get; set; }

        public int ProductionDetailsId { get; set; }
        public string BrandName { get; set; }

        public Nullable<decimal> ItemQuantity { get; set; }
        public decimal ItemCost { get; set; }
        public decimal SubTotal { get; set; }
        public int ProductionMasterId { get; set; }

       
        public int ProductId { get; set; }
        public decimal ProductionQuantity { get; set; }
        public decimal ProductPrice { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Productiondate { get; set; }
        public string ProductName { get; set; }



        public int DamagedItemId { get; set; }
       
        public string DamagedItemType { get; set; }
        public decimal DamageQuantity { get; set; }
        public decimal AvailableQty { get; set; }
        public int Inv_Details_ID { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DamageDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public string StoreName { get; set; }
        public string ItemDetails { get; set; }

        public int SalesElementSetupId { get; set; }
       
        public decimal SalesPricePercent { get; set; }
        public decimal SalesPriceAmount { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal VatPercent { get; set; }
        public decimal VarAmount { get; set; }
       
      
    }
}
