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
   public class CompanyServices
    {

        private UnitOfWork unitOfWork;

        public CompanyServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(CompanyViewModel companyViewModel)
        {
            var Company = new Company
            {

                CompanyName = companyViewModel.CompanyName

            };

            unitOfWork.ReligionServices.Insert(Company);
            unitOfWork.Save();
        }

        public void Update(CompanyViewModel companyViewModel)
        {
            var Company = new Company
            {
                CompanyId = companyViewModel.CompanyId,
               CompanyName= companyViewModel.CompanyName

            };


            unitOfWork.ReligionServices.Update(Company);

            unitOfWork.Save();
        }


        public CompanyViewModel GetByID(int id)
        {
            var data = (from s in unitOfWork.ReligionServices.Get()
                        where s.CompanyId == id
                        select new CompanyViewModel
                        {
                            CompanyId = s.CompanyId,
                            CompanyName = s.CompanyName



                        }).SingleOrDefault();

            return data;
        }

        public IEnumerable<CompanyViewModel> GetAll()
        {
            var data = (from s in unitOfWork.ReligionServices.Get()
                        select new CompanyViewModel
                        {
                            CompanyId = s.CompanyId,
                            CompanyName = s.CompanyName


                        }).AsEnumerable();

            return data;
        }


        public void Delete(int id)
        {
            var Company = new Company
            {

                CompanyId = id
            };

            unitOfWork.ReligionServices.Delete(Company);
            unitOfWork.Save();
        }



        public IEnumerable<CompanyViewModel> GetDropDown()
        {
            var data = (from s in unitOfWork.ReligionServices.Get()
                        select new CompanyViewModel
                        {
                            CompanyId = s.CompanyId,
                            CompanyName = s.CompanyName
                        }).AsEnumerable();

            return data;
        }


    }
}
