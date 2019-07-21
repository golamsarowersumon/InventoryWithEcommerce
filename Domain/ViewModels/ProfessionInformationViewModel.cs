using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class ProfessionInformationViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Profession Id")]
        public int ProfessionId { get; set; }

        [Display(Name = "Profession Name")]
        [Required(ErrorMessage = ("Please Enter Profession Name"))]
        public string ProfessionName { get; set; }
    }
}
