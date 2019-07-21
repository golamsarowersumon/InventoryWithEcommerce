using Core.Services;
using Domain.Repositories;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class ProductionController : Controller
    {

        private UnitOfWork unitOfWork;
        private ProductionServices productionServices;
        private ProductDetailsServices productDetailsServices;
        private ProductServices productServices;   
        private ItemServices itemServices;



        string strUserId = "";
        string strUserName = "";
        string strUserRole = "";
        string strEmail = "";
        int intStoreId;


        public ProductionController()
        {
            unitOfWork = new UnitOfWork();
            productionServices = new ProductionServices(unitOfWork);
            productDetailsServices = new ProductDetailsServices(unitOfWork);
            productServices = new ProductServices(unitOfWork);
            itemServices = new ItemServices(unitOfWork);
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

        // GET: Production
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

        public JsonResult GetProduct()
        {
            var data = productionServices.GetAll();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProductionElement(int id)
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
                var data = productionServices.GetById(id, intStoreId);
                return Json(data, JsonRequestBehavior.AllowGet);
               
            }


           
        }

        public JsonResult Save(ProductionMasterViewModel ProductionMasterViewModel, ProductionDetailsViewModel[] productionDetailsViewModels)
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
                productionServices.Create(ProductionMasterViewModel, productionDetailsViewModels, intStoreId);
                return Json(new { success = true });
            }
        }
    }
}