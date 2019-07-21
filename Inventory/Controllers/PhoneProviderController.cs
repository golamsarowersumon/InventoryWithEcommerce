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
    public class PhoneProviderController : Controller
    {
        private UnitOfWork unitOfWork;
        private PhoneProviderServices phoneProviderService;

        public PhoneProviderController()
        {
            unitOfWork = new UnitOfWork();
            phoneProviderService = new PhoneProviderServices(unitOfWork);
        }
        // GET: PhoneProvider
        public ActionResult Index()
        {
            var data = phoneProviderService.GetAll();
            return View(data);
        }


        // GET: PhoneProvider/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: PhoneProvider/Create
        [HttpPost]
        public ActionResult Create(PhoneProviderViewModel phoneProviderVM)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    phoneProviderService.Create(phoneProviderVM);

                }


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PhoneProvider/Details/5
        public ActionResult Details(int id = 0)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PhoneProviderViewModel details = phoneProviderService.GetById(id);
            if (details == null)
            {
                return HttpNotFound();
            }
            return View(details);

        }

        // GET: PhoneProvider/Edit/5
        public ActionResult Edit(int id)
        {
            var edit = phoneProviderService.GetById(id);
            return View(edit);
        }

        // POST:  PhoneProvider/Edit/5
        [HttpPost]
        public ActionResult Edit(PhoneProviderViewModel phoneProviderVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add update logic here

                    phoneProviderService.Update(phoneProviderVM);
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
                var delete = phoneProviderService.GetById(id);
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
                    phoneProviderService.Delete(id);

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
