using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
   public class StateViewModel
    {
        [Key]
        public int StateID { get; set; }

        [Required]
        [Display(Name = "State Name")]
        public string StateName { get; set; }
        public string CountryName { get; set; }
        [Required]
        [Display(Name = "Country Name")]
        public int CountryID { get; set; }

        public virtual Country Country { get; set; }
    }
}
