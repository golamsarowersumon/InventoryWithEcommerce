using Domain.Repositories;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Domain.ViewModels;

namespace Domain.Controllers
{
    public class ProcurementTypeController : Controller
    {
        private UnitOfWork UnitOfWork;
        private ProcrurementTypeServices ProcrurementServices;


        public ProcurementTypeController()
        {

            UnitOfWork = new UnitOfWork();
            ProcrurementServices = new ProcrurementTypeServices(UnitOfWork);

        }
        // GET: Country
        public ActionResult Index()
        {
            var data = ProcrurementServices.GetAll();
            return View(data);
        }

        // GET: Country/Details/5
        public ActionResult Details(int id = 0)
        {

            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcurementTypeViewModel ProcurementViewModel = ProcrurementServices.GetById(id);
            if (ProcurementViewModel == null)
            {
                return HttpNotFound();
            }
            return View(ProcurementViewModel);



        }

        // GET: Country/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Country/Create
        [HttpPost]
        public ActionResult Create(ProcurementTypeViewModel ProcurementViewModel)
        {
            try
            {
                // TODO: Add insert logic here
                ProcrurementServices.Create(ProcurementViewModel);
                TempData["message"] = "Inserted Successfully";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Country/Edit/5
        public ActionResult Edit(int id)
        {
            var data = ProcrurementServices.GetById(id);
            return View(data);
        }

        // POST: Country/Edit/5
        [HttpPost]
        public ActionResult Edit(ProcurementTypeViewModel ProcurementViewModel)
        {


            if (ModelState.IsValid)
            {

                try
                {
                    // TODO: Add update logic here

                    ProcrurementServices.Update(ProcurementViewModel);
                    TempData["message"] = "Update Successfully";
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }

            }

            return View();

        }

        // GET: Country/Delete/5
        public ActionResult Delete(int id = 0)
        {
            if (id != 0)
            {

                var data = ProcrurementServices.GetById(id);
                return View(data);
            }

            return View();

        }

        // POST: Country/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)

        {
            if (ModelState.IsValid)
            {

                try
                {
                    // TODO: Add update logic here



                    ProcrurementServices.Delete(id);
                    TempData["message"] = "Deleted Successfully";
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }

            }
            return RedirectToAction("Index");
        }
    }
}
