using Core.Services;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class StockItemController : Controller
    {
        private UnitOfWork UnitOfWork;
        private StockService StockService;



        string strUserId = "";
        string strUserName = "";
        string strUserRole = "";
        string strEmail = "";
        int intStoreId;
        public StockItemController()
        {

            UnitOfWork = new UnitOfWork();
            StockService = new StockService(UnitOfWork);


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
        // GET: StockItem
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
        public JsonResult GetStock()
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
                var stocklst = StockService.GetAllStock(intStoreId);
                return Json(stocklst, JsonRequestBehavior.AllowGet);

            }



           
        }
    }
}