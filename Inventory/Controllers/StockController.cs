using Core.Services;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class StockController : Controller
    {
        private UnitOfWork UnitOfWork;
        private StockService StockService;
       






        public StockController()
        {

            UnitOfWork = new UnitOfWork();
            StockService = new StockService(UnitOfWork);
           

        }
        // GET: Stock
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ShowStore()
        {
            var stockbystore = StockService.GetAll();
            return Json(stockbystore, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Showstockbystore(int id)
        {
            var stockbystore = StockService.Getstockbyid(id);
            return Json(stockbystore, JsonRequestBehavior.AllowGet);
        }
    }
}