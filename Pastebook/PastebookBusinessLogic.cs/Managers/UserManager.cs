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



    }
}
