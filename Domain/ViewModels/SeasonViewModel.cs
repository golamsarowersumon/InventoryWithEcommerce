using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class SeasonViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name ="Season Id")]
        public int SeasonId { get; set; }

        [Display(Name ="Seaon Name")]
        [Required(ErrorMessage ="Please Enter Season Name")]       
        public string SeasonName { get; set; }
    }
}
