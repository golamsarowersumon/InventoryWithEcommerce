using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
  public class ProcurementShowViewModel
    {
        public int PO_HD_ID { get; set; }
        //[DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> DateOfPurchase { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfReceive { get; set; }
        public decimal PO_TOTAL_AMT { get; set; }
        public decimal PO_DIS_RATE { get; set; }
        public decimal PO_DIS_AMT { get; set; }
        public decimal PO_Before_Tax_Amt { get; set; }
        public decimal PO_Tax_Rate { get; set; }
        public decimal PO_Tax_AMT { get; set; }
        public decimal PO_GRAND_TOTAL { get; set; }
        public int ProcurementTypeId { get; set; }
        public string ProcurementTypeName { get; set; }
        public int SupplierCompanyId { get; set; }
        public string SupplierCompanyName { get; set; }
        public Nullable<int> SubContractCompanyId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public string ProductName { get; set; }
        public Nullable<int> StoreId { get; set; }
        public string StoreName { get; set; }
        public Nullable<int> SubStoreId { get; set; }
        public Nullable<int> SubSubStoreId { get; set; }
        public Nullable<int> SubSubSubStoreId { get; set; }
        public Nullable<int> SubSubSubSubStoreId { get; set; }

        public Nullable<int> PurchasedBy_ProcureBy { get; set; }
        public Nullable<int> CreateBy { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> CreateDate { get; set; }
        public Nullable<int> UpdateBy { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime UpdateDate { get; set; }
        public string WarrantyPeriod { get; set; }
        public int Inv_Details_ID { get; set; }
        public decimal AvailableQty { get; set; }


        public int PO_Details_ID { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfExpired { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfNextMaintainance { get; set; }
        public String Barcode { get; set; }
        public String Item_Unique_Number { get; set; }
        public String Chesis_Number { get; set; }
        public String Engine_Number { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public Nullable<int> CountryId { get; set; }
        public Nullable<int> Principle_id { get; set; }
        public Nullable<int> ConditionOfItemId { get; set; }
        public Nullable<int> WarrantyId { get; set; }

        public decimal PO_QTD { get; set; }
        public decimal PO_Price { get; set; }
        public decimal PO_SubTotal { get; set; }
        public decimal Item_Dis_Rate { get; set; }
        public decimal Item_Dis_Amt { get; set; }
       
        public Nullable<int> CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Nullable<int> SubCategoryId { get; set; }
        public Nullable<int> SubSubCategoryId { get; set; }
        public Nullable<int> SubSubSubCategoryId { get; set; }
        public Nullable<int> SubSubSubSubCategoryId { get; set; }
        public Nullable<int> BrandId { get; set; }
        public Nullable<int> ModelId { get; set; }
        public Nullable<int> UnitId { get; set; }
        public string UnitName { get; set; }

    }
}
