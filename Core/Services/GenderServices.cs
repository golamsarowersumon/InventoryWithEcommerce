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
  public  class GenderServices
    {

        private UnitOfWork unitOfWork;


        public GenderServices(UnitOfWork _unitOfWork) {

            unitOfWork = _unitOfWork;
        }

        public void Create(GenderViewModel genderViewModel) {
            var Gender = new Gender {

                GenderName=genderViewModel.GenderName

            };

            unitOfWork.GenderRepository.Insert(Gender);
            unitOfWork.Save();
        }


        public void Update(GenderViewModel genderViewModel) {
            var Gender = new Gender {
                GenderID = genderViewModel.GenderID,
            GenderName = genderViewModel.GenderName

            };

            unitOfWork.GenderRepository.Update(Gender);
            unitOfWork.Save();

        }

        public GenderViewModel GetByID(int id) {

            var data = (from s in unitOfWork.GenderRepository.Get()
                        where s.GenderID == id
                        select new GenderViewModel
                        {
                            GenderID = s.GenderID,
                            GenderName = s.GenderName

                        }).SingleOrDefault();
            return data;
}

        public IEnumerable<GenderViewModel> GetAll() {

            var data = (from s in unitOfWork.GenderRepository.Get()
                        select new GenderViewModel
                        {
                            GenderID=s.GenderID,
                            GenderName=s.GenderName

                        });

            return data;


        }


        public void Delete(int id)
        {
            var Gender = new Gender
            {

                GenderID = id
            };

            unitOfWork.GenderRepository.Delete(Gender);
            unitOfWork.Save();
        }

        public IEnumerable<DropDownViewModel> GetDropDown()
        {
            var data = (from s in unitOfWork.GenderRepository.Get()
                        select new DropDownViewModel
                        {
                            Value = s.GenderID,
                            Text = s.GenderName
                        }).AsEnumerable();

            return data;
        }


    }
}
