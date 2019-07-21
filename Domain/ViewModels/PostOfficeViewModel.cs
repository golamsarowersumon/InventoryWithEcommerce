using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
   public class PostOfficeViewModel
    {

        [Key]
         public int PostOfficeId { get; set; }

        [Display(Name = "Post Office Id")]
        [Required(ErrorMessage = "Please Enter Post Office Name")]
        [RegularExpression("[A-Za-z ]*", ErrorMessage = "Invalid Name ")]
        public string PostOfficeName { get; set; }

        [Display(Name ="District Id")]
        public Nullable<int> DistrictId { get; set; }

        [Display(Name = "District Name")]
        public string DistrictName { get; set; }

        public virtual District District { get; set; }
        //public virtual ICollection<Profile> Profiles { get; set; }




    }
}
