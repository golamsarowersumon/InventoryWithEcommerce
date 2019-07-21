using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
  public  class SubSubCategoryViewModel
    {
        [Key]
        public int SubSubCategoryId { get; set; }
        [Display(Name = "SubSubCategoy Name")]
        [Required(ErrorMessage = "Please Enter SubSubCategoy Name!!")]
        public string SubSubCategoryName { get; set; }
        [Display(Name = "SubCategoy Id")]
        [Required(ErrorMessage = "Please Select SubCategoy Name!!")]
        public Nullable<int> SubCategoryId { get; set; }
        [Display(Name = "Categoy Id")]
        [Required(ErrorMessage = "Please Select SubCategoy Name!!")]
        public Nullable<int> CategoryId { get; set; }
        [Display(Name = "SubCategory Name")]
        public string SubCategoryName { get; set; }

        [Display(Name = "Category Name I")]
        public string CategoryName { get; set; }


        public virtual Category Category { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        
        public virtual ICollection<SubSubSubCategory> SubSubSubCategories { get; set; }
        public virtual ICollection<SubSubSubSubCategory> SubSubSubSubCategories { get; set; }
        public virtual ICollection<Brand> Brands { get; set; }
    }
}
