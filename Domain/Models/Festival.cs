using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
  public  class Festival
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FestivalId { get; set; }
        public string FestivalName { get; set; }
        public int ReligionId { get; set; }
        public virtual Religion Religion { get; set; }



    }
}
