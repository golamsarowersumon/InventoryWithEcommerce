using Domain.Repositories;
using Domain.ViewModels;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class BloodGroupServices
    {
        private UnitOfWork unitOfWork;

        public BloodGroupServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(BloodGroupViewModel bloodGroupVM)
        {
            var BloodGroup = new BloodGroup
            {

                BloodGroupName = bloodGroupVM.BloodGroupName
            };

            unitOfWork.BloodGroupRepository.Insert(BloodGroup);
            unitOfWork.Save();
        }


        public void Update(BloodGroupViewModel bloodGroupVM)
        {
            var BloodGroup = new BloodGroup
            {
                BloodGroupId = bloodGroupVM.BloodGroupId,
                BloodGroupName = bloodGroupVM.BloodGroupName
            };
            unitOfWork.BloodGroupRepository.Update(BloodGroup);
            unitOfWork.Save();
        }

        public BloodGroupViewModel GetById(int id)
        {
            var data = (from s in unitOfWork.BloodGroupRepository.Get()
                        where s.BloodGroupId == id
                        select new BloodGroupViewModel
                        {
                            BloodGroupId = s.BloodGroupId,
                            BloodGroupName = s.BloodGroupName
                        }).SingleOrDefault();
            return data;
        }
        public IEnumerable<BloodGroupViewModel> GetAll()
        {
            var data = (from s in unitOfWork.BloodGroupRepository.Get()
                        select new BloodGroupViewModel
                        {

                            BloodGroupId = s.BloodGroupId,
                            BloodGroupName = s.BloodGroupName

                        }).AsEnumerable();
            return data;
        }

        public void Delete(int id)
        {
            var BloodGroup = new BloodGroup
            {
                BloodGroupId = id
            };

            unitOfWork.BloodGroupRepository.Delete(BloodGroup);
            unitOfWork.Save();

        }



        public IEnumerable<DropDownViewModel> GetDropDown()
        {
            var data = (from s in unitOfWork.BloodGroupRepository.Get()
                        select new DropDownViewModel
                        {
                            Value = s.BloodGroupId,
                            Text = s.BloodGroupName
                        }).AsEnumerable();

            return data;
        }



    }
}
