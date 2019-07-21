using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
   public class District
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DistrictId { get; set; }

        public string DistrictName { get; set; }
        public decimal ShippingCharge { get; set; }
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<Upazila> Upazilas { get; set; }
        //public virtual ICollection<Profile> EmployeeDetails { get; set; }
        public virtual ICollection<PostOffice> PostOffices { get; set; }
        public virtual ICollection<PoliceStation> PoliceStations { get; set; }
        public virtual ICollection<PressentAddress> PressentAddresses { get; set; }
        public virtual ICollection<PermanentAddress> PermanentAddresses { get; set; }



    }
}
