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
    public class EmailProviderController : Controller
    {
        private UnitOfWork unitOfWork;
        private EmailProviderServices emailProviderService;

        public EmailProviderController()
        {
            unitOfWork = new UnitOfWork();
            emailProviderService = new EmailProviderServices(unitOfWork);
        }
        // GET: EmailProvider
        public ActionResult Index()
        {
            var data = emailProviderService.GetAll();
            return View(data);
        }


        // GET: EmailProvider/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: EmailProvider/Create
        [HttpPost]
        public ActionResult Create(EmailProviderViewModel emailProviderVM)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    emailProviderService.Create(emailProviderVM);

                }


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EmailProvider/Details/5
        public ActionResult Details(int id = 0)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EmailProviderViewModel details = emailProviderService.GetById(id);
            if (details == null)
            {
                return HttpNotFound();
            }
            return View(details);

        }

        // GET: EmailProvider/Edit/5
        public ActionResult Edit(int id)
        {
            var edit = emailProviderService.GetById(id);
            return View(edit);
        }

        // POST:  EmailProvider/Edit/5
        [HttpPost]
        public ActionResult Edit(EmailProviderViewModel emailProviderVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add update logic here

                    emailProviderService.Update(emailProviderVM);
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
                var delete = emailProviderService.GetById(id);
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
                    emailProviderService.Delete(id);

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
