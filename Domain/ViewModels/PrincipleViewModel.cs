using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
  public  class PrincipleViewModel
    {
        [Key]
        public int Principle_id { get; set; }

        [Required]
        [Display(Name = "Principle Name")]
        public string Principle_name { get; set; }
        public string CountryName { get; set; }
        [Required]
        [Display(Name = "Country Name")]
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

    }
}
