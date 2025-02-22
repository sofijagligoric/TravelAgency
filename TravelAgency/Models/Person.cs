using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.DataAccess;

namespace TravelAgency.Models
{
    public class Person : INotifyPropertyChanged
    {
        private string _firstName;
        private string _lastName;
        private string _fullName;
        private string _jmb;
        private string _address;
        private string _email;
        private string _dateOfBirth;
        private string _phoneNumber;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    OnPropertyChanged(nameof(FirstName));
                    FullName = _firstName + " " + LastName; 
                }
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged(nameof(LastName));
                    FullName = FirstName + " " + _lastName;
                }
            }
        }

        public string FullName
        {
            get { return _fullName; }
            private set
            {
                if (_fullName != value)
                {
                    _fullName = value;
                    OnPropertyChanged(nameof(FullName));
                }
            }
        }

        public string Jmb
        {
            get { return _jmb; }
            set
            {
                if (_jmb != value)
                {
                    _jmb = value;
                    OnPropertyChanged(nameof(Jmb));
                }
            }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                if (_address != value)
                {
                    _address = value;
                    OnPropertyChanged(nameof(Address));
                }
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        public string DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                if (_dateOfBirth != value)
                {
                    _dateOfBirth = value;
                    OnPropertyChanged(nameof(DateOfBirth));
                }
            }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    OnPropertyChanged(nameof(PhoneNumber));
                }
            }
        }

        
        public Person(string firstName, string lastName, string jmb, string address, string email, string dateOfBirth, string phoneNumber)
        {
            _firstName = firstName;
            _lastName = lastName;
            FullName = firstName + " " + lastName;
            _jmb = jmb;
            _address = address;
            _email = email;
            _dateOfBirth = dateOfBirth;
            _phoneNumber = phoneNumber;
        }

       
        public Person()
        {
            _firstName = string.Empty;
            _lastName = string.Empty;
            FullName = string.Empty;
            _jmb = string.Empty;
            _address = string.Empty;
            _email = string.Empty;
            _dateOfBirth = string.Empty;
            _phoneNumber = string.Empty;
        }

        public Person(Person other)
        {
            _firstName = other.FirstName;
            _lastName = other.LastName;
            FullName = other.FullName;
            _jmb = other.Jmb;
            _address = other.Address;
            _email = other.Email;
            _dateOfBirth = other.DateOfBirth;
            _phoneNumber = other.PhoneNumber;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return FirstName + " " + LastName + ", " + Jmb + ", " + Address + ", " + PhoneNumber + ", " + Email + ", " + DateOfBirth;
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
