using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pastebook.Models
{
    public class LikeViewModel
    {
        public int ID { get; set; }
        public int PostID { get; set; }
        public int LikeBy { get; set; }

        public string LikeByName { get; set; }
        public byte[] LikeByPicture { get; set; }

    }
}