using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PastebookDataLibrary;

namespace PastebookBusinessLogic
{
    public class CountryManager2
    {

        public List<CountryEntity> RetrieveCountryList()
        {
            List<CountryEntity> countryList = new List<CountryEntity>();
            try
            {
                using (var context = new DB_PASTEBOOKEntities())
                {
                    countryList = context.REF_COUNTRY.Select(cntry => new CountryEntity() { ID =cntry.ID , Country = cntry.Country}).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return countryList;
        }

    }
}
