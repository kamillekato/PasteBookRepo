using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pastebook.Models
{
    public class UserModel
    { 
        public int ID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }  
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        public int? CountryID { get; set; }
        public string MobileNo { get; set; }
        [Required]
        public string Gender { get; set; }
        [DataType(DataType.ImageUrl)]
        public byte[] ProfilePic { get; set; }
        public string AboutMe { get; set; }
        public DateTime DateCreated { get; set; }
        [Required]
        public string EmailAddress { get; set; }

         
    }
}