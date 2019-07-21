using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Repositories;
using Domain.ViewModels;

namespace Core.Services
{
  public  class ConditionOfItemServices
    {
        public UnitOfWork UnitOfWork;

        public ConditionOfItemServices(UnitOfWork _UnitOfWork) {
            UnitOfWork = _UnitOfWork;

        }


        public void Create(ConditionOfItemViewModel conditionOfItemViewModel) {

            var CondtionOfItem = new ConditionOfItem
            {

              
                ConditionOfItemName= conditionOfItemViewModel.ConditionOfItemName

            };

            UnitOfWork.ConditionOfItemRepository.Insert(CondtionOfItem);
            UnitOfWork.Save();



        }


        public void Update(ConditionOfItemViewModel conditionOfItemViewModel) {

            var ConditonOfItem = new ConditionOfItem
            {

                ConditionOfItemId = conditionOfItemViewModel.ConditionOfItemId,
                ConditionOfItemName=conditionOfItemViewModel.ConditionOfItemName
            };

            UnitOfWork.ConditionOfItemRepository.Update(ConditonOfItem);
            UnitOfWork.Save();

        }


        public IEnumerable<ConditionOfItemViewModel>  GetAll() {

            var data = (from s in UnitOfWork.ConditionOfItemRepository.Get()
                       select new ConditionOfItemViewModel {

                           ConditionOfItemId=s.ConditionOfItemId,
                           ConditionOfItemName=s.ConditionOfItemName
                       }).AsEnumerable();


            return data;
        }


        public ConditionOfItemViewModel GetByID(int id) {

            var data= (from s in UnitOfWork.ConditionOfItemRepository.Get()
                       where s.ConditionOfItemId==id
                       select new ConditionOfItemViewModel
                       {

                           ConditionOfItemId=s.ConditionOfItemId,
                           ConditionOfItemName=s.ConditionOfItemName


            }).SingleOrDefault();


            return data;
        }


        public void Delete(int id) {

            var ConditionOfItem = new ConditionOfItem {

                ConditionOfItemId=id
            };

            UnitOfWork.ConditionOfItemRepository.Delete(ConditionOfItem);
            UnitOfWork.Save();

        }

    }
}
