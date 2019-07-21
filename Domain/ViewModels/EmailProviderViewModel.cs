using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class EmailProviderViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Email Provider Id")]
        public int EmailProviderId { get; set; }

        [Display(Name = "Email Provider Name")]
        [Required(ErrorMessage = ("Please Enter Email Provider Name"))]
        public string EmailProviderName { get; set; }
    }
}
