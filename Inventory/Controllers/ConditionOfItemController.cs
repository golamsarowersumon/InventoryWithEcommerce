using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Models;
using Domain.ViewModels;
using Domain.Repositories;
using Core.Services;
namespace Inventory.Controllers
{
    public class ConditionOfItemController : Controller
    {
        private ConditionOfItemServices ConditionOfItemServices;
        private UnitOfWork UnitOfWork;

        public ConditionOfItemController()
        {
            UnitOfWork = new UnitOfWork();
            ConditionOfItemServices = new ConditionOfItemServices(UnitOfWork);

        }

        // GET: ConditionOfItem
        public ActionResult Index()
        {
            var data = ConditionOfItemServices.GetAll();
            return View(data);
        }

        // GET: ConditionOfItem/Details/5
        public ActionResult Details(int id)
        {
            var data = ConditionOfItemServices.GetByID(id);
            return View(data);
        }

        // GET: ConditionOfItem/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ConditionOfItem/Create
        [HttpPost]
        public ActionResult Create(ConditionOfItemViewModel conditionOfItemViewModel)
        {
            try
            {
                // TODO: Add insert logic here
                ConditionOfItemServices.Create(conditionOfItemViewModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ConditionOfItem/Edit/5
        public ActionResult Edit(int id)
        {

            var data = ConditionOfItemServices.GetByID(id);
            return View(data);
        }

        // POST: ConditionOfItem/Edit/5
        [HttpPost]
        public ActionResult Edit(ConditionOfItemViewModel conditionOfItemViewModel)
        {
            try
            {
                // TODO: Add update logic here
                ConditionOfItemServices.Update(conditionOfItemViewModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ConditionOfItem/Delete/5
        public ActionResult Delete(int id)
        {
            var data = ConditionOfItemServices.GetByID(id);
            return View(data);
        }

        // POST: ConditionOfItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                // TODO: Add delete logic here
                ConditionOfItemServices.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
