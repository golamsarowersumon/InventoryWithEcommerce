using Core.Services;
using Domain.Repositories;

using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class PoliceStationController : Controller
    {


        private PoliceStationServices PoliceStationServices;
        DistrictServices DistrictServices;
        private UnitOfWork unitOfWork;



        public PoliceStationController()
        {

            unitOfWork = new UnitOfWork();
            PoliceStationServices = new PoliceStationServices(unitOfWork);
            DistrictServices = new DistrictServices(unitOfWork);

        }

        // GET: PoliceStation
        public ActionResult Index()
        {
            var data = PoliceStationServices.GetAll();
            return View(data);
        }

        // GET: PoliceStation/Details/5
        public ActionResult Details(int id)
        {
            var data = PoliceStationServices.GetByID(id);
            return View(data);
        }

        // GET: PoliceStation/Create
        public ActionResult Create()
        {
            ViewBag.DistrictList = new SelectList(DistrictServices.GetDropDown(), "Value", "Text");
            return View();
        }

        // POST: PoliceStation/Create
        [HttpPost]
        public ActionResult Create(PoliceStationViewModel policeStationViewModel)
        {
            try
            {
                // TODO: Add insert logic here
                PoliceStationServices.Create(policeStationViewModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PoliceStation/Edit/5
        public ActionResult Edit(int id)
        {
            var data = PoliceStationServices.GetByID(id);
            return View(data);
        }

        // POST: PoliceStation/Edit/5
        [HttpPost]
        public ActionResult Edit(PoliceStationViewModel policeStationViewModel)
        {
            try
            {
                // TODO: Add update logic here
                PoliceStationServices.Update(policeStationViewModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PoliceStation/Delete/5
        public ActionResult Delete(int id)
        {
            var data = PoliceStationServices.GetByID(id);
            return View(data);
        }

        // POST: PoliceStation/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                // TODO: Add delete logic here
                PoliceStationServices.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
