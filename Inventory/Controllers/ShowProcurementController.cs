using Core.Services;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class ShowProcurementController : Controller
    {

        private UnitOfWork unitOfWork;
        private ProcurementServices ProcurementServices;


        public ShowProcurementController()
        {
            unitOfWork = new UnitOfWork();
            ProcurementServices = new ProcurementServices(unitOfWork);
        }
        // GET: ShowProcurement
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAllProcurement()
        {
            var procurementlst = ProcurementServices.GetAll();
            return Json(procurementlst, JsonRequestBehavior.AllowGet);
        }
    }
}