using PastebookDataLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pastebook.Models
{
    public class SettingsViewModel
    {
        public USER User; 
        public string ConfirmPassword;
        
        public SettingsViewModel()
        {
            User = new USER();
        } 
    }
}