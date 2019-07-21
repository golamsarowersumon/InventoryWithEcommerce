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
    public class StoreViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Store Id")]
        public int StoreId { get; set; }

        [Display(Name = "Store Name")]
        [Required(ErrorMessage = "Please Enter Store Name")]
        public string StoreName { get; set; }
      

        public ICollection<SubStore> SubStores { get; set; }
        public ICollection<SubSubStore> SubSubStores { get; set; }
        public ICollection<SubSubSubStore> SubSubSubStores { get; set; }
        public ICollection<SubSubSubSubStore> SubSubSubSubStores { get; set; }
    }
}
