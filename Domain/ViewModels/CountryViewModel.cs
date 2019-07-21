using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
  public  class CountryViewModel
    {


       [Key]
        public int CountryId { get; set; }
        [Required]
        [Display(Name = "Country Name")]

        public string CountryName { get; set; }



        public virtual ICollection<Principle> Principles { get; set; }
        public virtual ICollection<State> States { get; set; }
        public virtual ICollection<Province> Provinces { get; set; }




    }
}
