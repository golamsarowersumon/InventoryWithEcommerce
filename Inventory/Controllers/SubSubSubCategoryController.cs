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
    public class SubSubSubCategoryController : Controller
    {
        private SubSubSubCategoryServices SubSubSubCategoryServices;
        private SubSubCategoryServices SubSubCategoryServices;
        private CategoryServices CategoryServices;
        private SubCategoryServices SubCategoryServices;
        private UnitOfWork unitOfWork;



        public SubSubSubCategoryController()
        {

            unitOfWork = new UnitOfWork();
            SubSubSubCategoryServices = new SubSubSubCategoryServices(unitOfWork);
            SubSubCategoryServices = new SubSubCategoryServices(unitOfWork);
            SubCategoryServices = new SubCategoryServices(unitOfWork);
            CategoryServices = new CategoryServices(unitOfWork);

        }

        // GET: SubSubSubCategory
        public ActionResult Index()
        {
            var data = SubSubSubCategoryServices.GetAll();
            return View(data);
        }

        // GET: SubSubSubCategory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubSubSubCategoryViewModel subSubSubCategoryViewModel = SubSubSubCategoryServices.GetByID(id);
            if (subSubSubCategoryViewModel == null)
            {
                return HttpNotFound();
            }
            return View(subSubSubCategoryViewModel);
        }

        // GET: SubSubSubCategory/Create
        public ActionResult Create()
        {
            loadAll();
            return View();
        }

        // POST: SubSubSubCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubSubSubCategoryViewModel subSubSubCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                SubSubSubCategoryServices.Create(subSubSubCategoryViewModel);
                return RedirectToAction("Index");
            }

            loadAll();
            return View(subSubSubCategoryViewModel);
        }

        // GET: SubSubSubCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            loadAll();
            SubSubSubCategoryViewModel subSubSubCategoryViewModel = SubSubSubCategoryServices.GetByID(id);
            if (subSubSubCategoryViewModel == null)
            {
                return HttpNotFound();
            }
            
            return View(subSubSubCategoryViewModel);
        }

        // POST: SubSubSubCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SubSubSubCategoryViewModel subSubSubCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                SubSubSubCategoryServices.Update(subSubSubCategoryViewModel);
                return RedirectToAction("Index");
            }
         
            return View(subSubSubCategoryViewModel);
        }

        // GET: SubSubSubCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubSubSubCategoryViewModel subSubSubCategoryViewModel = SubSubSubCategoryServices.GetByID(id);
            if (subSubSubCategoryViewModel == null)
            {
                return HttpNotFound();
            }
            return View(subSubSubCategoryViewModel);
        }

        // POST: SubSubSubCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubSubSubCategoryServices.Delete(id);
            return RedirectToAction("Index");
        }

        public void loadAll()
        {
            var CatgoryList = CategoryServices.GetAll();
            ViewBag.categorylist = new SelectList(CatgoryList, "CategoryId", "CategoryName");

            var SubCategoryList = SubCategoryServices.GetAll();
            ViewBag.subCategorylist = new SelectList(SubCategoryList, "SubCategoryId", "SubCategoryName");

            var SubSubCategoryList = SubSubCategoryServices.GetAll();
            ViewBag.subSubCategoryList = new SelectList(SubSubCategoryList, "SubSubCategoryId", "SubSubCategoryName");

        }
    }
}
