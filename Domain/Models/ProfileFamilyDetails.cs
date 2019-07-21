using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
   public class ProfileFamilyDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProfileDetailsId { get; set; }
        public int ProfileId { get; set; }
        public string SpouseName { get; set; }
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SpouseDOB { get; set; }
        public string SpouseProfession { get; set; }
       
        public int SpouseGenderId { get; set; }
        public int SlNoOfMarriage { get; set; }
        public string ChildName { get; set; }
        public DateTime ChildDOB { get; set; }
        public string ChildProfession { get; set; }
        public Nullable<int> GenderID { get; set; }
        public int SlNoOfChild { get; set; }
        public virtual Gender Gender { get; set; }
       public virtual Profile Profile { get; set; }

    }
}
