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
using Domain.Models;
using Domain.Repositories;
using Domain.ViewModels;

namespace Inventory.Controllers
{
    public class SubCategoryController : Controller
    {
        private  SubCategoryServices SubCategoryServices;
        CategoryServices CategoryServices;
        private UnitOfWork unitOfWork;



        public SubCategoryController()
        {

            unitOfWork = new UnitOfWork();
            SubCategoryServices = new SubCategoryServices(unitOfWork);
            CategoryServices = new CategoryServices(unitOfWork);

        }


        // GET: State
        public ActionResult Index()
        {
            var data = SubCategoryServices.GetAll();
            return View(data);

        }

        // GET: State/Details/5
        public ActionResult Details(int id)
        {
            var post = SubCategoryServices.GetByID(id);
            return View(post);

        }

        // GET: State/Create
        public ActionResult Create()
        {
            loadCategory();
            return View();
        }

        // POST: State/Create
        [HttpPost]
        public ActionResult Create(SubCategoryViewModel SubCategoryViewModel)
        {
            try
            {
                // TODO: Add insert logic here
                SubCategoryServices.Create(SubCategoryViewModel);
                TempData["message"] = "Inserted Successfully";
                return RedirectToAction("Index");
            }
            catch
            {
                return View(SubCategoryViewModel);
            }
        }

        // GET: State/Edit/5
        public ActionResult Edit(int? id)
        {
            loadCategory();
            SubCategoryViewModel SubCategoryViewModel = SubCategoryServices.GetByID(id);
            return View(SubCategoryViewModel);

        }

        // POST: State/Edit/5
        [HttpPost]
        public ActionResult Edit(SubCategoryViewModel SubCategoryViewModel)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    // TODO: Add update logic here

                    SubCategoryServices.Update(SubCategoryViewModel);
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
            var post = SubCategoryServices.GetByID(id);
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
                SubCategoryServices.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }




        public void loadCategory()
        {
            var CatgoryList = CategoryServices.GetAll();
            ViewBag.categorylist = new SelectList(CatgoryList, "CategoryId", "CategoryName");

        }
    }
}
