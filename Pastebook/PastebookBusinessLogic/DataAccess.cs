using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookBusinessLogic 
{
    public class DataAccess
    { 
        UserManager userManager = new UserManager();  
        public bool CreateUser(UserEntity user)
        {
            bool returnValue = false;
            if (!CheckIfUserNameExist(user.UserName) && !CheckIfEmailAddressExist(user.EmailAddress))
            {
                 
                returnValue = userManager.AddUser(user);
            }
            return returnValue;
        } 
        
        public bool CheckIfUserNameExist(string userName)
        {
            bool returnValue = false;
            returnValue = userManager.CheckIfUserNameExist(userName); 
            return returnValue;
        }

        public bool CheckIfEmailAddressExist(string emailAddress)
        {
            bool returnValue = false;
            returnValue = userManager.CheckIfEmailAddressExist(emailAddress); 
            return returnValue;
        }

        public bool LoginUser(string email,string password)
        {
            bool returnValue = false;
            returnValue = userManager.CheckLoginAttempt(email, password);
            return returnValue;
        }


        CountryManager countryManager = new CountryManager();
        public List<CountryEntity> GetListOfCountry()
        {
            List<CountryEntity> countryList = new List<CountryEntity>();
            countryList = countryManager.RetrieveCountryList();
            return countryList;
        }




    }
}
