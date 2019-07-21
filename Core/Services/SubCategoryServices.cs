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
   public class SubCategoryServices
    {
     private UnitOfWork unitOfWork;

        public SubCategoryServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(SubCategoryViewModel SubCategoryViewModel)
        {
            var SubCategory = new SubCategory
            {

                SubCategoryName = SubCategoryViewModel.SubCategoryName,
                CategoryId = SubCategoryViewModel.CategoryId
               

            };

            unitOfWork.SubCategoryRepository.Insert(SubCategory);
            unitOfWork.Save();
        }

        public void Update(SubCategoryViewModel SubCategoryViewModel)
        {
            var SubCategory = new SubCategory
            {
                SubCategoryId=SubCategoryViewModel.SubCategoryId,
                SubCategoryName = SubCategoryViewModel.SubCategoryName,
                CategoryId = SubCategoryViewModel.CategoryId

            };


            unitOfWork.SubCategoryRepository.Update(SubCategory);

            unitOfWork.Save();
        }


        public SubCategoryViewModel GetByID(int? id)
        {
            var data = (from s in unitOfWork.SubCategoryRepository.Get()
                        where s.SubCategoryId == id
                        select new SubCategoryViewModel
                        {
                            SubCategoryId = s.SubCategoryId,
                            SubCategoryName = s.SubCategoryName,
                            CategoryId = s.CategoryId,
                            CategoryName=s.Category.CategoryName



                        }).SingleOrDefault();

            return data;
        }

        public IEnumerable<SubCategoryViewModel> GetAll()
        {
            var data = (from s in unitOfWork.SubCategoryRepository.Get()
                        select new SubCategoryViewModel
                        {
                            SubCategoryId = s.SubCategoryId,
                            SubCategoryName = s.SubCategoryName,
                            CategoryId = s.CategoryId,
                            CategoryName = s.Category.CategoryName

                        }).AsEnumerable();

            return data;
        }


        public void Delete(int id)
        {
            var SubCategory = new SubCategory
            {

                SubCategoryId = id
            };

            unitOfWork.SubCategoryRepository.Delete(SubCategory);
            unitOfWork.Save();
        }



        public IEnumerable<SubCategoryViewModel> GetDropDown()
        {
            var data = (from s in unitOfWork.SubCategoryRepository.Get()
                        select new SubCategoryViewModel
                        {
                            SubCategoryId = s.SubCategoryId,
                            SubCategoryName = s.SubCategoryName
                        }).AsEnumerable();

            return data;
        }


    }
}
