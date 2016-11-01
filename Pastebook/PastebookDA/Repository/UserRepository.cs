using PastebookDataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace PastebookDataAccess
{
    public class UserRepository : BaseRepository<USER>, IRepository<USER> { 
            

        public bool CheckIfUserNameExist(string userName)
        {
            bool returnValue = false;
            
                using (var context = new DB_PASTEBOOKEntities())
                {
                    returnValue = context.USERs.Any(user => user.USER_NAME == userName);
                }
             
            return returnValue;
        }

        public bool CheckIfEmailExist(string email)
        {
            bool returnValue = false;
            
                using (var context =new DB_PASTEBOOKEntities())
                {
                    returnValue = context.USERs.Any(user => user.EMAIL_ADDRESS == email);
                }
          
            return returnValue;
        }

        public override USER Get(Expression<Func<USER, bool>> where )
        {
            USER user = new USER();
           
                using ( var context =new DB_PASTEBOOKEntities())
                {
                    user = context.USERs.Include("REF_COUNTRY").Where(where).FirstOrDefault();
                }
             
            return user;
        }

       

       
    }
}
