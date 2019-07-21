using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ItemElement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name ="Item Element Id")]
        public int ItemElementId { get; set; }
        [Display(Name ="Item Element Name")]
        public string ItemElementName { get; set; }
        [Display(Name ="Category Id")]
        public int  CategoryId { get; set; }
        [Display(Name = "SubCategory Id")]
        public Nullable<int> SubCategoryId { get; set; }
        [Display(Name = "SubSubCategory Id")]
        public Nullable<int> SubSubCategoryId { get; set; }
        [Display(Name = "SubSubSubCategory Id")]
        public Nullable<int> SubSubSubCategoryId { get; set; }
        [Display(Name = "SubSubSubSubCategory Id")]
        public Nullable<int> SubSubSubSubCategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual SubSubCategory SubSubCategory { get; set; }
        public virtual SubSubSubCategory SubSubSubCategory { get; set; }
        public virtual SubSubSubSubCategory SubSubSubSubCategory { get; set; }
        public virtual ICollection<RawMaterials> RawMaterials { get; set; }

    }
}
