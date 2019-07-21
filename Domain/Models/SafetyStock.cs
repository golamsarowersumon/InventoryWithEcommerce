using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class SafetyStock
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SafetyStockLimitId { get; set; }
        public int ItemOrProductId {get;set;} 
        public int SafetyStokLimit { get; set; }
        public string UnitId { get; set; }
        
    }
}
