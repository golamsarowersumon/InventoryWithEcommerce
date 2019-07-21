using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ProcurementType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProcurementTypeId { get; set; }
        public string ProcurementTypeName { get; set; }
        public virtual ICollection<ProcurementMaster> ProcurementMaster { get; set; }
    }
}
