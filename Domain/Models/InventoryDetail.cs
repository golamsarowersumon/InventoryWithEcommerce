using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
   public class InventoryDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Inv_Details_ID { get; set; }
        public int Inv_HD_ID { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> DateOfExpired { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> DateOfNextMaintainance { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> ProductionDate { get; set; }
        public String Barcode { get; set; }
        public String Item_Unique_Number { get; set; }
        public String Chesis_Number { get; set; }
        public String Engine_Number { get; set; }
        public int ItemId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<int> CountryId { get; set; }
        public Nullable<int> Principle_id { get; set; }
        public Nullable<int> ConditionOfItemId { get; set; }
        public Nullable<int> WarrantyId { get; set; }
        public Nullable<int> ProductionMasterId { get; set; }
        public decimal TransactionQty { get; set; }
        public decimal AvailableQty { get; set; }
        public decimal PO_Price { get; set; }
        public decimal SO_Price { get; set; }
        public decimal PO_SubTotal { get; set; }
       
        public decimal DamageQuantity { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> SubCategoryId { get; set; }
        public Nullable<int> SubSubCategoryId { get; set; }
        public Nullable<int> SubSubSubCategoryId { get; set; }
        public Nullable<int> SubSubSubSubCategoryId { get; set; }
        public Nullable<int> BrandId { get; set; }
        public Nullable<int> ModelId { get; set; }
        public Nullable<int> UnitId { get; set; }
        public Nullable<int> MethodId { get; set; }
        public bool IsHot { get; set; }
        public bool IsNew { get; set; }
        public virtual Category Category { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual SubSubCategory SubSubCategory { get; set; }
        public virtual SubSubSubCategory SubSubSubCategory { get; set; }
        public virtual SubSubSubSubCategory SubSubSubSubCategory { get; set; }
        public virtual Country Country { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Model Model { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual ConditionOfItem ConditionOfItem { get; set; }
        public virtual Warranty Warranty { get; set; }
        public virtual InventoryMaster InventoryMaster { get; set; }
        public virtual ProductionMaster ProductionMaster { get; set; }
    }
}
