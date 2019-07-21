using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Domain.ViewModels
{
  public  class ProfileViewModel
    {

        [Key]
        public int ProfileId { get; set; }

        [DisplayName("Profile Name")]
        [Required(ErrorMessage ="Please Enter Profile Name..")]
        public string ProfileName { get; set; }

        [DisplayName("Father Name")]
        [Required(ErrorMessage = "Please Enter Your Father Name..")]
        public string FatherName { get; set; }

        [DisplayName("Mother Name")]
        [Required(ErrorMessage = "Please Enter Your Mother Name..")]
        public string MotherName { get; set; }

        [Display(Name ="Birth Date")]
        [Required(ErrorMessage = "Please Enter Your Birth Date..")]
        public System.DateTime DateofBirth { get; set; }
        [DisplayName("Birth Place")]
        [Required(ErrorMessage = "Please Enter Your Birth Place..")]
        public string BirthPlace { get; set; }
        //public Nullable<int> DistrictId { get; set; }

        [DisplayName("Gender")]
        [Required(ErrorMessage = "Please Select Your Gender ..")]
        public Nullable<int> Genderid { get; set; }
        [DisplayName("Blood Group")]
        public Nullable<int> BloodGroupId { get; set; }

        [DisplayName("Nationality")]
        public Nullable<int> NationalityID { get; set; }

        [DisplayName("Marital Status")]
        [Required(ErrorMessage = "Please Select Your Marital Status ..")]
        public Nullable<int> MaritalStatusId { get; set; }

        [DisplayName("Date Of Marriage")]
        public System.DateTime DateofMarriage { get; set; }

        [DisplayName("Region")]
        [Required(ErrorMessage = "Please Select Your Region..")]
        public Nullable<int> RegionId { get; set; }

        [DisplayName("NID")]
        public Nullable<int> NID { get; set; }
        [DisplayName("TIN")]
        public Nullable<int> TIN { get; set; }

     
        [DisplayName("MailAddress")]
        public string MailAddress { get; set; }

        [DisplayName("Contact Number")]
        public int ContactNumber { get; set; }

        [DisplayName("Em. Contact Number")]
        public int EmergencyContactNumber { get; set; }


        [DisplayName("Passport Number")]
        public string PassportNumber { get; set; }


        [DisplayName("Driving Licence Number")]
        public string DrivingLicenceNumber { get; set; }

        [DisplayName("Hobby")]
        public string Hobby { get; set; }

        [DisplayName("Create By")]
        public string CreateBy { get; set; }

        [DisplayName("Create Date")]
        public System.DateTime CreateDate { get; set; }

        [DisplayName("Update By")]
        public string UpdateBy { get; set; }
        [DisplayName("Update Date By")]
        public System.DateTime UpdateDate { get; set; }

        [Display(Name ="Current Address")]
        public string PresentAddressFull { get; set; }

        [Display(Name = "Police Station Name")]
        public Nullable<int> PrePoliceStationId { get; set; }
        [Display(Name = "Post Office Name")]
        public Nullable<int> PrePostOfficeId { get; set; }
        [Display(Name = "District Name")]
        public Nullable<int> PreDistrictId { get; set; }
        [Display(Name = "Division Name")]
        public Nullable<int> PreDivisionId { get; set; }
        [Display(Name = "Country Name")]
        public Nullable<int> PreCountryId { get; set; }


        [Display(Name = "Permanent Address")]
        public string PermanentAddressFull { get; set; }

        [Display(Name = "Police Station Name")]
        public Nullable<int> PerPoliceStationId { get; set; }
        [Display(Name = "Post Office Name")]
        public Nullable<int> PerPostOfficeId { get; set; }
        [Display(Name = "District Name")]
        public Nullable<int> PerDistrictId { get; set; }
        [Display(Name = "Division Name")]
        public Nullable<int> PerDivisionId { get; set; }
        [Display(Name = "Country Name")]
        public Nullable<int> PerCountryId { get; set; }

        [Display(Name = "Police Station Name")]
        public string PrePoliceStationName { get; set; }
        [Display(Name = "Post Office Name")]
        public string PrePostOfficeName { get; set; }
        [Display(Name = "District Name")]
        public string PreDistrictName { get; set; }
        [Display(Name = "Division Name")]
        public string PreDivisionName { get; set; }
        [Display(Name = "Country Name")]
        public string PreCountryName { get; set; }

        [Display(Name = "Police Station Name")]
        public string PerPoliceStationName { get; set; }
        [Display(Name = "Post Office Name")]
        public string PerPostOfficeName { get; set; }
        [Display(Name = "District Name")]
        public string PerDistrictName { get; set; }
        [Display(Name = "Division Name")]
        public string PerDivisionName { get; set; }
        [Display(Name = "Country Name")]
        public string PerCountryName { get; set; }

        [Display(Name ="Image")]
        public string ImagePath { get; set; }




        [Display(Name = "Gender Name")]
        public string GenderName { get; set; }
        [Display(Name = "Blood Group Name")]
        public string BloodGroupName { get; set; }
        [Display(Name = "Nationality Name")]
        public string NationalityName { get; set; }
        [Display(Name = "MaritalStatus Name")]
        public string MaritalStatusName { get; set; }
        [Display(Name = "Region Name")]
        public string RegionName { get; set; }




        public int ProfileDetailsId { get; set; }
        [Display(Name = "Spouse Name")]
        public string SpouseName { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime SpouseDOB { get; set; }

        [Display(Name = "Spouse Profession")]
        public string SpouseProfession { get; set; }

        [Display(Name = "Spouse Gender Name")]
        public int SpouseGenderId { get; set; }

        [Display(Name = "No. of Spouse")]
        public int SlNoOfMarriage { get; set; }

        [Display(Name = "Children Name")]
        public string ChildName { get; set; }
        [Display(Name = "Children DOB")]
        public DateTime ChildDOB { get; set; }

        [Display(Name = "Child Profession")]
        public string ChildProfession { get; set; }

        [Display(Name = "Child No.")]
        public int SlNoOfChild { get; set; }

        [Display(Name = "Spouse Gender Name")]
        public string SpouseGenderName { get; set; }

        [Display(Name = "Children Gender Name")]
        public string ChildGendername { get; set; }







    }

   
}
