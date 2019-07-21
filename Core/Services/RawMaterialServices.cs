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
  public  class RawMaterialServices
    {

        private UnitOfWork unitOfWork;

        public RawMaterialServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(RawMaterialsViewModel RawMaterialsViewModel)
        {

            //string strmsg = "";
            int x = RawMaterialsViewModel.ItemElementId1.Count();
            for (int i = 0; i < x; i++)
            {
                //var arrayItemId = RawMaterialsViewModel.ItemId1[i];
                var arrayItemElementId = RawMaterialsViewModel.ItemElementId1[i];



                var RawMaterials = new RawMaterials
                {
                    ItemId =RawMaterialsViewModel.ItemId1,
                    ItemElementId = arrayItemElementId


                };

                unitOfWork.RawMaterialsRepository.Insert(RawMaterials);
                unitOfWork.Save();
            }
}

        public void Update(RawMaterialsViewModel RawMaterialsViewModel)
        {
            var RawMaterials = new RawMaterials
            {
                RawMaterialId=RawMaterialsViewModel.RawMaterialId,
                ItemId = RawMaterialsViewModel.ItemId,
                ItemElementId = RawMaterialsViewModel.ItemElementId
            };

            unitOfWork.RawMaterialsRepository.Update(RawMaterials);
            unitOfWork.Save();
        }

        public RawMaterialsViewModel GetById(int id)
        {
            var data = (from s in unitOfWork.RawMaterialsRepository.Get()
                        where s.RawMaterialId == id
                        select new RawMaterialsViewModel
                        {
                            RawMaterialId = s.RawMaterialId,
                            ItemId = s.ItemId,
                            ItemName=s.Item.ItemName,
                            ItemElementId = s.ItemElementId,
                            ItemElementName=s.ItemElement.ItemElementName

                        }).SingleOrDefault();

            return data;

        }

        public IEnumerable<RawMaterialsViewModel> GetAll()
        {
            var data = (from s in unitOfWork.RawMaterialsRepository.Get()
                        select new RawMaterialsViewModel
                        {
                            RawMaterialId = s.RawMaterialId,
                            ItemId = s.ItemId,
                            ItemName = s.Item.ItemName,
                            ItemElementId = s.ItemElementId,
                            ItemElementName = s.ItemElement.ItemElementName


                        }).AsEnumerable();
            return data;

        }

        public void Delete(int id)
        {
            var RawMaterials = new RawMaterials
            {
                RawMaterialId = id
            };
            unitOfWork.RawMaterialsRepository.Delete(RawMaterials);
            unitOfWork.Save();
        }




    }
}
