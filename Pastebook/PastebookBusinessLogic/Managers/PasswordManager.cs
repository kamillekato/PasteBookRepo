using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PastebookBusinessLogic
{
    public class PasswordManager
    {
        
        public string GeneratePasswordHash(string plainTextPassword,out string salt)
        {
            salt = GetSaltString();
            string finalString = plainTextPassword + salt;
            return GetPasswordHashAndSalt(finalString);
        }

        public bool IsPasswordMatch(string password,string salt,string hash)
        {
            string saltAndPassword = password + salt;
            return hash == GetPasswordHashAndSalt(saltAndPassword);
        }

        private const int SaltSize = 24;         
        string GetSaltString()
        {
            RNGCryptoServiceProvider CryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] saltBytes = new byte[SaltSize];
            CryptoServiceProvider.GetNonZeroBytes(saltBytes);
            string saltString = ConvertByteToString(saltBytes);
            return saltString;
        }

        byte[] ConvertStringToByte(string password)
        {
            Encoding Encoder = Encoding.ASCII;
            byte[] toByte = Encoder.GetBytes(password);
            return toByte;
        }

        string ConvertByteToString(byte[] password)
        {
            Encoding Encoder = Encoding.ASCII;
            string toString = Encoder.GetString(password);
            return toString;
        }

        string GetPasswordHashAndSalt(string message)
        {
            SHA256 sha = new SHA256CryptoServiceProvider();
            byte[] dataBytes = ConvertStringToByte(message);
            byte[] resultBytes = sha.ComputeHash(dataBytes);
            return ConvertByteToString(resultBytes);
        }
    }
}
