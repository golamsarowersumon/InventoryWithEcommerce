using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class SafetyStockViewModel
    {

        [Key]

        public int SafetyStockLimitId { get; set; }
        [Display(Name = "Item Or Product Name")]
        [Required(ErrorMessage ="Please Enter Item or Product Name")]
        public int ItemOrProductId { get; set; }
        [Display(Name = "Safety Stock Limit")]
        [Required(ErrorMessage = "Please Enter Safety Stock Limit..")]
        public int SafetyStokLimit { get; set; }
        [Display(Name = "Unit Name")]
        [Required(ErrorMessage = "Please Select Unit.")]
        public string UnitId { get; set; }


    }
}
