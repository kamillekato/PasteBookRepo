using PastebookDataAccess;
using PastebookDataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookBusinessLogic
{
    public class PostManager
    {
        PostRepository postRepo;
        public PostManager()
        {
            postRepo = new PostRepository();
        }


        public bool CreatePost(POST post)
        {
            bool returnValue = false;
            post.CREATED_DATE = DateTime.Now;
            returnValue = postRepo.Add(post); 
            return returnValue;
        }

        public List<POST> GetUsersTimelinePost(int id)
        {
            List<POST> timelinePost = null;
            timelinePost = postRepo.GetUserTimelinePost(id);
            return timelinePost;
        }

        public POST GetPost(int postID)
        {
            POST post = new POST();
            post = postRepo.Get(x=>x.ID== postID);
            return post;
        }


    }
}
