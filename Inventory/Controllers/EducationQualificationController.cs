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
    public class EducationQualificationController : Controller
    {
        private UnitOfWork unitOfWork;
        private EducationQualificationServices educationQualificationService;

        public EducationQualificationController()
        {
            unitOfWork = new UnitOfWork();
            educationQualificationService = new EducationQualificationServices(unitOfWork);
        }
        // GET: EducationQualification
        public ActionResult Index()
        {
            var data = educationQualificationService.GetAll();
            return View(data);
        }


        // GET: EducationQualification/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: EducationQualification/Create
        [HttpPost]
        public ActionResult Create(EducationQualificationViewModel educationQualificationVM)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    educationQualificationService.Create(educationQualificationVM);

                }


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EducationQualification/Details/5
        public ActionResult Details(int id = 0)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EducationQualificationViewModel details = educationQualificationService.GetById(id);
            if (details == null)
            {
                return HttpNotFound();
            }
            return View(details);

        }

        // GET: EducationQualification/Edit/5
        public ActionResult Edit(int id)
        {
            var edit = educationQualificationService.GetById(id);
            return View(edit);
        }

        // POST:  EducationQualification/Edit/5
        [HttpPost]
        public ActionResult Edit(EducationQualificationViewModel educationQualificationVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add update logic here

                    educationQualificationService.Update(educationQualificationVM);
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
                var delete = educationQualificationService.GetById(id);
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
                    educationQualificationService.Delete(id);

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
