using Core.Services;
using Domain.Repositories;
using System;
using Domain.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class CompanyController : Controller
    {
        private UnitOfWork UnitOfWork;
        private CompanyServices CompanyServices;


        public CompanyController()
        {

            UnitOfWork = new UnitOfWork();
            CompanyServices = new CompanyServices(UnitOfWork);

        }
        // GET: Country
        public ActionResult Index()
        {
            var data = CompanyServices.GetAll();
            return View(data);
        }

        // GET: Country/Details/5
        public ActionResult Details(int id = 0)
        {

            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyViewModel companyViewModel = CompanyServices.GetByID(id);
            if (companyViewModel == null)
            {
                return HttpNotFound();
            }
            return View(companyViewModel);



        }

        // GET: Country/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Country/Create
        [HttpPost]
        public ActionResult Create(CompanyViewModel companyViewModel)
        {
            try
            { 
                // TODO: Add insert logic here
                CompanyServices.Create(companyViewModel);
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
            var data = CompanyServices.GetByID(id);
            return View(data);
        }

        // POST: Country/Edit/5
        [HttpPost]
        public ActionResult Edit(CompanyViewModel companyViewModel)
        {


            if (ModelState.IsValid)
            {

                try
                {
                    // TODO: Add update logic here

                    CompanyServices.Update(companyViewModel);
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

                var data = CompanyServices.GetByID(id);
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



                CompanyServices.Delete(id);
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
    }
}
