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

    public class TransferController : Controller
    {

      
        private UnitOfWork UnitOfWork;
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

        string strUserId = "";
        string strUserName = "";
        string strUserRole = "";
        string strEmail = "";
        string strStoreId = "";



        public TransferController()
        {

            UnitOfWork = new UnitOfWork();
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
           

        }

        #region
        public void LoadSession()
        {
            strUserId = Session["strUserId"].ToString().Trim();
            strUserName = Session["strUserName"].ToString().Trim();
            if (Session["strUserRole"]==null)
            {

                Session["strUserRole"] = "";
            }
            else
            {
                strUserRole = Session["strUserRole"].ToString().Trim();
            }

            strEmail = Session["strUserEmail"].ToString().Trim();
            strStoreId = Session["strStoreId"].ToString().Trim();

        }

        #endregion



        // GET: Transfer
        public ActionResult Index()
        {
            if (Session["strUserId"] == null || Session["strUserRole"]==null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                LoadSession();

            }
            return View();
        }

        // /Transfer/SaveTransferRecord

        [HttpGet]
        public ActionResult SaveTransferRecord() {
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


        [HttpPost]
        public JsonResult SaveTransferRecord(TransferMasterViewModel TransferMasterViewModel, TransferDetailsViewModel[] TransferDetailsViewModel)
        {
            if (Session["strUserId"] == null || Session["strUserRole"] == null)
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


                TransferMasterViewModel.FromStoreId = Convert.ToInt32(strStoreId);
                TransferMasterViewModel.CreateBy = strUserName;
                string msg = TransferService.SaveTransferRecord(TransferMasterViewModel, TransferDetailsViewModel);
                TempData["message"] = msg;
                return Json(new { success = msg });
            }
          
        }


        [HttpGet]
        public ActionResult ReceiveTransfer()
        {
            if (Session["strUserId"] == null || Session["strUserRole"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                LoadSession();
              
                ViewBag.Pending = TransferService.GetPending(strStoreId);

                TransferViewModel TransferViewModel = new TransferViewModel();
                TransferViewModel.TransferViewModelList = new List<TransferViewModel>();

                TransferViewModel.TransferViewModelList = TransferService.TransferPendingInformation(strStoreId);
                return View(TransferViewModel);
            }
          

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReceiveTransfer(TransferViewModel transferViewModel)
        {
            if (Session["strUserId"] == null || Session["strUserRole"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                LoadSession();

                if (transferViewModel.TransferViewModelList != null)
                {
                    transferViewModel.TransferViewModelList.RemoveAll(x => !x.IsChecked);
                    if (transferViewModel.TransferViewModelList.Any())
                    {
                        transferViewModel.StoreId = Convert.ToInt32(strStoreId);
                        transferViewModel.RecieveBy = strUserName;
                        string msg = TransferService.ReceiveTransfer(transferViewModel);
                        TempData["OperationMessage"] = msg;
                        ModelState.Clear();
                    }
                }

          
                ViewBag.Pending = TransferService.GetPending(strStoreId);
                transferViewModel.TransferViewModelList = TransferService.TransferPendingInformation(strStoreId);
                return View(transferViewModel);
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
                ViewBag.Transferlist = TransferService.CountOrderInformattion(strStoreId);
                ViewBag.Receivelist = TransferService.CountOrderReceiveInformattion(strStoreId);
                ViewBag.Pendinglist = TransferService.CountOrderPendingInformattion(strStoreId);
                ViewBag.Cancellist = TransferService.CountOrderCancelInformattion(strStoreId);
                //ViewBag.Orderlist = TransferService.CountOrderInformattion(strStoreId);

                TransferViewModel TransferViewModel = new TransferViewModel();
                TransferViewModel.TransferViewModelList = new List<TransferViewModel>();

                TransferViewModel.TransferViewModelList = TransferService.LoadTransferInformation(strStoreId);
                return View(TransferViewModel);
            }


        }



        [HttpPost]
        public ActionResult Search(TransferViewModel transferViewModel)
        {
            if (Session["strUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                LoadSession();
                transferViewModel.FromStoreId = Convert.ToInt32(strStoreId);
                TransferViewModel TransferViewModel = new TransferViewModel();
                TransferViewModel.TransferViewModelList = new List<TransferViewModel>();
                TransferViewModel.TransferViewModelList = TransferService.GetSearchInformation(transferViewModel);
               
                ModelState.Clear();
                LoadAll(strStoreId);
                return View(TransferViewModel);
            }


        }







        public void LoadAll(string strStoreId)
        {

            var StoreList = TransferService.GETALLBRANCHSTORE(strStoreId);
            ViewBag.StoreList = new SelectList(StoreList, "SID", "Name");

            var ItemList = ItemServices.GetDropDown();
            ViewBag.ItemList = new SelectList(ItemList, "ItemId", "ItemName");

            var TransferTypeList = TransferTypeServices.GetAll();
            ViewBag.TransferTypeList = new SelectList(TransferTypeList, "TransferTypeId", "TransferTypeName");

            var ConditionList = ConditionOfItemServices.GetAll();
            ViewBag.ConditionList = new SelectList(ConditionList, "ConditionOfItemId", "ConditionOfItemName");

        }

        public JsonResult GetTransferInformation(int TransferDetailsId)
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

                var data = TransferService.GetTransferInformation(TransferDetailsId);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
        }



    }
}