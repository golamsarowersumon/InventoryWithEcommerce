using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Method
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MethodId { get; set; }
        public string MethodName { get; set; }
        public virtual ICollection<Item> Item { get; set; }
        public ICollection<TemporaryTransferInformation> TemporaryTransferInformation { get; set; }
    }
}
