using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
   public class TransferOrder
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransferOrderId { get; set; }
        public int ToStoreId { get; set; }
        public int FromStoreId { get; set; }
     
         public int ItemId { get; set; }
        public int TransactionQuantity { get; set; }
        public string TransferOrderby { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? TransferOrderdate { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? TransferOrderDeliverydate { get; set; }


        public string OrderRecieve { get; set; }
        public string TransferOrderReceiveby { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? TransferOrderReceiveDate { get; set; }



        public Nullable<int> TransferTypeId { get; set; }
        public string TransferTypeName { get; set; }
        public Nullable<int> UnitId { get; set; }
        public string UnitName { get; set; }
        public string TransferOrderSent { get; set; }
        public string CreateBy { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? UpdateDate { get; set; }
        public Nullable<int> ConditionOfItemId { get; set; } 
        public virtual Item Item { get; set; }
        public virtual ConditionOfItem ConditionOfItem { get; set; }
        public virtual TransferType TransferType { get; set; }
        //public virtual ICollection<TransferMaster> TransferMaster { get; set; }

    }
}
