using PastebookDataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookBusinessLogic 
{
    public class DataAccess
    {
        //    UserManager userManager = new UserManager();  
        //    public bool CreateUser(UserEntity user)
        //    {
        //        bool returnValue = false;
        //        if (!CheckIfUserNameExist(user.UserName) && !CheckIfEmailAddressExist(user.EmailAddress))
        //        {
        //            returnValue = userManager.AddUser(user);
        //        }
        //        return returnValue;
        //    } 

        //    public bool CheckIfUserNameExist(string userName)
        //    {
        //        bool returnValue = false;
        //        returnValue = userManager.CheckIfUserNameExist(userName); 
        //        return returnValue;
        //    }

        //    public bool CheckIfEmailAddressExist(string emailAddress)
        //    {
        //        bool returnValue = false;
        //        returnValue = userManager.CheckIfEmailAddressExist(emailAddress); 
        //        return returnValue;
        //    }

        //    public bool LoginUser(string email,string password)
        //    {
        //        bool returnValue = false;
        //        returnValue = userManager.CheckLoginAttempt(email, password);
        //        return returnValue;
        //    }

        //    public UserEntity GetUserByEmail(string emailAddress)
        //    {
        //        UserEntity user = userManager.RetrieveUserByEmail(emailAddress);
        //        return user;
        //    }

        //    public UserEntity GetUserByUserName(string userName)
        //    {
        //        UserEntity user = new UserEntity();
        //        user = userManager.RetreiveUserByUserName(userName);
        //        return user;
        //    }





        //    CountryManager countryManager = new CountryManager();
        //    public List<CountryEntity> GetListOfCountry()
        //    {
        //        List<CountryEntity> countryList = new List<CountryEntity>();
        //        countryList = countryManager.RetrieveCountryList();
        //        return countryList;
        //    }

        //    PostManager postManager = new PostManager();
        //    public bool CreatePost(PostEntity post)
        //    {
        //        bool returnValue = false;
        //        returnValue = postManager.CreatePost(post);
        //        return returnValue;
        //    }

        //    public List<PostEntity> GetTimelinePost(int userID)
        //    {
        //        List<PostEntity> postList = new List<PostEntity>();
        //        postList = postManager.GetUserRelatedPost(userID);
        //        return postList;
        //    }



        //    LikeManager<LIKE> likeManager = new LikeManager<LIKE>();
        //    public bool CreateOrDeleteLike(LikeEntity like)
        //    {
        //        bool returnValue = false;
        //        returnValue = likeManager.CreateDeleteLike(new LIKE() {ID = like.ID ,POST_ID = like.PostID,LIKE_BY =like.LikeBy });
        //        return returnValue;
        //    }

        //    public List<LikeEntity> RetrieveLikes(int postID)
        //    {
        //        List<LikeEntity> likeList = new List<LikeEntity>();
        //        likeList = likeManager.RetrieveLikes(postID);
        //        return likeList;
        //    }
    }
}
