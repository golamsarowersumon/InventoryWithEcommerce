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
    public class ProductDetailsServices
    {

        private UnitOfWork unitOfWork;

        public ProductDetailsServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(ProductDetailsViewModel productDetailsVM)
        {

            string strmsg = "";
            int x = productDetailsVM.ArrayItemId.Count();
            for (int i = 0; i < x; i++)
            {
                
                var arrayItemId = productDetailsVM.ArrayItemId[i];
                var arrayItemQuantity =Convert.ToDecimal(productDetailsVM.ArraryItemQuantity[i]);
               




                var ProductDetails = new ProductDetails
                {

                    ProductId = productDetailsVM.ProductId,
                    ProductQuantity = productDetailsVM.ProductQuantity,
                    ItemId = arrayItemId,
                    ItemQuantity = arrayItemQuantity,
                   
                };


                unitOfWork.ProductDetailsRepository.Insert(ProductDetails);
                unitOfWork.Save();

            }


          
        }

        public void Update(ProductDetailsViewModel productDetailsVM)
        {
            var ProductDetails = new ProductDetails
            {
                SerialId = productDetailsVM.SerialId,
                ProductId = productDetailsVM.ProductId,
                ProductQuantity = productDetailsVM.ProductQuantity,
                ItemId = productDetailsVM.ItemId,
                ItemQuantity = productDetailsVM.ItemQuantity,
             

            };


            unitOfWork.ProductDetailsRepository.Update(ProductDetails);

            unitOfWork.Save();
        }


        public ProductDetailsViewModel GetById(int id)
        {
            var data = (from s in unitOfWork.ProductDetailsRepository.Get()
                       
                        where s.SerialId == id
                        select new ProductDetailsViewModel
                        {
                            SerialId=s.SerialId,
                            ProductId = s.ProductId,
                            ProductName =s.Product.ProductName,
                            ProductQuantity = s.ProductQuantity,
                            ItemId = s.ItemId,
                            ItemName=s.Item.ItemName,
                            ItemQuantity = s.ItemQuantity,
                           



                        }).SingleOrDefault();

            return data;
        }

        public IEnumerable<ProductDetailsViewModel> GetAll()
        {
            var data = (from s in unitOfWork.ProductDetailsRepository.Get()
                      
                        select new ProductDetailsViewModel
                        {
                            SerialId=s.SerialId,
                            ProductId = s.ProductId,
                            ProductName = s.Product.ProductName,
                            ProductQuantity = s.ProductQuantity,
                            ItemId = s.ItemId,
                            ItemName = s.Item.ItemName,
                            ItemQuantity = s.ItemQuantity,
                         

                        }).AsEnumerable();

            return data;
        }


        public void Delete(int id)
        {
            var ProductDetails = new ProductDetails
            {

                SerialId = id
            };

            unitOfWork.ProductDetailsRepository.Delete(ProductDetails);
            unitOfWork.Save();
        }



        //public IEnumerable<DropDownViewModel> GetDropDown()
        //{
        //    var data = (from s in unitOfWork.ProductDetailsRepository.Get()
        //                select new DropDownViewModel
        //                {
        //                    Value = s.ProductId,
        //                    Text = s.ProductName
        //                }).AsEnumerable();

        //    return data;
        //}


       


    }
}
