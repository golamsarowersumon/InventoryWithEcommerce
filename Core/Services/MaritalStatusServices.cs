using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Domain.Models;
using Domain.Repositories;
using Domain.ViewModels;

namespace Core.Services
{
   public class MaritalStatusServices
    {
        public UnitOfWork UnitOfWork;

        public MaritalStatusServices(UnitOfWork unitOfWork) {

            UnitOfWork = unitOfWork;
        }


        public void Create(MaritalStatusViewModel maritalStatusViewModel) {


            var MaritalStatus = new MaritalStatus
            {

                MaritalStatusName = maritalStatusViewModel.MaritalStatusName
      


            };

            UnitOfWork.MaritalStatusRepository.Insert(MaritalStatus);
            UnitOfWork.Save();


        }



        public void Update(MaritalStatusViewModel maritalStatusViewModel) {

            var MaritalStatus = new MaritalStatus
            {
                MaritalStatusId=maritalStatusViewModel.MaritalStatusId,
                MaritalStatusName = maritalStatusViewModel.MaritalStatusName

 };

            UnitOfWork.MaritalStatusRepository.Update(MaritalStatus);
            UnitOfWork.Save();
}



        public IEnumerable<MaritalStatusViewModel> GetAll() {

            var data = (from s in UnitOfWork.MaritalStatusRepository.Get()
                        select new MaritalStatusViewModel
                        {
                            MaritalStatusId=s.MaritalStatusId,
                            MaritalStatusName=s.MaritalStatusName

                        }).AsEnumerable();

            return data;

        }


        public MaritalStatusViewModel GetByID(int id) {

            var data = (from s in UnitOfWork.MaritalStatusRepository.Get()
                          where s.MaritalStatusId==id
                        select new MaritalStatusViewModel
                        {
                            MaritalStatusId=s.MaritalStatusId,
                            MaritalStatusName=s.MaritalStatusName


                        }).SingleOrDefault();
            return data;



        }


        public void Delete(int id) {

            var MaritalStatus = new MaritalStatus
            {
                MaritalStatusId = id
            };
            UnitOfWork.MaritalStatusRepository.Delete(MaritalStatus);
            UnitOfWork.Save();
        }



        public IEnumerable<DropDownViewModel> GetDropDown()
        {
            var data = (from s in UnitOfWork.MaritalStatusRepository.Get()
                        select new DropDownViewModel
                        {
                            Value = s.MaritalStatusId,
                            Text = s.MaritalStatusName
                        }).AsEnumerable();

            return data;
        }


    }
}
