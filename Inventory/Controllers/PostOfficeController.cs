using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Repositories;
using Domain.ViewModels;
using Core.Services;

namespace Inventory.Controllers
{
    public class PostOfficeController : Controller
    {

        private PostOfficeServices PostOfficeServices;
        DistrictServices DistrictServices;
        private UnitOfWork unitOfWork;



        public PostOfficeController()
        {

            unitOfWork = new UnitOfWork();
            PostOfficeServices = new PostOfficeServices(unitOfWork);
            DistrictServices = new DistrictServices(unitOfWork);

        }


        // GET: PostOffice
        public ActionResult Index()
        {

        
            var data = PostOfficeServices.GetAll();
            return View(data);
        }

        // GET: PostOffice/Details/5
        public ActionResult Details(int id)
        {
            var data = PostOfficeServices.GetByID(id);
            return View(data);
        }

        // GET: PostOffice/Create
        public ActionResult Create()
        {
            ViewBag.PostOfficeList = new SelectList(DistrictServices.GetDropDown(), "Value", "Text");
            return View();
        }

        // POST: PostOffice/Create
        [HttpPost]
        public ActionResult Create(PostOfficeViewModel postOfficeViewModel)
        {
            try
            {
                // TODO: Add insert logic here
                PostOfficeServices.Create(postOfficeViewModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PostOffice/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.PostOfficeList = new SelectList(DistrictServices.GetDropDown(), "Value", "Text");
            var data = PostOfficeServices.GetByID(id);
            return View(data);
        }

        // POST: PostOffice/Edit/5
        [HttpPost]
        public ActionResult Edit(PostOfficeViewModel postOfficeViewModel)
        {
            try
            {
                // TODO: Add update logic here
                PostOfficeServices.Update(postOfficeViewModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PostOffice/Delete/5
        public ActionResult Delete(int id)
        {
            var data = PostOfficeServices.GetByID(id);
            return View(data);
        }

        // POST: PostOffice/Delete/5
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                // TODO: Add delete logic here
                PostOfficeServices.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
