using PastebookDataAccess;
using PastebookDataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookBusinessLogic
{
    public class FriendManager
    {
        FriendRepository friendRepo;
        public FriendManager()
        {
            friendRepo = new FriendRepository();
        }

        public bool AddFriend(FRIEND friend)
        {
            bool returnValue = false;
            friend.REQUEST = "N";
            friend.BLOCKED = "N";
            friend.CREATED_DATE = DateTime.Now;
            returnValue = friendRepo.Add(friend);
            return returnValue;
        }

        public bool AcceptFriend(FRIEND friend)
        {
            bool returnValue = false;
            friend.REQUEST = "Y";
            returnValue = friendRepo.Update(friend);
            return returnValue;
        }

        public bool Unfriend(FRIEND friend)
        {
            bool returnValue = false;
            returnValue = friendRepo.Remove(friend);
            return returnValue;
        }

         public bool CheckIfFriendExist(int userID ,int friendID)
        {
            bool returnValue = false;
            returnValue = friendRepo.CheckIfFriendExist(userID, friendID);
            return returnValue;
        }

         public FRIEND GetFriend(int userID , int friendID)
        {
            FRIEND getFriend = new FRIEND();
            getFriend = friendRepo.Get(friend=>
                            (friend.USER_ID == userID && friend.FRIEND_ID == friendID ) ||
                            (friend.USER_ID == friendID && friend.FRIEND_ID == userID));

            return getFriend;
        }

        

    }
}
