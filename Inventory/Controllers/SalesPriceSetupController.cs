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
    public class SalesPriceSetupController : Controller
    {
        private UnitOfWork UnitOfWork;
        private ProcurementServices ProcurementServices;


        public SalesPriceSetupController()
        {

            UnitOfWork = new UnitOfWork();
            ProcurementServices = new ProcurementServices(UnitOfWork);


        }
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult ItemLodforSalesprice()
        {
            var lstitem = ProcurementServices.ItemLoad();
            return Json(lstitem, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoadElementofSalesAmount()
        {
            var lstamount = ProcurementServices.SalesAmountView();
            return Json(lstamount, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Editsaleselement(int id)
        {
            var lstelement = ProcurementServices.Saleselementedit(id);
            return Json(lstelement, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Itemwisepurchaseprice(int id)
        {
            var itemelement = ProcurementServices.Itemwiseelementload(id);
            return Json(itemelement, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Create(SalesElementStupViewModel salesElementStupViewModel)
        {
            ProcurementServices.Create(salesElementStupViewModel);
            return Json(new {success=true });
        }
    }
}
