using Core.Services;
using Domain.Repositories;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class StoreController : Controller
    {
        private UnitOfWork unitOfWork;
        private StoreServices storeService;

        public StoreController()
        {
            unitOfWork = new UnitOfWork();
            storeService = new StoreServices(unitOfWork);
        }
        // GET:  Store
        public ActionResult Index()
        {
            var data = storeService.GetAll();
            return View(data);
        }


        // GET:  Store/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST:  Store/Create
        [HttpPost]
        public ActionResult Create(StoreViewModel storeVM)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    storeService.Create(storeVM);

                }


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Store/Details/5
        public ActionResult Details(int id = 0)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            StoreViewModel details = storeService.GetById(id);
            if (details == null)
            {
                return HttpNotFound();
            }
            return View(details);

        }

        // GET:  Store/Edit/5
        public ActionResult Edit(int id)
        {
            var edit = storeService.GetById(id);
            return View(edit);
        }

        // POST:   Store/Edit/5
        [HttpPost]
        public ActionResult Edit(StoreViewModel storeVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add update logic here

                    storeService.Update(storeVM);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        public ActionResult Delete(int id = 0)
        {
            if (id != 0)
            {
                var delete = storeService.GetById(id);
                return View(delete);
            }
            return View();
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    storeService.Delete(id);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }
    }
}
