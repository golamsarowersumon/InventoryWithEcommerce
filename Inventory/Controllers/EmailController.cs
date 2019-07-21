using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Threading.Tasks;
using Domain.ViewModels;
using Core.Services;
using Domain.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Inventory.Controllers
{
    public class EmailController : Controller
    {
        private UnitOfWork UnitOfWork;
        private EmailServices EmailServices;
        private TransferService TransferService;
        private ItemServices ItemServices;
        private StoreServices StoreServices;
        private SubStoreServices SubStoreServices;
        private SubSubStoreServices SubSubStoreServices;
        private SubSubSubStoreServices SubSubSubStoreServices;
        private SubSubSubSubStoreServices SubSubSubSubStoreServices;
        private UnitServices UnitServices;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        string strUserId = "";
        string strUserName = "";
        string strUserRole = "";
        string strEmail = "";
        string strStoreId = "";
        public EmailController(){

            UnitOfWork = new UnitOfWork();
            EmailServices = new EmailServices(UnitOfWork);
            ItemServices = new ItemServices(UnitOfWork);
            StoreServices = new StoreServices(UnitOfWork);
            SubStoreServices = new SubStoreServices(UnitOfWork);
            SubSubStoreServices = new SubSubStoreServices(UnitOfWork);
            SubSubSubStoreServices = new SubSubSubStoreServices(UnitOfWork);
            SubSubSubSubStoreServices = new SubSubSubSubStoreServices(UnitOfWork);
            TransferService = new TransferService(UnitOfWork);
            UnitServices = new UnitServices(UnitOfWork);


}


        #region
        public void LoadSession()
        {
            strUserId = Session["strUserId"].ToString().Trim();
            strUserName = Session["strUserName"].ToString().Trim();
            strUserRole = Session["strUserRole"].ToString().Trim();
            strEmail = Session["strUserEmail"].ToString().Trim();
            strStoreId = Session["strStoreId"].ToString().Trim();

        }

        #endregion


        public void EmptySession()
        {
            Session["strUserId"] = "";
            Session["strUserName"] = "";
            Session["strUserRole"] = "";
            Session["strUserEmail"] = "";
            Session["strStoreId"] = "";

        }


        // GET: Email
        public ActionResult Index()
        {
            //if (Session["strUserId"] == null)
            //{
            //    return RedirectToAction("Login", "Account");
            //}
            //else
            //{
            //    LoadSession();
            //    ViewBag.InEmailCount = EmailServices.CountAllInEmaill(strEmail);
            //    ViewBag.SendEmailCount = EmailServices.CountAllSendEmaill(strEmail);
            //}

            return View();
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpPost]
       [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendEmail(EmailViewModel model)
        {
           //var id = HttpContext.User.Identity.GetUserId();
           // model.FromName= HttpContext.User.Identity.GetUserName();
           // model.FromEmail= await UserManager.GetEmailAsync(id);

            var isExist = IsEmailExist(model.ToEmail);
            //if (ModelState.IsValid)
            //{
            //if (isExist == true)
            //{
                var body = "<p>Email From: {0} ({1})</p><p>Subject:{2}</p><p>Message:</p><p>{3}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress(model.ToEmail));  // replace with valid value 
                //if (!string.IsNullOrWhiteSpace(model.CC))
                //{
                //    message.CC.Add(model.CC);
                //}
                //if (!string.IsNullOrEmpty(model.Bcc))
                //{
                //    message.Bcc.Add(model.Bcc);
                //}
                message.From = new MailAddress( model.FromName,model.FromEmail);  // replace with valid value
               
                message.Subject = model.Subject;
                message.Body = string.Format(body, model.FromName, model.FromEmail, model.Subject, model.Message);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    await smtp.SendMailAsync(message);
                    EmailServices.Create(model);
                    TempData["message"] = "Email Send Successfylly!!";
                }

            //}

            //else {

            //    TempData["message"] = "Email Not Found!!";
            //}

           
            return RedirectToAction("Index");
            //}
            // return View("Index",model);
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        [NonAction]
        public bool IsEmailExist(string EmailId)
        {
            var user = SignInManager.UserManager.Users.Where(x => x.Email.Equals(EmailId)).FirstOrDefault();
           
            if (user != null) {

                return true;

            }
            return false; ;





        }



        

    }
}