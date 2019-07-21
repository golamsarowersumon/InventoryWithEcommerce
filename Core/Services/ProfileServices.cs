using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Domain.Models;
using Domain.ViewModels;
using Domain.Repositories;


namespace Core.Services
{
   public class ProfileServices
    {


        private UnitOfWork unitOfWork;

        public ProfileServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(ProfileViewModel ProfileViewModel)
        {
            var Profile = new Profile
            {

                ProfileName=ProfileViewModel.ProfileName,
                FatherName=ProfileViewModel.FatherName,
                MotherName=ProfileViewModel.MotherName,
                DateofBirth=ProfileViewModel.DateofBirth,
                BirthPlace=ProfileViewModel.BirthPlace,
                Genderid=ProfileViewModel.Genderid,
                BloodGroupId=ProfileViewModel.BloodGroupId,
                NationalityID=ProfileViewModel.NationalityID,
                MaritalStatusId=ProfileViewModel.MaritalStatusId,
                DateofMarriage=ProfileViewModel.DateofMarriage,
                RegionId=ProfileViewModel.RegionId,
                NID=ProfileViewModel.NID,
                TIN=ProfileViewModel.TIN,
                SpouseName=ProfileViewModel.SpouseName,
                SpouseProfession=ProfileViewModel.SpouseProfession,
                MailAddress=ProfileViewModel.MailAddress,
                ContactNumber=ProfileViewModel.ContactNumber,
                EmergencyContactNumber=ProfileViewModel.EmergencyContactNumber,
                PassportNumber=ProfileViewModel.PassportNumber,
                DrivingLicenceNumber=ProfileViewModel.DrivingLicenceNumber,
                Hobby=ProfileViewModel.Hobby,
                CreateBy=ProfileViewModel.CreateBy,
                CreateDate=ProfileViewModel.CreateDate,
                UpdateBy=ProfileViewModel.UpdateBy,
                UpdateDate=ProfileViewModel.UpdateDate,
                ImagePath=ProfileViewModel.ImagePath


            };



            var PressentAddress = new PressentAddress
            {
                ProfileId= ProfileViewModel.ProfileId,
                
                PresentAddressFull=ProfileViewModel.PresentAddressFull,
                PrePostOfficeId=ProfileViewModel.PrePostOfficeId,
                PrePoliceStationId=ProfileViewModel.PrePoliceStationId,
                PreDistrictId=ProfileViewModel.PreDistrictId,
                PreDivisionId=ProfileViewModel.PreDivisionId,
                PreCountryId=ProfileViewModel.PreCountryId



            };

            var PermanentAddress = new PermanentAddress {

                ProfileId = ProfileViewModel.ProfileId,

                PermanentAddressFull = ProfileViewModel.PermanentAddressFull,
                PerPostOfficeId = ProfileViewModel.PerPostOfficeId,
                PerPoliceStationId = ProfileViewModel.PerPoliceStationId,
                PerDistrictId = ProfileViewModel.PerDistrictId,
                PerDivisionId = ProfileViewModel.PerDivisionId,
                PerCountryId = ProfileViewModel.PerCountryId

            };




            unitOfWork.ProfileRepository.Insert(Profile);
            unitOfWork.PressentAddressRepository.Insert(PressentAddress);
            unitOfWork.PermanentAddressRepository.Insert(PermanentAddress);
        
            unitOfWork.Save();
        }

        public void Update(ProfileViewModel ProfileViewModel)
        {
            var Profile = new Profile
            {
                ProfileId=ProfileViewModel.ProfileId,
                ProfileName = ProfileViewModel.ProfileName,
                FatherName = ProfileViewModel.FatherName,
                MotherName = ProfileViewModel.MotherName,
                DateofBirth = ProfileViewModel.DateofBirth,
                BirthPlace = ProfileViewModel.BirthPlace,
                Genderid = ProfileViewModel.Genderid,
                BloodGroupId = ProfileViewModel.BloodGroupId,
                NationalityID = ProfileViewModel.NationalityID,
                MaritalStatusId = ProfileViewModel.MaritalStatusId,
                DateofMarriage = ProfileViewModel.DateofMarriage,
                RegionId = ProfileViewModel.RegionId,
                NID = ProfileViewModel.NID,
                TIN = ProfileViewModel.TIN,
                SpouseName = ProfileViewModel.SpouseName,
                SpouseProfession = ProfileViewModel.SpouseProfession,
                MailAddress = ProfileViewModel.MailAddress,
                ContactNumber = ProfileViewModel.ContactNumber,
                EmergencyContactNumber = ProfileViewModel.EmergencyContactNumber,
                PassportNumber = ProfileViewModel.PassportNumber,
                DrivingLicenceNumber = ProfileViewModel.DrivingLicenceNumber,
                Hobby = ProfileViewModel.Hobby,
                CreateBy = ProfileViewModel.CreateBy,
                CreateDate = ProfileViewModel.CreateDate,
                UpdateBy = ProfileViewModel.UpdateBy,
                UpdateDate = ProfileViewModel.UpdateDate,
                ImagePath = ProfileViewModel.ImagePath

            };


            unitOfWork.ProfileRepository.Update(Profile);

            unitOfWork.Save();
        }


        public ProfileViewModel GetByID(int id)
        {
            var data = (from s in unitOfWork.ProfileRepository.Get()
                        join bl in unitOfWork.BloodGroupRepository.Get() on s.BloodGroupId equals bl.BloodGroupId
                        join gn in unitOfWork.GenderRepository.Get() on s.Genderid equals gn.GenderID
                        join ms in unitOfWork.MaritalStatusRepository.Get() on s.MaritalStatusId equals ms.MaritalStatusId
                        join Na in unitOfWork.NationalityRepositoy.Get() on s.NationalityID equals Na.NationalityID
                        join Rn in unitOfWork.RegionRepository.Get() on s.RegionId equals Rn.RegionId
                        join t in unitOfWork.PressentAddressRepository.Get() on s.ProfileId equals t.ProfileId
                        join u in unitOfWork.PermanentAddressRepository.Get() on s.ProfileId equals u.ProfileId
                        join po in unitOfWork.PoliceStationRepository.Get() on t.PrePoliceStationId equals po.PoliceStationId
                        join post in unitOfWork.PostOfficeRepository.Get() on t.PrePostOfficeId equals post.PostOfficeId
                        join Dis in unitOfWork.DistrictRepository.Get() on t.PreDistrictId equals Dis.DistrictId
                        join Div in unitOfWork.DivisionRepository.Get() on t.PreDivisionId equals Div.DivisionId
                        join Co in unitOfWork.CountryRepository.Get() on t.PreCountryId equals Co.CountryId

                        join pol in unitOfWork.PoliceStationRepository.Get() on u.PerPoliceStationId equals pol.PoliceStationId
                        join pos in unitOfWork.PostOfficeRepository.Get() on u.PerPostOfficeId equals pos.PostOfficeId
                        join Dist in unitOfWork.DistrictRepository.Get() on u.PerDistrictId equals Dist.DistrictId
                        join Divi in unitOfWork.DivisionRepository.Get() on u.PerDivisionId equals Divi.DivisionId
                        join Cou in unitOfWork.CountryRepository.Get() on u.PerCountryId equals Cou.CountryId
                        where s.ProfileId==t.ProfileId && s.ProfileId==u.ProfileId && s.ProfileId==id
                        select new ProfileViewModel
                        {
                            ProfileId = s.ProfileId,
                            ProfileName = s.ProfileName,
                            FatherName = s.FatherName,
                            MotherName = s.MotherName,
                            DateofBirth = s.DateofBirth,
                            BirthPlace = s.BirthPlace,
                            Genderid = s.Genderid,
                            GenderName = gn.GenderName,
                            BloodGroupId = s.BloodGroupId,
                            BloodGroupName = bl.BloodGroupName,
                            NationalityID = s.NationalityID,
                            NationalityName = Na.NationalityName,
                            MaritalStatusId = s.MaritalStatusId,
                            MaritalStatusName = ms.MaritalStatusName,
                            DateofMarriage = s.DateofMarriage,
                            RegionId = s.RegionId,
                            RegionName = Rn.RegionName,
                            NID = s.NID,
                            TIN = s.TIN,
                            SpouseName = s.SpouseName,
                            SpouseProfession = s.SpouseProfession,
                            MailAddress = s.MailAddress,
                            ContactNumber = s.ContactNumber,
                            EmergencyContactNumber = s.EmergencyContactNumber,
                            PassportNumber = s.PassportNumber,
                            DrivingLicenceNumber = s.DrivingLicenceNumber,
                            Hobby = s.Hobby,
                            CreateBy = s.CreateBy,
                            CreateDate =s.CreateDate,
                            UpdateBy = s.UpdateBy,
                            UpdateDate = s.UpdateDate,
                            ImagePath = s.ImagePath,
                            PresentAddressFull = t.PresentAddressFull,
                            PrePostOfficeId = t.PrePostOfficeId,
                            PrePostOfficeName = post.PostOfficeName,
                            PrePoliceStationId = t.PrePoliceStationId,
                            PrePoliceStationName = po.PoliceStationName,
                            PreDistrictId = t.PreDistrictId,
                            PreDistrictName = Dis.DistrictName,
                            PreDivisionId = t.PreDivisionId,
                            PreDivisionName = Div.DivisionName,
                            PreCountryId = t.PreCountryId,
                            PreCountryName = Co.CountryName,
                            PermanentAddressFull = u.PermanentAddressFull,
                            PerPostOfficeId = u.PerPostOfficeId,
                            PerPostOfficeName = pos.PostOfficeName,
                            PerPoliceStationId = u.PerPoliceStationId,
                            PerPoliceStationName = pol.PoliceStationName,
                            PerDistrictId = u.PerDistrictId,
                            PerDistrictName = Dist.DistrictName,
                            PerDivisionId = u.PerDivisionId,
                            PerDivisionName = Divi.DivisionName,
                            PerCountryId = u.PerCountryId,
                            PerCountryName = Cou.CountryName,
                            
                  



                        }).SingleOrDefault();

            return data;
        }

        public IEnumerable<ProfileViewModel> GetAll()
        {
            var data = (from s in unitOfWork.ProfileRepository.Get()
                        join bl in unitOfWork.BloodGroupRepository.Get() on s.BloodGroupId equals bl.BloodGroupId
                        join gn in unitOfWork.GenderRepository.Get() on s.Genderid equals gn.GenderID
                        join ms in unitOfWork.MaritalStatusRepository.Get() on s.MaritalStatusId equals ms.MaritalStatusId
                        join Na in unitOfWork.NationalityRepositoy.Get() on s.NationalityID equals Na.NationalityID
                        join Rn in unitOfWork.RegionRepository.Get() on s.RegionId equals Rn.RegionId
                        join t in unitOfWork.PressentAddressRepository.Get() on s.ProfileId equals t.ProfileId
                        join u in unitOfWork.PermanentAddressRepository.Get() on s.ProfileId equals u.ProfileId
                        join po in unitOfWork.PoliceStationRepository.Get() on t.PrePoliceStationId equals po.PoliceStationId
                        join post in unitOfWork.PostOfficeRepository.Get() on t.PrePostOfficeId equals post.PostOfficeId
                        join Dis in unitOfWork.DistrictRepository.Get() on t.PreDistrictId equals Dis.DistrictId
                        join Div in unitOfWork.DivisionRepository.Get() on t.PreDivisionId equals Div.DivisionId
                        join Co in unitOfWork.CountryRepository.Get() on t.PreCountryId equals Co.CountryId

                        join pol in unitOfWork.PoliceStationRepository.Get() on u.PerPoliceStationId equals pol.PoliceStationId
                        join pos in unitOfWork.PostOfficeRepository.Get() on u.PerPostOfficeId equals pos.PostOfficeId
                        join Dist in unitOfWork.DistrictRepository.Get() on u.PerDistrictId equals Dist.DistrictId
                        join Divi in unitOfWork.DivisionRepository.Get() on u.PerDivisionId equals Divi.DivisionId
                        join Cou in unitOfWork.CountryRepository.Get() on u.PerCountryId equals Cou.CountryId


                        select new ProfileViewModel
                        {

                            ProfileId = s.ProfileId,
                            ProfileName = s.ProfileName,
                            FatherName = s.FatherName,
                            MotherName = s.MotherName,
                            DateofBirth = s.DateofBirth,
                            BirthPlace = s.BirthPlace,
                            Genderid = s.Genderid,
                            GenderName = gn.GenderName,
                            BloodGroupId = s.BloodGroupId,
                            BloodGroupName = bl.BloodGroupName,
                            NationalityID = s.NationalityID,
                            NationalityName = Na.NationalityName,
                            MaritalStatusId = s.MaritalStatusId,
                            MaritalStatusName = ms.MaritalStatusName,
                            DateofMarriage = s.DateofMarriage,
                            RegionId = s.RegionId,
                            RegionName = Rn.RegionName,
                            NID = s.NID,
                            TIN = s.TIN,
                            SpouseName = s.SpouseName,
                            SpouseProfession = s.SpouseProfession,
                            MailAddress = s.MailAddress,
                            ContactNumber = s.ContactNumber,
                            EmergencyContactNumber = s.EmergencyContactNumber,
                            PassportNumber = s.PassportNumber,
                            DrivingLicenceNumber = s.DrivingLicenceNumber,
                            Hobby = s.Hobby,
                            CreateBy = s.CreateBy,
                            CreateDate = s.CreateDate,
                            UpdateBy = s.UpdateBy,
                            UpdateDate = s.UpdateDate,
                            ImagePath = s.ImagePath,
                            PresentAddressFull = t.PresentAddressFull,
                            PrePostOfficeId = t.PrePostOfficeId,
                            PrePostOfficeName = post.PostOfficeName,
                            PrePoliceStationId = t.PrePoliceStationId,
                            PrePoliceStationName = po.PoliceStationName,
                            PreDistrictId = t.PreDistrictId,
                            PreDistrictName = Dis.DistrictName,
                            PreDivisionId = t.PreDivisionId,
                            PreDivisionName = Div.DivisionName,
                            PreCountryId = t.PreCountryId,
                            PreCountryName = Co.CountryName,
                            PermanentAddressFull = u.PermanentAddressFull,
                            PerPostOfficeId = u.PerPostOfficeId,
                            PerPostOfficeName = pos.PostOfficeName,
                            PerPoliceStationId = u.PerPoliceStationId,
                            PerPoliceStationName = pol.PoliceStationName,
                            PerDistrictId = u.PerDistrictId,
                            PerDistrictName =Dist.DistrictName,
                            PerDivisionId = u.PerDivisionId,
                            PerDivisionName = Divi.DivisionName,
                            PerCountryId = u.PerCountryId,
                            PerCountryName = Cou.CountryName

                        }).AsEnumerable();

            return data;
        }


        public void Delete(int id)
        {
            var Profile = new Profile
            {

                ProfileId=id
            };

            unitOfWork.ProfileRepository.Delete(Profile);
            unitOfWork.Save();
        }



        public IEnumerable<ProfileViewModel> GetDropDown()
        {
            var data = (from s in unitOfWork.ProfileRepository.Get()
                        select new ProfileViewModel
                        {
                            ProfileId = s.ProfileId,
                            ProfileName = s.ProfileName
                        }).AsEnumerable();


            return data;
        }


    }
}
