using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Services;
using System.Net;
using Domain.ViewModels;

namespace Inventory.Controllers
{
    public class ProfileFamilyDetailsController : Controller
    {
        private UnitOfWork UnitOfWork;
        ProfileFamilyDetailsService ProfileFamilyDetailsService;
        GenderServices genderServices;
        public ProfileFamilyDetailsController()
        {

            UnitOfWork = new UnitOfWork();
            ProfileFamilyDetailsService = new ProfileFamilyDetailsService(UnitOfWork);
            genderServices = new GenderServices(UnitOfWork);


        }
        public ActionResult Index()
        {
            var data = ProfileFamilyDetailsService.GetAll();
            return View(data);
        }

        public ActionResult Create()
        {
            loadGender();
            return View();
        }

       
        [HttpPost]
        public ActionResult Create(ProfileViewModel profileFamilyDetailsViewModel)
        {
            try
            {
                
                if (ModelState.IsValid)
                {
                    ProfileFamilyDetailsService.Create(profileFamilyDetailsViewModel);

                }


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            loadGender();
            var edit = ProfileFamilyDetailsService.GetById(id);
           
            return View(edit);
        }

        // POST:  BloodGroup/Edit/5
        [HttpPost]
        public ActionResult Edit(ProfileViewModel profileFamilyDetailsViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add update logic here

                    ProfileFamilyDetailsService.Update(profileFamilyDetailsViewModel);
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
                var delete = ProfileFamilyDetailsService.GetById(id);
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
                    ProfileFamilyDetailsService.Delete(id);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }
        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            ProfileViewModel profileFamilyDetailsViewModel = ProfileFamilyDetailsService.GetById(id);
            if (profileFamilyDetailsViewModel == null)
            {
                return HttpNotFound();
            }
            return View(profileFamilyDetailsViewModel);
        }

        public void loadGender()
        {
            var Genderlist = genderServices.GetAll();
            ViewBag.genderlist = new SelectList(Genderlist, "GenderID", "GenderName");

        }
    }
}