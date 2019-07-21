using Domain.Repositories;
using Domain.ViewModels;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;

namespace Inventory.Controllers
{
    public class LayerAssignController : Controller
    {

        private ApplicationRoleManager _roleManager;
        public UnitOfWork unitOfWork;
        public LayerAssignServices LayerAssignServices;
        public LayerServices LayerServices;


        public LayerAssignController() {
            unitOfWork = new UnitOfWork();
            LayerAssignServices = new LayerAssignServices(unitOfWork);
            LayerServices = new LayerServices(unitOfWork);
        }


        public LayerAssignController(ApplicationRoleManager roleManager)
        {

            RoleManager = roleManager;
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }






        // GET: LayerAssign
        public ActionResult Index()
        {
            var data = LayerAssignServices.GetAll();
            return View(data);
        }

        // GET: LayerAssign/Details/5
        public ActionResult Details(int id)
        {
            var data = LayerAssignServices.GetById(id);
            return View(data);
        }

        // GET: LayerAssign/Create
        public ActionResult Create()
        {
            LoadAll();
            return View();
        }

        // POST: LayerAssign/Create
        [HttpPost]
        public ActionResult Create(LayerAssignViewModel layerAssignViewModel)
        {
            try
            {
                // TODO: Add insert logic here
                LayerAssignServices.Create(layerAssignViewModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: LayerAssign/Edit/5
        public ActionResult Edit(int id)
        {
            LoadAll();
            var data = LayerAssignServices.GetById(id);
            return View(data);
     
        }

        // POST: LayerAssign/Edit/5
        [HttpPost]
        public ActionResult Edit(LayerAssignViewModel layerAssignViewModel)
        {
            try
            {
                // TODO: Add update logic here
                LayerAssignServices.Update(layerAssignViewModel);
                return RedirectToAction("Index");
            }
            catch
            {
                LoadAll();
                return View();
            }
        }

        // GET: LayerAssign/Delete/5
        public ActionResult Delete(int id)
        {
            var data = LayerAssignServices.GetById(id);
            return View(data);
      
        }

        // POST: LayerAssign/Delete/5
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                // TODO: Add delete logic here
                LayerAssignServices.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        public void LoadAll() {

            List<SelectListItem> Rolelist = new List<SelectListItem>();
            foreach (var role in RoleManager.Roles)
            {
                Rolelist.Add(new SelectListItem() { Value = role.Name, Text = role.Name });
                ViewBag.RoleList = Rolelist;

            }

            var LayerList = LayerServices.GetAll();
            ViewBag.LayerList = new SelectList(LayerList, "LayerId", "LayerName");
        }
    }
}
