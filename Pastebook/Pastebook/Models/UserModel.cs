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
        [Display(Name ="Username")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }  
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name ="Last name")]
        public string LastName { get; set; }
        [Required] 
        public DateTime Birthday { get; set; }
        [Display(Name ="Country")]
        public int? CountryID { get; set; }
        [Display(Name ="Mobile no.")]
        public string MobileNo { get; set; }
        [Required]
        public string Gender { get; set; }
        [DataType(DataType.ImageUrl)]
        [Display(Name ="Picture")]
        public byte[] ProfilePic { get; set; } 
        public string AboutMe { get; set; }
        public DateTime DateCreated { get; set; }
        [Required]
        [Display(Name = "Email address")]
        public string EmailAddress { get; set; }

         
    }
}