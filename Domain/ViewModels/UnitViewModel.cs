using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class UnitViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name ="Unit Id")]
        public int UnitId { get; set; }

        [Display(Name="Unit Name")]
        [Required(ErrorMessage = "Please Enter Unit Name")]
        public string UnitName { get; set; }
    }
}
