using BCrypt.Net;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace WebCrawler.Core.Helpers
{
    public static class PasswordHelper
    {

        public static bool ValidatePassword(string salt, string hashedPassword, string passwordToValidate)
        {
            var saltedPass = SaltPassword(salt, passwordToValidate);
            return BCrypt.Net.BCrypt.Verify(saltedPass, hashedPassword);
        }


        public static string SaltPassword(string salt, string password)
        {
            return $"{salt}{password}";
        }


        public static string HashPassword(string salt, string password)
        {
            var factor = 6;
            var saltedPass = SaltPassword(salt, password);
            return BCrypt.Net.BCrypt.HashPassword(saltedPass, factor, SaltRevision.Revision2A);
        }


        public static string GenerateSalt(int length)
        {
            byte[] randomArray = new byte[length];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomArray);
            var randomString = Convert.ToBase64String(randomArray);
            return randomString;

        }

    }
}
