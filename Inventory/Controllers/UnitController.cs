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
    public class UnitController : Controller
    {
        private UnitOfWork unitOfWork;
        private UnitServices unitService;

        public UnitController()
        {
            unitOfWork = new UnitOfWork();
            unitService = new UnitServices(unitOfWork);
        }
        // GET: Unit
        public ActionResult Index()
        {
            var data = unitService.GetAll();
            return View(data);
        }


        // GET: Unit/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Unit/Create
        [HttpPost]
        public ActionResult Create(UnitViewModel unitVM)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    unitService.Create(unitVM);

                }


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Unit/Details/5
        public ActionResult Details(int id = 0)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UnitViewModel details = unitService.GetById(id);
            if (details == null)
            {
                return HttpNotFound();
            }
            return View(details);

        }

        // GET: Unit/Edit/5
        public ActionResult Edit(int id)
        {
            var edit = unitService.GetById(id);
            return View(edit);
        }

        // POST:  Unit/Edit/5
        [HttpPost]
        public ActionResult Edit(UnitViewModel unitVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add update logic here

                    unitService.Update(unitVM);
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
                var delete = unitService.GetById(id);
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
                    unitService.Delete(id);

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
