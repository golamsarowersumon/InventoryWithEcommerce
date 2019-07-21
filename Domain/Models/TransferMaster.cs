using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
   public class TransferMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransferId { get; set; }

        public Nullable<int> StoreId { get; set; }
        public Nullable<int> SubStoreId { get; set; }
        public Nullable<int> SubSubStoreId { get; set; }
        public Nullable<int> SubSubSubStoreId { get; set; }
       
        public Nullable<int> SubSubSubSubStoreId { get; set; }
        public int FromStoreId { get; set; }
        public Nullable<int> Transferby { get; set; }
      

       

        public string CreateBy { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? UpdateDate { get; set; }

        public virtual Store Store { get; set; }
        public virtual SubStore SubStore { get; set; }
        public virtual SubSubStore SubSubStore { get; set; }
        public virtual SubSubSubStore SubSubSubStore { get; set; }
        public virtual SubSubSubSubStore SubSubSubSubStore { get; set; }

        public virtual ICollection<InventoryMaster> InventoryMaster { get; set; }
        public virtual ICollection<TransferDetails> TransferDetails { get; set; }
        public virtual ICollection<TemporaryTransferInformation> TemporaryTransferInformation { get; set; }
        //public virtual TransferOrder TransferOrder { get; set; }

    }
}
