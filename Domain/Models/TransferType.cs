using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
  public  class TransferType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransferTypeId { get; set; }
        public string TransferTypeName { get; set; }

        public ICollection<TransferDetails> TransferDetails { get; set; }
        public ICollection<TemporaryTransferInformation> TemporaryTransferInformation { get; set; }
       
    }
}
