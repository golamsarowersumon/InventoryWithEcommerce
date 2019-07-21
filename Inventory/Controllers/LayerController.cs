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
    public class LayerController : Controller
    {
        private UnitOfWork unitOfWork;
        private LayerServices LayerServices;

        public LayerController()
        {
            unitOfWork = new UnitOfWork();
            LayerServices = new LayerServices(unitOfWork);
        }
        // GET: Layer
        public ActionResult Index()
        {
            var data = LayerServices.GetAll();
            return View(data);
        }

        // GET: Layer/Details/5
        public ActionResult Details(int id)
        {
            var data = LayerServices.GetById(id);
            return View(data);
        }

        // GET: Layer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Layer/Create
        [HttpPost]
        public ActionResult Create(LayerViewModel layerViewModel)
        {
            try
            {
                // TODO: Add insert logic here
                LayerServices.Create(layerViewModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Layer/Edit/5
        public ActionResult Edit(int id=0)
        {
            if (id > 0)
            {
                var data = LayerServices.GetById(id);
                return View(data);
            }
            return View();
        }

        // POST: Layer/Edit/5
        [HttpPost]
        public ActionResult Edit(LayerViewModel layerViewModel)
        {
            try
            {
                // TODO: Add update logic here
                LayerServices.Update(layerViewModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Layer/Delete/5
        public ActionResult Delete(int id=0)
        {
            if (id > 0)
            {
                var data = LayerServices.GetById(id);
                return View(data);
            }
            return View();
        }

        // POST: Layer/Delete/5
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                // TODO: Add delete logic here
                LayerServices.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


    }
}
