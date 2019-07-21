using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
  public  class TransferDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransferDetailId { get; set; }
        public int TransferId { get; set; }
        public int TransferOrderId { get; set; }
        public Nullable<int> ToStoreId { get; set; }
        public int ItemId { get; set; }
        public decimal TransactionQuantity { get; set; }
        public Nullable<int> TransferTypeId { get; set; }
        public Nullable<int> UnitId { get; set; }
        public Nullable<int> ConditionOfItemId { get; set; }




        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateOfActualTransfer { get; set; }
        public string Recieve { get; set; }
        public string RecieveBy { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DateOfReceive { get; set; }
        public Nullable<decimal> ReceiveItemQuantity { get; set; }
        public Nullable<decimal> PendingItemQuantity { get; set; }
        public virtual Item Item { get; set; }
        public virtual ConditionOfItem ConditionOfItem { get; set; }
        public virtual TransferType TransferType { get; set; }
        public virtual TransferMaster TransferMaster { get; set; }
       

    }
}
