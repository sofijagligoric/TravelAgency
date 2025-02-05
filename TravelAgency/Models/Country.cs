using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Models
{
    public class Country
    {
        public string CountryName { get; set; }

        public Country(string countryName)
        {
            CountryName = countryName;
        }

        public Country()
        {
            CountryName = string.Empty;
        }

        public Country(Country other)
        {
            CountryName = other.CountryName;
        }

        public override bool Equals(object obj)
        {
            return obj is Country country &&
                   CountryName == country.CountryName;
        }

        /*
        public override int GetHashCode()
        {
            return -1255590651 + CountryName.GetHashCode();
        }
        */

        public override string ToString()
        {
            return CountryName;
           // return $"Naziv drzave: {CountryName}";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CountryName);
        }
    }
}
