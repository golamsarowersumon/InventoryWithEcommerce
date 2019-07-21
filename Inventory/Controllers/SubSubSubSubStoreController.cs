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
    public class SubSubSubSubStoreController : Controller
    {
        private UnitOfWork unitOfWork;
        private SubSubSubSubStoreServices subSubSubSubStoreService;
        private StoreServices storeService;
        private SubStoreServices subStoreService;
        private SubSubStoreServices subSubStoreService;
        private SubSubSubStoreServices subSubSubStoreService;

        public SubSubSubSubStoreController()
        {
            unitOfWork = new UnitOfWork();
            subSubSubSubStoreService = new SubSubSubSubStoreServices(unitOfWork);
            storeService = new StoreServices(unitOfWork);
            subStoreService = new SubStoreServices(unitOfWork);
            subSubStoreService = new SubSubStoreServices(unitOfWork);
            subSubSubStoreService = new SubSubSubStoreServices(unitOfWork);

        }
        // GET:SubSubSubSubStore
        public ActionResult Index()
        {
            var data = subSubSubSubStoreService.GetAll();
            return View(data);
        }


        // GET: SubSubSubSubStore/Create
        public ActionResult Create()
        {
            ViewBag.StoreList = new SelectList(storeService.GetDropDown(), "Value", "Text");
            ViewBag.SubStoreList = new SelectList(subStoreService.GetDropDown(), "Value", "Text");
            ViewBag.SubSubStoreList = new SelectList(subSubStoreService.GetDropDown(), "Value", "Text");
            ViewBag.SubSubSubStoreList = new SelectList(subSubSubStoreService.GetDropDown(), "Value", "Text");

            return View();
        }

        // POST: SubSubSubSubStore/Create
        [HttpPost]
        public ActionResult Create(SubSubSubSubStoreViewModel subSubSubSubStoreVM)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    subSubSubSubStoreService.Create(subSubSubSubStoreVM);

                }


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SubSubSubSubStore/Details/5
        public ActionResult Details(int id = 0)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SubSubSubSubStoreViewModel details = subSubSubSubStoreService.GetById(id);
            if (details == null)
            {
                return HttpNotFound();
            }
            return View(details);

        }

        // GET:SubSubSubSubStore/Edit/5
        public ActionResult Edit(int id)
        {
            var edit = subSubSubSubStoreService.GetById(id);
            ViewBag.StoreList = new SelectList(storeService.GetDropDown(), "Value", "Text",edit.StoreId);
            ViewBag.SubStoreList = new SelectList(subStoreService.GetDropDown(), "Value", "Text",edit.SubStore);
            ViewBag.SubSubStoreList = new SelectList(subSubStoreService.GetDropDown(), "Value", "Text",edit.SubSubStoreId);
            ViewBag.SubSubSubStoreList = new SelectList(subSubSubStoreService.GetDropDown(), "Value", "Text",edit.SubSubSubStoreId);
            return View(edit);
        }

        // POST:  SubSubSubSubStore/Edit/5
        [HttpPost]
        public ActionResult Edit(SubSubSubSubStoreViewModel subSubSubSubStoreVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add update logic here

                    subSubSubSubStoreService.Update(subSubSubSubStoreVM);
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
                var delete = subSubSubSubStoreService.GetById(id);
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
                    subSubSubSubStoreService.Delete(id);

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
