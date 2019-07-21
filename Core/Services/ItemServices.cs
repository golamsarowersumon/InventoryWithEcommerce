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
    public class ItemServices
    {
        private UnitOfWork unitOfWork;

        public ItemServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(ItemViewModel itemVM)
        {
            var Item = new Item
            {
                ItemName = itemVM.ItemName,
                CategoryId = itemVM.CategoryId,
                SubCategoryId = itemVM.SubCategoryId,
                SubSubCategoryId = itemVM.SubSubCategoryId,
                SubSubSubCategoryId = itemVM.SubSubSubCategoryId,
                SubSubSubSubCategoryId = itemVM.SubSubSubSubCategoryId,
                BrandId = itemVM.BrandId,
                ModelId = itemVM.ModelId,

                UnitId = itemVM.UnitId,
                Height = itemVM.Height,
                Width = itemVM.Width,
                Weight = itemVM.Weight,
                MethodId = itemVM.MethodId,
                ItemDetails = itemVM.ItemDetails,

                Product_Image = itemVM.Product_Image,
                Product_Image1 = itemVM.Product_Image1,
                Product_Image2 = itemVM.Product_Image2



            };
            unitOfWork.ItemRepository.Insert(Item);
            unitOfWork.Save();
        }

        public void Update(ItemViewModel itemVM)
        {
            var Item = new Item
            {
                ItemId = itemVM.ItemId,
                ItemName = itemVM.ItemName,
                CategoryId = itemVM.CategoryId,
                SubCategoryId = itemVM.SubCategoryId,
                SubSubCategoryId = itemVM.SubSubCategoryId,
                SubSubSubCategoryId = itemVM.SubSubSubCategoryId,
                SubSubSubSubCategoryId = itemVM.SubSubSubSubCategoryId,
                BrandId = itemVM.BrandId,
                ModelId = itemVM.ModelId,
                UnitId = itemVM.UnitId,
                Height = itemVM.Height,
                Width = itemVM.Width,
                Weight = itemVM.Weight,
                MethodId = itemVM.MethodId,
                ItemDetails = itemVM.ItemDetails,
                Product_Image = itemVM.Product_Image,
                Product_Image1 = itemVM.Product_Image1,
                Product_Image2 = itemVM.Product_Image2

            };

            unitOfWork.ItemRepository.Update(Item);
            unitOfWork.Save();
        }

        public ItemViewModel GetById(int id)
        {
            var data = (from s in unitOfWork.ItemRepository.Get()
                        where s.ItemId == id
                        select new ItemViewModel
                        {
                            ItemId = s.ItemId,
                            ItemName = s.ItemName,
                            CategoryId = s.CategoryId,
                            CategoryName = s.Category.CategoryName,
                            SubCategoryId = s.SubCategoryId,
                            SubCategoryName = s.SubCategory.SubCategoryName,
                            SubSubCategoryId = s.SubSubCategoryId,
                            SubSubCategoryName = s.SubSubCategory.SubSubCategoryName,
                            SubSubSubCategoryId = s.SubSubSubCategoryId,
                            SubSubSubCategoryName = s.SubSubSubCategory.SubSubSubCategoryName,
                            SubSubSubSubCategoryId = s.SubSubSubSubCategoryId,
                            SubSubSubSubCategoryName = s.SubSubSubSubCategory.SubSubSubSubCategoryName,
                            BrandId = s.BrandId,
                            ModelId = s.ModelId,
                            BrandName = s.Brand.BrandName,
                            ModelName = s.Model.ModelName,

                            UnitId = s.UnitId,
                            UnitName = s.Unit.UnitName,
                            Height = s.Height,
                            Width = s.Width,
                            Weight = s.Weight,
                            MethodId = s.MethodId,
                            Product_Image = s.Product_Image,
                            Product_Image1 = s.Product_Image1,
                            Product_Image2 = s.Product_Image2,



                        }).SingleOrDefault();

            return data;

        }

        public IEnumerable<ItemViewModel> GetAll()
        {
            var data = (from s in unitOfWork.ItemRepository.Get()
                        select new ItemViewModel
                        {
                            ItemId = s.ItemId,
                            ItemName = s.ItemName,
                            CategoryId = s.CategoryId,
                            CategoryName = s.Category.CategoryName,
                            SubCategoryId = s.SubCategoryId,
                            SubCategoryName = s.SubCategory.SubCategoryName,
                            SubSubCategoryId = s.SubSubCategoryId,
                            SubSubCategoryName = s.SubSubCategory.SubSubCategoryName,
                            SubSubSubCategoryId = s.SubSubSubCategoryId,
                            SubSubSubCategoryName = s.SubSubSubCategory.SubSubSubCategoryName,
                            SubSubSubSubCategoryId = s.SubSubSubSubCategoryId,
                            SubSubSubSubCategoryName = s.SubSubSubSubCategory.SubSubSubSubCategoryName,

                            UnitId = s.UnitId,
                            UnitName = s.Unit.UnitName,
                            Height = s.Height,
                            Width = s.Width,
                            Weight = s.Weight,
                            MethodId = s.MethodId,
                            Product_Image = s.Product_Image


                        }).AsEnumerable();
            return data;

        }

        public void Delete(int id)
        {
            var Item = new Item
            {
                ItemId = id
            };
            unitOfWork.ItemRepository.Delete(Item);
            unitOfWork.Save();
        }




        public IEnumerable<ItemViewModel> GetDropDown()
        {
            var data = (from s in unitOfWork.ItemRepository.Get()
                        select new ItemViewModel
                        {
                            ItemId = s.ItemId,
                            ItemName = s.ItemName,
                        }).AsEnumerable();

            return data;
        }

        public IEnumerable<ItemViewModel> GetUnit(int i)
        {
            var data = (from s in unitOfWork.ItemRepository.Get()
                        where s.ItemId == i
                        select new ItemViewModel
                        {
                            UnitId = s.UnitId,
                            UnitName = s.Unit.UnitName
                        }).AsEnumerable();

            return data;
        }

    }
}
