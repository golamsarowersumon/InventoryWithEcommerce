using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models;

namespace Domain.ViewModels
{
  public  class CategoryViewModel
    {

        [Key]
         public int CategoryId { get; set; }

        [Display(Name = "Categoy Name")]
        [Required(ErrorMessage = "Please Enter Category Name!!")]
        public string CategoryName { get; set; }


        public virtual ICollection<SubCategoryViewModel> SubCategories { get; set; }
        public virtual ICollection<SubSubCategoryViewModel> SubSubCategories { get; set; }
        public virtual ICollection<SubSubSubCategoryViewModel> SubSubSubCategories { get; set; }
        public virtual ICollection<SubSubSubSubCategoryViewModel> SubSubSubSubCategories { get; set; }
        public virtual ICollection<Brand> Brands { get; set; }


    }
}
