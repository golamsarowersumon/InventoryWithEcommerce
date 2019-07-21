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

    public class ProductDetailsController : Controller
    {
        private UnitOfWork unitOfWork;
        private ProductDetailsServices productDetailsService;
        private ItemServices itemServices;
        private ProductServices productServices;

        public ProductDetailsController()
        {
            unitOfWork = new UnitOfWork();
            productDetailsService = new ProductDetailsServices(unitOfWork);
            itemServices = new ItemServices(unitOfWork);
            productServices = new ProductServices(unitOfWork);
        }
        // GET:  ProductDetails
        public ActionResult Index()
        {
            var data = productDetailsService.GetAll();
            return View(data);
        }


        // GET:  ProductDetails/Create
        public ActionResult Create()
        {
            ViewBag.ItemList = new SelectList(itemServices.GetDropDown(), "ItemId", "ItemName");
            ViewBag.ProductList = new SelectList(productServices.GetDropDown(), "Value", "Text");


            return View();
        }

        // POST:  ProductDetails/Create
        [HttpPost]
        public ActionResult Create(ProductDetailsViewModel ProductDetailsVM)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    productDetailsService.Create(ProductDetailsVM);
                    //ViewBag.ItemList = new SelectList(itemServices.GetDropDown(), "ItemId", "ItemName");

                }


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductDetails/Details/5
        public ActionResult Details(int id = 0)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProductDetailsViewModel details = productDetailsService.GetById(id);
            if (details == null)
            {
                return HttpNotFound();
            }
            return View(details);

        }

        // GET:  ProductDetails/Edit/5
        public ActionResult Edit(int id)
        {
            var edit = productDetailsService.GetById(id);
            ViewBag.ItemList = new SelectList(itemServices.GetDropDown(), "ItemId", "ItemName");
            ViewBag.ProductList = new SelectList(productServices.GetDropDown(), "Value", "Text");
            return View(edit);
        }

        // POST:   ProductDetails/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductDetailsViewModel productDetailsVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add update logic here

                    productDetailsService.Update(productDetailsVM);
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
                var delete = productDetailsService.GetById(id);
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
                    productDetailsService.Delete(id);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }
    }
}