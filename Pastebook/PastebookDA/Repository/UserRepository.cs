using PastebookDataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookDataAccess
{
    public class UserRepository : BaseRepository<USER>, IRepository<USER> { 
            

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
            catch 
            {
                return returnValue;
            }
            return returnValue;
        }

        public bool CheckIfEmailExist(string email)
        {
            bool returnValue = false;
            try
            {
                using (var context =new DB_PASTEBOOKEntities())
                {
                    returnValue = context.USERs.Any(user => user.EMAIL_ADDRESS == email);
                }
            }
            catch
            {

                throw;
            }
            return returnValue;
        }

        
    }
}
