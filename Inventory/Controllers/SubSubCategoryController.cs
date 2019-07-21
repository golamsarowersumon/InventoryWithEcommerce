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
    public class SubSubCategoryController : Controller
    {
        private SubSubCategoryServices SubSubCategoryServices;
        CategoryServices CategoryServices;
        SubCategoryServices SubCategoryServices;
        private UnitOfWork unitOfWork;



        public SubSubCategoryController()
        {

            unitOfWork = new UnitOfWork();
            SubSubCategoryServices = new SubSubCategoryServices(unitOfWork);
            SubCategoryServices = new SubCategoryServices(unitOfWork);
            CategoryServices = new CategoryServices(unitOfWork);

        }

        // GET: SubSubCategory
        public ActionResult Index()
        {
            var data = SubSubCategoryServices.GetAll();
            return View(data);
        }

        // GET: SubSubCategory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubSubCategoryViewModel subSubCategoryViewModel = SubSubCategoryServices.GetByID(id);
            if (subSubCategoryViewModel == null)
            {
                return HttpNotFound();
            }
            return View(subSubCategoryViewModel);
        }

        // GET: SubSubCategory/Create
        public ActionResult Create()
        {
            loadAll();
            return View();
        }

        // POST: SubSubCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubSubCategoryViewModel subSubCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                SubSubCategoryServices.Create(subSubCategoryViewModel);
                return RedirectToAction("Index");
            }
            loadAll();
            return View(subSubCategoryViewModel);
        }

        // GET: SubSubCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            loadAll();
            SubSubCategoryViewModel subSubCategoryViewModel = SubSubCategoryServices.GetByID(id);
            if (subSubCategoryViewModel == null)
            {
                return HttpNotFound();
            }
            
            return View(subSubCategoryViewModel);
        }

        // POST: SubSubCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SubSubCategoryViewModel subSubCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                SubSubCategoryServices.Update(subSubCategoryViewModel);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(CategoryServices.GetAll(), "CategoryId", "CategoryName", subSubCategoryViewModel.CategoryId);
            ViewBag.SubCategoryId = new SelectList(SubCategoryServices.GetAll(), "SubCategoryId", "SubCategoryName", subSubCategoryViewModel.SubCategoryId);
            return View(subSubCategoryViewModel);
        }

        // GET: SubSubCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubSubCategoryViewModel subSubCategoryViewModel = SubSubCategoryServices.GetByID(id);
            if (subSubCategoryViewModel == null)
            {
                return HttpNotFound();
            }
            return View(subSubCategoryViewModel);
        }

        // POST: SubSubCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubSubCategoryServices.Delete(id);
            return RedirectToAction("Index");
        }


        public void loadAll()
        {
            var CatgoryList = CategoryServices.GetAll();
            ViewBag.categorylist = new SelectList(CatgoryList, "CategoryId", "CategoryName");

            var SubCategoryList = SubCategoryServices.GetAll();
            ViewBag.subCategorylist = new SelectList(SubCategoryList,"SubCategoryId","SubCategoryName");

        }
    }

    
}
