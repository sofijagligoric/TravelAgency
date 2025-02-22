using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
