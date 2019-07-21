using Domain.Repositories;
using Domain.ViewModels;
using Domain.Models;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
   public class SubSubSubSubCategoryServices
    {



        private UnitOfWork unitOfWork;

        public SubSubSubSubCategoryServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(SubSubSubSubCategoryViewModel SubSubSubSubCategoryViewModel)
        {
            var SubSubSubSubCategory = new SubSubSubSubCategory
            {

                SubSubSubSubCategoryName = SubSubSubSubCategoryViewModel.SubSubSubSubCategoryName,
                SubSubSubCategoryId = SubSubSubSubCategoryViewModel.SubSubSubCategoryId,
                SubSubCategoryId = SubSubSubSubCategoryViewModel.SubSubCategoryId,
                SubCategoryId = SubSubSubSubCategoryViewModel.SubCategoryId,
                CategoryId = SubSubSubSubCategoryViewModel.CategoryId


            };

            unitOfWork.SubSubSubSubCategoryRepository.Insert(SubSubSubSubCategory);
            unitOfWork.Save();
        }

        public void Update(SubSubSubSubCategoryViewModel SubSubSubSubCategoryViewModel)
        {
            var SubSubSubSubCategory = new SubSubSubSubCategory
            {
                SubSubSubSubCategoryId = SubSubSubSubCategoryViewModel.SubSubSubSubCategoryId,
                SubSubSubSubCategoryName = SubSubSubSubCategoryViewModel.SubSubSubSubCategoryName,
                SubSubSubCategoryId = SubSubSubSubCategoryViewModel.SubSubSubCategoryId,
                SubSubCategoryId = SubSubSubSubCategoryViewModel.SubSubCategoryId,
                SubCategoryId = SubSubSubSubCategoryViewModel.SubCategoryId,
                CategoryId = SubSubSubSubCategoryViewModel.CategoryId

            };


            unitOfWork.SubSubSubSubCategoryRepository.Update(SubSubSubSubCategory);

            unitOfWork.Save();
        }


        public SubSubSubSubCategoryViewModel GetByID(int? id)
        {
            var data = (from s in unitOfWork.SubSubSubSubCategoryRepository.Get()
                        where s.SubSubSubCategoryId == id
                        select new SubSubSubSubCategoryViewModel
                        {
                            SubSubSubSubCategoryId=s.SubSubSubSubCategoryId,
                            SubSubSubSubCategoryName=s.SubSubSubSubCategoryName,
                            SubSubSubCategoryId = s.SubSubSubCategoryId,
                            SubSubSubCategoryName = s.SubSubSubCategory.SubSubSubCategoryName,
                            SubSubCategoryId = s.SubSubCategoryId,
                            SubSubCategoryName = s.SubSubCategory.SubSubCategoryName,
                            SubCategoryId = s.SubCategoryId,
                            SubCategoryName = s.SubCategory.SubCategoryName,
                            CategoryId = s.CategoryId,
                            CategoryName = s.Category.CategoryName



                        }).SingleOrDefault();

            return data;
        }

        public IEnumerable<SubSubSubSubCategoryViewModel> GetAll()
        {
            var data = (from s in unitOfWork.SubSubSubSubCategoryRepository.Get()
                        select new SubSubSubSubCategoryViewModel
                        {
                            SubSubSubSubCategoryId = s.SubSubSubSubCategoryId,
                            SubSubSubSubCategoryName = s.SubSubSubSubCategoryName,
                            SubSubSubCategoryId = s.SubSubSubCategoryId,
                            SubSubSubCategoryName = s.SubSubSubCategory.SubSubSubCategoryName,
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
            var SubSubSubSubCategory = new SubSubSubSubCategory
            {

                SubSubSubSubCategoryId = id
            };

            unitOfWork.SubSubSubSubCategoryRepository.Delete(SubSubSubSubCategory);
            unitOfWork.Save();
        }



        public IEnumerable<SubSubSubSubCategoryViewModel> GetDropDown()
        {
            var data = (from s in unitOfWork.SubSubSubSubCategoryRepository.Get()
                        select new SubSubSubSubCategoryViewModel
                        {
                            SubSubSubSubCategoryId = s.SubSubSubSubCategoryId,
                            SubSubSubSubCategoryName = s.SubSubSubSubCategoryName
                        }).AsEnumerable();

            return data;
        }

    }
}
