using Domain.Repositories;
using Domain.Models;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
  public  class PoliceStationServices
    {


        private UnitOfWork unitOfWork;

        public PoliceStationServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(PoliceStationViewModel PoliceStationViewModel)
        {
            var PoliceStation = new PoliceStation
            {


                PoliceStationName = PoliceStationViewModel.PoliceStationName,
                DistrictId = PoliceStationViewModel.DistrictId



            };

            unitOfWork.PoliceStationRepository.Insert(PoliceStation);
            unitOfWork.Save();
        }

        public void Update(PoliceStationViewModel PoliceStationViewModel)
        {
            var PoliceStation = new PoliceStation
            {
                PoliceStationId= PoliceStationViewModel.PoliceStationId,
                PoliceStationName = PoliceStationViewModel.PoliceStationName,
                DistrictId = PoliceStationViewModel.DistrictId



            };


            unitOfWork.PoliceStationRepository.Update(PoliceStation);

            unitOfWork.Save();
        }


        public PoliceStationViewModel GetByID(int? id)
        {
            var data = (from s in unitOfWork.PoliceStationRepository.Get()
                        where s.PoliceStationId == id
                        select new PoliceStationViewModel
                        {
                            PoliceStationId = s.PoliceStationId,
                            PoliceStationName = s.PoliceStationName,
                            DistrictId = s.DistrictId,
                            DistrictName = s.District.DistrictName





                        }).SingleOrDefault();

            return data;
        }

        public IEnumerable<PoliceStationViewModel> GetAll()
        {
            var data = (from s in unitOfWork.PoliceStationRepository.Get()
                        select new PoliceStationViewModel
                        {
                            PoliceStationId = s.PoliceStationId,
                            PoliceStationName = s.PoliceStationName,
                            DistrictId = s.DistrictId,
                            DistrictName = s.District.DistrictName

                        }).AsEnumerable();

            return data;
        }


        public void Delete(int id)
        {
            var PoliceStation = new PoliceStation
            {

                PoliceStationId = id
            };

            unitOfWork.PoliceStationRepository.Delete(PoliceStation);
            unitOfWork.Save();
        }

        public IEnumerable<DropDownViewModel> GetDropDown()
        {
            var data = (from s in unitOfWork.PoliceStationRepository.Get()
                        select new DropDownViewModel
                        {
                            Value = s.PoliceStationId,
                            Text = s.PoliceStationName
                        }).AsEnumerable();

            return data;
        }


    }
}
