using Pastebook.Manager;
using Pastebook.Models;
using PastebookBusinessLogic;
using PastebookDataLibrary;
using System;
using System.Collections.Generic;
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
            notifManager.AddNotification(new NOTIFICATION() {
                RECEIVER_ID =friend.FRIEND_ID,
                SEEN ="Y",
                SENDER_ID=friend.USER_ID,
                NOTIF_TYPE ="F" 
            });
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
            return PartialView("GeneratePostList", timelinePost); 
        }

        public ActionResult ViewFriendRequest(int userID)
        {
            List<NOTIFICATION> friendRequest = new List<NOTIFICATION>();
            friendRequest = notifManager.GetFriendRequest(userID);
            return PartialView("FriendRequestList", friendRequest);
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







        






        public JsonResult CountFriendRequest(int userID)
        {
            int count = 0;
            count = notifManager.CountFriendRequest(userID);
            return Json(count,JsonRequestBehavior.AllowGet);
        }


        public JsonResult CheckUserIfFriend(int userID, int friendID)
        {
            FRIEND getFriend = new FRIEND();
            bool cntr = friendManager.CheckIfFriendExist(userID,friendID);
            if (cntr)
            {
                getFriend = friendManager.GetFriend(userID, friendID);
                
                return Json(new { Status =cntr ,ID = getFriend.ID,RequestFriend = getFriend.REQUEST, UserID = getFriend.USER_ID,FriendID = getFriend.FRIEND_ID},JsonRequestBehavior.AllowGet);
            }else
            {
                return Json(new { Status = cntr, UserID = 0, FriendID = 0 },JsonRequestBehavior.AllowGet);
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

        public JsonResult LikeUnLikePost(int postID, int userID)
        {
            bool returnValue = false;
            returnValue = likeManager.LikeUnlikePost(new LIKE() { POST_ID = postID , LIKE_BY = userID});
            return Json(new { Status = returnValue }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PostComment(string content,int userID,int postID)
        {
            bool returnValue = false;
            returnValue = commentManager.AddComment(new COMMENT() {
                CONTENT = content,
                POSTER_ID = userID,
                POST_ID = postID
            });

            return Json(new { Status = returnValue }, JsonRequestBehavior.AllowGet);
        }

    }
}