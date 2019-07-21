using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.ViewModels;
using Domain.Repositories;
using Core.Services;
namespace Inventory.Controllers
{
    
    public class MenuSubSubController : Controller
    {
        private MenuSubSubServices MenuSubSubServices;
        private MenuSubServices MenuSubServices;
        private MenuMainServices MenuMainServices;

        private UnitOfWork UnitOfWork;

        public MenuSubSubController()
        {
            UnitOfWork = new UnitOfWork();
            MenuSubServices = new MenuSubServices(UnitOfWork);
            MenuSubSubServices = new MenuSubSubServices(UnitOfWork);
            MenuMainServices = new MenuMainServices(UnitOfWork);


        }


        // GET: MenuSubSub
        public ActionResult Index()
        {
            LoadAll();
            var data = MenuSubSubServices.GetAll();
            return View(data);
        }

        // GET: MenuSubSub/Details/5
        public ActionResult Details(int id)
        {
            var data = MenuSubSubServices.GetByID(id);
            return View(data);
        }

        // GET: MenuSubSub/Create
        public ActionResult Create()
        {
            LoadAll();
            return View();
        }

        // POST: MenuSubSub/Create
        [HttpPost]
        public ActionResult Create(MenuViewModel MenuViewModel)
        {
            try
            {
                // TODO: Add insert logic here
                MenuSubSubServices.Create(MenuViewModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MenuSubSub/Edit/5
        public ActionResult Edit(int id)
        {
            LoadAll();
            var data = MenuSubSubServices.GetByID(id);
            return View(data);
        }

        // POST: MenuSubSub/Edit/5
        [HttpPost]
        public ActionResult Edit(MenuViewModel MenuViewModel)
        {
            try
            {
                // TODO: Add update logic here
                MenuSubSubServices.Update(MenuViewModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MenuSubSub/Delete/5
        public ActionResult Delete(int id)
        {
            var data = MenuSubSubServices.GetByID(id);
            return View(data);
        }

        // POST: MenuSubSub/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                // TODO: Add delete logic here
                MenuSubSubServices.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        public void LoadAll()
        {



            var MenuSubList = MenuSubServices.GetDropDown();
            ViewBag.MenuSubList = new SelectList(MenuSubList, "SubMenuId", "SubMenuName");

            var MainMenuList = MenuMainServices.GetDropDown();
            ViewBag.MainMenuList = new SelectList(MainMenuList, "MainMenuId", "MenuName");

        }
    }
}
