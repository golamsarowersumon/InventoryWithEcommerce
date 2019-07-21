using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
   public class RegionViewModel
    {

        [Key]
       
        public int RegionId { get; set; }
        [Display(Name ="Please Enter Region Name..")]
        public string RegionName { get; set; }
        public virtual ICollection<Profile> Profile { get; set; }
    }
}
