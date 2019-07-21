using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Domain.ViewModels;
using Domain.Repositories;
using Core.Services;

namespace Inventory.Controllers
{
    public class RawMaterialsController : Controller
    {

        private RawMaterialServices RawMaterialServices;
       private ItemServices ItemServices;
        private ItemElementServices ItemElementServices;
        private UnitOfWork unitOfWork;



        public RawMaterialsController()
        {

            unitOfWork = new UnitOfWork();
            RawMaterialServices = new RawMaterialServices(unitOfWork);
            ItemServices = new ItemServices(unitOfWork);
            ItemElementServices = new ItemElementServices(unitOfWork);

        }

        // GET: RawMaterials
        public ActionResult Index()
        {
            var data = RawMaterialServices.GetAll();
            return View(data);
        }

        // GET: RawMaterials/Details/5
        public ActionResult Details(int id)
        {
            var data = RawMaterialServices.GetById(id);
            return View(data);
        }

        // GET: RawMaterials/Create
        public ActionResult Create()
        {
            LoadAll();
            return View();
        }

        // POST: RawMaterials/Create
        [HttpPost]
        public ActionResult Create(RawMaterialsViewModel rawMaterialsViewModel)
        {
            try
            {
                
                // TODO: Add insert logic here
                RawMaterialServices.Create(rawMaterialsViewModel);
                LoadAll();
                ModelState.Clear();
              
                return View("");
            }
            catch
            {
                LoadAll();
                return View();
            }
        }

        // GET: RawMaterials/Edit/5
        public ActionResult Edit(int id)
        {
            LoadAll();
            var data = RawMaterialServices.GetById(id);
            return View(data);
        }

        // POST: RawMaterials/Edit/5
        [HttpPost]
        public ActionResult Edit(RawMaterialsViewModel rawMaterialsViewModel)
        {
            try
            {
                // TODO: Add update logic here
                RawMaterialServices.Update(rawMaterialsViewModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: RawMaterials/Delete/5
        public ActionResult Delete(int id)
        {
            RawMaterialServices.Delete(id);
            return RedirectToAction("Index");
        }

        // POST: RawMaterials/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        public void LoadAll() {



            var ItemList = ItemServices.GetDropDown();
            ViewBag.ItemList = new SelectList(ItemList, "ItemId", "ItemName");

            var ItemElementList = ItemElementServices.GetDropDown();
            ViewBag.ItemElementList = new SelectList(ItemElementList, "ItemElementId", "ItemElementName");


        }

    }
}
