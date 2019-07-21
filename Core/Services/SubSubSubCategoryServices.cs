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
   public class SubSubSubCategoryServices
    {

        private UnitOfWork unitOfWork;

        public SubSubSubCategoryServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(SubSubSubCategoryViewModel SubSubSubCategoryViewModel)
        {
            var SubSubSubCategory = new SubSubSubCategory
            {

                SubSubSubCategoryName = SubSubSubCategoryViewModel.SubSubSubCategoryName,
                SubSubCategoryId = SubSubSubCategoryViewModel.SubSubCategoryId,
                SubCategoryId = SubSubSubCategoryViewModel.SubCategoryId,
                CategoryId = SubSubSubCategoryViewModel.CategoryId


            };

            unitOfWork.SubSubSubCategoryRepository.Insert(SubSubSubCategory);
            unitOfWork.Save();
        }

        public void Update(SubSubSubCategoryViewModel SubSubSubCategoryViewModel)
        {
            var SubSubSubCategory = new SubSubSubCategory
            {
                SubSubSubCategoryId= SubSubSubCategoryViewModel.SubSubSubCategoryId,
                SubSubSubCategoryName = SubSubSubCategoryViewModel.SubSubSubCategoryName,
                SubSubCategoryId = SubSubSubCategoryViewModel.SubSubCategoryId,
                SubCategoryId = SubSubSubCategoryViewModel.SubCategoryId,
                CategoryId = SubSubSubCategoryViewModel.CategoryId

            };


            unitOfWork.SubSubSubCategoryRepository.Update(SubSubSubCategory);

            unitOfWork.Save();
        }


        public SubSubSubCategoryViewModel GetByID(int? id)
        {
            var data = (from s in unitOfWork.SubSubSubCategoryRepository.Get()
                        where s.SubSubSubCategoryId == id
                        select new SubSubSubCategoryViewModel
                        {
                            SubSubSubCategoryId=s.SubSubSubCategoryId,
                            SubSubSubCategoryName=s.SubSubSubCategoryName,
                            SubSubCategoryId = s.SubSubCategoryId,
                            SubSubCategoryName = s.SubSubCategory.SubSubCategoryName,
                            SubCategoryId = s.SubCategoryId,
                            SubCategoryName = s.SubCategory.SubCategoryName,
                            CategoryId = s.CategoryId,
                            CategoryName = s.Category.CategoryName



                        }).SingleOrDefault();

            return data;
        }

        public IEnumerable<SubSubSubCategoryViewModel> GetAll()
        {
            var data = (from s in unitOfWork.SubSubSubCategoryRepository.Get()
                        select new SubSubSubCategoryViewModel
                        {
                            SubSubSubCategoryId = s.SubSubSubCategoryId,
                            SubSubSubCategoryName = s.SubSubSubCategoryName,
                            SubSubCategoryId = s.SubSubCategoryId,
                            SubSubCategoryName = s.SubSubCategory.SubSubCategoryName,
                            SubCategoryId = s.SubCategoryId,
                            SubCategoryName = s.SubCategory.SubCategoryName,
                            CategoryId = s.CategoryId,
                            CategoryName = s.Category.CategoryName

                        }).AsEnumerable();

            return data;
        }


        public void Delete(int id)
        {
            var SubSubSubCategory = new SubSubSubCategory
            {

                SubSubSubCategoryId = id
            };

            unitOfWork.SubSubSubCategoryRepository.Delete(SubSubSubCategory);
            unitOfWork.Save();
        }



        public IEnumerable<SubSubSubCategoryViewModel> GetDropDown()
        {
            var data = (from s in unitOfWork.SubSubSubCategoryRepository.Get()
                        select new SubSubSubCategoryViewModel
                        {
                            SubSubSubCategoryId = s.SubSubSubCategoryId,
                            SubSubSubCategoryName = s.SubSubSubCategoryName
                        }).AsEnumerable();

            return data;
        }


    }
}
