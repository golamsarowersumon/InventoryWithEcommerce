using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class SubSubSubStoreViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "SubSubSubStore Id")]
        public int SubSubSubStoreId { get; set; }

        [Display(Name = "SubSubSubStore Name")]
        [Required(ErrorMessage = "Please Enter SubSubSubStore Name")]
        public string SubSubSubStoreName { get; set; }

        public int StoreId { get; set; }
        public Nullable<int> SubStoreId { get; set; }
        public Nullable<int> SubSubStoreId { get; set; }

        public string StoreName { get; set; }
        public string SubStoreName { get; set; }
        public string SubSubStoreName { get; set; }

        public virtual Store Store { get; set; }
     
        public virtual SubStore SubBranch { get; set; }
   
        public virtual SubSubStore SubSubStore { get; set; }

        public ICollection<SubSubSubSubStore> SubSubSubSubStores { get; set; }
    }
}
