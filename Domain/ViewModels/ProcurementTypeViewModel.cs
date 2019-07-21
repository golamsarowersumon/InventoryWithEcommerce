using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class ProcurementTypeViewModel
    {
        
        [Display(Name ="Procurement Id")]
        public int ProcurementTypeId { get; set; }
        [Display(Name ="Procurement Name")]
        [Required(ErrorMessage ="Please Enter Procurement Name")]
        public string ProcurementTypeName { get; set; }
    }
}
