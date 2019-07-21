using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Repositories;
using Domain.ViewModels;
using System.Data;
using System.Data.Entity;
using Core.Services;
using System.IO;
using System.Net;

namespace Domain.Controllers
{
    public class CountryController : Controller
    {
        private UnitOfWork UnitOfWork;
        private CountryServices CountryServices;
        

            public CountryController()
        {

            UnitOfWork = new UnitOfWork();
            CountryServices = new CountryServices(UnitOfWork);

        }
        // GET: Country
        public ActionResult Index()
        {
            var data = CountryServices.GetAll();
            return View(data);
        }

        // GET: Country/Details/5
        public ActionResult Details(int id=0)
        {

            if (id == 0 )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CountryViewModel countryViewModel = CountryServices.GetByID(id);
            if (countryViewModel == null)
            {
                return HttpNotFound();
            }
            return View(countryViewModel);


            
        }

        // GET: Country/Create
        public ActionResult Create()
        {
          
            return View();
        }

        // POST: Country/Create
        [HttpPost]
        public ActionResult Create(CountryViewModel countryViewModel)
        {
            try
            {
                // TODO: Add insert logic here
                CountryServices.Create(countryViewModel);
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
            var data = CountryServices.GetByID(id);
            return View(data);
        }

        // POST: Country/Edit/5
        [HttpPost]
        public ActionResult Edit(CountryViewModel countryViewModel)
        {


            if (ModelState.IsValid)
            {

                try
                {
                    // TODO: Add update logic here

                    CountryServices.Update(countryViewModel);
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
        public ActionResult Delete(int id=0)
        {
            if (id !=0)
            {

                var data = CountryServices.GetByID(id);
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



                    CountryServices.Delete(id);
                    TempData["message"] = "Deleted Successfully";
                    ModelState.Clear();
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    return View(ex);
                }

            //}
          
        }
    }
}
