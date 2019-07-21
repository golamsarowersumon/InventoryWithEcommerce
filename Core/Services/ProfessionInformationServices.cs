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
    public class ProfessionInformationServices
    {
        private UnitOfWork unitOfWork;

        public ProfessionInformationServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(ProfessionInformationViewModel professionInformationVM)
        {
            var ProfessionInformation = new ProfessionInformation
            {

                ProfessionName = professionInformationVM.ProfessionName
            };

            unitOfWork.ProfessionInformationRepository.Insert(ProfessionInformation);
            unitOfWork.Save();
        }


        public void Update(ProfessionInformationViewModel professionInformationVM)
        {
            var ProfessionInformation = new ProfessionInformation
            {
                ProfessionId = professionInformationVM.ProfessionId,
                ProfessionName = professionInformationVM.ProfessionName
            };
            unitOfWork.ProfessionInformationRepository.Update(ProfessionInformation);
            unitOfWork.Save();
        }

        public ProfessionInformationViewModel GetById(int id)
        {
            var data = (from s in unitOfWork.ProfessionInformationRepository.Get()
                        where s.ProfessionId == id
                        select new ProfessionInformationViewModel
                        {
                            ProfessionId = s.ProfessionId,
                            ProfessionName = s.ProfessionName
                        }).SingleOrDefault();
            return data;
        }
        public IEnumerable<ProfessionInformationViewModel> GetAll()
        {
            var data = (from s in unitOfWork.ProfessionInformationRepository.Get()
                        select new ProfessionInformationViewModel
                        {

                            ProfessionId = s.ProfessionId,
                            ProfessionName = s.ProfessionName

                        }).AsEnumerable();
            return data;
        }

        public void Delete(int id)
        {
            var ProfessionInformation = new ProfessionInformation
            {
                ProfessionId = id
            };

            unitOfWork.ProfessionInformationRepository.Delete(ProfessionInformation);
            unitOfWork.Save();

        }
    }
}
