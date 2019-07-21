using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
   public class CelebrationViewModel
    {

        [Key]
        
        public int CelebrationId { get; set; }
        [Display(Name ="Celebration Name ")]
        [Required(ErrorMessage ="Please Ente Celebration Name..")]
        public string CelebrationName { get; set; }
        [Display(Name = "Celebration Type ")]
        [Required(ErrorMessage = "Please Ente Celebration Type..")]
        public string CelebrationType { get; set; }

    }
}
