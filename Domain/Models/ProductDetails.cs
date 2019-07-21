using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ProductDetails
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SerialId { get; set; }

        public int ProductId { get; set; }
      
        public decimal ProductQuantity { get; set; }

        public int ItemId { get; set; }
     

        public decimal ItemQuantity { get; set; }
      

        public virtual Item Item { get; set; }

        public virtual Product Product { get; set; }

        public ICollection<ProductionMaster> Productions { get; set; }
       
    }
}
