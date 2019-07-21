using Core.Services;
using Domain.Repositories;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class ItemElementController : Controller
    {
        private UnitOfWork unitOfWork;
        private ItemElementServices itemElementService;
        private CategoryServices categoryServices;
        private SubCategoryServices subCategoryServices;
        private SubSubCategoryServices subSubCategoryServices;
        private SubSubSubCategoryServices subSubSubCategoryServices;
        private SubSubSubSubCategoryServices subSubSubSubCategoryServices;
       

        public ItemElementController()
        {
            unitOfWork = new UnitOfWork();
            itemElementService = new ItemElementServices(unitOfWork);
            categoryServices = new CategoryServices(unitOfWork);
            subCategoryServices = new SubCategoryServices(unitOfWork);
            subSubCategoryServices = new SubSubCategoryServices(unitOfWork);
            subSubSubCategoryServices = new SubSubSubCategoryServices(unitOfWork);
            subSubSubSubCategoryServices = new SubSubSubSubCategoryServices(unitOfWork);
        }
        // GET: ItemElement
        public ActionResult Index()
        {
            var data = itemElementService.GetAll();
            return View(data);
        }


        // GET: ItemElement/Create
        public ActionResult Create()
        {
            loadAll();

            return View();
        }

        // POST: ItemElement/Create
        [HttpPost]
        public ActionResult Create(ItemElementViewModel itemElementVM)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    itemElementService.Create(itemElementVM);
                    loadAll();
                }


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ItemElement/Details/5
        public ActionResult Details(int id = 0)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ItemElementViewModel details = itemElementService.GetById(id);
            if (details == null)
            {
                return HttpNotFound();
            }
            return View(details);

        }

        // GET: ItemElement/Edit/5
        public ActionResult Edit(int id)
        {
            loadAll();
            var edit = itemElementService.GetById(id);
            return View(edit);
        }

        // POST:  ItemElement/Edit/5
        [HttpPost]
        public ActionResult Edit(ItemElementViewModel ItemElementVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add update logic here

                    itemElementService.Update(ItemElementVM);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        public ActionResult Delete(int id = 0)
        {
            if (id != 0)
            {
                var delete = itemElementService.GetById(id);
                return View(delete);
            }
            return View();
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    itemElementService.Delete(id);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        public void loadAll()
        {

            var CategoryList = categoryServices.GetAll();
            ViewBag.categorylist = new SelectList(CategoryList, "CategoryId", "CategoryName");

            var SubCategoryList = subCategoryServices.GetAll();
            ViewBag.subCategorylist = new SelectList(SubCategoryList, "SubCategoryId", "SubCategoryName");

            var SubSubCategoryList = subSubCategoryServices.GetAll();
            ViewBag.subSubCategoryList = new SelectList(SubSubCategoryList, "SubSubCategoryId", "SubSubCategoryName");

            var SubSubSubCategoryList = subSubSubCategoryServices.GetAll();
            ViewBag.subSubSubCategoryList = new SelectList(SubSubSubCategoryList, "SubSubSubCategoryId", "SubSubSubCategoryName");

            var SubSubSubSubCategoryList = subSubSubSubCategoryServices.GetAll();
            ViewBag.subSubSubSubCategoryList = new SelectList(SubSubSubSubCategoryList, "SubSubSubSubCategoryId", "SubSubSubSubCategoryName");

           

           
        }

    }
}