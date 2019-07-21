using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Domain.Models;
using Domain.Repositories;
using Domain.ViewModels;
using Core.Services;

namespace Inventory.Controllers
{
    public class DivisionController : Controller
    {
        private UnitOfWork UnitOfWork;
        private DivisionServices DivisionServices;


        public DivisionController()
        {

            UnitOfWork = new UnitOfWork();
            DivisionServices = new DivisionServices(UnitOfWork);

        }
        // GET: Division
        public ActionResult Index()
        {
            var data = DivisionServices.GetAll();
            return View(data);
        }

        // GET: Division/Details/5
        public ActionResult Details(int id)
        {
            DivisionViewModel divisionViewModel = DivisionServices.GetByID(id);
            return View(divisionViewModel);
        }

        // GET: Division/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Division/Create
        [HttpPost]
        public ActionResult Create(DivisionViewModel DivisionViewModel)
        {
            try
            {
                // TODO: Add insert logic here
                DivisionServices.Create(DivisionViewModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Division/Edit/5
        public ActionResult Edit(int id)
        {
            DivisionViewModel DivisionViewModel = DivisionServices.GetByID(id);
            return View(DivisionViewModel);
        }

        // POST: Division/Edit/5
        [HttpPost]
        public ActionResult Edit(DivisionViewModel DivisionViewModel)
        {
            try
            {
                // TODO: Add update logic here
                DivisionServices.Update(DivisionViewModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Division/Delete/5
        public ActionResult Delete(int id)
        {
            var data = DivisionServices.GetByID(id);
            return View(data);
        }

        // POST: Division/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                // TODO: Add delete logic here
                DivisionServices.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
