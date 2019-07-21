using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
   public class E_commerceSalesOrderMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int E_SalerOrderMasterId { get; set; }
        public int CustomerId { get; set; }
        public DateTime E_OrderDate { get; set; }
        public ICollection<E_commerceSalesOrderDetails> E_CommerceSalesOrderDetails { get; set; }
    }
}
