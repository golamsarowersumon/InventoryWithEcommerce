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
    public class SubContractCompanyController : Controller
    {
        private UnitOfWork unitOfWork;
        private SubContractCompanyServices subContractCompanyService;

        public SubContractCompanyController()
        {
            unitOfWork = new UnitOfWork();
            subContractCompanyService = new SubContractCompanyServices(unitOfWork);
        }
        // GET: SubContractCompany
        public ActionResult Index()
        {
            var data = subContractCompanyService.GetAll();
            return View(data);
        }


        // GET: SubContractCompany/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: SubContractCompany/Create
        [HttpPost]
        public ActionResult Create(SubContractCompanyViewModel subContractCompanyVM)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    subContractCompanyService.Create(subContractCompanyVM);

                }


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SubContractCompany/Details/5
        public ActionResult Details(int id = 0)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SubContractCompanyViewModel details = subContractCompanyService.GetById(id);
            if (details == null)
            {
                return HttpNotFound();
            }
            return View(details);

        }

        // GET: SubContractCompany/Edit/5
        public ActionResult Edit(int id)
        {
            var edit = subContractCompanyService.GetById(id);
            return View(edit);
        }

        // POST:  SubContractCompany/Edit/5
        [HttpPost]
        public ActionResult Edit(SubContractCompanyViewModel subContractCompanyVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add update logic here

                    subContractCompanyService.Update(subContractCompanyVM);
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
                var delete = subContractCompanyService.GetById(id);
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
                    subContractCompanyService.Delete(id);

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
