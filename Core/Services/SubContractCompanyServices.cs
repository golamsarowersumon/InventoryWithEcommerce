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
    public class SubContractCompanyServices
    {
        private UnitOfWork unitOfWork;

        public SubContractCompanyServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(SubContractCompanyViewModel subContractCompanyVM)
        {
            var SubContractCompany = new SubContractCompany
            {

                SubContractCompanyName = subContractCompanyVM.SubContractCompanyName
            };

            unitOfWork.SubContractCompanyRepository.Insert(SubContractCompany);
            unitOfWork.Save();
        }


        public void Update(SubContractCompanyViewModel subContractCompanyVM)
        {
            var SubContractCompany = new SubContractCompany
            {
                SubContractCompanyId = subContractCompanyVM.SubContractCompanyId,
                SubContractCompanyName = subContractCompanyVM.SubContractCompanyName
            };
            unitOfWork.SubContractCompanyRepository.Update(SubContractCompany);
            unitOfWork.Save();
        }

        public SubContractCompanyViewModel GetById(int id)
        {
            var data = (from s in unitOfWork.SubContractCompanyRepository.Get()
                        where s.SubContractCompanyId == id
                        select new SubContractCompanyViewModel
                        {
                            SubContractCompanyId = s.SubContractCompanyId,
                            SubContractCompanyName = s.SubContractCompanyName
                        }).SingleOrDefault();
            return data;
        }
        public IEnumerable<SubContractCompanyViewModel> GetAll()
        {
            var data = (from s in unitOfWork.SubContractCompanyRepository.Get()
                        select new SubContractCompanyViewModel
                        {

                            SubContractCompanyId = s.SubContractCompanyId,
                            SubContractCompanyName = s.SubContractCompanyName

                        }).AsEnumerable();
            return data;
        }

        public void Delete(int id)
        {
            var SubContractCompany = new SubContractCompany
            {
                SubContractCompanyId = id
            };

            unitOfWork.SubContractCompanyRepository.Delete(SubContractCompany);
            unitOfWork.Save();

        }
    }
}
