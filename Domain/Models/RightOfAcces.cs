using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class RightOfAcces
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RightOfAccesId { get; set; }
        public int TransferId { get; set; }
        public int TransferDetailsId { get; set; }
        public int TransferOrderId { get; set; }
        public int ItemId { get; set; }
        public decimal ItemQuantity { get; set; }
        public int FromStoreId { get; set; }
        public Nullable<int> ToStoreId { get; set; }
        public string Reason { get; set; }
        public string Remarks { get; set; }
        public decimal PO_Price { get; set; }
        public Nullable<int> UnitId { get; set; }
        public string UnitName { get; set; }
        public Nullable<int> ProductId { get; set; }
        public string ProductName { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Item Item { get; set; }
        public virtual TransferMaster TransferMaster { get; set; }
  
    }
}
