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
    public class ItemElementServices
    {
        private UnitOfWork unitOfWork;

        public ItemElementServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(ItemElementViewModel itemElementVM)
        {
            var ItemElement = new ItemElement
            {

                ItemElementName = itemElementVM.ItemElementName,
                CategoryId=itemElementVM.CategoryId,
               

                SubCategoryId=itemElementVM.SubCategoryId,
                
                SubSubCategoryId=itemElementVM.SubSubCategoryId,
                SubSubSubCategoryId=itemElementVM.SubSubSubCategoryId,
                SubSubSubSubCategoryId=itemElementVM.SubSubSubSubCategoryId

            };

            unitOfWork.ItemElementRepository.Insert(ItemElement);
            unitOfWork.Save();
        }


        public void Update(ItemElementViewModel itemElementVM)
        {
            var ItemElement = new ItemElement
            {
                ItemElementId = itemElementVM.ItemElementId,
                ItemElementName = itemElementVM.ItemElementName,
                CategoryId = itemElementVM.CategoryId,
                SubCategoryId = itemElementVM.SubCategoryId,
                SubSubCategoryId = itemElementVM.SubSubCategoryId,
                SubSubSubCategoryId = itemElementVM.SubSubSubCategoryId,
                SubSubSubSubCategoryId = itemElementVM.SubSubSubSubCategoryId
            };
            unitOfWork.ItemElementRepository.Update(ItemElement);
            unitOfWork.Save();
        }

        public ItemElementViewModel GetById(int id)
        {
            var data = (from s in unitOfWork.ItemElementRepository.Get()
                        where s.ItemElementId == id
                        select new ItemElementViewModel
                        {
                            ItemElementId = s.ItemElementId,
                            ItemElementName = s.ItemElementName,
                            CategoryId = s.CategoryId,
                            CategoryName=s.Category.CategoryName,
                            SubCategoryId = s.SubCategoryId,
                            SubCategoryName=s.SubCategory.SubCategoryName,
                            SubSubCategoryId = s.SubSubCategoryId,
                            SubSubCategoryName=s.SubSubCategory.SubSubCategoryName,
                            SubSubSubCategoryId = s.SubSubSubCategoryId,
                            SubSubSubCategoryName=s.SubSubSubCategory.SubSubSubCategoryName,
                            SubSubSubSubCategoryId = s.SubSubSubSubCategoryId,
                            SubSubSubSubCategoryName=s.SubSubSubSubCategory.SubSubSubSubCategoryName
                            
                        }).SingleOrDefault();
            return data;
        }
        public IEnumerable<ItemElementViewModel> GetAll()
        {
            var data = (from s in unitOfWork.ItemElementRepository.Get()
                        select new ItemElementViewModel
                        {

                            ItemElementId = s.ItemElementId,
                            ItemElementName = s.ItemElementName,
                            CategoryId = s.CategoryId,
                            CategoryName = s.Category.CategoryName,
                            SubCategoryId = s.SubCategoryId,
                            SubCategoryName = s.SubCategory.SubCategoryName,
                            SubSubCategoryId = s.SubSubCategoryId,
                            SubSubCategoryName = s.SubSubCategory.SubSubCategoryName,
                            SubSubSubCategoryId = s.SubSubSubCategoryId,
                            SubSubSubCategoryName = s.SubSubSubCategory.SubSubSubCategoryName,
                            SubSubSubSubCategoryId = s.SubSubSubSubCategoryId,
                            SubSubSubSubCategoryName = s.SubSubSubSubCategory.SubSubSubSubCategoryName

                        }).AsEnumerable();
            return data;
        }

        public void Delete(int id)
        {
            var ItemElement = new ItemElement
            {
                ItemElementId = id
            };

            unitOfWork.ItemElementRepository.Delete(ItemElement);
            unitOfWork.Save();

        }


        public IEnumerable<ItemElementViewModel> GetDropDown()
        {
            var data = (from s in unitOfWork.ItemElementRepository.Get()
                        select new ItemElementViewModel
                        {
                            ItemElementId = s.ItemElementId,
                            ItemElementName = s.ItemElementName,
                        }).AsEnumerable();

            return data;
        }


    }
}
