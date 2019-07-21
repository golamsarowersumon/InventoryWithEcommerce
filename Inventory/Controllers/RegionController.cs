using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Models;
using Domain.Repositories;
using Domain.ViewModels;
using Core.Services;

namespace Inventory.Controllers
{
    public class RegionController : Controller
    {
        private UnitOfWork UnitOfWork;
        private RegionServices RegionServices;

        public RegionController() {
            UnitOfWork = new UnitOfWork();
            RegionServices = new RegionServices(UnitOfWork);



        }

        // GET: Region
        public ActionResult Index()
        {
            var data = RegionServices.GetAll();
            return View(data);
        }

        // GET: Region/Details/5
        public ActionResult Details(int id)
        {
            var data = RegionServices.GetByID(id);
            return View(data);
        }

        // GET: Region/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Region/Create
        [HttpPost]
        public ActionResult Create(RegionViewModel regionViewModel)
        {
            try
            {
                // TODO: Add insert logic here
                RegionServices.Create(regionViewModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Region/Edit/5
        public ActionResult Edit(int id)
        {
            var data = RegionServices.GetByID(id);
            return View(data);
        }

        // POST: Region/Edit/5
        [HttpPost]
        public ActionResult Edit(RegionViewModel regionViewModel)
        {
            try
            {
                // TODO: Add update logic here
                RegionServices.Update(regionViewModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Region/Delete/5
        public ActionResult Delete(int id)
        {
            var data = RegionServices.GetByID(id);
            return View(data);
        }

        // POST: Region/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                // TODO: Add delete logic here
                RegionServices.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
