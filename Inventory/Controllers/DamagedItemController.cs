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
    public class DamagedItemController : Controller
    {
        private UnitOfWork unitOfWork;
        private DamagedItemServices damagedItemServices;
        private ItemServices ItemServices;
        private StoreServices storeServices;
      
       
        public DamagedItemController()
        {
            unitOfWork = new UnitOfWork();
            damagedItemServices = new DamagedItemServices(unitOfWork);
            ItemServices = new ItemServices(unitOfWork);
            storeServices = new StoreServices(unitOfWork);
           
          
        }



        // GET: DamagedItem
        public ActionResult Index()
        {
           
            return View();
        }
       
      
        public JsonResult ItemGet()
        {
            var itemslst = ItemServices.GetDropDown();
            return Json(itemslst, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Itemload(int id)
        {
            var itemslst = damagedItemServices.Itemshowfordamage(id);
            return Json(itemslst, JsonRequestBehavior.AllowGet);
        }
       
        public JsonResult Getdamageitem(int id)
        {
            var damitemlst = damagedItemServices.DamageItemShow(id);
            return Json(damitemlst, JsonRequestBehavior.AllowGet);
        }
       
        public JsonResult SaveDamage(DamagedItemViewModel[] damagedItemVM)
        {
            damagedItemServices.Create(damagedItemVM);
            return Json(new { success = true });
        }

        //public void loadAll()
        //{
        //    var Itemlist = ItemServices.GetAll();
        //    ViewBag.Itemlist = new SelectList(Itemlist, "ItemId", "ItemName");

        //    var storelst = storeServices.GetAll();
        //    ViewBag.storelst = new SelectList(storelst, "StoreId", "StoreName");
        //}
    }
}