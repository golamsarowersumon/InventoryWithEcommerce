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
   public class SubSubCategoryServices
    {

        private UnitOfWork unitOfWork;

        public SubSubCategoryServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(SubSubCategoryViewModel SubSubCategoryViewModel)
        {
            var SubSubCategory = new SubSubCategory
            {

                SubSubCategoryName = SubSubCategoryViewModel.SubSubCategoryName,
                SubCategoryId=SubSubCategoryViewModel.SubCategoryId,
                CategoryId = SubSubCategoryViewModel.CategoryId
               

            };

            unitOfWork.SubSubCategoryRepository.Insert(SubSubCategory);
            unitOfWork.Save();
        }

        public void Update(SubSubCategoryViewModel SubSubCategoryViewModel)
        {
            var SubSubCategory = new SubSubCategory
            {
                SubSubCategoryId=SubSubCategoryViewModel.SubSubCategoryId,
                SubSubCategoryName = SubSubCategoryViewModel.SubSubCategoryName,
                SubCategoryId = SubSubCategoryViewModel.SubCategoryId,
                CategoryId = SubSubCategoryViewModel.CategoryId

            };


            unitOfWork.SubSubCategoryRepository.Update(SubSubCategory);

            unitOfWork.Save();
        }


        public SubSubCategoryViewModel GetByID(int? id)
        {
            var data = (from s in unitOfWork.SubSubCategoryRepository.Get()
                        where s.SubSubCategoryId == id
                        select new SubSubCategoryViewModel
                        {
                            SubSubCategoryId=s.SubSubCategoryId,
                            SubSubCategoryName=s.SubSubCategoryName,
                            SubCategoryId = s.SubCategoryId,
                            SubCategoryName = s.SubCategory.SubCategoryName,
                            CategoryId = s.CategoryId,
                            CategoryName=s.Category.CategoryName



                        }).SingleOrDefault();

            return data;
        }

        public IEnumerable<SubSubCategoryViewModel> GetAll()
        {
            var data = (from s in unitOfWork.SubSubCategoryRepository.Get()
                        select new SubSubCategoryViewModel
                        {
                            SubSubCategoryId = s.SubSubCategoryId,
                            SubSubCategoryName = s.SubSubCategoryName,
                            SubCategoryId = s.SubCategoryId,
                            SubCategoryName = s.SubCategory.SubCategoryName,
                            CategoryId = s.CategoryId,
                            CategoryName = s.Category.CategoryName

                        }).AsEnumerable();

            return data;
        }


        public void Delete(int id)
        {
            var SubSubCategory = new SubSubCategory
            {

                SubSubCategoryId = id
            };

            unitOfWork.SubSubCategoryRepository.Delete(SubSubCategory);
            unitOfWork.Save();
        }



        public IEnumerable<SubSubCategoryViewModel> GetDropDown()
        {
            var data = (from s in unitOfWork.SubSubCategoryRepository.Get()
                        select new SubSubCategoryViewModel
                        {
                            SubSubCategoryId = s.SubSubCategoryId,
                            SubSubCategoryName = s.SubSubCategoryName
                        }).AsEnumerable();

            return data;
        }



    }
}
