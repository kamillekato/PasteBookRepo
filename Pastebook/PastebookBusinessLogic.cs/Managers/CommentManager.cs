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
        public CommentManager()
        {
            commentRepo = new CommentRepository();
        }

        public bool AddComment(COMMENT comment)
        {
            bool returnValue = false;
            comment.DATE_CREATED = DateTime.Now;
            returnValue = commentRepo.Add(comment);
            return returnValue; 
        }

    }
}
