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
    public class NationalityController : Controller
    {
        private UnitOfWork UnitOfWork;
        private  NationalityServices NationalityServices;


        public NationalityController()
        {

            UnitOfWork = new UnitOfWork();
            NationalityServices = new NationalityServices(UnitOfWork);

        }

        // GET: Nationality
        public ActionResult Index()
        {
            var data = NationalityServices.GetAll();
            return View(data);
        }

        // GET: Nationality/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NationalityViewModel nationalityViewModel = NationalityServices.GetByID(id);
            if (nationalityViewModel == null)
            {
                return HttpNotFound();
            }
            return View(nationalityViewModel);
        }

        // GET: Nationality/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Nationality/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( NationalityViewModel nationalityViewModel)
        {
            if (ModelState.IsValid)
            {
                NationalityServices.Create(nationalityViewModel);
                return RedirectToAction("Index");
            }

            return View(nationalityViewModel);
        }

        // GET: Nationality/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NationalityViewModel nationalityViewModel = NationalityServices.GetByID(id);
            if (nationalityViewModel == null)
            {
                return HttpNotFound();
            }
            return View(nationalityViewModel);
        }

        // POST: Nationality/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NationalityViewModel nationalityViewModel)
        {
            if (ModelState.IsValid)
            {
                NationalityServices.Update(nationalityViewModel);
                return RedirectToAction("Index");
            }
            return View(nationalityViewModel);
        }

        // GET: Nationality/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NationalityViewModel nationalityViewModel = NationalityServices.GetByID(id);
            if (nationalityViewModel == null)
            {
                return HttpNotFound();
            }
            return View(nationalityViewModel);
        }

        // POST: Nationality/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NationalityServices.Delete(id);
            return RedirectToAction("Index");
        }

       
    }
}
