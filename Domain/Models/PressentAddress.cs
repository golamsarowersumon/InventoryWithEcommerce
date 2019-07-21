using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PressentAddress
    {
        [Key]
        public int ProfileId { get; set; }

        public string PresentAddressFull { get; set; }
        public Nullable<int> PrePoliceStationId { get; set; }
        public Nullable<int> PrePostOfficeId { get; set; }
        public Nullable<int> PreDistrictId { get; set; }
        public Nullable<int> PreDivisionId { get; set; }
        public Nullable<int> PreCountryId { get; set; }

        public virtual PoliceStation PoliceStation { get; set; }
        public virtual PostOffice PostOffice { get; set; }
        public virtual District District { get; set; }
        public virtual Division Division { get; set; }
        public virtual Country Country { get; set; }

        public virtual ICollection<Profile> Profile { get; set; }
       

    }
}
