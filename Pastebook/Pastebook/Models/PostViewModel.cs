using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pastebook.Models
{
    public class PostViewModel
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public int Profile_Owner_ID { get; set; }
        public int Poster_ID { get; set; }
        [DisplayFormat(DataFormatString ="{0:MMMM/dd/yyyy hh:mm t}")]
        public DateTime DateCreated { get; set; }

        public string PosterName { get; set; }
        public byte[] PosterPicture { get; set; }
        public string OwnerName { get; set; }
        public int? NumberOfLikes { get; set; } 
        public int? NumberOfComments { get; set; }
        public int IsLike { get; set; }
    }
}