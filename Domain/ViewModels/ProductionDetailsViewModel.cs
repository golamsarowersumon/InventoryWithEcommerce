using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
   public class ProductionDetailsViewModel
    {
       
        public int ProductionDetailsId { get; set; }
        public int ItemId { get; set; }
        public Nullable<decimal> ItemQuantity { get; set; }
      
        public decimal ItemCost { get; set; }
        public decimal SubTotal { get; set; }
        public int ProductionMasterId { get; set; }
     



    }
}
