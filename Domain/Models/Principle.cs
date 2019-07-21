using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Domain.Models
{
    public class Principle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Principle_id { get; set; }
        public string Principle_name { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

    }
}