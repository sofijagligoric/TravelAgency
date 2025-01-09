using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Models
{
    public class Person
    {
        public string FirstName  { get; set; }
        public string LastName  { get; set; }
        public string Jmb { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string DateOfBirth { get; set; }

        // Konstruktor sa parametrima
        public Person(string firstName , string lastName , string jmb, string address, string email, string dateOfBirth)
        {
            FirstName  = firstName ;
            LastName  = lastName ;
            Jmb = jmb;
            Address = address;
            Email = email;
            DateOfBirth = dateOfBirth;
        }


        public Person() {
            FirstName = string.Empty;
            LastName = string.Empty;
            Jmb = string.Empty;
            Address = string.Empty;
            Email = string.Empty;
            DateOfBirth = string.Empty;
        }

        public override string ToString()
        {
            return FirstName + " " + LastName + ", " + Jmb + ", " + Address + ", " + Email + ", " + DateOfBirth;
            // return $"Ime: {FirstName} {LastName}\nJMB: {Jmb}\nadresa: {Address}\ne-mail: {Email}\ndatum rodjenja: {DateOfBirth}";
        }

       /*
        *  public override int GetHashCode()
        {
            return -1255590651 + Jmb.GetHashCode();
        }*/

        public override bool Equals(object obj)
        {
            return obj is Person person &&
                   Jmb == person.Jmb;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Jmb);
        }
    }
}
