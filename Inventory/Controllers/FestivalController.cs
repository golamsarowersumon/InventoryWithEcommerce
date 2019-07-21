using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Core.Services;
using Domain;
using Domain.Repositories;
using Domain.ViewModels;

namespace Inventory.Controllers
{
    public class FestivalController : Controller
    {
        private UnitOfWork UnitOfWork;
        private FestivalServices FestivalServices;
        ReligionServices ReligionServices;


        public FestivalController()
        {

            UnitOfWork = new UnitOfWork();
            FestivalServices = new FestivalServices(UnitOfWork);
            ReligionServices = new ReligionServices(UnitOfWork);




        }

        // GET: Festival
        public ActionResult Index()
        {
            var data = FestivalServices.GetAll();
            return View(data);
        }

        // GET: Festival/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FestivalViewModel festivalViewModel = FestivalServices.GetByID(id);
            if (festivalViewModel == null)
            {
                return HttpNotFound();
            }
            return View(festivalViewModel);
        }

        // GET: Festival/Create
        public ActionResult Create()
        {
            loadReligion();
            return View();
        }

        // POST: Festival/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FestivalViewModel festivalViewModel)
        {
            if (ModelState.IsValid)
            {
                FestivalServices.Create(festivalViewModel);
                return RedirectToAction("Index");
            }
            loadReligion();
            //ViewBag.ReligionId = new SelectList(ReligionServices.GetAll(), "ReligionId", "ReligionName", festivalViewModel.ReligionId);
            return View(festivalViewModel);
        }

        // GET: Festival/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            loadReligion();

            FestivalViewModel festivalViewModel = FestivalServices.GetByID(id);
            if (festivalViewModel == null)
            {
                return HttpNotFound();
            }
        
            return View(festivalViewModel);
        }

        // POST: Festival/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( FestivalViewModel festivalViewModel)
        {
            if (ModelState.IsValid)
            {
                FestivalServices.Update(festivalViewModel);
                return RedirectToAction("Index");
            }
            ViewBag.ReligionId = new SelectList(FestivalServices.GetAll(), "ReligionId", "ReligionName", festivalViewModel.ReligionId);
            return View(festivalViewModel);
        }

        // GET: Festival/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FestivalViewModel festivalViewModel = FestivalServices.GetByID(id);
            if (festivalViewModel == null)
            {
                return HttpNotFound();
            }
            return View(festivalViewModel);
        }

        // POST: Festival/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FestivalServices.Delete(id);
            return RedirectToAction("Index");
        }


        public void loadReligion()
        {
            var Religionlist = ReligionServices.GetAll();
            ViewBag.Religionlist = new SelectList(Religionlist, "ReligionId", "ReligionName");

        }
    }
}
