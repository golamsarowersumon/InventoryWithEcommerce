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
    public class ProfessionInformationController : Controller
    {
        private UnitOfWork unitOfWork;
        private ProfessionInformationServices professionInformationService;

        public ProfessionInformationController()
        {
            unitOfWork = new UnitOfWork();
            professionInformationService = new ProfessionInformationServices(unitOfWork);
        }
        // GET: ProfessionInformation
        public ActionResult Index()
        {
            var data = professionInformationService.GetAll();
            return View(data);
        }


        // GET: ProfessionInformation/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: ProfessionInformation/Create
        [HttpPost]
        public ActionResult Create(ProfessionInformationViewModel professionInformationVM)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    professionInformationService.Create(professionInformationVM);

                }


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ProfessionInformation/Details/5
        public ActionResult Details(int id = 0)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProfessionInformationViewModel details = professionInformationService.GetById(id);
            if (details == null)
            {
                return HttpNotFound();
            }
            return View(details);

        }

        // GET: ProfessionInformation/Edit/5
        public ActionResult Edit(int id)
        {
            var edit = professionInformationService.GetById(id);
            return View(edit);
        }

        // POST:  ProfessionInformation/Edit/5
        [HttpPost]
        public ActionResult Edit(ProfessionInformationViewModel professionInformationVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add update logic here

                    professionInformationService.Update(professionInformationVM);
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
                var delete = professionInformationService.GetById(id);
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
                    professionInformationService.Delete(id);

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
