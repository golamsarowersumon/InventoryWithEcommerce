using Core.Services;
using Domain.Repositories;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Domain.Controllers
{
    public class PrincipleController : Controller
    {
        private PrincipleServices principleServices;
        CountryServices CountryServices;
        private UnitOfWork unitOfWork;



        public PrincipleController()
        {

            unitOfWork = new UnitOfWork();
            principleServices = new PrincipleServices(unitOfWork);
            CountryServices = new CountryServices(unitOfWork);

        }


        // GET: Principle
        public ActionResult Index()
        {
            var data = principleServices.GetAll();
            return View(data);

        }

        // GET: Principle/Details/5
        public ActionResult Details(int id)
        {
            var post = principleServices.GetByID(id);
            return View(post);

        }

        // GET: Principle/Create
        public ActionResult Create()
        {
            loadcountry();
            return View();
        }

        // POST: Principle/Create
        [HttpPost]
        public ActionResult Create(PrincipleViewModel pricipleViewModel)
        {
            try
            {
                // TODO: Add insert logic here
                principleServices.Create(pricipleViewModel);
                TempData["message"] = "Inserted Successfully";
                return RedirectToAction("Index");
            }
            catch
            {
                return View(pricipleViewModel);
            }
        }

        // GET: Principle/Edit/5
        public ActionResult Edit(int? id)
        {
            loadcountry();
            PrincipleViewModel pricipleViewModel = principleServices.GetByID(id);
            return View(pricipleViewModel);

        }

        // POST: Principle/Edit/5
        [HttpPost]
        public ActionResult Edit(PrincipleViewModel principleViewModel)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    // TODO: Add update logic here

                    principleServices.Update(principleViewModel);
                    TempData["message"] = "Update Successfully";
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }

            }
            loadcountry();
            return View();
        }

        // GET: Principle/Delete/5
        public ActionResult Delete(int id)
        {
            var post = principleServices.GetByID(id);
            return View(post);

        }

        // POST: Principle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                // TODO: Add delete logic here
                principleServices.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }




        public void loadcountry()
        {
            var countrylist = CountryServices.GetAll();
            ViewBag.countrylist = new SelectList(countrylist, "CountryId", "CountryName");

        }
    }
}

