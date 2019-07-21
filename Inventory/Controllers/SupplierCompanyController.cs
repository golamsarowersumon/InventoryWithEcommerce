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
    public class SupplierCompanyController : Controller
    {
        private UnitOfWork unitOfWork;
        private SupplierCompanyServices supplierCompanyService;

        public SupplierCompanyController()
        {
            unitOfWork = new UnitOfWork();
            supplierCompanyService = new SupplierCompanyServices(unitOfWork);
        }
        // GET: SupplierCompany
        public ActionResult Index()
        {
            var data = supplierCompanyService.GetAll();
            return View(data);
        }


        // GET: SupplierCompany/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: SupplierCompany/Create
        [HttpPost]
        public ActionResult Create(SupplierCompanyViewModel supplierCompanyVM)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    supplierCompanyService.Create(supplierCompanyVM);
                    return RedirectToAction("Index");
                }


                
            }
            catch
            {
                return View();
            }
            return View();
        }

        // GET: SupplierCompany/Details/5
        public ActionResult Details(int id = 0)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SupplierCompanyViewModel details = supplierCompanyService.GetById(id);
            if (details == null)
            {
                return HttpNotFound();
            }
            return View(details);

        }

        // GET: SupplierCompany/Edit/5
        public ActionResult Edit(int id)
        {
            var edit = supplierCompanyService.GetById(id);
            return View(edit);
        }

        // POST:  SupplierCompany/Edit/5
        [HttpPost]
        public ActionResult Edit(SupplierCompanyViewModel supplierCompanyVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add update logic here

                    supplierCompanyService.Update(supplierCompanyVM);
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
                var delete = supplierCompanyService.GetById(id);
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
                    supplierCompanyService.Delete(id);

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
