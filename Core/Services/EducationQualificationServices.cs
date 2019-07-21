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
    public class EducationQualificationServices
    {
        private UnitOfWork unitOfWork;

        public EducationQualificationServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(EducationQualificationViewModel educationQualificationVM)
        {
            var EducationQualification = new EducationQualification
            {

                EducationQualificationName = educationQualificationVM.EducationQualificationName
            };

            unitOfWork.EducationQualificationRepository.Insert(EducationQualification);
            unitOfWork.Save();
        }


        public void Update(EducationQualificationViewModel educationQualificationVM)
        {
            var EducationQualification = new EducationQualification
            {
                EducationQualificationId = educationQualificationVM.EducationQualificationId,
                EducationQualificationName = educationQualificationVM.EducationQualificationName
            };
            unitOfWork.EducationQualificationRepository.Update(EducationQualification);
            unitOfWork.Save();
        }

        public EducationQualificationViewModel GetById(int id)
        {
            var data = (from s in unitOfWork.EducationQualificationRepository.Get()
                        where s.EducationQualificationId == id
                        select new EducationQualificationViewModel
                        {
                            EducationQualificationId = s.EducationQualificationId,
                            EducationQualificationName = s.EducationQualificationName
                        }).SingleOrDefault();
            return data;
        }
        public IEnumerable<EducationQualificationViewModel> GetAll()
        {
            var data = (from s in unitOfWork.EducationQualificationRepository.Get()
                        select new EducationQualificationViewModel
                        {

                            EducationQualificationId = s.EducationQualificationId,
                            EducationQualificationName = s.EducationQualificationName

                        }).AsEnumerable();
            return data;
        }

        public void Delete(int id)
        {
            var EducationQualification = new EducationQualification
            {
                EducationQualificationId = id
            };

            unitOfWork.EducationQualificationRepository.Delete(EducationQualification);
            unitOfWork.Save();

        }
    }
}
