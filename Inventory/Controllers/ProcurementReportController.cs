using Core.Services;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class ProcurementReportController : Controller
    {

        private UnitOfWork unitOfWork;
        private ReportService reportService;
       


        public ProcurementReportController()
        {
            unitOfWork = new UnitOfWork();
            reportService = new ReportService(unitOfWork);
           
        }
        // GET: ProcurementReport
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetProcureitem()
        {
            var data = reportService.Getprocureitem();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProcureType()
        {
            var data = reportService.Getprocuretype();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProcurement(int itemid,int? procuretypeid,DateTime? Fromdate,DateTime? Todate)
        {
            var data = reportService.GetProcurementReport(itemid, procuretypeid,Fromdate, Todate);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}