
using Pastebook.Models;
using PastebookBusinessLogic;
using PastebookDataLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            if (Session["CurrentUserID"]!= null)
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
            
            if (Session["CurrentUserID"] != null)
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
            return RedirectToAction("SignUp");
        }

        [HttpGet]
        [Route("Login")]
        public ActionResult Login()
        {
            if (Session["CurrentUserID"] != null)
            {
                return RedirectToAction("Home", "Pastebook");
            }
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult Login(USER loginUser)
        {
            if (Session["CurrentUserID"] != null)
            {
                return RedirectToAction("Home", "Pastebook");
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
        [Route("")]
        public ActionResult Home()
        { 
            if (Session["CurrentUserID"] != null)
            { 
                return View();
            }else
            {
                return RedirectToAction("Login", "Pastebook");
            }
        }
         
        [Route("{userName}")]
        public ActionResult ViewProfile(string userName)
        {
            if (Session["CurrentUserID"] == null)
            {
                return RedirectToAction("Login", "Pastebook");
            }
            USER user = new USER(); 
            user = userManager.GetUser(userName);
            int userID = (int)Session["CurrentUserID"];
            string status = friendManager.GetFriendStatus(userID, user.ID);

            return View(new ProfileViewModel() { User= user,Status = status});
        }
 
        public ActionResult AddFriend(FRIEND friend)
        {
            if (Session["CurrentUserID"] == null)
            {
                return RedirectToAction("Login", "Pastebook");
            }
            friendManager.AddFriend(friend); 
            string userName = string.Empty;
            userName = userManager.GetUserNameByID(friend.FRIEND_ID);
            return RedirectToAction("ViewProfile","Pastebook",new { userName = userName});
        }

        public ActionResult AcceptFriendRequest(FRIEND friend)
        {
            if (Session["CurrentUserID"] == null)
            {
                return RedirectToAction("Login", "Pastebook");
            }
            friend = friendManager.GetFriend(friend.USER_ID, friend.FRIEND_ID);
            var getValue = friendManager.AcceptFriend(friend);
            var userName = userManager.GetUserNameByID(friend.FRIEND_ID);
            return RedirectToAction("ViewProfile","Pastebook", new { userName = userName });
        }

        
        public ActionResult NewsFeed(int userID)
        {

            List<int> friendsID = new List<int>();
            friendsID = friendManager.GetFriendsID(userID);
            friendsID.Add(userID);
            List<POST> newsFeed = postManager.GetUsersNewsFeed(friendsID);

            return PartialView("PostList",newsFeed);
        }
         
        public ActionResult Timeline(int userID)
        {
            List<POST> timelinePost = new List<POST>();
            timelinePost = postManager.GetUsersTimelinePost(userID);
            return PartialView("PostList", timelinePost); 
        } 

        [Route("Post/{postID}")]
        public ActionResult ViewPost(int postID)
        {
            if (Session["CurrentUserID"] == null)
            {
                return RedirectToAction("Login", "Pastebook");
            }
            POST post = new POST();
            post = postManager.GetPost(postID); 
            return View(post);
        }
         
        public ActionResult ViewFriendRequest(int userID)
        {
            List<NOTIFICATION> friendRequest = new List<NOTIFICATION>();
            friendRequest = notifManager.GetFriendRequest(userID);
            return PartialView("FriendRequestList", friendRequest);
        } 

        public ActionResult ViewNotifications(int userID)
        {
            List<NOTIFICATION> notifications = new List<NOTIFICATION>();
            notifications = notifManager.GetNotification(userID);
            return PartialView("NotificationList",notifications);
        }

        public ActionResult ViewLikes(int postID)
        {
            List<LIKE> likeList = new List<LIKE>();
            //likeList = PbManager.GetPostLikes(postID);
            likeList = likeManager.GetPostLikes(postID);
            return PartialView("GenerateLikesList",likeList);
        }

        
        [Route("Friends")]
        public ActionResult FriendList()
        {
            if (Session["CurrentUserID"] != null)
            {
                List<FRIEND> friendList = new List<FRIEND>();
                friendList = friendManager.GetFriendList((int)Session["CurrentUserID"]);
                List<USER> users = new List<USER>();
                users = userManager.GetUserFriendList(friendList,(int)Session["CurrentUserID"]);
                return View(users);
            }else
            {
                return RedirectToAction("Login", "Pastebook");
            }
            
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login","Pastebook");
            
        }

        [Route("Settings")]
        public ActionResult Settings()
        {
            if (Session["CurrentUserID"] == null)
            {
                return RedirectToAction("Login", "Pastebook");
            }
            return View();
        }
        
        [HttpGet]
        [Route("Settings/UpdateInformation")]
        public ActionResult UpdateInformation()
        {
            if (Session["CurrentUserID"] == null)
            {
                return RedirectToAction("Login", "Pastebook");
            }
            USER user = new USER();
            int userID = (int)Session["CurrentUserID"];
            user = userManager.GetUserByID(userID);
            ViewBag.CountryList = CountryManager.GetCountryList();

            return View(user);
        }

        [HttpPost]
        [Route("Settings/UpdateInformation")]
        public ActionResult UpdateInformation(USER userUpdate)
        { 
            if (Session["CurrentUserID"] == null)
            {
                return RedirectToAction("Login", "Pastebook");
            }
            if (!ModelState.IsValidField("MOBILE_NO"))
            {
                ModelState.AddModelError("MOBILE_NO","Mobile number is invalid");
                ViewBag.CountryList = CountryManager.GetCountryList();
                return View();
            }else
            {
                var userID = (int)Session["CurrentUserID"];
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
        public ActionResult ChangeEmailAddress()
        {
            if (Session["CurrentUserID"] == null)
            {
                return RedirectToAction("Login", "Pastebook");
            }
            USER user = new USER();
            int userID = (int)Session["CurrentUserID"];
            user = userManager.GetUserByID(userID); 
            return View(user);
        }

        [HttpPost]
        [Route("Settings/ChangeEmailAddress")]
        public ActionResult ChangeEmailAddress(USER userUpdate,string confirmPassword)
        {
            if (Session["CurrentUserID"] == null)
            {
                return RedirectToAction("Login", "Pastebook");
            }
            if (!ModelState.IsValidField("EMAIL_ADDRESS"))
            {
                ModelState.AddModelError("EMAIL_ADDRESS","Email Address is invalid");
                return View();
            }else { 
                int userID = (int)Session["CurrentUserID"];
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
                        ModelState.AddModelError("MainError", "Password is incorrect");
                    }
                    
                    return View(getUser);
                }
            }
        }

        [HttpGet]
        [Route("Settings/ChangePassword")]
        public ActionResult ChangePassword()
        {
            if (Session["CurrentUserID"] == null)
            {
                return RedirectToAction("Login", "Pastebook");
            }
            USER user = new USER();
            int userID = (int)Session["CurrentUserID"];
            user = userManager.GetUserByID(userID); 
            return View(user);
        }

        [HttpPost]
        [Route("Settings/ChangePassword")]
        public ActionResult ChangePassword(string password,string confirmPassword,string oldPassword)
        {
            if (Session["CurrentUserID"] == null)
            {
                return RedirectToAction("Login", "Pastebook");
            }
            if (confirmPassword !=password)
            {
                ModelState.AddModelError("ConfirmPasswordError","Confirm Password do not match to new password");
                return View();
            }
            int userID = (int)Session["CurrentUserID"]; 
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
        public ActionResult SearchUser(string searchText)
        {
            if (Session["CurrentUserID"] == null)
            {
                return RedirectToAction("Login", "Pastebook");
            }
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

        public JsonResult GetUserImage(int userID)
        {
            string imageString = null;
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

        public JsonResult SaveAboutMe(int userID,string aboutMeContent)
        {
            bool returnValue = false;
            if (userID != 0 && !string.IsNullOrEmpty(aboutMeContent))
            {
                var user = userManager.GetUserByID(userID);
                user.ABOUT_ME = aboutMeContent;
                returnValue = userManager.UpdateUser(user);
            }
            
            return Json(new { Result = returnValue},JsonRequestBehavior.AllowGet);
        }
         
        public JsonResult ChangeProfilePicture(int userID)
        {
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

        public JsonResult CancelFriendRequestInNotif(int userID,int friendID)
        {
            bool returnValue = false;
            if (userID != 0 && friendID != 0 )
            {
                var friend = friendManager.GetFriend(userID, friendID);
                returnValue = friendManager.RemoveFriend(friend);
            }
            return Json(returnValue, JsonRequestBehavior.AllowGet);
        }
         
        public JsonResult AcceptFriendRequestInNotif(int userID,int friendID)
        {
            bool returnValue = false;
            if (userID != 0 && friendID != 0)
            {
                var friend = friendManager.GetFriend(userID, friendID);
                returnValue = friendManager.AcceptFriend(friend); 
            }
            return Json(returnValue , JsonRequestBehavior.AllowGet);
        }

        public JsonResult CountNotification(int userID)
        {
            int countFriendRequest = 0;
            int countNotification = 0;
            if (userID != 0 )
            {
                countFriendRequest = notifManager.CountFriendRequest(userID);
                countNotification = notifManager.CountNotification(userID);
            }
            return Json(new { countFriendRequest = countFriendRequest , countNotification = countNotification},JsonRequestBehavior.AllowGet);
        }  

        public JsonResult Post(string content, int poster_ID, int profile_Owner_ID)
        {
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
         
        public JsonResult LikePost(int postID,int userID,int postOwnerID)
        {
            bool returnValue = false;
            if (postID != 0 && userID != 0 && postOwnerID != 0)
            {
                returnValue = likeManager.LikePost(new LIKE() { POST_ID = postID, LIKE_BY = userID }, postOwnerID);
            }
            return Json(new { Status = returnValue }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UnlikePost(int postID,int userID)
        {
            bool returnValue = false;
            if (postID != 0 && userID != 0)
            {
                returnValue = likeManager.UnlikePost(new LIKE() { POST_ID = postID, LIKE_BY = userID });
            }
            return Json(new { Status = returnValue }, JsonRequestBehavior.AllowGet);
        } 
        public JsonResult PostComment(string content,int userID,int postID,int postOwnerID)
        {
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