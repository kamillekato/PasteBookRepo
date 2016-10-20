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
                returnValue = dA.CreateUser(MVCMapper.MapUserModelToUserEntity(user));
            }

            return returnValue;
       }

        public bool LoginUserAttempt(string email,string password)
        {
            bool returnValue = false;
            returnValue = dA.LoginUser(email, password);
            return returnValue;
        }

        public bool UserCreatePost(PostViewModel post)
        {
            bool returnValue = false;
            returnValue = dA.CreatePost(MVCMapper.MapPostViewModelToPostEntity(post));
            return returnValue;
        }

        public UserModel GetUserByEmail(string emailAddress)
        { 
            var getUser =  dA.GetUserByEmail(emailAddress);
            UserModel user = new UserModel();
            user = MVCMapper.MapUserEntityToUserModel(getUser);
            return user;
        }
         
        public UserModel GetUserByUserName(string userName)
        {
            UserModel user = new UserModel();
            var getUser = dA.GetUserByUserName(userName);
            user = MVCMapper.MapUserEntityToUserModel(getUser);
            return user;
        }

        public List<PostViewModel> GenerateTimeline(int userID)
        {
            List<PostViewModel> timeline = new List<PostViewModel>();
            var getPostList = dA.GetTimelinePost(userID);
            foreach (var post in getPostList)
            {
                timeline.Add(MVCMapper.MapPostEntityToPostViewModel(post));
            }
            return timeline;
        }


    }
}