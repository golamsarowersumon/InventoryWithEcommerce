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
    public class ReligionController : Controller
    {
        private UnitOfWork UnitOfWork;
        private ReligionServices ReligionServices;


        public ReligionController()
        {

            UnitOfWork = new UnitOfWork();
            ReligionServices = new ReligionServices(UnitOfWork);

        }
        // GET: Country
        public ActionResult Index()
        {
            var data = ReligionServices.GetAll();
            return View(data);
        }

        // GET: Country/Details/5
        public ActionResult Details(int id = 0)
        {

            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReligionViewModel religionViewModel = ReligionServices.GetByID(id);
            if (religionViewModel == null)
            {
                return HttpNotFound();
            }
            return View(religionViewModel);



        }

        // GET: Country/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Country/Create
        [HttpPost]
        public ActionResult Create(ReligionViewModel religionViewModel)
        {
            try
            {
                // TODO: Add insert logic here
                ReligionServices.Create(religionViewModel);
                TempData["message"] = "Inserted Successfully";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Country/Edit/5
        public ActionResult Edit(int id)
        {
            var data = ReligionServices.GetByID(id);
            return View(data);
        }

        // POST: Country/Edit/5
        [HttpPost]
        public ActionResult Edit(ReligionViewModel religionViewModel)
        {


            if (ModelState.IsValid)
            {

                try
                {
                    // TODO: Add update logic here

                    ReligionServices.Update(religionViewModel);
                    TempData["message"] = "Update Successfully";
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }

            }

            return View();

        }

        // GET: Country/Delete/5
        //public ActionResult Delete(int id = 0)
        //{
        //    if (id != 0)
        //    {

        //        var data = ReligionServices.GetByID(id);
        //        return View(data);
        //    }

        //    return View();

        //}

        IEnumerable<ReligionViewModel> GetAllDistrict()
        {
            return (ReligionServices.GetAll());
        }



        //[HttpPost]
        //public ActionResult Delete(int id)
        //{
        //    try
        //    {
        //        ReligionServices.Delete(id);
        //        return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "Index", GetAllDistrict()), message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
        //    }
        //}


    }
}
