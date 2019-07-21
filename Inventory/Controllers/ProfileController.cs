using Domain.Repositories;
using Domain.ViewModels;
using Core.Services;
using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class ProfileController : Controller
    {
        private UnitOfWork UnitOfWork;
        private ProfileServices ProfileServices;
        private GenderServices GenderServices;
        private BloodGroupServices BloodGroupServices;
        private NationalityServices NationalityServices;
        private MaritalStatusServices MaritalStatusServices;
        private RegionServices RegionServices;
        private DistrictServices DistrictServices;
        private PostOfficeServices PostOfficeServices;
        private PoliceStationServices PoliceStationServices;
        private DivisionServices DivisionServices;
        private CountryServices CountryServices;
        





        public ProfileController()
        {

            UnitOfWork = new UnitOfWork();
            ProfileServices = new ProfileServices(UnitOfWork);
            GenderServices = new GenderServices(UnitOfWork);
            BloodGroupServices = new BloodGroupServices(UnitOfWork);
            NationalityServices = new NationalityServices(UnitOfWork);
            MaritalStatusServices = new MaritalStatusServices(UnitOfWork);
            RegionServices = new RegionServices(UnitOfWork);
            DistrictServices = new DistrictServices(UnitOfWork);
            PostOfficeServices = new PostOfficeServices(UnitOfWork);
            PoliceStationServices = new PoliceStationServices(UnitOfWork);
            DivisionServices = new DivisionServices(UnitOfWork);
            CountryServices = new CountryServices(UnitOfWork);



        }
        // GET: Profile
        public ActionResult Index()
        {
            var data = ProfileServices.GetAll();
            return View(data);
        }

        // GET: Profile/Details/5
        public ActionResult Details(int id)
        {
         
            var data = ProfileServices.GetByID(id);
            return View(data);
        }

        // GET: Profile/Create
        public ActionResult Create()
        {


            LoadAll();
            return View();
        }

        // POST: Profile/Create
        [HttpPost]
        public ActionResult Create(ProfileViewModel ProfileViewModel, HttpPostedFileBase ImageUpload)
        {
            try
            {
                // TODO: Add insert logic here
                if (ImageUpload!=null) {

                    string fileName = Path.GetFileNameWithoutExtension(ImageUpload.FileName);
                    string extension = Path.GetExtension(ImageUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    ProfileViewModel.ImagePath = "~/AppFiles/Images/" + fileName;
                    ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFiles/Images/"), fileName));

                }

                ProfileServices.Create(ProfileViewModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Profile/Edit/5
        public ActionResult Edit(int id)
        {
            LoadAll();
            var data = ProfileServices.GetByID(id);
            return View(data);
        }

        // POST: Profile/Edit/5
        [HttpPost]
        public ActionResult Edit(ProfileViewModel ProfileViewModel, HttpPostedFileBase ImageUpload)
        {
            try
            {
                // TODO: Add insert logic here
                if (ImageUpload != null)
                {

                    string fileName = Path.GetFileNameWithoutExtension(ImageUpload.FileName);
                    string extension = Path.GetExtension(ImageUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    ProfileViewModel.ImagePath = "~/AppFiles/Images/" + fileName;
                    ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFiles/Images/"), fileName));

                }

                ProfileServices.Update(ProfileViewModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Profile/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: Profile/Delete/5
        [HttpPost]
        public JsonResult Delete(int ProfileId)
        {
            try
            {
                // TODO: Add delete logic here
                ProfileServices.Delete(ProfileId);
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Not Deleted", JsonRequestBehavior.AllowGet);
            }
        }



        public void LoadAll() {


            //var GenderList = GenderServices.GetAll();
            //ViewBag.GenderList = new SelectList(GenderList, "GenderID", "GenderName");

            ViewBag.GenderList = new SelectList(GenderServices.GetDropDown(), "Value", "Text");
            ViewBag.PostOfficeList = new SelectList(PostOfficeServices.GetDropDown(), "Value", "Text");

            ViewBag.BloodGroupList = new SelectList(BloodGroupServices.GetDropDown(), "Value", "Text");
            ViewBag.NationalityList = new SelectList(NationalityServices.GetDropDownValue(), "Value", "Text");
            ViewBag.MaritalList = new SelectList(MaritalStatusServices.GetDropDown(), "Value", "Text");
            ViewBag.RegionList = new SelectList(RegionServices.GetDropDown(), "Value", "Text");
            ViewBag.DistrictList = new SelectList(DistrictServices.GetDropDown(), "Value", "Text");
            ViewBag.PostOfficeList = new SelectList(PostOfficeServices.GetDropDown(), "Value", "Text");
            ViewBag.PoliceStationList = new SelectList(PoliceStationServices.GetDropDown(), "Value", "Text");
            ViewBag.DivisionList = new SelectList(DivisionServices.GetDropDown(), "DivisionId", "DivisionName");
            ViewBag.CountryList = new SelectList(CountryServices.GetDropDown(), "CountryId", "CountryName");


        }


        //public ActionResult Edit2(int id)
        //{
        //    LoadAll();
        //    var data = ProfileServices.GetByID(id);
        //    return View(data);
        //}




        //[HttpGet]
        //public ActionResult Save() {

        //    LoadAll();


        //    return View();




        //}



        //[HttpPost]
        //public ActionResult Save(ProfileViewModel ProfileViewModel, HttpPostedFileBase ImageUpload)
        //{

        //    try
        //    {
        //    TODO: Add insert logic here
        //        if (ImageUpload != null)
        //        {

        //            string fileName = Path.GetFileNameWithoutExtension(ImageUpload.FileName);
        //            string extension = Path.GetExtension(ImageUpload.FileName);
        //            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
        //            ProfileViewModel.ImagePath = "~/AppFiles/Images/" + fileName;
        //            ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFiles/Images/"), fileName));

        //        }

        //        ProfileServices.Create(ProfileViewModel);
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }







        //}

    }
}
