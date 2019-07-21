using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ConditionOfItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ConditionOfItemId { get; set; }
        public string ConditionOfItemName { get; set; }
        public virtual ICollection<ProcurementDetails> ProcurementDetails { get; set; }
        public virtual ICollection<TemporaryTransferInformation> TemporaryTransferInformation { get; set; }
    }
}
