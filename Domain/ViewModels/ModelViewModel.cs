
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
  public  class ModelViewModel
    {

        [Key]
      
        [Display(Name = "Model Id")]
        public int ModelId { get; set; }

        [Display(Name = "Model Name")]
        [Required(ErrorMessage = "Please Enter Model Name")]
        public string ModelName { get; set; }


        [Display(Name = "Brand Id")]
        public int BrandId { get; set; }

        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }

        public virtual Brand Brand { get; set; }
    }
}
