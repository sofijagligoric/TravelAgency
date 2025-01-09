using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Models
{
    public class Package
    {
        public int PackageId { get; set; }
        public String StartDate {  get; set; }
        public String EndDate { get; set; }
        public decimal Price {  get; set; }
        public String About {  get; set; }
        public Destination Destination { get; set; }

        public Package(int packageId, string startDate, string endDate, decimal price, string about, Destination destination)
        {
            PackageId = packageId;
            StartDate = startDate;
            EndDate = endDate;
            Price = price;
            About = about;
            Destination = destination;
        }

        public Package()
        {
            PackageId = 0;
            StartDate = string.Empty;
            EndDate = string.Empty;
            Price = 0;
            About = string.Empty;
            Destination = new Destination();
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PackageId);
        }

        public override bool Equals(object obj)
        {
            return obj is Package package &&
                   PackageId == package.PackageId;
        }

        public override string ToString()
        {
            return PackageId + ", " + StartDate + ", " + EndDate + ", " + Price + ", " + Destination.DestinationName + ", " + About;
            /*
            return "Aranžman ID: " + PackageId +
                   "\nDatum polaska: " + StartDate +
                   ", Datum povratka: " + EndDate +
                   "\nCijena: " + Price + " KM" +
                   "\nDestinacija: " + Destination.DestinationName +
                   "\nOpis: " + About;
            */
        }

    }
}
