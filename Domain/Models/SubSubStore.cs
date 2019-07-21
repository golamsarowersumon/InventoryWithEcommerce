using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class SubSubStore
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubSubStoreId { get; set; }
        public string SubSubStoreName { get; set; }
        public virtual Store Store { get; set; }
        public virtual SubStore SubStore { get; set; }

        public int StoreId { get; set; }
        public Nullable<int> SubStoreId { get; set; }

       

        public ICollection<SubSubSubStore> SubSubSubStores { get; set; }
        public ICollection<SubSubSubSubStore> SubSubSubSubStores { get; set; }
        public virtual ICollection<ProcurementMaster> ProcurementMasters { get; set; }
        public virtual ICollection<ProductionMaster> ProductionMaster { get; set; }
    }
}
