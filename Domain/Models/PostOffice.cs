using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
   public class PostOffice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostOfficeId { get;set; }
        public string PostOfficeName { get; set; }
        public Nullable<int> DistrictId { get; set; }

        public virtual District District { get; set; }
        //public virtual ICollection<Profile> Profiles { get; set; }
        public virtual ICollection<PressentAddress> PressentAddresses { get; set; }
        public virtual ICollection<PermanentAddress> PermanentAddresses { get; set; }


    }
}
