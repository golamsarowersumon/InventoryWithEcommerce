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
   public class ProvinceServices
    {

        private UnitOfWork unitOfWork;

        public ProvinceServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(ProvinceViewModel ProvinceViewModel)
        {
            var Province = new Province
            {

                ProvinceName = ProvinceViewModel.ProvinceName,
                CountryID = ProvinceViewModel.CountryID


            };

            unitOfWork.ProvinceRepository.Insert(Province);
            unitOfWork.Save();
        }

        public void Update(ProvinceViewModel ProvinceViewModel)
        {
            var Province = new Province
            {
                ProvinceID = ProvinceViewModel.ProvinceID,
                ProvinceName = ProvinceViewModel.ProvinceName,
                CountryID = ProvinceViewModel.CountryID


            };


            unitOfWork.ProvinceRepository.Update(Province);

            unitOfWork.Save();
        }


        public ProvinceViewModel GetByID(int? id)
        {
            var data = (from s in unitOfWork.ProvinceRepository.Get()
                        where s.ProvinceID == id
                        select new ProvinceViewModel
                        {
                            ProvinceID = s.ProvinceID,
                            ProvinceName = s.ProvinceName,
                            CountryID= s.CountryID,
                            CountryName = s.Country.CountryName





                        }).SingleOrDefault();

            return data;
        }

        public IEnumerable<ProvinceViewModel> GetAll()
        {
            var data = (from s in unitOfWork.ProvinceRepository.Get()
                        select new ProvinceViewModel
                        {
                            ProvinceID = s.ProvinceID,
                            ProvinceName = s.ProvinceName,
                            CountryID = s.CountryID,
                            CountryName = s.Country.CountryName


                        }).AsEnumerable();

            return data;
        }


        public void Delete(int id)
        {
            var Province = new Province
            {

                ProvinceID = id
            };

            unitOfWork.ProvinceRepository.Delete(Province);
            unitOfWork.Save();
        }

    }
}
