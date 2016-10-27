using PastebookDataAccess;
using PastebookDataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookBusinessLogic
{
    public class UserManager
    {
        private UserRepository userRepo;
        private PasswordManager pwdManager;

        public bool UpdateUserInformation(USER userUpdate)
        {
            bool returnValue = false;
            var user = GetUserByID(userUpdate.ID);
            user.FIRST_NAME = userUpdate.FIRST_NAME;
            user.LAST_NAME = userUpdate.LAST_NAME;
            user.BIRTHDAY = userUpdate.BIRTHDAY;
            user.COUNTRY_ID = userUpdate.COUNTRY_ID;
            user.GENDER = userUpdate.GENDER;
            user.MOBILE_NO = userUpdate.MOBILE_NO;

            if (user.USER_NAME == userUpdate.USER_NAME)
            {
                user.USER_NAME = userUpdate.USER_NAME;
                returnValue = UpdateUser(user);
                return returnValue;
            }else
            {
                if (IsUserNameValid(userUpdate.USER_NAME))
                {
                    user.USER_NAME = userUpdate.USER_NAME;
                    returnValue = UpdateUser(user);
                }
            }
            return returnValue;
        }

        public bool ChangeEmailAddress(USER userUpdate,string confirmPassword)
        {
            bool returnValue = false;
            var user = GetUserByID(userUpdate.ID);
            bool cntr = pwdManager.IsPasswordMatch(confirmPassword,user.SALT, user.PASSWORD);
            if (cntr)
            {
                if (user.EMAIL_ADDRESS == userUpdate.EMAIL_ADDRESS)
                {
                    return true;
                }
                else
                {
                    user.EMAIL_ADDRESS = userUpdate.EMAIL_ADDRESS;
                    returnValue = UpdateUser(user);
                }
            }
            return returnValue;

        }

         

        public bool ChangePassword(USER userUpdate,string oldPassword)
        {
            bool returnValue = false;
            var getUser = GetUserByID(userUpdate.ID);
            bool cntr = pwdManager.IsPasswordMatch(oldPassword,getUser.SALT,getUser.PASSWORD);
            if (cntr)
            {
                string salt = string.Empty;
                string password = pwdManager.GeneratePasswordHash(userUpdate.PASSWORD, out salt);
                getUser.SALT = salt;
                getUser.PASSWORD = password;
                returnValue = UpdateUser(getUser);
            }
            return returnValue;
        }

        public UserManager()
        {
            userRepo = new UserRepository();
            pwdManager = new PasswordManager();
        }

        public bool RegisterUser(USER user)
        {
            bool returnValue = false;
            if (IsUserNameValid(user.USER_NAME) && IsEmailValid(user.EMAIL_ADDRESS))
            {
                string salt = string.Empty;
                string password = pwdManager.GeneratePasswordHash(user.PASSWORD, out salt);
                user.DATE_CREATED = DateTime.Now;
                user.SALT = salt;
                user.PASSWORD = password;
                returnValue = userRepo.Add(user);
            }
            return returnValue;
        } 
         
        public bool UpdateUser(USER user)
        {
            bool returnValue = false;
            returnValue = userRepo.Update(user);
            return returnValue;
        }

        public USER LoginUser(string email,string password)
        {
            USER getUser = null;
            getUser = userRepo.Get(usr=>usr.EMAIL_ADDRESS == email,usr=>new USER {ID=usr.ID,USER_NAME= usr.USER_NAME,SALT = usr.SALT,PASSWORD = usr.PASSWORD });
            if (getUser!=null)
            {
                bool cntr = pwdManager.IsPasswordMatch(password, getUser.SALT, getUser.PASSWORD);
                if (!cntr)
                {
                    return null;
                }
            }
            return getUser;
        }

        public USER GetUser(string userName)
        {
            USER user = null;
            user = userRepo.Get(usr => usr.USER_NAME == userName);
            return user;
        }

        public USER GetUserByID(int id)
        {
            USER user = new USER();
            user =userRepo.Get(usr=>usr.ID == id);
            return user;
        }

        public bool IsUserNameValid(string userName)
        {
            bool returnValue = false;
            returnValue = !userRepo.CheckIfUserNameExist(userName);
            return returnValue;
        }

        public bool IsEmailValid(string email)
        {
            bool returnValue = false; 
            returnValue = !userRepo.CheckIfEmailExist(email);
            return returnValue;
        }

        public string GetUserNameByID(int id)
        {
            string userName = string.Empty;
            userName = userRepo.Get(usr=>usr.ID == id,usr=>usr.USER_NAME);
            return userName;
        }

        public string GetEmailByID(int id)
        {
            string email = string.Empty;
            email = userRepo.Get(usr=>usr.ID == id , usr=>usr.EMAIL_ADDRESS);
            return email;
        }

        public List<USER> GetUserFriendList(List<FRIEND> friendList,int userID)
        {
            List<USER> users = new List<USER>();
            foreach (var friend in friendList)
            {
                int getUserID = 0;
                if (friend.USER_ID == userID)
                {
                    getUserID = friend.FRIEND_ID;
                }else if(friend.FRIEND_ID == userID)
                {
                    getUserID = friend.USER_ID;
                }
                var getUser = userRepo.Get(usr => usr.ID == getUserID);
                users.Add(getUser);
            }
            return users;
        }

        public List<USER> GetUserBySearch(string searchText)
        {
            List<USER> users = new List<USER>();
            users = userRepo.GetList(usr=>usr.FIRST_NAME == searchText || usr.LAST_NAME == searchText || searchText == (usr.FIRST_NAME + " " + usr.LAST_NAME));
            return users;
        }

        public byte[] GetUserImage(int userID)
        {
            USER user = new USER();
            user = userRepo.Get(usr=>usr.ID == userID);
            return user.PROFILE_PIC;
        }


    }
}
