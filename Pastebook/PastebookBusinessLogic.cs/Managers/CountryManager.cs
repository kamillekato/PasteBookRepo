using PastebookDataAccess;
using PastebookDataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookBusinessLogic
{
    public class CountryManager
    {
        CountryRepository countryRepo;
        public CountryManager()
        {
            countryRepo = new CountryRepository();
        }

        public List<REF_COUNTRY> GetCountryList()
        {
            List<REF_COUNTRY> countries = null;
            countries = countryRepo.GetAll();
            return countries;
        }

        public string GetCountryByID(int id)
        {
            string country = null;
            country = countryRepo.Get(id);
            return country;
        }

    }  
}
