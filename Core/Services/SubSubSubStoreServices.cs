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
    public class SubSubSubStoreServices
    {
        private UnitOfWork unitOfWork;

        public SubSubSubStoreServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(SubSubSubStoreViewModel subSubSubStoreVM)
        {
            var SubSubSubStore = new SubSubSubStore
            {

                SubSubSubStoreName = subSubSubStoreVM.SubSubSubStoreName,
                StoreId = subSubSubStoreVM.StoreId,
                SubStoreId = subSubSubStoreVM.SubStoreId,
                SubSubStoreId = subSubSubStoreVM.SubSubStoreId

            };

            unitOfWork.SubSubSubStoreRepository.Insert(SubSubSubStore);
            unitOfWork.Save();
        }


        public void Update(SubSubSubStoreViewModel subSubSubStoreVM)
        {
            var SubSubSubStore = new SubSubSubStore
            {
                SubSubSubStoreId = subSubSubStoreVM.SubSubSubStoreId,
                SubSubSubStoreName = subSubSubStoreVM.SubSubSubStoreName,
                StoreId = subSubSubStoreVM.StoreId,
                SubStoreId = subSubSubStoreVM.SubStoreId,
                SubSubStoreId = subSubSubStoreVM.SubSubStoreId

            };
            unitOfWork.SubSubSubStoreRepository.Update(SubSubSubStore);
            unitOfWork.Save();
        }

        public SubSubSubStoreViewModel GetById(int id)
        {
            var data = (from s in unitOfWork.SubSubSubStoreRepository.Get()
                        where s.SubSubSubStoreId == id
                        select new SubSubSubStoreViewModel
                        {
                            SubSubSubStoreId = s.SubSubSubStoreId,
                            SubSubSubStoreName = s.SubSubSubStoreName,
                            StoreId = s.StoreId,
                            SubStoreId = s.SubStoreId,
                            SubSubStoreId = s.SubSubStoreId,
                            StoreName = s.Store.StoreName,
                            SubStoreName = s.SubStore.SubStoreName,
                            SubSubStoreName = s.SubSubStore.SubSubStoreName

                        }).SingleOrDefault();
            return data;
        }


        public IEnumerable<SubSubSubStoreViewModel> SubSubSubStoreGet(int id)
        {
            var data = (from sst in unitOfWork.SubSubStoreRepository.Get() join ssst in unitOfWork.SubSubSubStoreRepository.Get()
                        on sst.SubSubStoreId equals ssst.SubSubStoreId where sst.SubSubStoreId == id
                        select new SubSubSubStoreViewModel
                        {

                            SubSubSubStoreId = ssst.SubSubSubStoreId,
                            SubSubSubStoreName = ssst.SubSubSubStoreName,
                           

                        }).AsEnumerable();
            return data;
        }
        public IEnumerable<SubSubSubStoreViewModel> GetAll()
        {
            var data = (from s in unitOfWork.SubSubSubStoreRepository.Get()
                        select new SubSubSubStoreViewModel
                        {

                            SubSubSubStoreId = s.SubSubSubStoreId,
                            SubSubSubStoreName = s.SubSubSubStoreName,
                            StoreId = s.StoreId,
                            SubStoreId = s.SubStoreId,
                            SubSubStoreId = s.SubSubStoreId,
                            StoreName = s.Store.StoreName,
                            SubStoreName = s.SubStore.SubStoreName,
                            SubSubStoreName = s.SubSubStore.SubSubStoreName

                        }).AsEnumerable();
            return data;
        }

        public void Delete(int id)
        {
            var SubSubSubStore = new SubSubSubStore
            {
                SubSubSubStoreId = id
            };

            unitOfWork.SubSubSubStoreRepository.Delete(SubSubSubStore);
            unitOfWork.Save();

        }

        public IEnumerable<DropDownViewModel> GetDropDown()
        {
            var data = (from s in unitOfWork.SubSubSubStoreRepository.Get()
                        select new DropDownViewModel
                        {
                            Value = s.SubSubSubStoreId,
                            Text = s.SubSubSubStoreName
                        }).AsEnumerable();

            return data;
        }
    }
}
