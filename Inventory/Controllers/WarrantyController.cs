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
    public class WarrantyController : Controller
    {
        private UnitOfWork unitOfWork;
        private WarrantyServices warrantyService;

        public WarrantyController()
        {
            unitOfWork = new UnitOfWork();
            warrantyService = new WarrantyServices(unitOfWork);
        }
        // GET: Warranty
        public ActionResult Index()
        {
            var data = warrantyService.GetAll();
            return View(data);
        }


        // GET: Warranty/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Warranty/Create
        [HttpPost]
        public ActionResult Create(WarrantyViewModel warrantyVM)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    warrantyService.Create(warrantyVM);

                }


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Warranty/Details/5
        public ActionResult Details(int id = 0)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            WarrantyViewModel details = warrantyService.GetById(id);
            if (details == null)
            {
                return HttpNotFound();
            }
            return View(details);

        }

        // GET: Warranty/Edit/5
        public ActionResult Edit(int id)
        {
            var edit = warrantyService.GetById(id);
            return View(edit);
        }

        // POST:  Warranty/Edit/5
        [HttpPost]
        public ActionResult Edit(WarrantyViewModel warrantyVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add update logic here

                    warrantyService.Update(warrantyVM);
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
                var delete = warrantyService.GetById(id);
                return View();
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
                    warrantyService.Delete(id);

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
