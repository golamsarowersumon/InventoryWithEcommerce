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
    public class SubStoreServices
    {
        private UnitOfWork unitOfWork;

        public SubStoreServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(SubStoreViewModel subStoreVM)
        {
            var SubStore = new SubStore
            {

                SubStoreName = subStoreVM.SubStoreName,
                StoreId = subStoreVM.StoreId

            };

            unitOfWork.SubStoreRepository.Insert(SubStore);
            unitOfWork.Save();
        }


        public void Update(SubStoreViewModel subStoreVM)
        {
            var SubStore = new SubStore
            {
                SubStoreId = subStoreVM.SubStoreId,
                SubStoreName = subStoreVM.SubStoreName,
                StoreId = subStoreVM.StoreId
            };
            unitOfWork.SubStoreRepository.Update(SubStore);
            unitOfWork.Save();
        }

        public SubStoreViewModel GetById(int id)
        {
            var data = (from s in unitOfWork.SubStoreRepository.Get()
                        where s.SubStoreId == id
                        select new SubStoreViewModel
                        {
                            SubStoreId = s.SubStoreId,
                            SubStoreName = s.SubStoreName,
                            StoreId = s.StoreId,
                            StoreName = s.Store.StoreName

                        }).SingleOrDefault();
            return data;
        }
        public IEnumerable<SubStoreViewModel> SubStorebystore(int id)
        {
            var data = (from s in unitOfWork.StoreRepository.Get() join st in unitOfWork.SubStoreRepository.Get() on
                        s.StoreId equals st.StoreId
                        where s.StoreId == id
                        select new SubStoreViewModel
                        {
                            SubStoreId = st.SubStoreId,
                            SubStoreName = st.SubStoreName,
                           

                        }).AsEnumerable();
            return data;
        }
        public IEnumerable<SubStoreViewModel> GetAll()
        {
            var data = (from s in unitOfWork.SubStoreRepository.Get()
                        select new SubStoreViewModel
                        {

                            SubStoreId = s.SubStoreId,
                            SubStoreName = s.SubStoreName,
                            StoreId = s.StoreId,
                            StoreName=s.Store.StoreName

                            

                        }).AsEnumerable();
            return data;
        }

        public void Delete(int id)
        {
            var SubStore = new SubStore
            {
                SubStoreId = id
            };

            unitOfWork.SubStoreRepository.Delete(SubStore);
            unitOfWork.Save();

        }

        public IEnumerable<DropDownViewModel> GetDropDown()
        {
            var data = (from s in unitOfWork.SubStoreRepository.Get()
                        select new DropDownViewModel
                        {
                            Value = s.SubStoreId,
                            Text = s.SubStoreName
                        }).AsEnumerable();

            return data;
        }
    }
}
