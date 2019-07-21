using Core.Services;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class DamageReportController : Controller
    {
        private UnitOfWork unitOfWork;
        private ReportService reportService;



        public DamageReportController()
        {
            unitOfWork = new UnitOfWork();
            reportService = new ReportService(unitOfWork);

        }
        // GET: DamageReport
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Getdamageproduct()
        {
            var data = reportService.GetDamageProduct();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Getdamageitem()
        {
            var data = reportService.GetDamageitem();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetdamageRepo(int? itmid, int? productid,DateTime? fromdate,DateTime? todate)
        {
            var data = reportService.GetDamageReport(itmid, productid, fromdate, todate);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}