using Core.Services;
using Domain.Repositories;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Domain.Controllers
{



    public class DistrictController : Controller
    {
        private UnitOfWork UnitOfWork;
        private DistrictServices districtServices;
        private CountryServices countryServices;

        public DistrictController()
        {

            UnitOfWork = new UnitOfWork();
            districtServices = new DistrictServices(UnitOfWork);
            countryServices = new CountryServices(UnitOfWork);
        }
        // GET: Country
        public ActionResult Index()
        {
            var data = districtServices.GetAll();
            return View(data);
        }

        // GET: Country/Details/5
        public ActionResult Details(int id = 0)
        {

            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DistrictViewModel districtViewModel = districtServices.GetByID(id);
            if (districtViewModel == null)
            {
                return HttpNotFound();
            }
            return View(districtViewModel);



        }

        // GET: Country/Create
        public ActionResult Create()
        {
            Loaddropdown();
            return View();
        }

        // POST: Country/Create
        [HttpPost]
        public ActionResult Create(DistrictViewModel districtViewModel)
        {
            try
            {
                // TODO: Add insert logic here
                districtServices.Create(districtViewModel);
              
                Loaddropdown();
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
            var data = districtServices.GetByID(id);
            return View(data);
        }

        // POST: Country/Edit/5
        [HttpPost]
        public ActionResult Edit(DistrictViewModel districtViewModel)
        {


            if (ModelState.IsValid)
            {

                try
                {
                    // TODO: Add update logic here

                    districtServices.Update(districtViewModel);
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

                var data = districtServices.GetByID(id);
                return View(data);
            }

            return View();

        }

        // POST: Country/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)

        {
            //if (ModelState.IsValid)
            //{

            try
            {
                // TODO: Add update logic here



                districtServices.Delete(id);
                TempData["message"] = "Deleted Successfully";
                ModelState.Clear();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(ex);
            }


            //}

        }

        public void Loaddropdown()
        {
            var CountryList = countryServices.GetAll();
            ViewBag.countrylst = new SelectList(CountryList, "CountryId", "CountryName");
        }
    }
 
}
