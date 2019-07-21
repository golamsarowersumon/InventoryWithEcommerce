using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
  public  class PermanentAddress
    {

        [Key]
         public int ProfileId { get; set; }

        public string PermanentAddressFull { get; set; }

     

        public Nullable<int> PerPoliceStationId { get; set; }
        public Nullable<int> PerPostOfficeId { get; set; }
        public Nullable<int> PerDistrictId { get; set; }
        public Nullable<int> PerDivisionId { get; set; }
        public Nullable<int> PerCountryId { get; set; }

        public virtual PoliceStation PoliceStation { get; set; }
        public virtual PostOffice PostOffice { get; set; }
        public virtual District District { get; set; }
        public virtual Division Division { get; set; }
        public virtual Country Country { get; set; }

        public virtual ICollection<Profile> Profile { get; set; }


    }
}
