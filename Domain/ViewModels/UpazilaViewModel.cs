using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class UpazilaViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Upazila Id")]
        public int UpazilaId { get; set; }

        [Display(Name = "Upazila Name")]
        [Required(ErrorMessage = "Please Enter Upazila Name")]
        public string UpazilaName { get; set; }

        public int DistrictId { get; set; }

        [Display(Name = "District Name")]
        [Required(ErrorMessage = "Enter District Name..")]
        public string DistrictName { get; set; }
    }
}
