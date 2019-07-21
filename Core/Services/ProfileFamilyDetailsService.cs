using Domain.Models;
using Domain.Repositories;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
   public class ProfileFamilyDetailsService
    {
        private UnitOfWork unitOfWork;

        public ProfileFamilyDetailsService(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }


        public void Create(ProfileViewModel profileFamilyDetailsVm)
        {
            var profileFamilyDetails = new ProfileFamilyDetails
            {

                ProfileId = profileFamilyDetailsVm.ProfileId,
                SpouseName= profileFamilyDetailsVm.SpouseName,
                SpouseDOB = profileFamilyDetailsVm.SpouseDOB,
                SpouseProfession = profileFamilyDetailsVm.SpouseProfession,
                SpouseGenderId = profileFamilyDetailsVm.SpouseGenderId,
                SlNoOfMarriage= profileFamilyDetailsVm.SlNoOfMarriage,
                ChildName = profileFamilyDetailsVm.ChildName,
                ChildDOB = profileFamilyDetailsVm.ChildDOB,
                ChildProfession = profileFamilyDetailsVm.ChildProfession,
                GenderID = profileFamilyDetailsVm.Genderid,
                SlNoOfChild = profileFamilyDetailsVm.SlNoOfChild
            };

            unitOfWork.profileFamilyDetailsRepository.Insert(profileFamilyDetails);
            unitOfWork.Save();
        }

        public void Update(ProfileViewModel profileFamilyDetailsVm)
        {
            var profileFamilyDetails = new ProfileFamilyDetails
            {
                ProfileDetailsId = profileFamilyDetailsVm.ProfileDetailsId,
                ProfileId = profileFamilyDetailsVm.ProfileId,
                SpouseName = profileFamilyDetailsVm.SpouseName,
                SpouseDOB = profileFamilyDetailsVm.SpouseDOB,
                SpouseProfession = profileFamilyDetailsVm.SpouseProfession,
                SpouseGenderId = profileFamilyDetailsVm.SpouseGenderId,
                SlNoOfMarriage = profileFamilyDetailsVm.SlNoOfMarriage,
                ChildName = profileFamilyDetailsVm.ChildName,
                ChildDOB = profileFamilyDetailsVm.ChildDOB,
                ChildProfession = profileFamilyDetailsVm.ChildProfession,
                GenderID = profileFamilyDetailsVm.Genderid,
                SlNoOfChild = profileFamilyDetailsVm.SlNoOfChild
            };
            unitOfWork.profileFamilyDetailsRepository.Update(profileFamilyDetails);
            unitOfWork.Save();
        }

        public ProfileViewModel GetById(int id)
        {
            var data = (from s in unitOfWork.profileFamilyDetailsRepository.Get()
                        where s.ProfileDetailsId == id
                        select new ProfileViewModel
                        {
                            ProfileDetailsId = s.ProfileDetailsId,
                            ProfileId = s.ProfileId,
                            SpouseName = s.SpouseName,
                            SpouseDOB = s.SpouseDOB,
                            SpouseProfession = s.SpouseProfession,
                            SpouseGenderId = s.SpouseGenderId,
                            SlNoOfMarriage = s.SlNoOfMarriage,
                            ChildName = s.ChildName,
                            ChildDOB = s.ChildDOB,
                            ChildProfession = s.ChildProfession,
                            Genderid = s.GenderID,
                            SlNoOfChild = s.SlNoOfChild,
                            ChildGendername = s.Gender.GenderName,
                            SpouseGenderName = s.Gender.GenderName

                        }).SingleOrDefault();
            return data;
        }

        public IEnumerable<ProfileViewModel> GetAll()
        {
            var data = (from s in unitOfWork.profileFamilyDetailsRepository.Get()
                        select new ProfileViewModel
                        {

                            ProfileDetailsId = s.ProfileDetailsId,
                            ProfileId = s.ProfileId,
                            SpouseName = s.SpouseName,
                            SpouseDOB = s.SpouseDOB,
                            SpouseProfession = s.SpouseProfession,
                            SpouseGenderId = s.SpouseGenderId,
                            SlNoOfMarriage = s.SlNoOfMarriage,
                            ChildName = s.ChildName,
                            ChildDOB = s.ChildDOB,
                            ChildProfession = s.ChildProfession,
                            Genderid = s.GenderID,
                            SlNoOfChild = s.SlNoOfChild

                        }).AsEnumerable();
            return data;
        }


        public void Delete(int id)
        {
            var profileFamilyDetails = new ProfileFamilyDetails
            {
                ProfileDetailsId = id
            };




            unitOfWork.profileFamilyDetailsRepository.Delete(profileFamilyDetails);
            unitOfWork.Save();
        }
    }
}
