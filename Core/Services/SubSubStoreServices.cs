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
    public class SubSubStoreServices
    {
        private UnitOfWork unitOfWork;

        public SubSubStoreServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(SubSubStoreViewModel subSubStoreVM)
        {
            var SubSubStore = new SubSubStore
            {

                SubSubStoreName = subSubStoreVM.SubSubStoreName,
                StoreId = subSubStoreVM.StoreId,
                SubStoreId = subSubStoreVM.SubStoreId

            };

            unitOfWork.SubSubStoreRepository.Insert(SubSubStore);
            unitOfWork.Save();
        }


        public void Update(SubSubStoreViewModel subSubStoreVM)
        {
            var SubSubStore = new SubSubStore
            {
                SubSubStoreId = subSubStoreVM.SubSubStoreId,
                SubSubStoreName = subSubStoreVM.SubSubStoreName,
                StoreId = subSubStoreVM.StoreId,
                SubStoreId = subSubStoreVM.SubStoreId
            };
            unitOfWork.SubSubStoreRepository.Update(SubSubStore);
            unitOfWork.Save();
        }

        public SubSubStoreViewModel GetById(int id)
        {
            var data = (from s in unitOfWork.SubSubStoreRepository.Get()
                        where s.SubSubStoreId == id
                        select new SubSubStoreViewModel
                        {
                            SubSubStoreId = s.SubSubStoreId,
                            SubSubStoreName = s.SubSubStoreName,
                            StoreId = s.StoreId,
                            SubStoreId = s.SubStoreId,
                            StoreName = s.Store.StoreName,
                            SubStoreName = s.SubStore.SubStoreName


                        }).SingleOrDefault();
            return data;
        }

        public IEnumerable<SubSubStoreViewModel> subsubstorebysubstore(int id)
        {
            var data = (from st in unitOfWork.SubStoreRepository.Get() join sst in unitOfWork.SubSubStoreRepository.Get()
                        on st.SubStoreId equals sst.SubStoreId where st.SubStoreId == id
                        select new SubSubStoreViewModel
                        {

                            SubSubStoreId = sst.SubSubStoreId,
                            SubSubStoreName = sst.SubSubStoreName,
                           

                        }).AsEnumerable();
            return data;
        }
        public IEnumerable<SubSubStoreViewModel> GetAll()
        {
            var data = (from s in unitOfWork.SubSubStoreRepository.Get()
                        select new SubSubStoreViewModel
                        {

                            SubSubStoreId = s.SubSubStoreId,
                            SubSubStoreName = s.SubSubStoreName,
                            StoreId = s.StoreId,
                            SubStoreId = s.SubStoreId,
                            StoreName = s.Store.StoreName,
                            SubStoreName = s.SubStore.SubStoreName
                           

                        }).AsEnumerable();
            return data;
        }

        public void Delete(int id)
        {
            var SubSubStore = new SubSubStore
            {
                SubSubStoreId = id
            };

            unitOfWork.SubSubStoreRepository.Delete(SubSubStore);
            unitOfWork.Save();

        }
        public IEnumerable<DropDownViewModel> GetDropDown()
        {
            var data = (from s in unitOfWork.SubSubStoreRepository.Get()
                        select new DropDownViewModel
                        {
                            Value = s.SubSubStoreId,
                            Text = s.SubSubStoreName
                        }).AsEnumerable();

            return data;
        }
    }
}
