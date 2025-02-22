using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Models
{
    public class Employee : Person
    {
        private string _username;
        private string _password;
        private string _roleType;
        private string _employmentDate;
        private decimal _salary;
        private string _theme;

        public string Username
        {
            get { return _username; }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username)); // This works because Person implements INotifyPropertyChanged
                }
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        public string RoleType
        {
            get { return _roleType; }
            set
            {
                if (_roleType != value)
                {
                    _roleType = value;
                    OnPropertyChanged(nameof(RoleType));
                }
            }
        }

        public string EmploymentDate
        {
            get { return _employmentDate; }
            set
            {
                if (_employmentDate != value)
                {
                    _employmentDate = value;
                    OnPropertyChanged(nameof(EmploymentDate));
                }
            }
        }

        public decimal Salary
        {
            get { return _salary; }
            set
            {
                if (_salary != value)
                {
                    _salary = value;
                    OnPropertyChanged(nameof(Salary));
                }
            }
        }

        public string Theme
        {
            get { return _theme; }
            set
            {
                if (_theme != value)
                {
                    _theme = value;
                    OnPropertyChanged(nameof(Theme));
                }
            }
        }

        public Employee(string firstName, string lastName, string jmb, string address, string email, string dateOfBirth, string phoneNumber, string username, string password, string roleType, string employmentDate, decimal salary, string theme)
        : base(firstName, lastName, jmb, address, email, dateOfBirth, phoneNumber)
        {
            _username = username;
            _password = password;
            _roleType = roleType;
            _employmentDate = employmentDate;
            _salary = salary;
            _theme = theme;
        }

        public Employee()
        {
            _username = string.Empty;
            _password = string.Empty;
            _roleType = string.Empty;
            _employmentDate = string.Empty;
            _salary = 0;
            _theme = string.Empty;
        }

        public override string ToString()
        {
            return base.ToString() + ", " + Username + ", " + RoleType + ", " + Salary + ", " + EmploymentDate;
            // return base.ToString() + $"Korisnicko ime: {Username}\nuloga: {RoleType}\nplata: {Salary}\ndatum zaposlenja: {EmploymentDate}";
        }
    }
}
