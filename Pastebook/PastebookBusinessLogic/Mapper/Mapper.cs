﻿
using PastebookDataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookBusinessLogic
{
    public class Mapper
    {

        public static USER MapUserEntityToUSER(UserEntity user)
        {
            return new USER() {
                ID = user.ID,
                USER_NAME = user.UserName,
                PASSWORD = user.Password,
                SALT = user.Salt,
                FIRST_NAME = user.FirstName,
                LAST_NAME = user.LastName,
                BIRTHDAY = user.BirthDay,
                COUNTRY_ID = user.CountryID,
                MOBILE_NO = user.MobileNo,
                GENDER = user.Gender,
                PROFILE_PIC = user.ProfilePic,
                DATE_CREATED = user.DateCreated,
                ABOUT_ME = user.AboutMe,
                EMAIL_ADDRESS = user.EmailAddress
            };
        }

        public static UserEntity MapUSERToUserEntity(USER user)
        {
            return new UserEntity() {
                ID = user.ID,
                UserName = user.USER_NAME,
                Password = user.PASSWORD,
                Salt = user.SALT,
                FirstName = user.FIRST_NAME,
                LastName = user.LAST_NAME,
                BirthDay = user.BIRTHDAY,
                CountryID = user.COUNTRY_ID,
                MobileNo = user.MOBILE_NO,
                Gender = user.GENDER,
                ProfilePic = user.PROFILE_PIC,
                DateCreated = user.DATE_CREATED,
                AboutMe = user.ABOUT_ME,
                EmailAddress = user.EMAIL_ADDRESS
            };
        }

        public CountryEntity MapREF_COUNTRYToCountryEntity(REF_COUNTRY country)
        {
            return new CountryEntity()
            {
                ID = country.ID,
                Country = country.Country, 
            };
        }
    }
}
