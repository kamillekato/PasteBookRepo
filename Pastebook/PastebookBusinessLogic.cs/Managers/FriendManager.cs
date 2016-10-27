using PastebookDataAccess;
using PastebookDataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookBusinessLogic
{
    public class FriendManager
    {
        FriendRepository friendRepo;
        NotificationManager notifManager;
        public FriendManager()
        {
            friendRepo = new FriendRepository();
            notifManager = new NotificationManager();
        }

        public bool AddFriend(FRIEND friend)
        {
            bool returnValue = false;
            friend.REQUEST = "N";
            friend.BLOCKED = "N";
            friend.CREATED_DATE = DateTime.Now;
            returnValue = friendRepo.Add(friend);
            if (returnValue == true)
            {
                var getReturn =notifManager.AddNotification(new NOTIFICATION()
                {
                    RECEIVER_ID = friend.FRIEND_ID,
                    SEEN = "N",
                    SENDER_ID = friend.USER_ID,
                    NOTIF_TYPE = "F"
                });
            }
            return returnValue;
        }

        public bool AcceptFriend(FRIEND friend)
        {
            bool returnValue = false;
            friend.REQUEST = "Y";
            returnValue = friendRepo.Update(friend);
            if (returnValue == true)
            {
                notifManager.RemoveFriendNotification(friend.USER_ID, friend.FRIEND_ID);
            } 
            return returnValue;
        }

        public bool RemoveFriend(FRIEND friend)
        {
            bool returnValue = false;
            returnValue = friendRepo.Remove(friend);
            if (returnValue ==  true)
            {
                notifManager.RemoveFriendNotification(friend.USER_ID , friend.FRIEND_ID);
            }
            
            return returnValue;
        }

        public bool CheckIfFriendExist(int userID ,int friendID)
        {
            bool returnValue = false;
            returnValue = friendRepo.CheckIfFriendExist(userID, friendID);
            return returnValue;
        }

        public FRIEND GetFriend(int userID , int friendID)
        {
            FRIEND getFriend = new FRIEND();
            getFriend = friendRepo.Get(friend=>
                            (friend.USER_ID == userID && friend.FRIEND_ID == friendID ) ||
                            (friend.USER_ID == friendID && friend.FRIEND_ID == userID));

            return getFriend;
        }

        public List<FRIEND> GetFriendList(int userID)
        {
            List<FRIEND> friendList = new List<FRIEND>();
            friendList = friendRepo.GetList(frnd=> (frnd.USER_ID == userID || frnd.FRIEND_ID == userID) && (frnd.REQUEST == "Y"));
            return friendList;
        }

        public string GetFriendStatus(int userID ,int friendID)
        {
            string status = string.Empty;
            #region checkStatus
            if (userID == friendID)
            {
                status = "Own";
            }
            else
            { 
                var getFriend = friendRepo.Get(frnd => (frnd.USER_ID == userID && frnd.FRIEND_ID == friendID) ||
                                                    (frnd.USER_ID == friendID && frnd.FRIEND_ID == userID));
                if (getFriend == null)
                {
                    status = "None";
                }
                else if (getFriend.REQUEST == "Y")
                {
                    status = "Friend";
                }
                else if (getFriend.REQUEST == "N")
                {
                    if (getFriend.USER_ID == userID)
                    {
                        status = "Request";
                    }else
                    {
                        status = "Accept";
                    }
                }
            }
            #endregion 
            return status;
        }

        public List<int> GetFriendsID(int userID)
        {
            List<FRIEND> friendList = new List<FRIEND>();
            friendList = GetFriendList(userID);
            List<int> friendsID = new List<int>();
            foreach (var friend in friendList)
            {
                if (userID == friend.USER_ID)
                {
                    friendsID.Add(friend.FRIEND_ID);
                }else
                {
                    friendsID.Add(friend.USER_ID);
                }
            }
            return friendsID; 
        }

    }
}
