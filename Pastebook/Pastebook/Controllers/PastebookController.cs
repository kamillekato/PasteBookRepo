using Pastebook.Manager;
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
        public ActionResult Index()
        { 
            return View();
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            ViewBag.CountryList = CountryManager.GetCountryList(); 
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(USER user)
        {
            //PbManager.SignUpUser(user);
            userManager.RegisterUser(user);
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
        public ActionResult Login(USER loginUser)
        {
            if (ModelState.IsValid)
            {
                //bool returnValue = PbManager.LoginUserAttempt(loginModel.EmailAddress, loginModel.Password);
                var getUser = userManager.LoginUser(loginUser.EMAIL_ADDRESS,loginUser.PASSWORD);
                if (getUser!=null)
                {
                    //UserModel user = PbManager.GetUserByEmail(loginModel.EmailAddress);
                    Session["CurrentUserID"] = getUser.ID;
                    Session["CurrentUserName"] = getUser.USER_NAME;
                    return RedirectToAction("Home","Pastebook");
                }
            } 
            return View();
        }
         
        [HttpGet] 
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

        
         
        public ActionResult ViewProfile(string userName)
        {
            USER user = new USER();
            user = userManager.GetUser(userName);
            return View(user);
        }
 
        public ActionResult AddFriend(FRIEND friend)
        {
            friendManager.AddFriend(friend); 
            string userName = string.Empty;
            userName = userManager.GetUserNameByID(friend.FRIEND_ID);
            return RedirectToAction("ViewProfile","Pastebook",new { userName = userName});
        }

        public ActionResult AcceptFriendRequest(FRIEND friend)
        {
            friend = friendManager.GetFriend(friend.USER_ID, friend.FRIEND_ID);
            var getValue = friendManager.AcceptFriend(friend);
            var userName = userManager.GetUserNameByID(friend.FRIEND_ID);
            return RedirectToAction("ViewProfile","Pastebook", new { userName = userName });
        }

        [HttpGet]
        public ActionResult NewsFeed(int userID)
        {
            
            return PartialView();
        }

        

        public ActionResult Timeline(int userID)
        {
            List<POST> timelinePost = new List<POST>();
            timelinePost = postManager.GetUsersTimelinePost(userID);
            return PartialView("PostList", timelinePost); 
        }

         
        public ActionResult ViewPost(int postID)
        {
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

        public ActionResult DisplayComments(List<COMMENT> comments)
        {
            return PartialView("CommentList", comments);
        }

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













        public JsonResult SaveAboutMe(int userID,string aboutMeContent)
        {
            var user = userManager.GetUserByID(userID);
            user.ABOUT_ME = aboutMeContent;
            userManager.UpdateUser(user);
            return Json(new { },JsonRequestBehavior.AllowGet);
        }


        public JsonResult ChangeProfilePicture(int userID)
        {  
            if (Request.Files.AllKeys.Any())
            {
                var pic = Request.Files["UploadImage"]; 
                using (MemoryStream ms = new MemoryStream())
                {
                    pic.InputStream.CopyTo(ms);
                    var user = userManager.GetUserByID(userID);
                    user.PROFILE_PIC = ms.GetBuffer();
                    userManager.UpdateUser(user);
                }
            }

            return Json(new { }, JsonRequestBehavior.AllowGet);
        }
        


        public JsonResult CancelFriendRequestInNotif(int userID,int friendID)
        {
            bool returnValue = false;
            var friend = friendManager.GetFriend(userID, friendID);
            returnValue = friendManager.RemoveFriend(friend);
            return Json(returnValue, JsonRequestBehavior.AllowGet);
        }
         
        public JsonResult AcceptFriendRequestInNotif(int userID,int friendID)
        {
            bool returnValue = false;
            var friend = friendManager.GetFriend(userID, friendID);
            returnValue = friendManager.AcceptFriend(friend); 
            return Json(returnValue , JsonRequestBehavior.AllowGet);
        }

        public JsonResult CountNotification(int userID)
        {
            int countFriendRequest = 0;
            int countNotification = 0;
            countFriendRequest = notifManager.CountFriendRequest(userID);
            countNotification = notifManager.CountNotification(userID);
            return Json(new { countFriendRequest = countFriendRequest , countNotification = countNotification},JsonRequestBehavior.AllowGet);
        } 

        public JsonResult CheckUserIfFriend(int userID, int friendID)
        {
            FRIEND getFriend = new FRIEND();
            bool cntr = friendManager.CheckIfFriendExist(userID,friendID);
            if (cntr)
            {
                getFriend = friendManager.GetFriend(userID, friendID); 
                return Json(new { Status =cntr ,ID = getFriend.ID,RequestFriend = getFriend.REQUEST},JsonRequestBehavior.AllowGet);
            }else
            {
                return Json(new { Status = cntr  },JsonRequestBehavior.AllowGet);
            } 
        }

        public JsonResult Post(string content, int poster_ID, int profile_Owner_ID)
        {
            bool returnValue = postManager.CreatePost(new POST()
            { CONTENT = content,
            POSTER_ID= poster_ID,
            PROFILE_OWNER_ID = profile_Owner_ID
            });
             
            return Json(new { Status = returnValue }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LikeUnLikePost(int postID, int userID,int postOwnerID)
        {
            bool returnValue = false;
            returnValue = likeManager.LikeUnlikePost(new LIKE() { POST_ID = postID , LIKE_BY = userID},postOwnerID);
            return Json(new { Status = returnValue }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PostComment(string content,int userID,int postID,int postOwnerID)
        {
            bool returnValue = false;
            returnValue = commentManager.AddComment(new COMMENT() {
                CONTENT = content,
                POSTER_ID = userID,
                POST_ID = postID
            },postOwnerID);

            return Json(new { Status = returnValue }, JsonRequestBehavior.AllowGet);
        }

    }
}