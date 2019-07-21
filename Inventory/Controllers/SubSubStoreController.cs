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
    public class SubSubStoreController : Controller
    {
        private UnitOfWork unitOfWork;
        private SubSubStoreServices subSubStoreService;
        private StoreServices storeService;
        private SubStoreServices subStoreService;

        public SubSubStoreController()
        {
            unitOfWork = new UnitOfWork();
            subSubStoreService = new SubSubStoreServices(unitOfWork);
            storeService = new StoreServices(unitOfWork);
            subStoreService = new SubStoreServices(unitOfWork);
        }
        // GET:SubSubStore
        public ActionResult Index()
        {
            var data = subSubStoreService.GetAll();
            return View(data);
        }


        // GET: SubSubStore/Create
        public ActionResult Create()
        {
            ViewBag.StoreList = new SelectList(storeService.GetDropDown(), "Value", "Text");
            ViewBag.SubStoreList = new SelectList(subStoreService.GetDropDown(), "Value", "Text");
            return View();
        }

        // POST: SubSubStore/Create
        [HttpPost]
        public ActionResult Create(SubSubStoreViewModel subSubStoreVM)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    subSubStoreService.Create(subSubStoreVM);

                }


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SubSubStore/Details/5
        public ActionResult Details(int id = 0)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SubSubStoreViewModel details = subSubStoreService.GetById(id);

            if (details == null)
            {
                return HttpNotFound();
            }
            return View(details);

        }

        // GET: SubSubStore/Edit/5
        public ActionResult Edit(int id)
        {
            var edit = subSubStoreService.GetById(id);
            ViewBag.StoreList = new SelectList(storeService.GetDropDown(), "Value", "Text");
            ViewBag.SubStoreList = new SelectList(subStoreService.GetDropDown(), "Value", "Text");
            return View(edit);
        }

        // POST:  SubSubStore/Edit/5
        [HttpPost]
        public ActionResult Edit(SubSubStoreViewModel subSubStoreVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add update logic here

                    subSubStoreService.Update(subSubStoreVM);
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
                var delete = subSubStoreService.GetById(id);
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
                    subSubStoreService.Delete(id);

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
