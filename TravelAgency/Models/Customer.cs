using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Models
{
    public class Customer : Person
    {
        
        public Customer() : base() { }

        public Customer(string firstName, string lastName, string jmb, string address, string email, string dateOfBirth, string phoneNumber)
            : base(firstName, lastName, jmb, address, email, dateOfBirth, phoneNumber) { }


        public Customer(Customer other)
    : base(other.FirstName, other.LastName, other.Jmb, other.Address, other.Email, other.DateOfBirth, other.PhoneNumber)
        {
            
        }

        public override string ToString()
        {
            return base.ToString();
        } 
    }
}
