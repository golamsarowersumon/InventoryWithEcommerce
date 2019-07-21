using Core.Services;
using Domain.Repositories;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class StateController : Controller
    {
        private StateServices StateServices;
        CountryServices CountryServices;
        private UnitOfWork unitOfWork;



        public StateController()
        {

            unitOfWork = new UnitOfWork();
            StateServices = new StateServices(unitOfWork);
            CountryServices = new CountryServices(unitOfWork);

        }


        // GET: State
        public ActionResult Index()
        {
            var data = StateServices.GetAll();
            return View(data);

        }

        // GET: State/Details/5
        public ActionResult Details(int id)
        {
            var post = StateServices.GetByID(id);
            return View(post);

        }

        // GET: State/Create
        public ActionResult Create()
        {
            loadcountry();
            return View();
        }

        // POST: State/Create
        [HttpPost]
        public ActionResult Create(StateViewModel StateViewModel)
        {
            try
            {
                // TODO: Add insert logic here
                StateServices.Create(StateViewModel);
                TempData["message"] = "Inserted Successfully";
                return RedirectToAction("Index");
            }
            catch
            {
                return View(StateViewModel);
            }
        }

        // GET: State/Edit/5
        public ActionResult Edit(int? id)
        {
            loadcountry();
            StateViewModel StateViewModel = StateServices.GetByID(id);
            return View(StateViewModel);

        }

        // POST: State/Edit/5
        [HttpPost]
        public ActionResult Edit(StateViewModel StateViewModel)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    // TODO: Add update logic here

                    StateServices.Update(StateViewModel);
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

        // GET: State/Delete/5
        public ActionResult Delete(int id)
        {
            var post = StateServices.GetByID(id);
            return View(post);

        }

        // POST: State/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                // TODO: Add delete logic here
                StateServices.Delete(id);
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
