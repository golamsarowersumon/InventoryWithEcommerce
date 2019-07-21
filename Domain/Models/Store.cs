using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Store
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StoreId { get; set; }      
        public string StoreName { get; set; }
        public ICollection<SubStore> SubStores { get; set; }
        public ICollection<SubSubStore> SubSubStores { get; set; }
        public ICollection<SubSubSubStore> SubSubSubStores { get; set; }
        public ICollection<SubSubSubSubStore> SubSubSubSubStores { get; set; }
        public virtual ICollection<ProcurementMaster> ProcurementMasters { get; set; }
        public virtual ICollection<ProductionMaster> ProductionMaster { get; set; }
    }
}
