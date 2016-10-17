using Pastebook.Models;
using PastebookBusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pastebook
{
    public class Mapper
    {
        
        public static UserEntity MapUserModelToUserEntity(UserModel user)
        {
            return new UserEntity()
            {
                ID = user.ID,
                UserName= user.UserName,
                Password = user.Password, 
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDay = user.Birthday,
                CountryID = user.CountryID,
                MobileNo = user.MobileNo,
                Gender = user.Gender,
                ProfilePic = user.ProfilePic,
                DateCreated = user.DateCreated,
                AboutMe = user.AboutMe,
                EmailAddress = user.EmailAddress
            };
        }

        
         

         
    }
}