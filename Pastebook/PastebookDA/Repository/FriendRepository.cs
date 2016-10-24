using PastebookDataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace PastebookDataAccess
{
    public class FriendRepository : BaseRepository<FRIEND>
    {
        public bool CheckIfFriendExist(int userID,int friendID)
        {
            bool returnValue = false;
            try
            {
                using (var context =new DB_PASTEBOOKEntities())
                {
                    returnValue = context.FRIENDs
                                    .Any(friend => (friend.USER_ID == userID && friend.FRIEND_ID == friendID)
                                        || (friend.USER_ID == friendID && friend.FRIEND_ID == userID));

                     
                }
            }
            catch 
            {
                return returnValue;
            }
            return returnValue;
        }

        public override List<FRIEND> GetList(Expression<Func<FRIEND, bool>> where)
        {
            List<FRIEND> friends = new List<FRIEND>();
            try
            {
                using ( var context = new DB_PASTEBOOKEntities())
                {
                    friends = context.FRIENDs.Include("USER").Include("USER1").Where(where).ToList();
                }
            }
            catch 
            {
                return null;
            }
            return friends;
        }

    }
}
