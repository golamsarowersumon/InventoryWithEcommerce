using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
   public class ProcurementDetailsViewModel
    {
      
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
        //public decimal Item_Dis_Rate { get; set; }
        //public decimal Item_Dis_Amt { get; set; }
        public int PO_HD_ID { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> SubCategoryId { get; set; }
        public Nullable<int> SubSubCategoryId { get; set; }
        public Nullable<int> SubSubSubCategoryId { get; set; }
        public Nullable<int> SubSubSubSubCategoryId { get; set; }
        public Nullable<int> BrandId { get; set; }
        public Nullable<int> ModelId { get; set; }
        public Nullable<int> UnitId { get; set; }

        public int MethodId { get; set; }
        public bool IsHot { get; set; }
        public bool IsNew { get; set; }
    }
}
