using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Core.Services;
using Domain.Repositories;
using Domain;
using Domain.ViewModels;

namespace Inventory.Controllers
{
    public class ProvinceController : Controller
    {
      
        private ProvinceServices ProvinceServices;
        CountryServices CountryServices;
        private UnitOfWork unitOfWork;


        public ProvinceController()
        {

            unitOfWork = new UnitOfWork();
            ProvinceServices = new ProvinceServices(unitOfWork);
            CountryServices = new CountryServices(unitOfWork);

        }

        // GET: Province
        public ActionResult Index()
        {
            var data = ProvinceServices.GetAll();
            return View(data);
        }

        // GET: Province/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProvinceViewModel provinceViewModel = ProvinceServices.GetByID(id);
            if (provinceViewModel == null)
            {
                return HttpNotFound();
            }
            return View(provinceViewModel);
        }

        // GET: Province/Create
        public ActionResult Create()
        {
            loadcountry();
            return View();
        }

        // POST: Province/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProvinceViewModel provinceViewModel)
        {
            if (ModelState.IsValid)
            {
                ProvinceServices.Create(provinceViewModel);
                TempData["message"] = "Inserted Successfully";
                return RedirectToAction("Index");
            }

            loadcountry();
            return View(provinceViewModel);
        }

        // GET: Province/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            loadcountry();
            ProvinceViewModel provinceViewModel = ProvinceServices.GetByID(id);
            if (provinceViewModel == null)
            {
                return HttpNotFound();
            }
       

            return View(provinceViewModel);
        }

        // POST: Province/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProvinceViewModel provinceViewModel)
        {
            if (ModelState.IsValid)
            {
                ProvinceServices.Update(provinceViewModel);
                TempData["message"] = "Updated Successfully";
                return RedirectToAction("Index");
            }
           
            return View(provinceViewModel);
        }

        // GET: Province/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProvinceViewModel provinceViewModel = ProvinceServices.GetByID(id);
            if (provinceViewModel == null)
            {
                return HttpNotFound();
            }
            return View(provinceViewModel);
        }

        // POST: Province/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProvinceServices.Delete(id);
            TempData["message"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }




        public void loadcountry()
        {
            var countrylist = CountryServices.GetAll();
            ViewBag.countrylist = new SelectList(countrylist, "CountryId", "CountryName");

        }
    }
}
