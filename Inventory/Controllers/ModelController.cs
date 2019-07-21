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
    public class ModelController : Controller
    {
        private ModelServices ModelServices;
        BrandServices BrandServices;
        private UnitOfWork unitOfWork;



        public ModelController()
        {

            unitOfWork = new UnitOfWork();
            ModelServices = new ModelServices(unitOfWork);
            BrandServices = new BrandServices(unitOfWork);

        }

        // GET: Model
        public ActionResult Index()
        {

            var data = ModelServices.GetAll();
            return View(data);
        }

        // GET: Model/Details/5
        public ActionResult Details(int id)
        {

            var data = ModelServices.GetById(id);
            return View(data);
        }

        // GET: Model/Create
        public ActionResult Create()
        {

           
            return View();
        }

        // POST: Model/Create
        [HttpPost]
        public ActionResult Create(ModelViewModel ModelViewModel)
        {
            try
            {
                //if (ModelViewModel.BrandName == " ")
                //{

                //    TempData["message"] = "Please Enter Brand Name!!";
                //}
                // TODO: Add insert logic here
                ModelServices.Create(ModelViewModel);
                return RedirectToAction("Index");
            }
            catch
            {
               
                return View();
            }
        }

        // GET: Model/Edit/5
        public ActionResult Edit(int id)
        {
           

            ModelViewModel modelViewModel = ModelServices.GetById(id);
            return View(modelViewModel);
        }

        // POST: Model/Edit/5
        [HttpPost]
        public ActionResult Edit(ModelViewModel modelViewModel)
        {
            try
            {
                // TODO: Add update logic here
                ModelServices.Update(modelViewModel);
                return RedirectToAction("Index");
            }
            catch
            {
               
                return View(modelViewModel);
            }
        }

        // GET: Model/Delete/5
        public ActionResult Delete(int id)
        {
            var data = ModelServices.GetById(id);
            return View(data);
        }

        // POST: Model/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                // TODO: Add delete logic here
                ModelServices.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
