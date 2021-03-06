﻿using PastebookDataAccess;
using PastebookDataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookBusinessLogic
{
    public class LikeManager
    {
        private LikeRepository likeRepo;
        private NotificationManager notifManager;
        public LikeManager()
        {
            likeRepo = new LikeRepository();
            notifManager = new NotificationManager();
        }
        public bool LikePost(LIKE like, int postOwnerID)
        {
            bool returnValue = false;
            var getLike = likeRepo.Get(x => x.POST_ID == like.POST_ID && x.LIKE_BY == like.LIKE_BY);
            if (getLike == null)
            {
                returnValue = likeRepo.Add(like);

                if (postOwnerID != like.LIKE_BY)
                {
                    notifManager.AddNotification(new NOTIFICATION()
                    {
                        RECEIVER_ID = postOwnerID,
                        SEEN = "N",
                        SENDER_ID = like.LIKE_BY,
                        NOTIF_TYPE = "L",
                        POST_ID = like.POST_ID
                    });
                }
            }
            
            return returnValue;
        }
        
        public bool UnlikePost(LIKE like)
        {
            bool returnValue = false;
            returnValue = likeRepo.Remove(lke=>lke.POST_ID == like.POST_ID && lke.LIKE_BY == like.LIKE_BY);
            notifManager.RemoveLikeNotification(like.POST_ID ,like.LIKE_BY);
            return returnValue;
        }
        public List<LIKE> GetPostLikes(int postID)
        {
            List<LIKE> getPostLikes = new List<LIKE>();
            getPostLikes = likeRepo.GetLikes(postID);
            return getPostLikes;
        }
    }
}
