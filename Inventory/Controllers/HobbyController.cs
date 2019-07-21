using Core.Services;
using Domain.Repositories;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class HobbyController : Controller
    {
        private UnitOfWork unitOfWork;
        private HobbyServices hobbyService;

        public HobbyController()
        {
            unitOfWork = new UnitOfWork();
            hobbyService = new HobbyServices(unitOfWork);
        }
        // GET: Hobby
        public ActionResult Index()
        {
            var data = hobbyService.GetAll();
            return View(data);
        }


        // GET: Hobby/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Hobby/Create
        [HttpPost]
        public ActionResult Create(HobbyViewModel hobbyVM)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    hobbyService.Create(hobbyVM);

                }


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Hobby/Details/5
        public ActionResult Details(int id = 0)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HobbyViewModel details = hobbyService.GetById(id);
            if (details == null)
            {
                return HttpNotFound();
            }
            return View(details);

        }

        // GET: Hobby/Edit/5
        public ActionResult Edit(int id)
        {
            var edit = hobbyService.GetById(id);
            return View(edit);
        }

        // POST:  Hobby/Edit/5
        [HttpPost]
        public ActionResult Edit(HobbyViewModel hobbyVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add update logic here

                    hobbyService.Update(hobbyVM);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        public ActionResult Delete(int id = 0)
        {
            if (id != 0)
            {
                var delete = hobbyService.GetById(id);
                return View(delete);
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
                    hobbyService.Delete(id);

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
