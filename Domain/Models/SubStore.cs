using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class SubStore
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubStoreId { get; set; }
        public string SubStoreName { get; set; }
        public virtual Store Store { get; set; }

        public int StoreId { get; set; }
        

        public ICollection<SubSubStore> SubSubStores { get; set; }
        public ICollection<SubSubSubStore> SubSubSubStores { get; set; }
        public ICollection<SubSubSubSubStore> SubSubSubSubStores { get; set; }
        public virtual ICollection<ProcurementMaster> ProcurementMasters { get; set; }
        public virtual ICollection<ProductionMaster> ProductionMaster { get; set; }
    }
}
