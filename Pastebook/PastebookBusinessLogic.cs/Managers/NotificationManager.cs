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

        public bool AddNotification(NOTIFICATION notification)
        {
            bool returnValue = false;
            notification.CREATED_DATE = DateTime.Now;
            returnValue = NotifRepo.Add(notification);
            return returnValue;
        }

        public List<NOTIFICATION> GetFriendRequest(int userID)
        {
            List<NOTIFICATION> friendRequest = new List<NOTIFICATION>();
            friendRequest = NotifRepo.GetList(notif=> notif.RECEIVER_ID == userID && notif.NOTIF_TYPE == "F");
            return friendRequest;
        }
    }
}
