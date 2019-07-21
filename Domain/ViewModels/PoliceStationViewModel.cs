using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.ViewModels
{
   public class PoliceStationViewModel
    {

        [Key]
        public int PoliceStationId { get; set; }


        [Display(Name = "Police Station Id")]
        [Required(ErrorMessage ="Please Enter Police Station Name")]
        [RegularExpression("[A-Za-z ]*", ErrorMessage = "Invalid Name ")]
        public string PoliceStationName { get; set; }

        [Display(Name = "DistrictId")]
        public Nullable<int> DistrictId { get; set; }

        [Display(Name = "District Name")]
        public string DistrictName { get; set; }

        public virtual District District { get; set; }
        //public virtual ICollection<Profile> Profiles { get; set; }

    }
}
