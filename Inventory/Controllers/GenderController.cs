using System.Web.Mvc;
using Domain.ViewModels;
using Domain.Repositories;
using Core.Services;


namespace Domain.Controllers
{
    public class GenderController : Controller
    {
        private UnitOfWork unitOfWork;
        private GenderServices GenderServices;


        public GenderController(){
            unitOfWork = new UnitOfWork();
            GenderServices = new GenderServices(unitOfWork);
}


        // GET: Gender
        public ActionResult Index()
        {
            var getdata = GenderServices.GetAll();
                
            return View(getdata);
        }

        // GET: Gender/Details/5
        public ActionResult Details(int id)
        {
            var data = GenderServices.GetByID(id);
            return View(data);
        }

        // GET: Gender/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Gender/Create
        [HttpPost]
        public ActionResult Create(GenderViewModel genderViewModel)
        {
            try
            {
                // TODO: Add insert logic here
                GenderServices.Create(genderViewModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Gender/Edit/5
        public ActionResult Edit(int id)
        {
            var data = GenderServices.GetByID(id);
       
            return View(data);
        }

        // POST: Gender/Edit/5
        [HttpPost]
        public ActionResult Edit(GenderViewModel genderViewModel)
        {
            try
            {
                // TODO: Add update logic here
                GenderServices.Update(genderViewModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Gender/Delete/5
        public ActionResult Delete(int id=0)
        {
            if (id != 0)
            {
                var data = GenderServices.GetByID(id);
                return View(data);
            }
            return View();
        }

        // POST: Gender/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                // TODO: Add delete logic here
                GenderServices.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
