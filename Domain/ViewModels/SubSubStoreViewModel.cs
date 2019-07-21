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
    public class SubSubStoreViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "SubSubStore Id")]
        public int SubSubStoreId { get; set; }

        [Display(Name = "SubSubStore Name")]
        [Required(ErrorMessage = "Please Enter SubSubStore Name")]
        public string SubSubStoreName { get; set; }
        public int StoreId { get; set; }
        public Nullable<int> SubStoreId { get; set; }
        public string StoreName { get; set; }
        public string SubStoreName { get; set; }
       


        public virtual Store Store { get; set; }      
        public virtual SubStore SubStore { get; set; }
        public ICollection<SubSubSubStore> SubSubSubStores { get; set; }
        public ICollection<SubSubSubSubStore> SubSubSubSubStores { get; set; }

    }
}
