using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
  public  class Brand
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<ProcurementDetails> ProcurementDetails { get; set; }
        public virtual ICollection<TemporaryTransferInformation> TemporaryTransferInformation { get; set; }



    }
}
