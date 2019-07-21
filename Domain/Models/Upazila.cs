using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Upazila
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UpazilaId { get; set; }
        public string UpazilaName { get; set; }
        public int DistrictId { get; set; }

        public virtual District District { get; set; }
    }
}
