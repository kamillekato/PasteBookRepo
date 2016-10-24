using PastebookDataAccess;
using PastebookDataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookBusinessLogic
{
    public class NotificationManager
    {
        private NotificationRepository NotifRepo;
        public NotificationManager()
        {
            NotifRepo = new NotificationRepository();
        }

        public int CountFriendRequest(int userID)
        {
            int returnValue = 0;
            returnValue = NotifRepo.CountFriendRequestNotification(userID);
            return returnValue;
        }

        public int CountNotification(int userID)
        {
            int returnValue = 0;
            returnValue = NotifRepo.CountNotification(userID);
            return returnValue;
        }

        public bool AddNotification(NOTIFICATION notification)
        {
            bool returnValue = false;
            notification.CREATED_DATE = DateTime.Now;
            returnValue = NotifRepo.Add(notification);
            return returnValue;
        }
        
        public bool RemoveNotification(int notifID)
        {
            bool returnValue = false;
            returnValue = NotifRepo.Remove(notif=>notif.ID == notifID); 
            return returnValue;
        }
        
        public bool RemoveFriendNotification(int userID,int friendID)
        {
            bool returnValue = false;
            returnValue = NotifRepo.Remove(notif =>
                            (notif.NOTIF_TYPE == "F" && ((notif.SENDER_ID == userID && notif.RECEIVER_ID == friendID) ||
                            (notif.SENDER_ID == friendID && notif.RECEIVER_ID == userID))));
            return returnValue;
        }

        public bool RemoveLikeNotification(int postID , int likeBy)
        {
            bool returnValue = false;
            returnValue = NotifRepo.Remove(notif=>notif.SENDER_ID == likeBy && notif.POST_ID == postID);
            return returnValue;
        }

        public List<NOTIFICATION> GetFriendRequest(int userID)
        {
            List<NOTIFICATION> friendRequest = new List<NOTIFICATION>();
            friendRequest = NotifRepo.GetList(notif=> notif.RECEIVER_ID == userID && notif.NOTIF_TYPE == "F") ;
            return friendRequest.OrderBy(friend=>friend.CREATED_DATE).ToList();
        }

        public List<NOTIFICATION> GetNotification(int userID)
        {
            List<NOTIFICATION> notifications = new List<NOTIFICATION>();
            notifications = NotifRepo.GetList(notif=> notif.RECEIVER_ID == userID && notif.NOTIF_TYPE != "F");
            return notifications.OrderBy(notif=>notif.CREATED_DATE).ToList();
        }
    }
}
