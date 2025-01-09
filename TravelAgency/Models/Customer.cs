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

        public Customer(string firstName, string lastName, string jmb, string address, string email, string dateOfBirth)
            : base(firstName, lastName, jmb, address, email, dateOfBirth) { }


        public override string ToString()
        {
            return base.ToString();
        } 
    }
}
