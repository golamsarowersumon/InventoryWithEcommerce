using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Domain.Models
{
    public partial class Profile
    {
        [Key]
        public int ProfileId { get; set; }
        public string ProfileName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public System.DateTime DateofBirth { get; set; }
        public string BirthPlace { get; set; }
        //public Nullable<int> DistrictId { get; set; }
        public Nullable<int> Genderid { get; set; }
        public Nullable<int> BloodGroupId { get; set; }
        public Nullable<int> NationalityID { get; set; }
        public Nullable<int> MaritalStatusId { get; set; }
        public System.DateTime DateofMarriage { get; set; }
        public Nullable<int> RegionId { get; set; }
        public Nullable<int> NID { get; set; }
        public Nullable<int> TIN { get; set; }
        public string SpouseName { get; set; }
        public string SpouseProfession { get; set; }
      
        public string MailAddress { get; set; }
        public int ContactNumber { get; set; }
        public int EmergencyContactNumber { get; set; }
        public string PassportNumber { get; set; }
        public string DrivingLicenceNumber { get; set; }
        public string Hobby { get; set; }
        public string CreateBy { get; set; }
        public System.DateTime CreateDate {get;set;}
        public string UpdateBy { get; set; }
        public System.DateTime UpdateDate { get; set; }

       
        public string ImagePath { get; set; }
      

        public virtual Gender Gender { get; set;  }
        public virtual District District { get; set; }
        public virtual BloodGroup BloodGroup { get; set; }
        public virtual Nationality Nationality { get; set; }
        public virtual MaritalStatus MaritalStatus { get; set; }
        public virtual Region Region { get; set; }
        public virtual PressentAddress PressentAddress { get; set; }
        public virtual PermanentAddress PermanentAddress { get; set; }
        //public virtual ProfileFamilyDetails ProfileFamilyDetails { get; set; }
        public virtual ICollection<ProfileFamilyDetails> ProfileFamilyDetails { get; set; }



    }
}
