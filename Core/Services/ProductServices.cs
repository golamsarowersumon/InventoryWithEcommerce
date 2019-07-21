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
    public class ProductServices
    {
        private UnitOfWork unitOfWork;
        
        public ProductServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }


        public void Create(ProductViewModel prductVM)
        {
            var Product = new Product
            {
                ProductName = prductVM.ProductName
            };
            unitOfWork.ProductRepository.Insert(Product);
            unitOfWork.Save();
        }

        public void Update(ProductViewModel productVM)
        {
            var Product = new Product
            {
                ProductId = productVM.ProductId,
                ProductName = productVM.ProductName
            };
            unitOfWork.ProductRepository.Update(Product);
            unitOfWork.Save();
        }

        public ProductViewModel GetById(int id)
        {
            var data = (from s in unitOfWork.ProductRepository.Get()
                        where s.ProductId == id
                        select new ProductViewModel
                        {
                            ProductId=s.ProductId,
                            ProductName=s.ProductName
                        }).SingleOrDefault();
            return data;
        }

        public IEnumerable<ProductViewModel> GetAll()
        {
            var data = (from s in unitOfWork.ProductRepository.Get()
                        select new ProductViewModel
                        {

                            ProductId = s.ProductId,
                            ProductName = s.ProductName

                        }).AsEnumerable();

            return data;
        }

        public void Delete(int id)
        {
            var Product = new Product
            {
                ProductId = id
            };

            unitOfWork.ProductRepository.Delete(Product);
            unitOfWork.Save();
        }

        public IEnumerable<DropDownViewModel> GetDropDown()
        {
            var data = (from s in unitOfWork.ProductRepository.Get()
                       
                        select new DropDownViewModel
                        {
                            Value = s.ProductId,
                            Text = s.ProductName
                        }).AsEnumerable();
            return data;
        }
    }
}
