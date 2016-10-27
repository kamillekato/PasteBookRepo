using PastebookDataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pastebook.Models
{
    public class ProfileViewModel
    {
        public USER User;
        public string Status;

        public ProfileViewModel()
        {
            User = new USER();

        }
    }
}