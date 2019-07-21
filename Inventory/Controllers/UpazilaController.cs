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
    public class UpazilaController : Controller
    {
        private UnitOfWork unitOfWork;
        private UpazilaServices upazilaService;
        private DistrictServices districtServices;

        public UpazilaController()
        {
            unitOfWork = new UnitOfWork();
            upazilaService = new UpazilaServices(unitOfWork);
            districtServices = new DistrictServices(unitOfWork);
        }
        // GET: Upazila
        public ActionResult Index()
        {
            var data = upazilaService.GetAll();
            return View(data);
        }


        // GET: Upazila/Create
        public ActionResult Create()
        {
            LoadDropdown();
            return View();
        }

        // POST: Upazila/Create
        [HttpPost]
        public ActionResult Create(UpazilaViewModel upazilaVM)
        {
            try
            {
                // TODO: Add insert logic here
               
                    upazilaService.Create(upazilaVM);
                

                LoadDropdown();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Upazila/Details/5
        public ActionResult Details(int id = 0)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UpazilaViewModel details = upazilaService.GetById(id);
            if (details == null)
            {
                return HttpNotFound();
            }
            return View(details);

        }

        // GET: Upazila/Edit/5
        public ActionResult Edit(int id)
        {
            var edit = upazilaService.GetById(id);
            return View(edit);
        }

        // POST:  Upazila/Edit/5
        [HttpPost]
        public ActionResult Edit(UpazilaViewModel upazilaVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add update logic here

                    upazilaService.Update(upazilaVM);
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
                var delete = upazilaService.GetById(id);
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
                    upazilaService.Delete(id);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }


        public void LoadDropdown()
        {
            var districlist = districtServices.GetAll();
            ViewBag.districlst = new SelectList(districlist, "DistrictId", "DistrictName");
        }
    }
}
