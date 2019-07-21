using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class WarrantyViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Waranty Id")]
        public int WarrantyId { get; set; }

        [Display(Name = "Waranty Name")]
        [Required(ErrorMessage = "Please Enter Waranty Name")]
        public string WarrantyName { get; set; }
    }
}
