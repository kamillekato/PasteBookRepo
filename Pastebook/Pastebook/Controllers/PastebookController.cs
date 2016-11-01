
using Pastebook.Models;
using Pastebook.Security;
using PastebookBusinessLogic;
using PastebookDataLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Pastebook.Controllers
{
    public class PastebookController : Controller
    {
        //private PastebookManager PbManager = new PastebookManager();
        private CountryManager CountryManager = new CountryManager();
        private UserManager userManager = new UserManager();
        private PostManager postManager = new PostManager();
        private LikeManager likeManager = new LikeManager();
        private CommentManager commentManager = new CommentManager();
        private FriendManager friendManager = new FriendManager();
        private NotificationManager notifManager = new NotificationManager();
        // GET: Login
         

        [HttpGet]
        [Route("SignUp")]
        public ActionResult SignUp()
        {
            if (Session["CurrentUserName"]!= null)
            {
                return RedirectToAction("Home","Pastebook");
            }
            ViewBag.CountryList = CountryManager.GetCountryList(); 
            return View();
        }

        [HttpPost]
        [Route("SignUp")]
        public ActionResult SignUp(USER user,string ConfirmPassword)
        {
            user.USER_NAME = user.USER_NAME.Trim();
            user.EMAIL_ADDRESS = user.EMAIL_ADDRESS.Trim();
            user.FIRST_NAME = user.FIRST_NAME.Trim();
            user.LAST_NAME = user.LAST_NAME.Trim();
            user.MOBILE_NO = user.MOBILE_NO.Trim(); 
            
            if (Session["CurrentUserName"] != null)
            {
                return RedirectToAction("Home", "Pastebook");
            }
            if (string.IsNullOrEmpty(ConfirmPassword))
            {
                ViewBag.CountryList = CountryManager.GetCountryList();
                ModelState.AddModelError("ConfirmPassword", "Confirm Password is required");
                return View();
            }
             
            if (ConfirmPassword != user.PASSWORD) 
            {
                ViewBag.CountryList = CountryManager.GetCountryList();
                ModelState.AddModelError("ConfirmPassword","Confirm password and password do not match");
                return View();
            }else
            {
                if (ModelState.IsValid)
                {
                    userManager.RegisterUser(user);
                    Session["CurrentUserID"] = user.ID;
                    Session["CurrentUserName"] = user.USER_NAME; 
                    return RedirectToAction("ViewProfile","Pastebook",new { userName = user.USER_NAME});
                }
            }
            ViewBag.CountryList = CountryManager.GetCountryList();
            return View();
        }

        [HttpGet]
        [Route("Login")]
        public ActionResult Login()
        {
            if (Session["CurrentUserName"] != null)
            {
                return RedirectToAction("Home", "Pastebook");
            }
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult Login(USER loginUser)
        {
            if (Session["CurrentUserName"] != null)
            {
                return RedirectToAction("Home", "Pastebook");
            }
            if ((string.IsNullOrEmpty(loginUser.EMAIL_ADDRESS) || string.IsNullOrWhiteSpace(loginUser.EMAIL_ADDRESS)) && string.IsNullOrEmpty(loginUser.PASSWORD))
            {
                ModelState.AddModelError("LoginEmailError","Email Address is required to login.");
                ModelState.AddModelError("LoginPasswordError", "Password is required to login.");
                return View();
            }
            if (string.IsNullOrEmpty(loginUser.EMAIL_ADDRESS) || string.IsNullOrWhiteSpace(loginUser.EMAIL_ADDRESS))
            {
                ModelState.AddModelError("LoginEmailError","Email Address is required to login.");
                return View();
            }
            if (string.IsNullOrEmpty(loginUser.PASSWORD))
            {
                ModelState.AddModelError("LoginPasswordError","Password is required to login.");
                return View();
            }
                var getUser = userManager.LoginUser(loginUser.EMAIL_ADDRESS,loginUser.PASSWORD);
                if (getUser!=null)
                {
                    Session["CurrentUserID"] = getUser.ID;
                    Session["CurrentUserName"] = getUser.USER_NAME;
                    return RedirectToAction("Home","Pastebook");
                }
                else
                {
                    ModelState.AddModelError("LoginError", "Invalid Email address or Password. Please double-check and try again.");
                }   
            return View();
        }
         
        [HttpGet]
        [CustomAuthorize]
        [Route("")]
        public ActionResult Home()
        {  
                return View();
            
        }

        [CustomAuthorize]
        [Route("{userName}")]
        public ActionResult ViewProfile(string userName)
        {
             
            USER user = new USER(); 
            user = userManager.GetUser(userName);
            int userID = (int)Session["CurrentUserID"];
            string status = friendManager.GetFriendStatus(userID, user.ID);

            return View(new ProfileViewModel() { User= user,Status = status});
        }
 

        public ActionResult AddFriend(FRIEND friend)
        {
             
            string userName = string.Empty;
            if (friend != null)
            {
                friend.USER_ID = (int)Session["CurrentUserID"];
                friendManager.AddFriend(friend);
                
                userName = userManager.GetUserNameByID(friend.FRIEND_ID);
            } 
            
            return RedirectToAction("ViewProfile","Pastebook",new { userName = userName});
        }


        public ActionResult AcceptFriendRequest(FRIEND friend)
        {
            if (Session["CurrentUserName"] == null)
            {
                return RedirectToAction("Login", "Pastebook");
            }
            var userName = string.Empty;
            if (friend != null)
            {
                friend.USER_ID = (int)Session["CurrentUserID"];
                friend = friendManager.GetFriend(friend.USER_ID, friend.FRIEND_ID);
                var getValue = friendManager.AcceptFriend(friend);
                userName = userManager.GetUserNameByID(friend.FRIEND_ID);
            }
            
            return RedirectToAction("ViewProfile","Pastebook", new { userName = userName });
        }

        
        public ActionResult NewsFeed()
        {
            int userID = 0;
            userID = (int)Session["CurrentUserID"];
            List<POST> newsFeed = new List<POST>();
            if (userID != 0 )
            { 
                List<int> friendsID = new List<int>();
                friendsID = friendManager.GetFriendsID(userID);
                friendsID.Add(userID);
                newsFeed = postManager.GetUsersNewsFeed(friendsID);
            } 
            return PartialView("PostList",newsFeed);
        }
         
        public ActionResult Timeline(int userID)
        {
            List<POST> timelinePost = new List<POST>();
            if (userID != 0)
            {
                timelinePost = postManager.GetUsersTimelinePost(userID);
            }
            return PartialView("PostList", timelinePost); 
        } 

        [Route("Post/{postID}")]
        [CustomAuthorize]
        public ActionResult ViewPost(int postID)
        {
            if (Session["CurrentUserName"] == null)
            {
                return RedirectToAction("Login", "Pastebook");
            }
            POST post = new POST();
            if (postID != 0)
            {
                post = postManager.GetPost(postID);
            }
            return View(post);
        }
         
         

        public ActionResult ViewNotifications(int userID)
        {
            List<NOTIFICATION> notifications = new List<NOTIFICATION>();
            if (userID != 0)
            {
                notifications = notifManager.GetNotification(userID); 
            }
            return PartialView("NotificationList",notifications);
        }


        [CustomAuthorize]
        [Route("Notifications")]
        public ActionResult ViewAllNotification()
        {
            List<NOTIFICATION> notifications = new List<NOTIFICATION>();
            int userID = (int)Session["CurrentUserID"];
            notifications = notifManager.GetNotification(userID);
            return PartialView("ViewAllNotification",notifications);
        }


        public ActionResult ViewLikes(int postID)
        {
            List<LIKE> likeList = new List<LIKE>();
            if (postID != 0)
            {
                likeList = likeManager.GetPostLikes(postID); 
            }
            return PartialView("GenerateLikesList",likeList);
        } 
        
        [Route("Friends")]
        [CustomAuthorize]
        public ActionResult FriendList()
        { 
            //List<FRIEND> friendList = new List<FRIEND>();
            int userID = 0;
            userID = (int)Session["CurrentUserID"]; 
            List<int> friendsID = new List<int>();
            friendsID = friendManager.GetFriendsID(userID);
            List<USER> users = new List<USER>();
            
            users = userManager.GetUserFriendList(friendsID);
            return View(users);
           
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login","Pastebook");
            
        }

        [Route("Settings")]
        [CustomAuthorize]
        public ActionResult Settings()
        { 
            return View();
        }
        
        [HttpGet]
        [Route("Settings/UpdateInformation")]
        [CustomAuthorize]
        public ActionResult UpdateInformation()
        {
            ViewBag.ListOfGender = new SelectList(new List<SelectListItem> {
                            new SelectListItem { Value="U",Text= "Unspecified",Selected =true },
                            new SelectListItem { Value = "M",Text="Male"},
                               new SelectListItem {Value = "F", Text ="Female" }
                           }, "Value", "Text");
            USER user = new USER();
            int userID = 0;
            userID = (int)Session["CurrentUserID"];
            user = userManager.GetUserByID(userID);
            ViewBag.CountryList = CountryManager.GetCountryList();

            return View(user);
        }

        [HttpPost]
        [Route("Settings/UpdateInformation")]
        [CustomAuthorize]
        public ActionResult UpdateInformation(USER userUpdate)
        {
            userUpdate.USER_NAME = userUpdate.USER_NAME.Trim();
            userUpdate.FIRST_NAME = userUpdate.FIRST_NAME.Trim();
            userUpdate.LAST_NAME = userUpdate.LAST_NAME.Trim();
            userUpdate.MOBILE_NO = userUpdate.MOBILE_NO.Trim();
             
            if (!ModelState.IsValidField("MOBILE_NO"))
            {
                ModelState.AddModelError("MOBILE_NO","Mobile number is invalid");
                ViewBag.CountryList = CountryManager.GetCountryList();
                return View();
            }else
            {
                var userID = 0;
                userID = (int)Session["CurrentUserID"];
                var getUser = userManager.GetUserByID(userID); 
                userUpdate.ID = userID;
                bool cntr = userManager.UpdateUserInformation(userUpdate);
                if (cntr)
                {
                    Session["CurrentUserName"] = userUpdate.USER_NAME;
                    return RedirectToAction("Settings", "Pastebook");
                }
                else
                {
                     
                    ViewBag.CountryList = CountryManager.GetCountryList();
                    return View();
                }

            } 
        }

        [HttpGet]
        [Route("Settings/ChangeEmailAddress")]
        [CustomAuthorize]
        public ActionResult ChangeEmailAddress()
        {
             
            USER user = new USER();
            int userID = 0;
            userID = (int)Session["CurrentUserID"];
            user = userManager.GetUserByID(userID); 
            return View(user);
        }

        [HttpPost]
        [Route("Settings/ChangeEmailAddress")]
        [CustomAuthorize]
        public ActionResult ChangeEmailAddress(USER userUpdate,string confirmPassword)
        {
            userUpdate.EMAIL_ADDRESS = userUpdate.EMAIL_ADDRESS.Trim();
             
            if (confirmPassword == "")
            {
                ModelState.AddModelError("MainError","Password is required.");
                return View();
            }
            else if (!ModelState.IsValidField("EMAIL_ADDRESS")) 
            {
                ModelState.AddModelError("EMAIL_ADDRESS", "Email Address is invalid.");
                return View();
            } else {
                int userID = 0;
                userID = (int)Session["CurrentUserID"];
                userUpdate.ID = userID;
                bool cntr = userManager.ChangeEmailAddress(userUpdate,confirmPassword);
                if (cntr)
                {
                    return RedirectToAction("Settings","Pastebook");
                }else
                {
                    var getUser = userManager.GetUserByID(userID);
                    var check = userManager.LoginUser(getUser.EMAIL_ADDRESS,confirmPassword);
                    if (check == null)
                    {
                        ModelState.AddModelError("MainError", "Password is incorrect.");
                    }
                    
                    return View(getUser);
                }
            }
        }

        [HttpGet]
        [Route("Settings/ChangePassword")]
        [CustomAuthorize]
        public ActionResult ChangePassword()
        {
             
            USER user = new USER();
            int userID = 0;
            userID = (int)Session["CurrentUserID"];
            user = userManager.GetUserByID(userID); 
            return View(user);
        }

        [HttpPost]
        [Route("Settings/ChangePassword")]
        [CustomAuthorize]
        public ActionResult ChangePassword(string password,string confirmPassword,string oldPassword)
        {
            if (password.Length > 50)
            {
                ModelState.AddModelError("PasswordError","Password must be at most 50 characters long");
                return View();
            } 

            if (confirmPassword !=password)
            {
                ModelState.AddModelError("ConfirmPasswordError","Confirm Password do not match to new password");
                return View();
            }
            int userID = 0;
            userID = (int)Session["CurrentUserID"]; 
            bool cntr = userManager.ChangePassword(new USER() { ID= userID,PASSWORD=password },oldPassword);
            if (cntr)
            {
                return RedirectToAction("Settings", "Pastebook");
            }else
            {
                ModelState.AddModelError("OldPasswordError","Old Password is incorrect");
                return View();
            }
        }

        [Route("Search")]
        [CustomAuthorize]
        public ActionResult SearchUser(string searchText)
        {
            searchText = searchText.Trim();
             
            List<USER> users = new List<USER>();
            users = userManager.GetUserBySearch(searchText);
            return View(users);
        }







        public JsonResult CheckEmailIfValid(string email)
        {
            bool returnValue = false;
            if (!string.IsNullOrEmpty(email))
            {
                returnValue = userManager.IsEmailValid(email);
            }
            return Json(new { IsValid = returnValue},JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckUserNameIfValid(string userName)
        {
            bool returnValue = false;
            if (!string.IsNullOrEmpty(userName))
            {
                returnValue = userManager.IsUserNameValid(userName);
            }
            return Json(new { IsValid = returnValue},JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserImage( )
        {
            string imageString = null;
            int userID = 0;
            userID = (int)Session["CurrentUserID"];
            if (userID != 0)
            { 
                var image = userManager.GetUserImage(userID); 
                if (image != null)
                {
                    imageString = Convert.ToBase64String(image); 
                }
            }
            return Json(new {imageString = imageString }, JsonRequestBehavior.AllowGet);
            
        }

        public JsonResult SaveAboutMe( string aboutMeContent)
        {
            int userID = 0;
            userID = (int)Session["CurrentUserID"];
            bool returnValue = false;
            if (aboutMeContent.Length <= 2000)
            { 
                if (userID != 0 )
                {
                    var user = userManager.GetUserByID(userID);
                    user.ABOUT_ME = aboutMeContent;
                    returnValue = userManager.UpdateUser(user);
                } 
            }
            return Json(new { Result = returnValue},JsonRequestBehavior.AllowGet);
        }
         
        public JsonResult ChangeProfilePicture( )
        {
            int userID = 0;
            userID = (int)Session["CurrentUserID"];
            bool returnValue = false;
            if (Request.Files.AllKeys.Any())
            {
                if (userID != 0)
                {
                    var pic = Request.Files["UploadImage"];
                    using (MemoryStream ms = new MemoryStream())
                    {
                        pic.InputStream.CopyTo(ms);
                        var user = userManager.GetUserByID(userID);
                        user.PROFILE_PIC = ms.GetBuffer();
                        returnValue = userManager.UpdateUser(user);
                    }
                }
                
            }

            return Json(new { Result = returnValue }, JsonRequestBehavior.AllowGet);
        } 

        //public JsonResult CancelFriendRequestInNotif(int userID,int friendID)
        //{
        //    bool returnValue = false;
        //    if (userID != 0 && friendID != 0 )
        //    {
        //        var friend = friendManager.GetFriend(userID, friendID);
        //        returnValue = friendManager.RemoveFriend(friend);
        //    }
        //    return Json(returnValue, JsonRequestBehavior.AllowGet);
        //}
         
        //public JsonResult AcceptFriendRequestInNotif(int userID,int friendID)
        //{
        //    bool returnValue = false;
        //    if (userID != 0 && friendID != 0)
        //    {
        //        var friend = friendManager.GetFriend(userID, friendID);
        //        returnValue = friendManager.AcceptFriend(friend); 
        //    }
        //    return Json(returnValue , JsonRequestBehavior.AllowGet);
        //}

        public JsonResult CountNotification( )
        {
            int userID = 0;
            userID = (int)Session["CurrentUserID"];
            int countNotification = 0;
            if (userID != 0 )
            { 
                countNotification = notifManager.CountNotification(userID);
            }
            return Json(new { countNotification = countNotification},JsonRequestBehavior.AllowGet);
        }  

        public JsonResult Post(string content , int profile_Owner_ID)
        { 
            content = content.Trim();
            int poster_ID = 0;
            poster_ID = (int)Session["CurrentUserID"];
            bool returnValue = false;   
            if (!string.IsNullOrEmpty(content) && poster_ID != 0 && profile_Owner_ID != 0 && !string.IsNullOrWhiteSpace(content))
            {
                returnValue = postManager.CreatePost(new POST()
                {
                    CONTENT = content,
                    POSTER_ID = poster_ID,
                    PROFILE_OWNER_ID = profile_Owner_ID
                });

            }

            return Json(new { Status = returnValue }, JsonRequestBehavior.AllowGet);
        }
         
        public JsonResult LikePost(int postID ,int postOwnerID)
        {
            int userID = 0;
            userID = (int)Session["CurrentUserID"];
            bool returnValue = false;
            if (postID != 0 && userID != 0 && postOwnerID != 0)
            {
                returnValue = likeManager.LikePost(new LIKE() { POST_ID = postID, LIKE_BY = userID }, postOwnerID);
            }
            return Json(new { Status = returnValue }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UnlikePost(int postID )
        {
            int userID = 0;
            userID = (int)Session["CurrentUserID"];
            bool returnValue = false;
            if (postID != 0 && userID != 0)
            {
                returnValue = likeManager.UnlikePost(new LIKE() { POST_ID = postID, LIKE_BY = userID });
            }
            return Json(new { Status = returnValue }, JsonRequestBehavior.AllowGet);
        } 
        public JsonResult PostComment(string content ,int postID,int postOwnerID)
        {
            int userID = 0;
            content = content.Trim();
            userID = (int)Session["CurrentUserID"];
            bool returnValue = false;
            if (!string.IsNullOrEmpty(content) && userID !=0 && postID!=0 && postOwnerID != 0)
            {
                returnValue = commentManager.AddComment(new COMMENT()
                {
                    CONTENT = content,
                    POSTER_ID = userID,
                    POST_ID = postID
                }, postOwnerID);
            } 

            return Json(new { Status = returnValue }, JsonRequestBehavior.AllowGet);
        }

    }
}