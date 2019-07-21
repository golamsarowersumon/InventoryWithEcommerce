using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
   public class FestivalViewModel
    {

        [Key]
        
        public int FestivalId { get; set; }
        public string FestivalName { get; set; }
        public int ReligionId { get; set; }
        public string ReligionName { get; set; }
        public virtual Religion Religion { get; set; }


    }
}
