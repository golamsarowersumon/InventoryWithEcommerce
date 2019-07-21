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
    public class WeatherServices
    {
        private UnitOfWork unitOfWork;

        public WeatherServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(WeatherViewModel weatherVM)
        {
            var Weather = new Weather
            {

                WeatherName = weatherVM.WeatherName
            };

            unitOfWork.WeatherRepository.Insert(Weather);
            unitOfWork.Save();
        }


        public void Update(WeatherViewModel weatherVM)
        {
            var Weather = new Weather
            {
                WeatherId = weatherVM.WeatherId,
                WeatherName = weatherVM.WeatherName
            };
            unitOfWork.WeatherRepository.Update(Weather);
            unitOfWork.Save();
        }

        public WeatherViewModel GetById(int id)
        {
            var data = (from s in unitOfWork.WeatherRepository.Get()
                        where s.WeatherId == id
                        select new WeatherViewModel
                        {
                            WeatherId = s.WeatherId,
                            WeatherName = s.WeatherName
                        }).SingleOrDefault();
            return data;
        }
        public IEnumerable<WeatherViewModel> GetAll()
        {
            var data = (from s in unitOfWork.WeatherRepository.Get()
                        select new WeatherViewModel
                        {

                            WeatherId = s.WeatherId,
                            WeatherName = s.WeatherName

                        }).AsEnumerable();
            return data;
        }

        public void Delete(int id)
        {
            var Weather = new Weather
            {
                WeatherId = id
            };

            unitOfWork.WeatherRepository.Delete(Weather);
            unitOfWork.Save();

        }
    }
}
