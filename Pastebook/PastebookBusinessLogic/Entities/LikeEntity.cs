using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookBusinessLogic
{
    public class LikeEntity
    {
        public int ID { get; set; }
        public int PostID { get; set; }
        public int LikeBy { get; set; }

        public string LikeByName { get; set; }
        public byte[] LikeByPicture { get; set; }
    }
}
