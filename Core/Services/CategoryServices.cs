using Domain.Repositories;
using Domain.Models;
using Core.Services;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
  public  class CategoryServices
    {



        private UnitOfWork unitOfWork;

        public CategoryServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(CategoryViewModel CategoryViewModel)
        {
            var Category = new Category
            {

                CategoryName = CategoryViewModel.CategoryName

            };

            unitOfWork.CategoryRepository.Insert(Category);
            unitOfWork.Save();
        }

        public void Update(CategoryViewModel CategoryViewModel)
        {
            var Category = new Category
            {
                CategoryId = CategoryViewModel.CategoryId,
                CategoryName = CategoryViewModel.CategoryName

            };


            unitOfWork.CategoryRepository.Update(Category);

            unitOfWork.Save();
        }


        public CategoryViewModel GetByID(int? id)
        {
            var data = (from s in unitOfWork.CategoryRepository.Get()
                        where s.CategoryId == id
                        select new CategoryViewModel
                        {
                            CategoryId = s.CategoryId,
                            CategoryName = s.CategoryName



                        }).SingleOrDefault();

            return data;
        }

        public IEnumerable<CategoryViewModel> GetAll()
        {
            var data = (from s in unitOfWork.CategoryRepository.Get()
                        select new CategoryViewModel
                        {
                            CategoryId = s.CategoryId,
                            CategoryName = s.CategoryName


                        }).AsEnumerable();

            return data;
        }


        public void Delete(int id)
        {
            var Category = new Category
            {

                CategoryId = id
            };

            unitOfWork.CategoryRepository.Delete(Category);
            unitOfWork.Save();
        }



        public IEnumerable<CategoryViewModel> GetDropDown()
        {
            var data = (from s in unitOfWork.CategoryRepository.Get()
                        select new CategoryViewModel
                        {
                            CategoryId = s.CategoryId,
                            CategoryName = s.CategoryName
                        }).AsEnumerable();

            return data;
        }


    }
}
