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
    public class SubSubSubSubStoreServices
    {
        private UnitOfWork unitOfWork;

        public SubSubSubSubStoreServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(SubSubSubSubStoreViewModel subSubSubSubStoreVM)
        {
            var SubSubSubSubStore = new SubSubSubSubStore
            {

                SubSubSubSubStoreName = subSubSubSubStoreVM.SubSubSubSubStoreName,
                StoreId = subSubSubSubStoreVM.StoreId,
                SubStoreId=subSubSubSubStoreVM.SubStoreId,
                SubSubStoreId=subSubSubSubStoreVM.SubSubStoreId,
                SubSubSubStoreId=subSubSubSubStoreVM.SubSubSubStoreId

               


            };

            unitOfWork.SubSubSubSubStoreRepository.Insert(SubSubSubSubStore);
            unitOfWork.Save();
        }


        public void Update(SubSubSubSubStoreViewModel subSubSubSubStoreVM)
        {
            var SubSubSubSubStore = new SubSubSubSubStore
            {
                SubSubSubSubStoreId = subSubSubSubStoreVM.SubSubSubSubStoreId,
                SubSubSubSubStoreName = subSubSubSubStoreVM.SubSubSubSubStoreName,
                StoreId = subSubSubSubStoreVM.StoreId,
                SubStoreId = subSubSubSubStoreVM.SubStoreId,
                SubSubStoreId = subSubSubSubStoreVM.SubSubStoreId,
                SubSubSubStoreId = subSubSubSubStoreVM.SubSubSubStoreId
            };
            unitOfWork.SubSubSubSubStoreRepository.Update(SubSubSubSubStore);
            unitOfWork.Save();
        }

        public SubSubSubSubStoreViewModel GetById(int id)
        {
            var data = (from s in unitOfWork.SubSubSubSubStoreRepository.Get()
                        where s.SubSubSubSubStoreId == id
                        select new SubSubSubSubStoreViewModel
                        {
                            SubSubSubSubStoreId = s.SubSubSubSubStoreId,
                            SubSubSubSubStoreName = s.SubSubSubSubStoreName,
                            StoreId = s.StoreId,
                            SubStoreId = s.SubStoreId,
                            SubSubStoreId = s.SubSubStoreId,
                            SubSubSubStoreId = s.SubSubSubStoreId,
                            StoreName = s.Store.StoreName,
                            SubStoreName = s.SubStore.SubStoreName,
                           SubSubStoreName=s.SubSubStore.SubSubStoreName,
                           SubSubSubStoreName=s.SubSubSubStore.SubSubSubStoreName


                            


                        }).SingleOrDefault();
            return data;
        }


        public IEnumerable<SubSubSubSubStoreViewModel> SubSubSubSubStoreget(int id)
        {
            var data = (from ssst in unitOfWork.SubSubSubStoreRepository.Get() join sssst in unitOfWork.SubSubSubSubStoreRepository.Get()
                        on ssst.SubSubSubStoreId equals sssst.SubSubSubStoreId where ssst.SubSubSubStoreId ==id
                        select new SubSubSubSubStoreViewModel
                        {

                            SubSubSubSubStoreId = sssst.SubSubSubSubStoreId,
                            SubSubSubSubStoreName = sssst.SubSubSubSubStoreName,
                           

                        }).AsEnumerable();
            return data;
        }
        public IEnumerable<SubSubSubSubStoreViewModel> GetAll()
        {
            var data = (from s in unitOfWork.SubSubSubSubStoreRepository.Get()
                        select new SubSubSubSubStoreViewModel
                        {

                            SubSubSubSubStoreId = s.SubSubSubSubStoreId,
                            SubSubSubSubStoreName = s.SubSubSubSubStoreName,
                            StoreId = s.StoreId,
                            SubStoreId = s.SubStoreId,
                            SubSubStoreId = s.SubSubStoreId,
                            SubSubSubStoreId = s.SubSubSubStoreId,
                            StoreName = s.Store.StoreName,
                            SubStoreName = s.SubStore.SubStoreName,
                            SubSubStoreName = s.SubSubStore.SubSubStoreName,
                            SubSubSubStoreName = s.SubSubSubStore.SubSubSubStoreName

                        }).AsEnumerable();
            return data;
        }

        public void Delete(int id)
        {
            var SubSubSubSubStore = new SubSubSubSubStore
            {
                SubSubSubSubStoreId = id
            };

            unitOfWork.SubSubSubSubStoreRepository.Delete(SubSubSubSubStore);
            unitOfWork.Save();

        }

        public IEnumerable<DropDownViewModel> GetDropDown()
        {
            var data = (from s in unitOfWork.SubSubSubSubStoreRepository.Get()
                        select new DropDownViewModel
                        {
                            Value = s.SubSubSubSubStoreId,
                            Text = s.SubSubSubSubStoreName
                        }).AsEnumerable();

            return data;
        }
    }
}
