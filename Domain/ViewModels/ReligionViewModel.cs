using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class ReligionViewModel
    {

        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReligionId { get; set; }
        [Display(Name ="Religion Name")]
        [Required(ErrorMessage ="Please Enter Religion Name!!")]
        public string ReligionName { get; set; }
        public virtual ICollection<FestivalViewModel> FestivalViewModels { get; set; }
    }
}
