using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class StoreAssignController : Controller
    {
        private UnitOfWork unitOfWork;
        private StoreAssignServices storeAssignServices;

        public StoreAssignController() {

            unitOfWork = new UnitOfWork();
            storeAssignServices = new StoreAssignServices(unitOfWork);

        }

        // GET: StoreAssign
        public ActionResult Index()
        {
           
            return View();
        }

        // GET: StoreAssign/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StoreAssign/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StoreAssign/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: StoreAssign/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StoreAssign/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: StoreAssign/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StoreAssign/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
