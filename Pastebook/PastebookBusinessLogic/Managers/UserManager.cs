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
        private PasswordManager pwdManager;
        public UserManager()
        {
            pwdManager = new PasswordManager();
        }

        public bool AddUser(UserEntity user)
        {
            bool returnValue = false;
            try
            {
                using (var context = new DB_PASTEBOOKEntities())
                {
                    if (CheckIfUserNameExist(user.UserName))
                    {
                        return false;
                    }
                    else if (CheckIfEmailAddressExist(user.EmailAddress))
                    {
                        return false;
                    }
                    else
                    {    
                        string salt = null;
                        string password = pwdManager.GeneratePasswordHash(user.Password, out salt);
                        user.Password = password;
                        user.Salt = salt;
                        user.DateCreated = DateTime.Now;
                        context.USERs.Add(Mapper.MapUserEntityToUSER(user));
                        returnValue = context.SaveChanges() != 0;
                    }  
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return returnValue;
        }

        public bool CheckIfUserNameExist(string userName)
        {
            bool returnValue = false;
            try
            {
                using (var context = new DB_PASTEBOOKEntities())
                {
                    returnValue = context.USERs.Any(user => user.USER_NAME == userName);
                }
            }
            catch (Exception ex)
            {
                return returnValue;
            }

            return returnValue;
        }

        public bool CheckIfEmailAddressExist(string emailAddress)
        {
            bool returnValue = false;
            try
            {
                using (var context = new DB_PASTEBOOKEntities())
                {
                    returnValue = context.USERs.Any(user => user.EMAIL_ADDRESS == emailAddress);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return returnValue;
        }

        public bool CheckLoginAttempt(string email ,string password)
        {
            bool returnValue = false;
            try
            {
                using (var context= new DB_PASTEBOOKEntities())
                {
                    var user = context.USERs.Where(usr => usr.EMAIL_ADDRESS == email).SingleOrDefault();
                    returnValue = pwdManager.IsPasswordMatch(password, user.SALT, user.PASSWORD);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return returnValue;
        }


        public UserEntity RetreiveUserByUserName(string userName)
        {
            UserEntity user = new UserEntity();
            try
            {
                using (var context = new DB_PASTEBOOKEntities())
                {
                    var getUser = context.USERs.Where(usr => usr.USER_NAME == userName).SingleOrDefault();
                    user = Mapper.MapUSERToUserEntity(getUser);
                }
            }
            catch (Exception)
            {
                return null;
            }
            return user;
        }

        public UserEntity RetrieveUserByEmail(string email)
        {
            UserEntity user = new UserEntity();
            try
            {
                using (var context =  new DB_PASTEBOOKEntities())
                {
                    var getUser= context.USERs.Where(usr => usr.EMAIL_ADDRESS == email).SingleOrDefault();
                    user = Mapper.MapUSERToUserEntity(getUser);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return user;
        }
        
        public UserEntity RetrieveUserByID(int userID)
        {
            UserEntity user = new UserEntity();
            try
            {
                using (var context = new DB_PASTEBOOKEntities())
                {
                    var getUser = context.USERs.Where(usr => usr.ID == userID).SingleOrDefault();
                    user = Mapper.MapUSERToUserEntity(getUser);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return user;
        }

    } 

}
