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
    public class UpazilaServices
    {
        private UnitOfWork unitOfWork;

        public UpazilaServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(UpazilaViewModel upazilaVM)
        {
            var Upazila = new Upazila
            {

                UpazilaName = upazilaVM.UpazilaName,
                DistrictId = upazilaVM.DistrictId
            };

            unitOfWork.UpazilaRepository.Insert(Upazila);
            unitOfWork.Save();
        }


        public void Update(UpazilaViewModel upazilaVM)
        {
            var Upazila = new Upazila
            {
                UpazilaId = upazilaVM.UpazilaId,
                UpazilaName = upazilaVM.UpazilaName
            };
            unitOfWork.UpazilaRepository.Update(Upazila);
            unitOfWork.Save();
        }

        public UpazilaViewModel GetById(int id)
        {
            var data = (from s in unitOfWork.UpazilaRepository.Get()
                        where s.UpazilaId == id
                        select new UpazilaViewModel
                        {
                            UpazilaId = s.UpazilaId,
                            UpazilaName = s.UpazilaName
                        }).SingleOrDefault();
            return data;
        }
        public IEnumerable<UpazilaViewModel> GetAll()
        {
            var data = (from s in unitOfWork.UpazilaRepository.Get()
                        select new UpazilaViewModel
                        {

                            UpazilaId = s.UpazilaId,
                            UpazilaName = s.UpazilaName,
                            DistrictName = s.District.DistrictName

                        }).AsEnumerable();
            return data;
        }

        public void Delete(int id)
        {
            var Upazila = new Upazila
            {
                UpazilaId = id
            };

            unitOfWork.UpazilaRepository.Delete(Upazila);
            unitOfWork.Save();

        }
    }
}
