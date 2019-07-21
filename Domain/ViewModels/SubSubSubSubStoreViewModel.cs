
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
    public class SubSubSubSubStoreViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "SubSubSubSubStore Id")]
        public int SubSubSubSubStoreId { get; set; }

        [Display(Name = "SubSubSubSubStore Name")]
        [Required(ErrorMessage = "Please Enter SubSubSubSubStore Name")]
        public string SubSubSubSubStoreName { get; set; }

        public int StoreId { get; set; }
        public Nullable<int> SubStoreId { get; set; }
        public Nullable<int> SubSubStoreId { get; set; }
        public Nullable<int> SubSubSubStoreId { get; set; }
        public string StoreName { get; set; }
        public string SubStoreName { get; set; }
        public string SubSubStoreName { get; set; }
        public string SubSubSubStoreName { get; set; }




        public virtual Store Store { get; set; }
            
        public virtual SubStore SubStore { get; set; }

        public virtual SubSubStore SubSubStore { get; set; }

        public virtual SubSubSubStore SubSubSubStore { get; set; }
    }
}
