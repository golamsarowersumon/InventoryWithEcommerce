using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
   public class SubSubSubCategory
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubSubSubCategoryId { get; set; }
        public string SubSubSubCategoryName { get; set; }
        public Nullable<int> SubSubCategoryId { get; set; }
        public Nullable<int> SubCategoryId { get; set; }
        public Nullable<int> CategoryId { get; set; }



        public virtual Category Category { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual SubSubCategory SubSubCategory { get; set; }
        public virtual ICollection<SubSubSubSubCategory> SubSubSubSubCategories { get; set; }
        public virtual ICollection<Brand> Brands { get; set; }
        public virtual ICollection<ProcurementDetails> ProcurementDetails { get; set; }
        public virtual ICollection<TemporaryTransferInformation> TemporaryTransferInformation { get; set; }
    }
}
