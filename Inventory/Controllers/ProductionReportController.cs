using Core.Services;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class ProductionReportController : Controller
    {

        private UnitOfWork unitOfWork;
        private ReportService reportService;



        public ProductionReportController()
        {
            unitOfWork = new UnitOfWork();
            reportService = new ReportService(unitOfWork);

        }
        // GET: ProductionReport
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetProduct()
        {
            var data = reportService.GetProduction();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetRawmaterialProductionwise(int id)
        {
            var data = reportService.GetProductionwiseItem(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}