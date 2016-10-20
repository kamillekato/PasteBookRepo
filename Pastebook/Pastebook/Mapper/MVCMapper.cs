using Pastebook.Models;
using PastebookBusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pastebook
{
    public class MVCMapper
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

        public static UserModel MapUserEntityToUserModel(UserEntity user)
        {
            return new UserModel()
            {
                ID = user.ID,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProfilePic= user.ProfilePic,
                Birthday = user.BirthDay,
                AboutMe = user.AboutMe,
                CountryID = user.CountryID,
                Gender = user.Gender,
                MobileNo = user.MobileNo,
                EmailAddress = user.EmailAddress
            };
        }

        public static PostEntity MapPostViewModelToPostEntity(PostViewModel post)
        {
            return new PostEntity() {
                Content = post.Content,
                Profile_Owner_ID = post.Profile_Owner_ID,
                Poster_ID = post.Poster_ID
            }; 
        }
        
        public static PostViewModel MapPostEntityToPostViewModel(PostEntity post)
        {
            return new PostViewModel()
            {
                ID = post.ID,
                Content = post.Content,
                DateCreated = post.CreatedDate,
                NumberOfLikes = post.NumberOfLikes,
                OwnerName = post.OwnerName,
                PosterName = post.PosterName,
                Poster_ID = post.Poster_ID,
                Profile_Owner_ID = post.Profile_Owner_ID,
                NumberOfComments = post.NumberOfComments
            };
        }

         
    }
}