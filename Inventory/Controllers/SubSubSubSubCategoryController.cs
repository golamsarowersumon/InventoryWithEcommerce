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
    public class SubSubSubSubCategoryController : Controller
    {
        private SubSubSubSubCategoryServices SubSubSubSubCategoryServices;
        private SubSubSubCategoryServices SubSubSubCategoryServices;
        private SubSubCategoryServices SubSubCategoryServices;
        private CategoryServices CategoryServices;
        private SubCategoryServices SubCategoryServices;
        private UnitOfWork unitOfWork;



        public SubSubSubSubCategoryController()
        {

            unitOfWork = new UnitOfWork();
            SubSubSubSubCategoryServices = new SubSubSubSubCategoryServices(unitOfWork);
            SubSubSubCategoryServices = new SubSubSubCategoryServices(unitOfWork);
            SubSubCategoryServices = new SubSubCategoryServices(unitOfWork);
            SubCategoryServices = new SubCategoryServices(unitOfWork);
            CategoryServices = new CategoryServices(unitOfWork);

        }

        // GET: SubSubSubSubCategory
        public ActionResult Index()
        {
            var data = SubSubSubSubCategoryServices.GetAll();
            return View(data);
        }

        // GET: SubSubSubSubCategory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubSubSubSubCategoryViewModel subSubSubSubCategoryViewModel = SubSubSubSubCategoryServices.GetByID(id);
            if (subSubSubSubCategoryViewModel == null)
            {
                return HttpNotFound();
            }
            return View(subSubSubSubCategoryViewModel);
        }

        // GET: SubSubSubSubCategory/Create
        public ActionResult Create()
        {
            loadAll();
            return View();
        }

        // POST: SubSubSubSubCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubSubSubSubCategoryViewModel subSubSubSubCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                SubSubSubSubCategoryServices.Create(subSubSubSubCategoryViewModel);
                return RedirectToAction("Index");
            }

            loadAll();
            return View(subSubSubSubCategoryViewModel);
        }

        // GET: SubSubSubSubCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            loadAll();
            SubSubSubSubCategoryViewModel subSubSubSubCategoryViewModel = SubSubSubSubCategoryServices.GetByID(id);
            if (subSubSubSubCategoryViewModel == null)
            {
                return HttpNotFound();
            }
            return View(subSubSubSubCategoryViewModel);
        }

        // POST: SubSubSubSubCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SubSubSubSubCategoryViewModel subSubSubSubCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                SubSubSubSubCategoryServices.Update(subSubSubSubCategoryViewModel);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(CategoryServices.GetAll(), "CategoryId", "CategoryName", subSubSubSubCategoryViewModel.CategoryId);
            ViewBag.SubCategoryId = new SelectList(SubCategoryServices.GetAll(), "SubCategoryId", "SubCategoryName", subSubSubSubCategoryViewModel.SubCategoryId);
            ViewBag.SubSubCategoryId = new SelectList(SubSubCategoryServices.GetAll(), "SubSubCategoryId", "SubSubCategoryName", subSubSubSubCategoryViewModel.SubSubCategoryId);
            ViewBag.SubSubSubCategoryId = new SelectList(SubSubSubCategoryServices.GetAll(), "SubSubSubCategoryId", "SubSubSubCategoryName", subSubSubSubCategoryViewModel.SubSubSubCategoryId);
            return View(subSubSubSubCategoryViewModel);
        }

        // GET: SubSubSubSubCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubSubSubSubCategoryViewModel subSubSubSubCategoryViewModel = SubSubSubSubCategoryServices.GetByID(id);
            if (subSubSubSubCategoryViewModel == null)
            {
                return HttpNotFound();
            }
            return View(subSubSubSubCategoryViewModel);
        }

        // POST: SubSubSubSubCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubSubSubSubCategoryServices.Delete(id);
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

            var SubSubSubCategoryList = SubSubSubCategoryServices.GetAll();
            ViewBag.subSubSubCategoryList = new SelectList(SubSubSubCategoryList, "SubSubSubCategoryId", "SubSubSubCategoryName");

        }
    }
}
