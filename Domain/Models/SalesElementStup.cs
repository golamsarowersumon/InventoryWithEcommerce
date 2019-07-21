using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
   public class SalesElementStup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SalesElementSetupId { get; set; }
        public int ItemId { get; set; }
        public decimal SalesPricePercent { get; set; }
        public decimal SalesPriceAmount { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal VatPercent { get; set; }
        public decimal VarAmount { get; set; }
        public decimal PurchasePrice { get; set; }
        public Nullable<int> BrandId { get; set; }
        public Nullable<int> ModelId { get; set; }
    }
}
