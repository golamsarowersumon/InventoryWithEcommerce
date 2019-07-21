using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
  public  class SubSubSubSubCategoryViewModel
    {

        [Key]
        public int SubSubSubSubCategoryId { get; set; }
        [Display(Name = "SubSubSubSubCategory Name")]
        [Required(ErrorMessage = "Please Enter SubSubSubSubCategory Name!!")]
        public string SubSubSubSubCategoryName { get; set; }

        [Display(Name = "SubSubSubCategory Id")]
        //[Required(ErrorMessage = "Please Select SubCategoy Name!!")]
        public Nullable<int> SubSubSubCategoryId { get; set; }

        [Display(Name = "SubSubCategory Id")]
        //[Required(ErrorMessage = "Please Select SubCategoy Name!!")]
        public Nullable<int> SubSubCategoryId { get; set; }


        [Display(Name = "SubCategory Id")]
        //[Required(ErrorMessage = "Please Select SubCategoy Name!!")]
        public Nullable<int> SubCategoryId { get; set; }

        [Display(Name = "Categoy Id")]
        [Required(ErrorMessage = "Please Select Category Name!!")]
        public Nullable<int> CategoryId { get; set; }






        [Display(Name = "SubSubSubCategory Name")]
        public string SubSubSubCategoryName { get; set; }

        [Display(Name = "SubSubCategory Name")]
        public string SubSubCategoryName { get; set; }

        [Display(Name = "SubCategory Name")]
        public string SubCategoryName { get; set; }

        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }


        public virtual Category Category { get; set; }
        public virtual SubCategory SubCategory{ get; set; }
        public virtual SubSubCategory SubSubCategory{ get; set; }
        public virtual SubSubSubCategory SubSubSubCategory{ get; set; }

        public virtual ICollection<Brand> Brands { get; set; }

    }
}
