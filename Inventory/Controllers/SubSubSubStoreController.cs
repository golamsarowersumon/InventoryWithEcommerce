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
    public class SubSubSubStoreController : Controller
    {
        private UnitOfWork unitOfWork;
        private SubSubSubStoreServices subSubSubStoreService;
        private StoreServices storeService;
        private SubStoreServices subStoreService;
        private SubSubStoreServices subSubStoreService;

        public SubSubSubStoreController()
        {
            unitOfWork = new UnitOfWork();
            subSubSubStoreService = new SubSubSubStoreServices(unitOfWork);
            storeService = new StoreServices(unitOfWork);
            subStoreService = new SubStoreServices(unitOfWork);
            subSubStoreService = new SubSubStoreServices(unitOfWork);
        }
        // GET:SubSubSubStore
        public ActionResult Index()
        {
            var data = subSubSubStoreService.GetAll();
            return View(data);
        }


        // GET: SubSubSubStore/Create
        public ActionResult Create()
        {
            ViewBag.StoreList = new SelectList(storeService.GetDropDown(), "Value", "Text");
            ViewBag.SubStoreList = new SelectList(subStoreService.GetDropDown(), "Value", "Text");
            ViewBag.SubSubStoreList = new SelectList(subSubStoreService.GetDropDown(), "Value", "Text");
            return View();
        }

        // POST:SubSubSubStore/Create
        [HttpPost]
        public ActionResult Create(SubSubSubStoreViewModel subSubSubStoreVM)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    subSubSubStoreService.Create(subSubSubStoreVM);

                }


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SubSubSubStore/Details/5
        public ActionResult Details(int id = 0)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SubSubSubStoreViewModel details = subSubSubStoreService.GetById(id);
            if (details == null)
            {
                return HttpNotFound();
            }
            return View(details);

        }

        // GET: SubSubSubStore/Edit/5
        public ActionResult Edit(int id)
        {
            var edit = subSubSubStoreService.GetById(id);
            ViewBag.StoreList = new SelectList(storeService.GetDropDown(), "Value", "Text");
            ViewBag.SubStoreList = new SelectList(subStoreService.GetDropDown(), "Value", "Text");
            ViewBag.SubSubStoreList = new SelectList(subSubStoreService.GetDropDown(), "Value", "Text");

            return View(edit);
        }

        // POST:  SubSubSubStore/Edit/5
        [HttpPost]
        public ActionResult Edit(SubSubSubStoreViewModel subSubSubStoreVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add update logic here

                    subSubSubStoreService.Update(subSubSubStoreVM);
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
                var delete = subSubSubStoreService.GetById(id);
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
                    subSubSubStoreService.Delete(id);

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
