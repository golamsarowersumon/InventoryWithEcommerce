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
    public class LanguageController : Controller
    {
        private UnitOfWork unitOfWork;
        private LanguageServices languageService;

        public LanguageController()
        {
            unitOfWork = new UnitOfWork();
            languageService = new LanguageServices(unitOfWork);
        }
        // GET: Language
        public ActionResult Index()
        {
            var data = languageService.GetAll();
            return View(data);
        }


        // GET: Language/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Language/Create
        [HttpPost]
        public ActionResult Create(LanguageViewModel  languageVM)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    languageService.Create(languageVM);

                }


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Language/Details/5
        public ActionResult Details(int id = 0)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            LanguageViewModel details = languageService.GetById(id);
            if (details == null)
            {
                return HttpNotFound();
            }
            return View(details);

        }

        // GET: Language/Edit/5
        public ActionResult Edit(int id)
        {
            var edit = languageService.GetById(id);
            return View(edit);
        }

        // POST:  Language/Edit/5
        [HttpPost]
        public ActionResult Edit(LanguageViewModel languageVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add update logic here

                    languageService.Update(languageVM);
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
                var delete = languageService.GetById(id);
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
                    languageService.Delete(id);

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
