using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class HobbyViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name ="HobbyId")]
        public int HobbyId { get; set; }

        [Display(Name ="Hobby Nmae")]
        [Required(ErrorMessage ="Please Enter Hobby Name")]
        public string HobbyName { get; set; }
    }
}
