using PastebookDataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace PastebookDataAccess
{
    public class LikeRepository : BaseRepository<LIKE>
    {

        public bool CheckIfLikeExist(LIKE like)
        {
            bool returnValue = false;
            try
            {
                using (var context = new DB_PASTEBOOKEntities())
                {
                    returnValue = context.LIKEs.Where(lke => lke.POST_ID == like.POST_ID).Any(lke => lke.LIKE_BY == like.LIKE_BY);
                }
            }
            catch
            {
                return returnValue;
            }
            return returnValue;
        }

         public List<LIKE> GetLikes(int postID)
        {
            List<LIKE> postLikes = null;
            try
            {
                using (var context = new DB_PASTEBOOKEntities())
                {
                    postLikes = context.LIKEs.Include("USER").Where(lke => lke.POST_ID == postID).ToList();
                }
            }
            catch 
            {
                return postLikes;
            }
            return postLikes;
        }

    }
}
