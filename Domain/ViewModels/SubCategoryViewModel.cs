using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
  public  class SubCategoryViewModel
    {
        [Key]
        public int SubCategoryId { get; set; }
        [Display(Name = "SubCategoy Name")]
        [Required(ErrorMessage = "Please Enter SubCategory Name!!")]
        public string SubCategoryName { get; set; }

        [Display(Name = "Category Id")]
        [Required(ErrorMessage = "Please Enter Category Id!!")]
        public Nullable<int> CategoryId { get; set; }

       
        public string CategoryName { get; set; }

        public virtual Category Category { get; set; }

       
        public virtual ICollection<SubSubCategory> SubSubCategories { get; set; }
        public virtual ICollection<SubSubSubCategory> SubSubSubCategories { get; set; }
        public virtual ICollection<SubSubSubSubCategory> SubSubSubSubCategories { get; set; }
        public virtual ICollection<Brand> Brands { get; set; }

    }
}
