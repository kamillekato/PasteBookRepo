using PastebookDataModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookBusinessLogic
{
    public class UserManager
    {
        public bool RegisterUser(USER user)
        {
            bool returnValue = false;
            try
            {
                using (var context = new DB_PASTEBOOKEntities())
                {
                    if (CheckIfUserNameExist(user.USER_NAME))
                    {
                        return false;
                    }
                    else if (CheckIfEmailAddressExist(user.EMAIL_ADDRESS))
                    {
                        return false;
                    }
                    else
                    {
                        context.USERs.Add(user);
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

        

    } 

}
