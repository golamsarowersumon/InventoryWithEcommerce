using Core.Services;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
   
    public class MenuController : Controller
    {
        private MenuMainServices MenuMainServices;
        private MenuSubServices MenuSubServices;
        private MenuSubSubServices MenuSubSubServices;
        private UnitOfWork UnitOfWork;

        public MenuController()
        {
            UnitOfWork = new UnitOfWork();
            MenuMainServices = new MenuMainServices(UnitOfWork);
            MenuSubServices = new MenuSubServices(UnitOfWork);
            MenuSubSubServices = new MenuSubSubServices(UnitOfWork);



        }
       
        [ChildActionOnly]
        public ActionResult LeftMenu()
        {


            //ViewBag.EmployeeImage = objMenuDAL.GetEmployeePhoto(objMenuModel);
            ViewBag.MainMenuList =MenuMainServices.GetAll();
            ViewBag.SubMenuList = MenuSubServices.GetAll();
            ViewBag.SubSubMenuList = MenuSubSubServices.GetAll();


            return PartialView("_LeftMenuPartial");
        }

        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }
    }
}