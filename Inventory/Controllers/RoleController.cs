﻿using Inventory.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
  //[Authorize(Roles ="Admin")]
    public class RoleController : Controller
    {

        private ApplicationRoleManager _roleManager;

        public RoleController()
        {
        }

        public RoleController(ApplicationRoleManager roleManager)
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



        // GET: Role
        public ActionResult Index()
        {
            List<RoleViewModel> list = new List<RoleViewModel>();
            foreach (var role in RoleManager.Roles)
                list.Add(new RoleViewModel(role));

            return View(list);
        }

        [HttpGet]
        public ActionResult Create() {
            return View();

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult>Create(RoleViewModel model)
        {
            var role = new ApplicationRole() { Name = model.Name };
            await RoleManager.CreateAsync(role);
            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<ActionResult> Edit(string id) {
            var role = await RoleManager.FindByIdAsync(id);
            return View(new RoleViewModel(role));

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RoleViewModel model)
        {
            var role = new ApplicationRole() { Id = model.Id, Name = model.Name };
            if (!await RoleManager.RoleExistsAsync(role.Name))
            {
                await RoleManager.UpdateAsync(role).ConfigureAwait(false);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<ActionResult> Details(string id) {

            var role = await RoleManager.FindByIdAsync(id);
            return View(new RoleViewModel(role));
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {

            var role = await RoleManager.FindByIdAsync(id);
            return View(new RoleViewModel(role));
        }

        [HttpPost,ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {

            var role = await RoleManager.FindByIdAsync(id);
            await RoleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        }

    }
}