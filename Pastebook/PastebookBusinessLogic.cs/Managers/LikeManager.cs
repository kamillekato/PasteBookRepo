using PastebookDataAccess;
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

        public LikeManager()
        {
            likeRepo = new LikeRepository();
        }

        public bool LikeUnlikePost(LIKE like)
        {
            bool returnValue = false;
            LIKE getLike = null;
            getLike = likeRepo.Get(x => x.POST_ID == like.POST_ID && x.LIKE_BY == like.LIKE_BY );
            if (getLike == null)
            { 
                returnValue = likeRepo.Add(like);
            }
            else
            {
                returnValue = likeRepo.Remove(getLike);
            } 
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
