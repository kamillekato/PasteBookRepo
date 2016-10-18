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

        public bool CreatePost(PostEntity post)
        {
            bool returnValue = false;
            try
            {
                using (var context = new DB_PASTEBOOKEntities())
                {
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
    }
}
