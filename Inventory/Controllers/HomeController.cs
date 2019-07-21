using Domain;
using Inventory.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace Inventory.Controllers
{

    public class HomeController : Controller
    {
        //string strUserId = "";
        //string strUserName = "";
  public  ActionResult Index()
        {

           


           

            if (Request.IsAuthenticated) {



              
                //Session["strUserId"] = User.Identity.GetUserId();
                //Session["strUserName"] = User.Identity.GetUserName();
                ViewBag.username = Session["strUserName"];
                ViewBag.uid = Session["strUserId"];


                TempData["UserName"] = User.Identity.GetUserName();
                TempData["UserId"] = User.Identity.GetUserId();

            }

         
           

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            

            return View();
        }


     
    }
}