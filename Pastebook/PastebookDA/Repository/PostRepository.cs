using PastebookDataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace PastebookDataAccess
{
    public class PostRepository : BaseRepository<POST> ,IRepository<POST>
    { 
        public List<POST> GetUserTimelinePost(int userID)
        {
            List<POST> timelinePost = null;
            try
            {
                using (var context = new DB_PASTEBOOKEntities())
                { 
                    timelinePost = context.POSTs.Include("USER").Include("USER1").Include("LIKEs").Include("COMMENTs.USER")
                                            .Where(pst => pst.PROFILE_OWNER_ID == userID || pst.POSTER_ID == userID).ToList();
                }
            }
            catch 
            {
                return timelinePost;
            }

            return timelinePost;
        }

        public override POST Get(Expression<Func<POST, bool>> where)
        {
            POST post = new POST();
            try
            {
                using (var context = new DB_PASTEBOOKEntities())
                {
                    post = context.POSTs.Include("USER").Include("USER1").Include("LIKEs.USER").Include("COMMENTs.USER")
                            .Where(where).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return post;
        }
    }
}
