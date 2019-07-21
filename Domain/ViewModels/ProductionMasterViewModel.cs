using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
  public class ProductionMasterViewModel
    {

       
        public int ProductionMasterId { get; set; }
        public int ProductId { get; set; }
        public decimal ProductionQuantity { get; set; }
        public decimal ProductPrice { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Productiondate { get; set; }
        public string ProductName { get; set; }
        public int StoreId { get; set; }

        public string CreatedBy { get; set; }
    }
}
