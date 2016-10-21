//using PastebookDataLibrary;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace PastebookBusinessLogic
//{

    
//    public class LikeManager2<T> where T : class
//    {

//        public bool CreateDeleteLike(T like)
//        {
//            bool returnValue = false;
//            try
//            {
//                using (var context = new DB_PASTEBOOKEntities())
//                {
//                    context.Entry(like).State = System.Data.Entity.EntityState.Added;
//                    returnValue = context.SaveChanges() != 0;
//                }
//            }
//            catch
//            {
//                return false;
//            }
//            return returnValue;
//        }
//        public bool CreateDeeteLike(LikeEntity like)
//        {
//            bool returnValue = false;
//            try
//            {
//                using (var context = new DB_PASTEBOOKEntities())
//                {
//                    var cntr = context.LIKEs.Where(lke => lke.POST_ID == like.PostID).Any(lke=>lke.LIKE_BY ==like.LikeBy);
//                    if (cntr)
//                    {
//                        context.LIKEs.Remove(context.LIKEs.Where(lke => lke.POST_ID == like.PostID && lke.LIKE_BY == like.LikeBy).SingleOrDefault()); 
//                    }else
//                    {
//                        context.LIKEs.Add(Mapper.MapLikeEntityToLIKE(like));
//                    }                        
//                        returnValue = context.SaveChanges() != 0; 
//                }
//            }
//            catch
//            {
//                return false;
//            }
//            return returnValue;
//        }

//        public List<LikeEntity> RetrieveLikes( int postID)
//        {
//            List<LikeEntity> likeList = new List<LikeEntity>();
//            try
//            {
//                using (var context = new DB_PASTEBOOKEntities())
//                {
//                    var getListOfLikes = (from lke in context.LIKEs
//                                          join user in context.USERs
//                                          on  lke.LIKE_BY equals user.ID
//                                          where lke.POST_ID == postID
//                                          select new {
//                                          ID = lke.ID
//                                          , PostID = lke.POST_ID
//                                          ,LikeBy = lke.LIKE_BY
//                                          ,LikeByName = user.FIRST_NAME + " " + user.LAST_NAME
//                                          ,LikeByPicture = user.PROFILE_PIC
//                                          }).ToList();
//                    foreach (var userLike in getListOfLikes)
//                    {
//                        likeList.Add(new LikeEntity() {
//                            ID=userLike.ID,
//                            PostID=userLike.PostID,
//                            LikeBy = userLike.LikeBy,
//                            LikeByName = userLike.LikeByName,
//                            LikeByPicture = userLike.LikeByPicture
//                        });
//                    }
//                    return likeList;
//                }
//            }
//            catch 
//            {
//                return null;
//            }
//        }
//    }
//}
