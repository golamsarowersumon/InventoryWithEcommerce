using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models;

namespace Domain.ViewModels
{
   public class GenderViewModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name ="Gender ID")]
        public int GenderID { get; set; }
        [Display(Name ="Gender Name")]
        [Required(ErrorMessage ="Please Enter Gender Name.")]
        public string GenderName { get; set; }

        public virtual ICollection<Profile> Profile { get; set; }
    }
}
