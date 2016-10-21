using PastebookDataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookDataAccess
{
    public class CountryRepository  
    {
        public List<REF_COUNTRY> GetAll()
        {
            List<REF_COUNTRY> countryList = null;
            try
            {
                using (var context = new DB_PASTEBOOKEntities())
                {
                    countryList = context.REF_COUNTRY.ToList();
                }
            }
            catch
            {
                return countryList;
            }
            return countryList;
        }

        public string Get(int id)
        {
            string country = string.Empty;
            try
            {
                using (var context = new DB_PASTEBOOKEntities() )
                {
                    country = context.REF_COUNTRY.Where(cntry => cntry.ID == id).Select(cntry=>cntry.Country).FirstOrDefault();
                }
            }
            catch 
            {
                return country;
            }
            return country;
        }

    }
}
