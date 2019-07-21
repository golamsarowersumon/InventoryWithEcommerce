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
    public class ProcrurementTypeServices
    {
        private UnitOfWork unitOfWork;

        public ProcrurementTypeServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(ProcurementTypeViewModel procurementVM)
        {
            var Procurement = new ProcurementType
            {

                ProcurementTypeName = procurementVM.ProcurementTypeName
            };

            unitOfWork.ProcurementTypeRepository.Insert(Procurement);
            unitOfWork.Save();
        }


        public void Update(ProcurementTypeViewModel procurementVM)
        {
            var Procurement = new ProcurementType
            {
                ProcurementTypeId = procurementVM.ProcurementTypeId,
                ProcurementTypeName = procurementVM.ProcurementTypeName
            };
            unitOfWork.ProcurementTypeRepository.Update(Procurement);
            unitOfWork.Save();
        }

        public ProcurementTypeViewModel GetById(int id)
        {
            var data = (from s in unitOfWork.ProcurementTypeRepository.Get()
                        where s.ProcurementTypeId == id
                        select new ProcurementTypeViewModel
                        {
                            ProcurementTypeId = s.ProcurementTypeId,
                            ProcurementTypeName = s.ProcurementTypeName
                        }).SingleOrDefault();
            return data;
        }
        public IEnumerable<ProcurementTypeViewModel> GetAll()
        {
            var data = (from s in unitOfWork.ProcurementTypeRepository.Get()
                        select new ProcurementTypeViewModel
                        {

                            ProcurementTypeId = s.ProcurementTypeId,
                            ProcurementTypeName = s.ProcurementTypeName

                        }).AsEnumerable();
            return data;
        }


        public void Delete(int id)
        {
            var Procurement = new ProcurementType
            {
                ProcurementTypeId = id
            };


           

            unitOfWork.ProcurementTypeRepository.Delete(Procurement);
            unitOfWork.Save();
        }

    }
}
