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
    public class WarrantyServices
    {
        private UnitOfWork unitOfWork;

        public WarrantyServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(WarrantyViewModel warrantyVM)
        {
            var Warranty = new Warranty
            {

                WarrantyPeriod = warrantyVM.WarrantyName
            };

            unitOfWork.WarrantyRepository.Insert(Warranty);
            unitOfWork.Save();
        }


        public void Update(WarrantyViewModel warrantyVM)
        {
            var Warranty = new Warranty
            {
                WarrantyId = warrantyVM.WarrantyId,
                WarrantyPeriod = warrantyVM.WarrantyName
            };
            unitOfWork.WarrantyRepository.Update(Warranty);
            unitOfWork.Save();
        }

        public WarrantyViewModel GetById(int id)
        {
            var data = (from s in unitOfWork.WarrantyRepository.Get()
                        where s.WarrantyId == id
                        select new WarrantyViewModel
                        {
                            WarrantyId = s.WarrantyId,
                            WarrantyName = s.WarrantyPeriod
                        }).SingleOrDefault();
            return data;
        }
        public IEnumerable<WarrantyViewModel> GetAll()
        {
            var data = (from s in unitOfWork.WarrantyRepository.Get()
                        select new WarrantyViewModel
                        {

                            WarrantyId = s.WarrantyId,
                            WarrantyName = s.WarrantyPeriod

                        }).AsEnumerable();
            return data;
        }

        public void Delete(int id)
        {
            var Warranty = new Warranty
            {
                WarrantyId = id
            };

            unitOfWork.WarrantyRepository.Delete(Warranty);
            unitOfWork.Save();

        }
    }
}
