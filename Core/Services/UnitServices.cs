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
    public class UnitServices
    {
        private UnitOfWork unitOfWork;

        public UnitServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(UnitViewModel unitVM)
        {
            var Unit = new Unit
            {

                UnitName = unitVM.UnitName
            };

            unitOfWork.UnitRepository.Insert(Unit);
            unitOfWork.Save();
        }


        public void Update(UnitViewModel unitVM)
        {
            var Unit = new Unit
            {
                UnitId = unitVM.UnitId,
                UnitName = unitVM.UnitName
            };
            unitOfWork.UnitRepository.Update(Unit);
            unitOfWork.Save();
        }

        public UnitViewModel GetById(int id)
        {
            var data = (from s in unitOfWork.UnitRepository.Get()
                        where s.UnitId == id
                        select new UnitViewModel
                        {
                            UnitId = s.UnitId,
                            UnitName = s.UnitName
                        }).SingleOrDefault();
            return data;
        }
        public IEnumerable<UnitViewModel> GetAll()
        {
            var data = (from s in unitOfWork.UnitRepository.Get()
                        select new UnitViewModel
                        {

                            UnitId = s.UnitId,
                            UnitName = s.UnitName

                        }).AsEnumerable();
            return data;
        }

        public void Delete(int id)
        {
            var Unit = new Unit
            {
                UnitId = id
            };

            unitOfWork.UnitRepository.Delete(Unit);
            unitOfWork.Save();

        }
    }
}
