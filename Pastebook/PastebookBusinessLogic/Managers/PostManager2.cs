using PastebookDataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookBusinessLogic
{
    public class PostManager2
    {

        public bool CreatePost(PostEntity post)
        {
            bool returnValue = false;
            try
            {
                using (var context = new DB_PASTEBOOKEntities())
                {
                    post.CreatedDate = DateTime.Now;
                    context.POSTs.Add(Mapper.MapPostEntityToPOST(post));
                    returnValue = context.SaveChanges() != 0;
                }
            }
            catch (Exception ex)
            {
                return false;

            }
            return returnValue;
        }


        public List<PostEntity> GetUserRelatedPost(int userID)
        {
            List<PostEntity> timelinePost = new List<PostEntity>();
            try
            {
                using (var context = new DB_PASTEBOOKEntities())
                {
                    var getPostList = context.SP_TIMELINE(userID).ToList();
                    foreach (var post in getPostList)
                    {
                        timelinePost.Add(Mapper.MapSPTimelineToPostEntity(post));
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return timelinePost;
        }
        
    }
}
