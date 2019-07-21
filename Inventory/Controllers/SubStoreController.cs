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
    public class SubStoreController : Controller
    {
        private UnitOfWork unitOfWork;
        private SubStoreServices subStoreService;
        private StoreServices storeService;

        public SubStoreController()
        {
            unitOfWork = new UnitOfWork();
            subStoreService = new SubStoreServices(unitOfWork);
            storeService = new StoreServices(unitOfWork);
            
        }
        // GET:SubStore
        public ActionResult Index()
        {
            var data = subStoreService.GetAll();
            return View(data);
        }


        // GET: SubStore/Create
        public ActionResult Create()
        {
            ViewBag.StoreList = new SelectList(storeService.GetDropDown(), "Value", "Text");
            return View();
        }

        // POST: SubStore/Create
        [HttpPost]
        public ActionResult Create(SubStoreViewModel subStoreVM)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    subStoreService.Create(subStoreVM);

                }


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SubStore/Details/5
        public ActionResult Details(int id = 0)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SubStoreViewModel details = subStoreService.GetById(id);
            if (details == null)
            {
                return HttpNotFound();
            }
            return View(details);

        }

        // GET:SubStore/Edit/5
        public ActionResult Edit(int id)
        {
            var edit = subStoreService.GetById(id);
            ViewBag.StoreList = new SelectList(storeService.GetDropDown(), "Value", "Text");
            return View(edit);
        }

        // POST:  SubStore/Edit/5
        [HttpPost]
        public ActionResult Edit(SubStoreViewModel subStoreVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add update logic here

                    subStoreService.Update(subStoreVM);

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
                var delete = subStoreService.GetById(id);
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
                    subStoreService.Delete(id);

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
