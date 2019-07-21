using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
   public class PoliceStation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       public int PoliceStationId { get; set; }
        public string PoliceStationName { get;set; }
         public Nullable<int> DistrictId { get;set; }

        public virtual District District { get; set; }
        //public virtual ICollection<Profile> Profiles { get; set; }
        public virtual ICollection<PressentAddress> PressentAddresses { get; set; }
        public virtual ICollection<PermanentAddress> PermanentAddresses { get; set; }


    }
}
