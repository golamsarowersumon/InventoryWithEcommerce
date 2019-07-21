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
    public class SeasonController : Controller
    {
        private UnitOfWork unitOfWork;
        private SeasonServices seasonService;

        public SeasonController()
        {
            unitOfWork = new UnitOfWork();
            seasonService = new SeasonServices(unitOfWork);
        }
        // GET: Season
        public ActionResult Index()
        {
            var data = seasonService.GetAll();
            return View(data);
        }


        // GET: Season/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Season/Create
        [HttpPost]
        public ActionResult Create(SeasonViewModel seasonVM)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    seasonService.Create(seasonVM);

                }


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Season/Details/5
        public ActionResult Details(int id = 0)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SeasonViewModel details = seasonService.GetById(id);
            if (details == null)
            {
                return HttpNotFound();
            }
            return View(details);

        }

        // GET: Season/Edit/5
        public ActionResult Edit(int id)
        {
            var edit = seasonService.GetById(id);
            return View(edit);
        }

        // POST:  Season/Edit/5
        [HttpPost]
        public ActionResult Edit(SeasonViewModel seasonVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add update logic here

                    seasonService.Update(seasonVM);
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
                var delete = seasonService.GetById(id);
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
                    seasonService.Delete(id);

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
