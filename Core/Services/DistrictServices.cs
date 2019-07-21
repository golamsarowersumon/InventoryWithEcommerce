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
    public class DistrictServices
    {
        private UnitOfWork unitOfWork;

        public DistrictServices(UnitOfWork _uniOfWork)
        {

            unitOfWork = _uniOfWork;
        }


        public void Create(DistrictViewModel DistrictViewModel)
        {
            var District = new District
            {
                DistrictName = DistrictViewModel.DistrictName,
                CountryId = DistrictViewModel.CountryId,
                ShippingCharge = DistrictViewModel.ShippingCharge

            };

            unitOfWork.DistrictRepository.Insert(District);
            unitOfWork.Save();

        }


        public void Update(DistrictViewModel DistrictViewModel)
        {

            var District = new District
            {

                DistrictId = DistrictViewModel.DistrictId,
                DistrictName = DistrictViewModel.DistrictName

            };

            unitOfWork.DistrictRepository.Update(District);
            unitOfWork.Save();
        }


        public DistrictViewModel GetByID(int id)
        {

            var data = (from s in unitOfWork.DistrictRepository.Get()
                        where s.DistrictId == id
                        select new DistrictViewModel
                        {
                            DistrictId = s.DistrictId,
                            DistrictName = s.DistrictName



                        }).SingleOrDefault();
            return data;
        }


        public IEnumerable<DistrictViewModel> GetAll()
        {

            var data = (from s in unitOfWork.DistrictRepository.Get()
                        select new DistrictViewModel
                        {
                            DistrictId = s.DistrictId,
                            DistrictName = s.DistrictName,
                            CountryName = s.Country.CountryName,
                            ShippingCharge = s.ShippingCharge

                        }).AsEnumerable();

            return data;

        }


        public void Delete(int id)
        {

            var District = new District
            {
                DistrictId = id
            };

            unitOfWork.DistrictRepository.Delete(District);
            unitOfWork.Save();
        }



        public IEnumerable<DropDownViewModel> GetDropDown()
        {
            var data = (from s in unitOfWork.DistrictRepository.Get()
                        select new DropDownViewModel
                        {
                            Value = s.DistrictId,
                            Text = s.DistrictName
                        }).AsEnumerable();

            return data;
        }

    }

}
