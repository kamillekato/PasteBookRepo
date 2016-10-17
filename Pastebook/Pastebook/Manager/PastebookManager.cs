using Pastebook.Models;
using PastebookBusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pastebook.Manager
{
    public class PastebookManager
    {
        public List<string> ErrorList { get; set; }

        public PastebookManager()
        {
            ErrorList = new List<string>();
        }

        DataAccess dA = new DataAccess();
        public List<CountryModel> GetCountryList()
        {
            List<CountryModel> listOfCountry = new List<CountryModel>();
            listOfCountry = dA.GetListOfCountry().Select(country => new CountryModel() { ID = country.ID, Country = country.Country }).ToList() ;
            return listOfCountry;
        }

        public bool ValidateUser(UserModel user)
        { 
            if (dA.CheckIfUserNameExist(user.UserName))
            {
                ErrorList.Add("Username already exist");
            }
            if (dA.CheckIfEmailAddressExist(user.EmailAddress))
            {
                ErrorList.Add("Email address already exist");
            }


            return ErrorList.Count() == 0;
        } 
       
       public bool SignUpUser(UserModel user)
       {
            ErrorList = new List<string>();
            bool returnValue = false;
            if (ValidateUser(user))
            {
                returnValue = dA.CreateUser(Mapper.MapUserModelToUserEntity(user));
            }

            return returnValue;
       }

        public bool LoginUserAttempt(string email,string password)
        {
            bool returnValue = false;
            returnValue = dA.LoginUser(email, password);
            return returnValue;
        }
    }
}