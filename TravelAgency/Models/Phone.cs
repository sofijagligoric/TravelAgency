using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Models
{
    public class Phone
    {
        public string PhoneNumber { get; set; }
        public Person Person { get; set; }

        public Phone(string phoneNumber, Person person)
        {
            PhoneNumber = phoneNumber;
            Person = person;
        }

        public Phone()
        {
            PhoneNumber = string.Empty;
            Person = new Person();
        }

        public override bool Equals(object obj)
        {
            return obj is Phone phone &&
                   PhoneNumber == phone.PhoneNumber;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PhoneNumber);
        }

        public override string ToString()
        {
            return PhoneNumber;
        }
    }
}
