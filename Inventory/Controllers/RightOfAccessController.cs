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
    [Authorize(Roles ="Admin")]
    public class RightOfAccessController : Controller
    {

        private RightOfAccessService RightOfAccessService;
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






        public RightOfAccessController()
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
            RightOfAccessService = new RightOfAccessService(UnitOfWork);

        }

   #region
        public void LoadSession()
        {
            strUserId = Session["strUserId"].ToString().Trim();
            strUserName = Session["strUserName"].ToString().Trim();
            if (Session["strUserRole"].ToString() == null)
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



        // GET: RightOfAccess
        public ActionResult Index()
        {
            if (Session["strUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                LoadSession();
                LoadAll(strStoreId);
                RightOfAccesViewModel rightOfAccesViewModel = new RightOfAccesViewModel();
                rightOfAccesViewModel.rightOfAccesViewModelslist = new List<RightOfAccesViewModel>();
                rightOfAccesViewModel.rightOfAccesViewModelslist = RightOfAccessService.LoadRequest(rightOfAccesViewModel);
                return View(rightOfAccesViewModel);
            }
           
          
        }

        public void LoadAll(string strStoreId) {
           
            var StoreList = TransferOrderServices.GETALLBRANCHSTORE(strStoreId);
            ViewBag.StoreList = new SelectList(StoreList, "SID", "Name");

            var ItemList = ItemServices.GetDropDown();
            ViewBag.ItemList = new SelectList(ItemList, "ItemId", "ItemName");

        }



        [HttpPost]
        public ActionResult Search(RightOfAccesViewModel rightOfAccesViewModel)
        {
            if (Session["strUserId"] == null)
            {
                return RedirectToAction("Login","Account");
            }
            else
            {
                LoadSession();
                
                rightOfAccesViewModel.rightOfAccesViewModelslist = new List<RightOfAccesViewModel>();
                rightOfAccesViewModel.rightOfAccesViewModelslist = RightOfAccessService.GetSearchInfo(rightOfAccesViewModel);
                LoadAll(strStoreId);
                return View("Index", rightOfAccesViewModel);


            }


        }




        [HttpPost]
        public ActionResult Save(RightOfAccesViewModel rightOfAccesViewModel)
        {
            if (Session["strUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                LoadSession();
                LoadAll(strStoreId);

                if (rightOfAccesViewModel.rightOfAccesViewModelslist != null)
                {
                    rightOfAccesViewModel.rightOfAccesViewModelslist.RemoveAll(x => !x.IsChecked);
                    if (rightOfAccesViewModel.rightOfAccesViewModelslist.Any())
                    {
                        rightOfAccesViewModel.CreateBy = strUserName;
                        RightOfAccessService.Save(rightOfAccesViewModel);
                        ModelState.Clear();
                        TempData["message"] = "Successfully Received Order";
                    
                    }
                }

                else
                {
                    TempData["message"] = "Empty Data";

                }


                return RedirectToAction("Index", rightOfAccesViewModel);


            }


        }




    }
}