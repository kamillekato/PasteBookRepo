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
        static SignUpViewModel signUpViewModel = new SignUpViewModel();
        // GET: Login
        public ActionResult Index()
        { 
            return View();
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            signUpViewModel.User = null;
            if (signUpViewModel.CountryList.Count() <= 0)
            { 
                signUpViewModel.CountryList = PbManager.GetCountryList();
            } 
            return View(signUpViewModel);
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
            if (Session["CurrentUserID"] != null)
            {
                return RedirectToAction("Home", "Pastebook");
            }
            return View();
        }

        [HttpPost]        
        public ActionResult Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                bool returnValue = PbManager.LoginUserAttempt(loginModel.EmailAddress, loginModel.Password);
                if (returnValue)
                {
                    UserModel user = PbManager.GetUserByEmail(loginModel.EmailAddress);
                    Session["CurrentUserID"] = user.ID;
                    Session["CurrentUserName"] = user.UserName;
                    return RedirectToAction("Home","Pastebook");
                }
            } 
            return View();
        }
         
        [HttpGet] 
        public ActionResult Home()
        {
            Session["CurrentUserID"] = 18;
            Session["CurrentUserName"] = "kamille";
            if (Session["CurrentUserID"] != null)
            { 
                return View();
            }else
            {
                return RedirectToAction("Login", "Pastebook");
            }
        }

        //[HttpGet]
        //public ActionResult UserProfile()
        //{

        //    return View();
        //}
        
        public ActionResult ViewProfile(string userName)
        {
            UserModel user = new UserModel();
            user = PbManager.GetUserByUserName(userName);
            return View(user);
        }
 

        [HttpGet]
        public ActionResult NewsFeed(int userID)
        {
            
            return PartialView();
        }

         
        
        public ActionResult Timeline(int userID)
        {
            List<PostViewModel> timelinePost = new List<PostViewModel>();
            timelinePost = PbManager.GenerateTimeline(userID);
            return PartialView(timelinePost);
        }






        public JsonResult Post(string Content,int Poster_ID,int Profile_Owner_ID)
        {
            bool returnValue = PbManager.UserCreatePost(new PostViewModel() {
                Content = Content,
                Poster_ID = Poster_ID,
                Profile_Owner_ID = Profile_Owner_ID
            });
            return Json(new { Status = returnValue }, JsonRequestBehavior.AllowGet); 
        }

        

    }
}