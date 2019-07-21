using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class LanguageViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Phone Language Id")]
        public int LanguageId { get; set; }

        [Display(Name = "Language Name")]
        [Required(ErrorMessage = ("Please Enter Language Name"))]
        public string LanguageName { get; set; }
    }
}
