using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class SupplierCompany
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SupplierCompanyId { get; set; }
        public string  SupplierCompanyName { get; set; }
        public virtual ICollection<ProcurementMaster> ProcurementMasters { get; set; }
    }
}
