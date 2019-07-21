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
    public class BloodGroupViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Blood Group Id")]
        public int BloodGroupId { get; set; }

        [Display(Name = "Blood Group Name")]
        [Required(ErrorMessage = ("Blood Group Name"))]
        public string BloodGroupName { get; set; }


        public virtual ICollection<Profile> Profile { get; set; }
    }
}
