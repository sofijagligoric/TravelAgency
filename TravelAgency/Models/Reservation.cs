using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public decimal Price { get; set; }
        public Hotel Hotel { get; set; }
        public Package Package { get; set; }
        public Customer Customer { get; set; }
        public byte AllPayed { get; set; }
        public string EmployeeJMB { get; set; }

        public Reservation(int reservationId, decimal price, Hotel hotel, Package package, Customer customer, byte allPayed, string employeeJMB)
        {
            ReservationId = reservationId;
            Price = price;
            Hotel = hotel;
            Package = package;
            Customer = customer;
            AllPayed = allPayed;
            EmployeeJMB = employeeJMB;
        }

        public Reservation()
        {
            ReservationId = 0;
            Price = 0;
            AllPayed = 0;
            Hotel = new Hotel();
            Package = new Package();
            Customer = new Customer();
            EmployeeJMB = string.Empty;
        }

        public override bool Equals(object obj)
        {
            return obj is Reservation reservation &&
                   ReservationId == reservation.ReservationId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ReservationId);
        }

        public override string ToString()
        {
            return ReservationId + ", " + Package.PackageId + ", " + Customer.LastName + " " + Customer.FirstName + ", " + Hotel.Name + ", " + Price + ", " + (AllPayed != 0 ? "payed" : "not payed");
        }
    }

}

