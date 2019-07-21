using Core.Services;
using Domain.Repositories;
using Domain.ViewModels;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class CatagorywiseSalesController : Controller
    {
        private UnitOfWork UnitOfWork;

        private EcommerceService ecommerceService;
        private CustomerRegisterService customerRegisterService;

       string strUserId ="";
       
        public CatagorywiseSalesController()
        {

            UnitOfWork = new UnitOfWork();
            ecommerceService = new EcommerceService(UnitOfWork);
            customerRegisterService = new CustomerRegisterService(UnitOfWork);
        }

        public void LoadSession()
        {
            strUserId = Session["cusId"].ToString().Trim();
           

        }

        public ActionResult Index()
        {
          
            return View();
        }

        public JsonResult CatagoryLoad(string itmtype)
        {
            var lstcat = ecommerceService.GetAll(itmtype);
            return Json(lstcat, JsonRequestBehavior.AllowGet);
        }
      
        public JsonResult CatagoryWiseShow(int id, string itmtype)
        {
            var lstitem = ecommerceService.Catagorywiseview(id, itmtype);
            return Json(lstitem, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Brandshow(int id)
        {
            var lstbrand = ecommerceService.BrandView(id);
            return Json(lstbrand, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Modelshow(int? id)
        {
            var lstmodel = ecommerceService.Modelview(id);
            return Json(lstmodel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Filteredshow(int? catid,int? braid,int? modid, decimal? fromprice, decimal? toprice,string itemtype)
        {
            var lstfilteritem = ecommerceService.FilteredView(catid, braid, modid, fromprice, toprice, itemtype);
            return Json(lstfilteritem, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ItemDetailsshortview(int id)
        {
            var lstdetails = ecommerceService.SingleItemDetails(id);
            return Json(lstdetails, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddCart()
        {
            return View();
        }
       
        public ActionResult OldItemView()
        {
            return View();
        }
        public JsonResult CountryLoad()
        {
            var countrylst = ecommerceService.CountryList(); 
            return Json(countrylst, JsonRequestBehavior.AllowGet);
        }
        public JsonResult countrywisedistricload(int id)
        {
            var lstdist = ecommerceService.CountrywiseDist(id);
            return Json(lstdist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult districtwiseshippingcharge(int id)
        {
            var lstdist = ecommerceService.Distwiseshipping(id);
            return Json(lstdist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckOut()
        {
            if (Session["cusId"] == null)
            {
                return RedirectToAction("E_Commerce_Account", "CatagorywiseSales");
            }
            else
            {
                LoadSession();

            }
            return View();
        }


        public ActionResult E_Commerce_Account()
        {
            return View();
        }
        public JsonResult CustomerLogIn(CustomerRegisterViewModel customerRegisterViewModel)
        {

            customerRegisterService.LogIn(customerRegisterViewModel);

            Session["cusId"] = customerRegisterViewModel.CustomerPhone;
           
            
            return Json(new { success = customerRegisterViewModel.Errormessage });
        }
        public JsonResult CustomerRegister(CustomerRegisterViewModel customerRegisterViewModel)
        {
           
            

            customerRegisterService.Register(customerRegisterViewModel);
            Session["cusId"] = customerRegisterViewModel.CustomerPhone;
            return Json(new { success = customerRegisterViewModel.Errormessage });
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "InitialIndex");
        }
    }
}