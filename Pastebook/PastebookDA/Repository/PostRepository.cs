using PastebookDataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    timelinePost = context.POSTs.Include("USER1").Include("USER").Include("LIKEs").Include("COMMENTs")
                                            .Where(pst => pst.PROFILE_OWNER_ID == userID || pst.POSTER_ID == userID).OrderByDescending(pst => pst.CREATED_DATE).ToList();
                }
            }
            catch 
            {
                return timelinePost;
            }

            return timelinePost;
        }
    }
}
