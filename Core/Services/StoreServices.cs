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
    public class StoreServices
    {
        private UnitOfWork unitOfWork;

        public StoreServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(StoreViewModel storeVM)
        {
            var Store = new Store
            {

                StoreName = storeVM.StoreName
            };

            unitOfWork.StoreRepository.Insert(Store);
            unitOfWork.Save();
        }


        public void Update(StoreViewModel storeVM)
        {
            var Store = new Store
            {
                StoreId = storeVM.StoreId,
                StoreName = storeVM.StoreName
            };
            unitOfWork.StoreRepository.Update(Store);
            unitOfWork.Save();
        }

        public StoreViewModel GetById(int id)
        {
            var data = (from s in unitOfWork.StoreRepository.Get()
                        where s.StoreId == id
                        select new StoreViewModel
                        {
                            StoreId = s.StoreId,
                            StoreName = s.StoreName
                        }).SingleOrDefault();
            return data;
        }
        public IEnumerable<StoreViewModel> GetAll()
        {
            var data = (from s in unitOfWork.StoreRepository.Get()
                        select new StoreViewModel
                        {

                             StoreId= s.StoreId,
                            StoreName = s.StoreName

                        }).AsEnumerable();
            return data;
        }

        public void Delete(int id)
        {
            var Store = new Store
            {
                StoreId = id
            };

            unitOfWork.StoreRepository.Delete(Store);
            unitOfWork.Save();

        }

        public IEnumerable<DropDownViewModel> GetDropDown()
        {
            var data = (from s in unitOfWork.StoreRepository.Get()
                        select new DropDownViewModel
                        {
                            Value = s.StoreId,
                            Text = s.StoreName
                        }).AsEnumerable();

            return data;
        }
    }
}
