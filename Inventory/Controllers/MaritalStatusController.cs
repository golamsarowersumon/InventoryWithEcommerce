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
    public class MaritalStatusController : Controller
    {
        private UnitOfWork UnitOfWork;
        private MaritalStatusServices MaritalStatusServices;

        public MaritalStatusController() {

            UnitOfWork = new UnitOfWork();
            MaritalStatusServices = new MaritalStatusServices(UnitOfWork);
        }


        // GET: MaritalStatus
        public ActionResult Index()
        {
            var data = MaritalStatusServices.GetAll();
            return View(data);
        }

        // GET: MaritalStatus/Details/5
        public ActionResult Details(int id)
        {
            var data = MaritalStatusServices.GetByID(id);
            return View(data);
           
        }

        // GET: MaritalStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MaritalStatus/Create
        [HttpPost]
        public ActionResult Create(MaritalStatusViewModel maritalStatusViewModel)
        {
            try
            {
                // TODO: Add insert logic here
                MaritalStatusServices.Create(maritalStatusViewModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MaritalStatus/Edit/5
        public ActionResult Edit(int id)
        {
            var data = MaritalStatusServices.GetByID(id);
            return View(data);
        }

        // POST: MaritalStatus/Edit/5
        [HttpPost]
        public ActionResult Edit(MaritalStatusViewModel maritalStatusViewModel)
        {
            try
            {
                // TODO: Add update logic here
                MaritalStatusServices.Update(maritalStatusViewModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MaritalStatus/Delete/5
        public ActionResult Delete(int id)
        {

            var data = MaritalStatusServices.GetByID(id);
            return View(data);
        }

        // POST: MaritalStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                // TODO: Add delete logic here
                MaritalStatusServices.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
