using Pastebook.Manager;
using Pastebook.Models;
using PastebookBusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pastebook.Controllers
{
    public class PastebookController : Controller
    {
        private PastebookManager PbManager = new PastebookManager();
        
        // GET: Login
        public ActionResult Index()
        {

            
            return View();
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            ViewBag.ListOfCountry = PbManager.GetCountryList();
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(UserModel user)
        {
            PbManager.SignUpUser(user);
            return RedirectToAction("SignUp");
        }

        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                bool returnValue = PbManager.LoginUserAttempt(loginModel.EmailAddress, loginModel.Password);
                if (returnValue)
                {
                    return View("Home");
                }
            } 
            return View();
        }
         
        [HttpGet]
        public ActionResult Home()
        {
            return View();
        }

    }
}