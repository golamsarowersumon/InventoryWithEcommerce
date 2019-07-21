using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class SubSubSubSubStore
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubSubSubSubStoreId { get; set; }
        public string SubSubSubSubStoreName { get; set; }

        public int StoreId { get; set; }
        public Nullable<int> SubStoreId { get; set; }
        public Nullable<int> SubSubStoreId { get; set; }
        public Nullable<int> SubSubSubStoreId { get; set; }

        public virtual Store Store{ get; set; }
        public virtual SubStore SubStore{ get; set; }
        public virtual SubSubStore SubSubStore{ get; set; }
        public virtual SubSubSubStore SubSubSubStore{ get; set; }
        public virtual ICollection<ProcurementMaster> ProcurementMasters { get; set; }
        public virtual ICollection<ProductionMaster> ProductionMaster { get; set; }
       
    }
}
