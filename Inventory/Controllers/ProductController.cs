using Core.Services;
using Domain.Repositories;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class ProductController : Controller
    {
        private UnitOfWork unitOfWork;
        private ProductServices productServices;

        public ProductController()
        {
            unitOfWork = new UnitOfWork();
            productServices = new ProductServices(unitOfWork);
        }



        // GET: Product
        public ActionResult Index()
        {
            var data = productServices.GetAll();
            return View(data);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            var data = productServices.GetById(id);
            return View(data);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductViewModel productVM)
        {
            try
            {
                // TODO: Add insert logic here

                productServices.Create(productVM);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            var data = productServices.GetById(id);
            return View(data);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductViewModel productVM)
        {
            try
            {
                // TODO: Add update logic here
                productServices.Update(productVM);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id=0)
        {
           
            if (id != 0)
            {
                var data = productServices.GetById(id);
                return View(data);
            }
            return View();
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                // TODO: Add delete logic here
                productServices.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
