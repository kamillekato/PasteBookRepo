using PastebookDataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace PastebookDataAccess
{
    public class NotificationRepository :BaseRepository<NOTIFICATION>
    {
        public int CountFriendRequestNotification(int userID)
        {
            int returnValue = 0;
            try
            {
                using (var context =  new DB_PASTEBOOKEntities())
                {
                    returnValue = context.NOTIFICATIONs.Where(notif => notif.RECEIVER_ID == userID && notif.NOTIF_TYPE == "F" && notif.SEEN == "N").ToList().Count();
                }
            }
            catch 
            {
                return returnValue;
            }
            return returnValue;
        }

        public int CountNotification(int userID)
        {
            int returnValue = 0;
            try
            {
                using ( var context = new DB_PASTEBOOKEntities())
                {
                    returnValue = context.NOTIFICATIONs.Where(notif => notif.RECEIVER_ID == userID && notif.NOTIF_TYPE != "F" && notif.SEEN == "N").ToList().Count();
                }
            }
            catch 
            {
                return returnValue;
            }
            return returnValue;
        }

        public int CountLikeCommentNotification(int userID)
        {
            int returnValue = 0;
            try
            {
                using (var context = new DB_PASTEBOOKEntities())
                {
                    returnValue = context.NOTIFICATIONs.Where(notif => notif.RECEIVER_ID == userID && notif.NOTIF_TYPE != "F" && notif.SEEN == "N").ToList().Count();
                }
            }
            catch 
            {
                return returnValue;
            }
            return returnValue;
        }

        public override List<NOTIFICATION> GetList(Expression<Func<NOTIFICATION, bool>> where)
        {
            List<NOTIFICATION> notifications = new List<NOTIFICATION>();
            try
            {
                using (var context = new DB_PASTEBOOKEntities())
                {
                    notifications = context.NOTIFICATIONs.Include("USER1").Where(where).ToList();
                }
            }
            catch
            {
                return notifications;
            }
            return notifications;
        }


    }
}
