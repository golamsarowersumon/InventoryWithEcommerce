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
   
    public class MenuSubController : Controller
    {
        private MenuSubServices MenuSubServices;
        private MenuMainServices MenuMainServices;
        private UnitOfWork UnitOfWork;

        public MenuSubController()
        {
            UnitOfWork = new UnitOfWork();
            MenuSubServices = new MenuSubServices(UnitOfWork);
            MenuMainServices = new MenuMainServices(UnitOfWork);


        }

        // GET: MenuSub
        public ActionResult Index()
        {
            var data = MenuSubServices.GetAll();
            return View(data);
        }

        // GET: MenuSub/Details/5
        public ActionResult Details(int id)
        {
            var data = MenuSubServices.GetByID(id);
            return View(data);
        }

        // GET: MenuSub/Create
        public ActionResult Create()
        {
            LoadAll();
            return View();
        }

        // POST: MenuSub/Create
        [HttpPost]
        public ActionResult Create(MenuViewModel MenuViewModel)
        {
            try
            {
                // TODO: Add insert logic here
                MenuSubServices.Create(MenuViewModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MenuSub/Edit/5
        public ActionResult Edit(int id)
        {
            LoadAll();
            var data = MenuSubServices.GetByID(id);
            return View(data);
        }

        // POST: MenuSub/Edit/5
        [HttpPost]
        public ActionResult Edit(MenuViewModel MenuViewModel)
        {
            try
            {
                // TODO: Add update logic here
                MenuSubServices.Update(MenuViewModel);
                return RedirectToAction("Index");
            }
            catch
            {
                LoadAll();
                return View(MenuViewModel);
            }
        }

        // GET: MenuSub/Delete/5
        public ActionResult Delete(int id)
        {
            var data = MenuSubServices.GetByID(id);
            return View(data);
        }

        // POST: MenuSub/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                // TODO: Add delete logic here
                MenuSubServices.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public void LoadAll() {



            var MenuMainList = MenuMainServices.GetDropDown();
            ViewBag.MenuMainList = new SelectList(MenuMainList, "MainMenuId", "MenuName");

        }
    }
}
