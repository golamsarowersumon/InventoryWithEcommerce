using Core.Services;
using Domain.Repositories;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{

    public class TransferOrderController : Controller
    {

        private UnitOfWork UnitOfWork;
        private TransferOrderServices TransferOrderServices;
        private TransferService TransferService;
        private StoreServices StoreServices;
        private SubStoreServices SubStoreServices;
        private SubSubStoreServices SubSubStoreServices;
        private SubSubSubStoreServices SubSubSubStoreServices;
        private SubSubSubSubStoreServices SubSubSubSubStoreServices;
        private ProcurementServices ProcurementServices;
        private TransferTypeServices TransferTypeServices;
        private UnitServices UnitServices;
        private ConditionOfItemServices ConditionOfItemServices;

        private ItemServices ItemServices;
        //private InventoryServices InventoryServices;

        string strUserId = "";
        string strUserName = "";
        string strUserRole = "";
        string strEmail = "";
        string strStoreId = "";
        





        public TransferOrderController()
        {

            UnitOfWork = new UnitOfWork();
            TransferOrderServices = new TransferOrderServices(UnitOfWork);
            TransferService = new TransferService(UnitOfWork);
            ItemServices = new ItemServices(UnitOfWork);
            ProcurementServices = new ProcurementServices(UnitOfWork);
            StoreServices = new StoreServices(UnitOfWork);
            SubStoreServices = new SubStoreServices(UnitOfWork);
            SubSubStoreServices = new SubSubStoreServices(UnitOfWork);
            SubSubSubStoreServices = new SubSubSubStoreServices(UnitOfWork);
            SubSubSubSubStoreServices = new SubSubSubSubStoreServices(UnitOfWork);
            TransferTypeServices = new TransferTypeServices(UnitOfWork);
            UnitServices = new UnitServices(UnitOfWork);
            ConditionOfItemServices = new ConditionOfItemServices(UnitOfWork);
           // InventoryServices = new InventoryServices(UnitOfWork);

        }



       
        #region
        public void LoadSession()
        {
            strUserId = Session["strUserId"].ToString().Trim();
            strUserName = Session["strUserName"].ToString().Trim();
            if (Session["strUserRole"]== null)
            {

                Session["strUserRole"] = "";
            }
            else {
                strUserRole = Session["strUserRole"].ToString().Trim();
            }
            
            strEmail = Session["strUserEmail"].ToString().Trim();
            strStoreId = Session["strStoreId"].ToString().Trim();

        }

#endregion





        // GET: TransferOrder
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

        public JsonResult LoadTransferType()
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
                var data = TransferTypeServices.GetAll();
                return Json(data, JsonRequestBehavior.AllowGet);
            }

           
        }

        public JsonResult LoadAllStore()
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
                var data = TransferOrderServices.LoadAllStore();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            
        }

        
        public JsonResult LoadItem()
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
                var data = ItemServices.GetDropDown();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
           

        }

        public JsonResult LoadConditionOfItem()
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
                var data = ConditionOfItemServices.GetAll();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
          

        }

        public JsonResult GETALLBRANCHSTORE()
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
                var data = TransferOrderServices.GETALLBRANCHSTORE(strStoreId);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
          
          
        }


        public JsonResult LoadUnit(int ItemId)
        {
            if (Session["strUserId"] == null)
            {
                return Json(new
                {
                    redirectUrl = Url.Action("Login", "Account"),
                    isRedirect = true
                });
            }
            else {
                var data = ItemServices.GetUnit(ItemId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            

        }

        public JsonResult GetInventoryItemInformation(TransferOrderViewModel TransferOrderViewModel)
        {
            if (Session["strUserId"] == null)
            {
                return Json(new
                {
                    redirectUrl = Url.Action("Login", "Account"),
                    isRedirect = true
                });
            }
            else {

                var data = TransferOrderServices.GetInventoryItemInformation(TransferOrderViewModel);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
           

        }





        public JsonResult SaveTransferOrderRecord(TransferOrderViewModel[] TransferOrderViewModel)
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
                TransferOrderServices.Create(TransferOrderViewModel, strStoreId,strUserName);
                return Json("Success", JsonRequestBehavior.AllowGet);
            }

        }




        public ActionResult TransferOrderReceive() {
            if (Session["strUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                LoadSession();

                ViewBag.Pending = TransferOrderServices.LoadOrderPending(strStoreId);
                TransferOrderViewModel TransferOrderViewModel = new TransferOrderViewModel();
                TransferOrderViewModel.TransferOrderList = new List<TransferOrderViewModel>();

                TransferOrderViewModel.TransferOrderList = TransferOrderServices.LoadOrderPendingInformation(strStoreId);
                return View(TransferOrderViewModel);
            }


        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult TransferOrderReceive(TransferOrderViewModel obj, string submit)
        //{
        //    switch (submit)
        //    {
        //        case "Save":
        //            TempData["OperationMessage"]= "Customer saved successfully!";
        //            ViewBag.Message = "Customer saved successfully!";
        //            break;
        //        case "Cancel":
        //            TempData["OperationMessage"] = "Customer Cancel successfully!";
        //            break;
        //    }
        //    return View(obj);
        //}




        [HttpPost]
        public ActionResult SaveForm(TransferOrderViewModel TransferOrderViewModel)
        {
            if (Session["strUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                LoadSession();
                TransferOrderViewModel.TransferOrderReceiveby = strUserId;
                if (TransferOrderViewModel.TransferOrderList != null)
                {
                    TransferOrderViewModel.TransferOrderList.RemoveAll(x => !x.IsChecked);
                    if (TransferOrderViewModel.TransferOrderList.Any())
                    {
                        TransferOrderServices.Receivetransferorder(TransferOrderViewModel);
                        TempData["message"] = "Successfully Received Order";
                        ModelState.Clear();
                    }
                }


                TransferOrderViewModel.TransferOrderList = TransferOrderServices.LoadOrderPendingInformation(strStoreId);
                return View("TransferOrderReceive", TransferOrderViewModel);
            }
        }

        [HttpPost]
        public ActionResult CancelForm(TransferOrderViewModel TransferOrderViewModel)
        {
            if (Session["strUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                LoadSession();
                TransferOrderViewModel.TransferOrderReceiveby = strUserId;
                if (TransferOrderViewModel.TransferOrderList != null)
                {
                    TransferOrderViewModel.TransferOrderList.RemoveAll(x => !x.IsChecked);
                    if (TransferOrderViewModel.TransferOrderList.Any())
                    {
                        TransferOrderServices.Canceltransferorder(TransferOrderViewModel);
                        TempData["message"] = "Successfully Cancel Order";
                        ModelState.Clear();
                    }
                }


                TransferOrderViewModel.TransferOrderList = TransferOrderServices.LoadOrderPendingInformation(strStoreId);
                return View("TransferOrderReceive", TransferOrderViewModel);
            }
        }


        public JsonResult GetTransferOrderInformation()
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
                var data = TransferOrderServices.GetTransferOrderInformation(strStoreId);
                return Json(data, JsonRequestBehavior.AllowGet);
            }

        }
        

              public JsonResult GetOrderInformation(int TransferOrderId)
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

                var data = TransferOrderServices.GetOrderInformation(TransferOrderId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
        }

        public JsonResult GetOrderInfo(int TransferOrderId)
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

                var data = TransferOrderServices.GetOrderInfo(TransferOrderId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
        }


        

  public JsonResult CheckStore(int StoreId)
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

                var chkId = strStoreId;
                if (StoreId == Convert.ToInt32(chkId))
                {
                    var msg = "You Can not Ordered Item/Product in Same Store!!";
                    return Json(msg);

                }
                else
                    return Json(null);

            }

           

        }


        [HttpGet]
        public ActionResult Search()
        {
            if (Session["strUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                LoadSession();
         
                LoadAll(strStoreId);
                ViewBag.Orderlist = TransferOrderServices.CountOrderInformattion(strStoreId);
                ViewBag.Receivelist = TransferOrderServices.CountOrderReceiveInformattion(strStoreId);
                ViewBag.Pendinglist = TransferOrderServices.CountOrderPendingInformattion(strStoreId);
                ViewBag.Cancellist = TransferOrderServices.CountOrderCancelInformattion(strStoreId);
                //ViewBag.Orderlist = TransferOrderServices.CountOrderInformattion(strStoreId);

                TransferOrderViewModel TransferOrderViewModel = new TransferOrderViewModel();
                TransferOrderViewModel.TransferOrderList = new List<TransferOrderViewModel>();

                TransferOrderViewModel.TransferOrderList = TransferOrderServices.LoadOrderInformation(strStoreId);
              return View(TransferOrderViewModel);
            }


        }



        [HttpPost]
        public ActionResult Search(TransferOrderViewModel transferOrderViewModel)
        {
            if (Session["strUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                LoadSession();
                transferOrderViewModel.FromStoreId = Convert.ToInt32(strStoreId);
                TransferOrderViewModel TransferOrderViewModel = new TransferOrderViewModel();
                TransferOrderViewModel.TransferOrderList = new List<TransferOrderViewModel>();
                TransferOrderViewModel.TransferOrderList = TransferOrderServices.GetSearchInformation(transferOrderViewModel);
                ModelState.Clear();
                LoadAll(strStoreId);
                return View(TransferOrderViewModel);
            }


        }







        public void LoadAll(string strStoreId)
        {

            var StoreList = TransferOrderServices.GETALLBRANCHSTORE(strStoreId);
            ViewBag.StoreList = new SelectList(StoreList, "SID", "Name");

            var ItemList = ItemServices.GetDropDown();
            ViewBag.ItemList = new SelectList(ItemList, "ItemId", "ItemName");

            var TransferTypeList = TransferTypeServices.GetAll();
            ViewBag.TransferTypeList = new SelectList(TransferTypeList, "TransferTypeId", "TransferTypeName");

            var ConditionList = ConditionOfItemServices.GetAll();
            ViewBag.ConditionList = new SelectList(ConditionList, "ConditionOfItemId", "ConditionOfItemName");

        }


        public ActionResult Requisition() {
            return View();


        }





    }
}