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
    public class DamagedProductController : Controller
    {

        private UnitOfWork unitOfWork;
        private DamagedItemServices damagedItemServices;
       
        private StoreServices storeServices;
        private ProductServices productServices;

        public DamagedProductController()
        {
            unitOfWork = new UnitOfWork();
            damagedItemServices = new DamagedItemServices(unitOfWork);
            storeServices = new StoreServices(unitOfWork);
            productServices = new ProductServices(unitOfWork);

        }

        // GET: DamagedProduct
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ProductGet()
        {
            var prodlst = productServices.GetAll();
            return Json(prodlst, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Productload(int id)
        {
            var prdlst = damagedItemServices.Productshowfordamage(id);
            return Json(prdlst, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Getdamageproduct(int id)
        {
            var damproductlst = damagedItemServices.Damageproductshow(id);
            return Json(damproductlst, JsonRequestBehavior.AllowGet);
        }
    }
}