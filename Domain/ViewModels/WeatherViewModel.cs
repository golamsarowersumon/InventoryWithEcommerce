using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class WeatherViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Weather Name")]
        public int WeatherId { get; set; }

        [Display(Name = "Weather Name")]
        [Required(ErrorMessage = "Please Enter Weather Name")]
        public string WeatherName { get; set; }
    }
}
