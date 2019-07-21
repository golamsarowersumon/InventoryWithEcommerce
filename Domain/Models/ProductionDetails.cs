using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
   public class ProductionDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductionDetailsId { get; set; }
        public Nullable<int> ItemId { get; set; }
        public Nullable<decimal> ItemQuantity { get; set; }
        public decimal ItemCost { get; set; }
        public decimal SubTotal { get; set; }
        public int ProductionMasterId { get; set; }
        public virtual ProductionMaster ProductionMaster { get; set; }
        public virtual Item Item { get; set; }

    }
}
