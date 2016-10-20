using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pastebook.Models
{
    public class SignUpViewModel
    {
        public UserModel User { get; set; }
        public List<CountryModel> CountryList = new List<CountryModel>();
         
    }
}