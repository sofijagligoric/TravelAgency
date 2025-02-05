using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Models
{
    public class Destination
    {
        public int PostCode { get; set; }
        public string DestinationName { get; set; }
        public string About {  get; set; }
        public int Distance {  get; set; }
        public string LocalLanguage {  get; set; }
        public Country Country { get; set; }

        public Destination() {
            PostCode = 0;
            DestinationName = string.Empty;
            About = string.Empty;
            Distance = 0;
            LocalLanguage = string.Empty;
            Country = new Country();
        }

        public Destination(Destination other)
        {
            PostCode = other.PostCode;
            DestinationName = other.DestinationName;
            About = other.About;
            Distance = other.Distance;
            LocalLanguage = other.LocalLanguage;
            Country = new Country(other.Country);
        }

        public Destination(int postCode, string destinationName, string about, int distance, string localLanguage, Country country)
        {
            PostCode = postCode;
            DestinationName = destinationName;
            About = about;
            Distance = distance;
            LocalLanguage = localLanguage;
            Country = country;
        }

        public override string ToString()
        {
            /*
            return $"Destinacija: {DestinationName},\n" +
                   $"posta: {PostCode},\n" +
                   $"opis: {About},\n" +
                   $"udaljenost: {Distance} km,\n" +
                   $"lokalni jezik: {LocalLanguage},\n" +
                   $"drzava: {Country?.CountryName}";
            */
            return DestinationName+ ", "+ PostCode + ", " + About + ", " + Distance + ", " + LocalLanguage + ", " + Country?.CountryName;

        }

        public override bool Equals(object obj)
        {
            return obj is Destination destination &&
                   PostCode == destination.PostCode &&
                   DestinationName == destination.DestinationName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PostCode, DestinationName);
        }
    }
}
