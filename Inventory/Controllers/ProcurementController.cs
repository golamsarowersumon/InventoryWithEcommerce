

using Core.Services;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.ViewModels;
using static Domain.ViewModels.ProcurementMasterViewModel;

namespace Inventory.Controllers
{
    public class ProcurementController : Controller
    {
        private UnitOfWork UnitOfWork;
        private ProcurementServices ProcurementServices;
        private ProcrurementTypeServices ProcrurementTypeServices;
        private SupplierCompanyServices SupplierCompanyServices;
        private StoreServices StoreService;
        private ItemServices ItemServices;
        private SubContractCompanyServices SubContractCompanyServices;
        private SubStoreServices SubStoreServices;
        private SubSubStoreServices SubSubStoreServices;
        private SubSubSubStoreServices SubSubSubStoreServices;
        private SubSubSubSubStoreServices SubSubSubSubStoreServices;
        private ConditionOfItemServices conditionOfItemServices;
        private WarrantyServices warrantyServices;
        private CountryServices countryServices;
        private PrincipleServices principleServices;


        string strUserId = "";
        string strUserName = "";
        string strUserRole = "";
        string strEmail = "";
        int intStoreId;




        public ProcurementController()
        {

            UnitOfWork = new UnitOfWork();
            ProcurementServices = new ProcurementServices(UnitOfWork);
            ProcrurementTypeServices = new ProcrurementTypeServices(UnitOfWork);
            SupplierCompanyServices = new SupplierCompanyServices(UnitOfWork);
            StoreService = new StoreServices(UnitOfWork);
            ItemServices = new ItemServices(UnitOfWork);
            SubContractCompanyServices = new SubContractCompanyServices(UnitOfWork);
            SubStoreServices = new SubStoreServices(UnitOfWork);
            SubSubStoreServices = new SubSubStoreServices(UnitOfWork);
            SubSubSubStoreServices = new SubSubSubStoreServices(UnitOfWork);
            SubSubSubSubStoreServices = new SubSubSubSubStoreServices(UnitOfWork);
            conditionOfItemServices = new ConditionOfItemServices(UnitOfWork);
            warrantyServices = new WarrantyServices(UnitOfWork);
            countryServices = new CountryServices(UnitOfWork);
            principleServices = new PrincipleServices(UnitOfWork);

        }



        #region
        public void LoadSession()
        {
            strUserId = Session["strUserId"].ToString().Trim();
            strUserName = Session["strUserName"].ToString().Trim();
            if (Session["strUserRole"] == null)
            {

                Session["strUserRole"] = "";
            }
            else
            {
                strUserRole = Session["strUserRole"].ToString().Trim();
            }

            strEmail = Session["strUserEmail"].ToString().Trim();
            string strStoreId = Session["strStoreId"].ToString().Trim();
            intStoreId = Convert.ToInt32(strStoreId);

        }

        #endregion

        // GET: Procurement
        public ActionResult Index()
        {
            if (Session["strUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                LoadSession();

            }
            return View();
        }


        public JsonResult Save(ProcurementMasterViewModel ProcurementMasterViewModel, ProcurementDetailsViewModel[] procurementDetailsViewModel)
        {
            if (Session["strUserId"] == null)
            {
                return Json(new
                {
                    redirectUrl = Url.Action("Login", "Account"),
                    isRedirect = true
                });
            }
            else
            {
                LoadSession();
                ProcurementServices.Create(ProcurementMasterViewModel, procurementDetailsViewModel, intStoreId, strUserName);
                return Json(new { success = true });
            }
           
        }

        public JsonResult ProcureType()
        {
            var ProcurementTypeList = ProcrurementTypeServices.GetAll();
            return Json(ProcurementTypeList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult supplierCompany()
        {
            var SuplierCompanyList = SupplierCompanyServices.GetAll();
            return Json(SuplierCompanyList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Conditionofitemshow()
        {
            var conditionofitemlst = conditionOfItemServices.GetAll();
            return Json(conditionofitemlst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Warrantyperiod()
        {
            var Warrantyperiodlst = warrantyServices.GetAll();
            return Json(Warrantyperiodlst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CountryShow()
        {
            var CountryShowlst = countryServices.GetAll();
            return Json(CountryShowlst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult loadPrincipalbycountry(int id)
        {
            var Principalbycountrylst = principleServices.princialbycountry(id);
            return Json(Principalbycountrylst, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Itemshow()
        {
            var ItemService = ItemServices.GetAll();
            return Json(ItemService, JsonRequestBehavior.AllowGet);
        }
        public JsonResult itemwisecatagory(int id)
        {
            var catagorylst = ItemServices.GetById(id);
            return Json(catagorylst, JsonRequestBehavior.AllowGet);
        }


    }
}