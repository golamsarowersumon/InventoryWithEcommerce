using Core.Services;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class EcommerceitemDetailsController : Controller
    {
        private UnitOfWork UnitOfWork;

        private EcommerceService ecommerceService;


        

        public EcommerceitemDetailsController()
        {

            UnitOfWork = new UnitOfWork();
            ecommerceService = new EcommerceService(UnitOfWork);

        }
       
        public ActionResult Index(int id)
        {
            TempData["iid"] = id;
           
            return View();
        }
       
        int itemid = 0;
        public JsonResult ItemDetails()
        {
           

            if (TempData.ContainsKey("iid"))
                itemid = int.Parse(TempData["iid"].ToString());
            var lstdetails = ecommerceService.SingleItemDetails(itemid);
            return Json(lstdetails, JsonRequestBehavior.AllowGet);
        }
    }
}