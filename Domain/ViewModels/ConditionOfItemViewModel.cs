using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
   public class ConditionOfItemViewModel
    {

        [Key]
      
        public int ConditionOfItemId { get; set; }
        [Display(Name ="Condition Name")]
        [Required(ErrorMessage ="Please Enter Condition Name of a Item/Product")]
        public string ConditionOfItemName { get; set; }

    }
}
