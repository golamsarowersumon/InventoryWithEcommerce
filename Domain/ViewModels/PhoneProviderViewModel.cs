using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class PhoneProviderViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name ="Phone Provider Id")]
        public int PhoneProviderId { get; set; }

        [Display(Name ="Phone Provider Name")]
        [Required(ErrorMessage =("Please Enter Phone Provider Name"))]
        public string PhoneProviderName { get; set; }
    }
}
