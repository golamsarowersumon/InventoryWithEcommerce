using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Domain.Models
{
    public class Country
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CountryId { get; set; }
        public string CountryName { get; set; }

        public virtual ICollection<Principle> Principles { get; set; }
        public virtual ICollection<State> States { get; set; }
        public virtual ICollection<Province> Provinces { get; set; }
        public virtual ICollection<PressentAddress> PressentAddresses { get; set; }
        public virtual ICollection<PermanentAddress> PermanentAddresses { get; set; }
        public virtual ICollection<ProcurementDetails> ProcurementDetails { get; set; }
        public virtual ICollection<TemporaryTransferInformation> TemporaryTransferInformation { get; set; }
        public virtual ICollection<District> Districts { get; set; }

    }
}