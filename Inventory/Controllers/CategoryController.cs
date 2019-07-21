using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Domain;
using Domain.ViewModels;
using Domain.Repositories;
using Core.Services;

namespace Inventory.Controllers
{
    public class CategoryController : Controller
    {
        private UnitOfWork UnitOfWork;
        private CategoryServices CategoryServices;


        public CategoryController()
        {

            UnitOfWork = new UnitOfWork();
            CategoryServices = new CategoryServices(UnitOfWork);

        }

        // GET: Category
        public ActionResult Index()
        {
            var data = CategoryServices.GetAll();
            return View(data);
        }

        // GET: Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryViewModel categoryViewModel = CategoryServices.GetByID(id);
            if (categoryViewModel == null)
            {
                return HttpNotFound();
            }
            return View(categoryViewModel);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                CategoryServices.Create(categoryViewModel);
                TempData["message"] = "Inserted Successfully";
                return RedirectToAction("Index");
            }

            return View(categoryViewModel);
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryViewModel categoryViewModel =CategoryServices.GetByID(id);
            if (categoryViewModel == null)
            {
                return HttpNotFound();
            }
            return View(categoryViewModel);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                CategoryServices.Update(categoryViewModel);
                TempData["message"] = "Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(categoryViewModel);
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryViewModel categoryViewModel = CategoryServices.GetByID(id);
            if (categoryViewModel == null)
            {
                return HttpNotFound();
            }
            return View(categoryViewModel);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoryServices.Delete(id);
            TempData["message"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }

        
    }
}
