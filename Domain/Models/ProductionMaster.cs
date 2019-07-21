using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ProductionMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductionMasterId { get; set; }
        public int ProductId { get; set; }
        public decimal ProductionQuantity { get; set; }
        public decimal ProductPrice { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Productiondate { get; set; }
        public int? StoreId { get; set; }
        public int? SubStoreId { get; set; }
        public int? SubSubStoreId { get; set; }
        public int? SubSubSubStoreId { get; set; }
        public int? SubSubSubSubStoreId { get; set; }
        public string CreatedBy { get; set; }
        public virtual Store Store { get; set; }
        public virtual SubStore SubStore { get; set; }
        public virtual SubSubStore SubSubStore { get; set; }
        public virtual SubSubSubStore SubSubSubStore { get; set; }
        public virtual SubSubSubSubStore SubSubSubSubStore { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<ProductionDetails> ProductionDetails { get; set; }
        public virtual ICollection<InventoryDetail> InventoryDetails { get; set; }
    }
}
