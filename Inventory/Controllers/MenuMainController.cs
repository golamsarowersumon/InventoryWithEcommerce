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

    public class MenuMainController : Controller
    {

        private MenuMainServices MenuMainServices;
        private UnitOfWork UnitOfWork;

        public MenuMainController()
        {
            UnitOfWork = new UnitOfWork();
            MenuMainServices = new MenuMainServices(UnitOfWork);


        }

        // GET: MenuMain
        public ActionResult Index()
        {
            var data = MenuMainServices.GetAll();
            return View(data);
        }

        // GET: MenuMain/Details/5
        public ActionResult Details(int id)
        {
            var data = MenuMainServices.GetByID(id);
            return View(data);
        }

        // GET: MenuMain/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MenuMain/Create
        [HttpPost]
        public ActionResult Create(MenuViewModel MenuViewModel)
        {

            try
            {
                // TODO: Add insert logic here
                MenuMainServices.Create(MenuViewModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MenuMain/Edit/5
        public ActionResult Edit(int id)
        {
            var data = MenuMainServices.GetByID(id);
            return View(data);
        }

        // POST: MenuMain/Edit/5
        [HttpPost]
        public ActionResult Edit(MenuViewModel MenuViewModel)
        {
            try
            {
                // TODO: Add update logic here

                MenuMainServices.Update(MenuViewModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MenuMain/Delete/5
        public ActionResult Delete(int id)
        {
            var data = MenuMainServices.GetByID(id);
            return View(data);
        }

        // POST: MenuMain/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                // TODO: Add delete logic here
                MenuMainServices.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
