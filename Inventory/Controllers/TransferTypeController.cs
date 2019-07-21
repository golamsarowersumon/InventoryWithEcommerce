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
    public class TransferTypeController : Controller
    {
        private UnitOfWork unitOfWork;
        private TransferTypeServices transferTypeServices;
       

        public TransferTypeController()
        {
            unitOfWork = new UnitOfWork();
            transferTypeServices = new TransferTypeServices(unitOfWork);
        }

        // GET: TransferType
        public ActionResult Index()
        {
            var data=transferTypeServices.GetAll();
            return View(data);
        }

        // GET: TransferType/Details/5
        public ActionResult Details(int id)
        {
            var data = transferTypeServices.GetById(id);
            return View(data);
        }

        // GET: TransferType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TransferType/Create
        [HttpPost]
        public ActionResult Create(TransferTypeViewModel transferTypeVM)
        {
            try
            {
                // TODO: Add insert logic here

                transferTypeServices.Create(transferTypeVM);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TransferType/Edit/5
        public ActionResult Edit(int id)
        {
            var data = transferTypeServices.GetById(id);
            return View(data);
        }

        // POST: TransferType/Edit/5
        [HttpPost]
        public ActionResult Edit(TransferTypeViewModel transferTypeVM)
        {
            try
            {
                // TODO: Add update logic here
                transferTypeServices.Update(transferTypeVM);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TransferType/Delete/5
        public ActionResult Delete(int id = 0)
        {
            if (id != 0)
            {
                var delete = transferTypeServices.GetById(id);
                return View();
            }
            return View();
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    transferTypeServices.Delete(id);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }
    }
}
