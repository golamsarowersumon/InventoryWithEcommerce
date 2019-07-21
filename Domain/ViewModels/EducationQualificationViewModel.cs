using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class EducationQualificationViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Display(Name = "Education Qualification Id")]
        [Required(ErrorMessage = ("Please Enter Email Provider Id"))]
        public int EducationQualificationId { get; set; }


        [Display(Name = "Education Qualification Name")]
        [Required(ErrorMessage = ("Please Enter Education Qualification Name"))]
        public string EducationQualificationName { get; set; }
    }
}
