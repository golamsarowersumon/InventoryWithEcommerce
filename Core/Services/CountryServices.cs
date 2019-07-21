using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Repositories;
using Domain.ViewModels;

namespace Core.Services
{
   public class CountryServices
    {


        private UnitOfWork unitOfWork;

        public CountryServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(CountryViewModel countryViewModel)
        {
            var Country = new Country
            {

                CountryName = countryViewModel.CountryName
                
            };

            unitOfWork.CountryRepository.Insert(Country);
            unitOfWork.Save();
        }

        public void Update(CountryViewModel countryViewModel)
        {
            var Country = new Country
            {
                CountryId = countryViewModel.CountryId,
                CountryName = countryViewModel.CountryName
               
            };


            unitOfWork.CountryRepository.Update(Country);

            unitOfWork.Save();
        }


        public CountryViewModel GetByID(int id)
        {
            var data = (from s in unitOfWork.CountryRepository.Get()
                        where s.CountryId == id
                        select new CountryViewModel
                        {
                            CountryId = s.CountryId,
                            CountryName = s.CountryName
                           


                        }).SingleOrDefault();

            return data;
        }

        public IEnumerable<CountryViewModel> GetAll()
        {
            var data = (from s in unitOfWork.CountryRepository.Get()
                        select new CountryViewModel
                        {
                            CountryId = s.CountryId,
                            CountryName = s.CountryName
                          

                        }).AsEnumerable();

            return data;
        }


        public void Delete(int id)
        {
            var Country = new Country
            {

                CountryId = id
            };

            unitOfWork.CountryRepository.Delete(Country);
            unitOfWork.Save();
        }



        public IEnumerable<CountryViewModel> GetDropDown()
        {
            var data = (from s in unitOfWork.CountryRepository.Get()
                        select new CountryViewModel
                        {
                            CountryId = s.CountryId,
                            CountryName = s.CountryName
                        }).AsEnumerable();

            return data;
        }



        public IEnumerable<DropDownViewModel> GetDropDownValue()
        {
            var data = (from s in unitOfWork.CountryRepository.Get()
                        select new DropDownViewModel
                        {
                            Value = s.CountryId,
                            Text = s.CountryName
                        }).AsEnumerable();

            return data;
        }

    }
}
