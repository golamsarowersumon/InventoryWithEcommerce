using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
   public class RawMaterials
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RawMaterialId { get; set; }
        public Nullable<int> ItemId { get; set; }
        public Nullable<int> ItemElementId { get; set; }


        public virtual Item Item { get; set; }
        public virtual ItemElement ItemElement { get; set; }

    }
}
