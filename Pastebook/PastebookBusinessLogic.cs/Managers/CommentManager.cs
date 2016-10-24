using PastebookDataAccess;
using PastebookDataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookBusinessLogic
{
    public class CommentManager
    {
        private CommentRepository commentRepo;
        private NotificationManager notifManager;
        public CommentManager()
        {
            commentRepo = new CommentRepository();
            notifManager = new NotificationManager();
        }

        public bool AddComment(COMMENT comment,int postOwnerID)
        {
            bool returnValue = false; 
            comment.DATE_CREATED = DateTime.Now;
            returnValue = commentRepo.Add(comment);
            if (comment.POSTER_ID != postOwnerID)
            {
                notifManager.AddNotification(new NOTIFICATION()
                {
                    RECEIVER_ID = postOwnerID,
                    SEEN = "N",
                    SENDER_ID = comment.POSTER_ID,
                    NOTIF_TYPE = "C",
                    POST_ID = comment.POST_ID,
                    COMMENT_ID = comment.ID
                });
            }
            return returnValue; 
        }

    }
}
