using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Models
{
    public class PackageOffersHotel
    {
        public Package Package { get; set; }
        public Hotel Hotel { get; set; }

        public PackageOffersHotel() {
            Package = new Package();
            Hotel = new Hotel();
        }

        public PackageOffersHotel(Package package, Hotel hotel)
        {
            Package = package;
            Hotel = hotel;
        }

        public override bool Equals(object obj)
        {
            return obj is PackageOffersHotel hotel &&
                   EqualityComparer<Package>.Default.Equals(Package, hotel.Package) &&
                   EqualityComparer<Hotel>.Default.Equals(Hotel, hotel.Hotel);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Package, Hotel);
        }

        public override string ToString()
        {
            return  Package.PackageId + " - " + Hotel.Name;
        }
    }
}
