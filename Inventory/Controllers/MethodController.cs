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
    public class MethodController : Controller
    {
        private UnitOfWork unitOfWork;
        private MethodServices methodServices;

        public MethodController()
        {
            unitOfWork = new UnitOfWork();
            methodServices = new MethodServices(unitOfWork);
        }

        // GET: Method
        public ActionResult Index()
        {
            var data = methodServices.GetAll();
            return View(data);
        }

        public ActionResult Details(int id=0)
        {
            if(id==0){
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            var data = methodServices.GetById(id);
            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(MethodViewModel methodVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    methodServices.Create(methodVM);
                    return RedirectToAction("index");
                }
               
            }
            catch
            {
                return View();
            }
            return View();
           
        }

        public ActionResult Edit(int id)
        {
            var data = methodServices.GetById(id);
            return View(id);
        }

        public ActionResult Edit(MethodViewModel methodVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    methodServices.Update(methodVM);

                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
           
            
       
        }

        public ActionResult Delete(int id=0)
        {
            if(id != 00)
            {
                var data = methodServices.GetById(id);
                return View();
            }
            return View();
        }
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    methodServices.Delete(id);
                    return RedirectToAction("Index");
                }
            }

            catch
            {
                return View();
            }
            return View();
        }


    }
}
