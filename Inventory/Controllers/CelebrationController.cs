using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.ViewModels;
using Domain.Repositories;
using Core.Services;


namespace Inventory.Controllers
{
    public class CelebrationController : Controller
    {

        private UnitOfWork UnitOfWork;
        private CelebrationServices CelebrationServices;


        public CelebrationController() {

            UnitOfWork = new UnitOfWork();
            CelebrationServices = new CelebrationServices(UnitOfWork);

        }

        // GET: Celebration
        public ActionResult Index()
        {
            var data = CelebrationServices.GetAll();
            return View(data);
        }

        // GET: Celebration/Details/5
        public ActionResult Details(int id)
        {
            var data = CelebrationServices.GetByID(id);
            return View(data);
        }

        // GET: Celebration/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Celebration/Create
        [HttpPost]
        public ActionResult Create(CelebrationViewModel celebrationViewModel)
        {
            try
            {
                // TODO: Add insert logic here
                CelebrationServices.Create(celebrationViewModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Celebration/Edit/5
        public ActionResult Edit(int id)
        {
            var data = CelebrationServices.GetByID(id);
            return View(data);
        }

        // POST: Celebration/Edit/5
        [HttpPost]
        public ActionResult Edit(CelebrationViewModel celebrationViewModel)
        {
            try
            {
                // TODO: Add update logic here
                CelebrationServices.Update(celebrationViewModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Celebration/Delete/5
        public ActionResult Delete(int id)
        {
            var data = CelebrationServices.GetByID(id);
            return View(data);
        }

        // POST: Celebration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                // TODO: Add delete logic here
                CelebrationServices.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
