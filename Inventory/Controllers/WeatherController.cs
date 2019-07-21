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
    public class WeatherController : Controller
    {
        private UnitOfWork unitOfWork;
        private WeatherServices weatherService;

        public WeatherController()
        {
            unitOfWork = new UnitOfWork();
            weatherService = new WeatherServices(unitOfWork);
        }
        // GET: Weather
        public ActionResult Index()
        {
            var data = weatherService.GetAll();
            return View(data);
        }


        // GET: Weather/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Weather/Create
        [HttpPost]
        public ActionResult Create(WeatherViewModel weatherVM)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    weatherService.Create(weatherVM);

                }


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Weather/Details/5
        public ActionResult Details(int id = 0)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            WeatherViewModel details = weatherService.GetById(id);
            if (details == null)
            {
                return HttpNotFound();
            }
            return View(details);

        }

        // GET: Weather/Edit/5
        public ActionResult Edit(int id)
        {
            var edit = weatherService.GetById(id);
            return View(edit);
        }

        // POST:  Weather/Edit/5
        [HttpPost]
        public ActionResult Edit(WeatherViewModel weatherVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add update logic here

                    weatherService.Update(weatherVM);
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
                var delete = weatherService.GetById(id);
                return View();
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
                    weatherService.Delete(id);

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
