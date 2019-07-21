using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
  public  class MaritalStatusViewModel
    {

        [Key]
        
        public int MaritalStatusId { get; set; }
        [Display(Name ="Marital Status Name")]
        [Required(ErrorMessage ="Please Enter Your Marital Status Name")]
        public string MaritalStatusName { get; set; }
        public virtual ICollection<Profile> Profile { get; set; }
    }
}
