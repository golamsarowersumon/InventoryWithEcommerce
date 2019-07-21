using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
   public class ProvinceViewModel
    {
        [Key]
        public int ProvinceID { get; set; }

        [Required]
        [Display(Name = "Province Name")]
        public string ProvinceName { get; set; }
        public string CountryName { get; set; }
        [Required]
        [Display(Name = "Country Name")]
        public int CountryID { get; set; }
        public virtual Country Country { get; set; }

    }
}
