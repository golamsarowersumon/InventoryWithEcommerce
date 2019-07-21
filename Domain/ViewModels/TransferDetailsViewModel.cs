using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
   public class TransferDetailsViewModel
    {

        public int TransferDetailId { get; set; }
        public int ToStoreId { get; set; }
        public int ItemId { get; set; }
        public decimal TransactionQuantity { get; set; }
        public Nullable<int> TransferTypeId { get; set; }
        public Nullable<int> UnitId { get; set; }
        public Nullable<int> ConditionOfItemId { get; set; }
        public int TransferId { get; set; }
        public int InventoryId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateOfTransferOrder { get; set; }

        public int TransferOrderId { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateOfActualTransfer { get; set; }
        public decimal AvailableQuantity { get; set; }


    }
}
