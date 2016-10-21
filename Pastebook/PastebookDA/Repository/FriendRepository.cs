using PastebookDataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        

    }
}
