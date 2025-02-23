using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Util
{
    public class General
    {

        public static string DateFromBase(string input)
        {
            DateTime date = DateTime.ParseExact(input, "dd.MM.yyyy. HH:mm:ss", null);
            string extractedDate = date.ToString("dd.MM.yyyy.");
            return extractedDate;
        }

        public static string DateForBase(string input)
        {
            DateTime parsedDate = DateTime.ParseExact(input, "dd.MM.yyyy.", null);
            string formattedDate = parsedDate.ToString("yyyy-MM-dd");
            return formattedDate;
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
