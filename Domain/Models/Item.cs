using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int CategoryId { get; set; }
        public Nullable<int> SubCategoryId { get; set; }
        public Nullable<int> SubSubCategoryId { get; set; }
        public Nullable<int> SubSubSubCategoryId { get; set; }
        public Nullable<int> SubSubSubSubCategoryId { get; set; }
        public Nullable<int> ModelId { get; set; }
        public Nullable<int> BrandId { get; set; }
        public int UnitId { get; set; }
        public Nullable<int> Height { get; set; }
        public Nullable<int> Width { get; set; }
        public Nullable<int> Weight { get; set; }
        public int MethodId { get; set; }
        public string ItemDetails { get; set; }
        public string Product_Image { get; set; }
        public string Product_Image1 { get; set; }
        public string Product_Image2 { get; set; }

        public virtual Model Model { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Method Method { get; set; }
        public virtual Category Category { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual SubSubCategory SubSubCategory { get; set; }
        public virtual SubSubSubCategory SubSubSubCategory { get; set; }
        public virtual SubSubSubSubCategory SubSubSubSubCategory { get; set; }
      
        public virtual Unit Unit { get; set; }
        public virtual ICollection<RawMaterials> RawMaterials { get; set; }
       public ICollection<TemporaryTransferInformation> TemporaryTransferInformation { get; set; }


    }
}
