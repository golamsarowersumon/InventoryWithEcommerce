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
    public class BrandController : Controller
    {

        private BrandServices BrandServices;
        private SubSubSubSubCategoryServices SubSubSubSubCategoryServices;
        private SubSubSubCategoryServices SubSubSubCategoryServices;
        private SubSubCategoryServices SubSubCategoryServices;
        private CategoryServices CategoryServices;
        private SubCategoryServices SubCategoryServices;
        private UnitOfWork unitOfWork;



        public BrandController()
        {

            unitOfWork = new UnitOfWork();
            BrandServices = new BrandServices(unitOfWork);
            SubSubSubSubCategoryServices = new SubSubSubSubCategoryServices(unitOfWork);
            SubSubSubCategoryServices = new SubSubSubCategoryServices(unitOfWork);
            SubSubCategoryServices = new SubSubCategoryServices(unitOfWork);
            SubCategoryServices = new SubCategoryServices(unitOfWork);
            CategoryServices = new CategoryServices(unitOfWork);

        }
        // GET: Brand
        public ActionResult Index()
        {
            var data = BrandServices.GetAll();
            return View(data);
        }

        // GET: Brand/Details/5
        public ActionResult Details(int id)
        {
            var data = BrandServices.GetById(id);
            return View(data);
        }

        // GET: Brand/Create
        public ActionResult Create()
        {
          
            return View();
        }

        // POST: Brand/Create
        [HttpPost]
        public ActionResult Create(BrandViewModel brandViewModel)
        {
            try
            {
                // TODO: Add insert logic here
                BrandServices.Create(brandViewModel);
                return RedirectToAction("Index");
            }
            catch
            {
              
                return View();
            }


            
        }

        // GET: Brand/Edit/5
        public ActionResult Edit(int id)
        {
          
            var data =  BrandServices.GetById(id);
            return View(data);
        }

        // POST: Brand/Edit/5
        [HttpPost]
        public ActionResult Edit(BrandViewModel BrandViewModel)
        {
            try
            {
                // TODO: Add update logic here
                BrandServices.Update(BrandViewModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
            
        }

        // GET: Brand/Delete/5
        public ActionResult Delete(int id)
        {
            var data = BrandServices.GetById(id);
            return View(data);
        }

        // POST: Brand/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                // TODO: Add delete logic here
                BrandServices.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
