using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class SubContractCompanyViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name ="Sub Contract Id")]
        public int SubContractCompanyId { get; set; }
        [Display(Name = "Sub Contract Name")]
        [Required(ErrorMessage ="Enter SCC Name")]
        public string SubContractCompanyName { get; set; }
    }
}
