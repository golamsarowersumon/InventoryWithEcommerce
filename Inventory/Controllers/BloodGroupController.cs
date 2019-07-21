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
    public class BloodGroupController : Controller
    {
        private UnitOfWork unitOfWork;
        private BloodGroupServices bloodGroupService;

        public BloodGroupController()
        {
            unitOfWork = new UnitOfWork();
            bloodGroupService = new BloodGroupServices(unitOfWork);
        }
        // GET: BloodGroup
        public ActionResult Index()
        {
            var data = bloodGroupService.GetAll();
            return View(data);
        }


        // GET: BloodGroup/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: BloodGroup/Create
        [HttpPost]
        public ActionResult Create(BloodGroupViewModel bloodGroupVM)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    bloodGroupService.Create(bloodGroupVM);

                }


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BloodGroup/Details/5
        public ActionResult Details(int id = 0)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BloodGroupViewModel details = bloodGroupService.GetById(id);
            if (details == null)
            {
                return HttpNotFound();
            }
            return View(details);

        }

        // GET: BloodGroup/Edit/5
        public ActionResult Edit(int id)
        {
            var edit = bloodGroupService.GetById(id);
            return View(edit);
        }

        // POST:  BloodGroup/Edit/5
        [HttpPost]
        public ActionResult Edit(BloodGroupViewModel bloodGroupVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add update logic here

                    bloodGroupService.Update(bloodGroupVM);
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
                var delete = bloodGroupService.GetById(id);
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
                    bloodGroupService.Delete(id);

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
