using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Models
{
    public class Employee : Person
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string RoleType { get; set; }
        public string EmploymentDate { get; set; }
        public decimal Salary { get; set; }
        public string Theme { get; set; }

        public Employee(string firstName, string lastName, string jmb, string address, string email, string dateOfBirth, string username, string password, string roleType, string employmentDate, decimal salary, string theme)
        : base(firstName, lastName, jmb, address, email, dateOfBirth)
        {
            Username = username;
            Password = password;
            RoleType = roleType;
            EmploymentDate = employmentDate;
            Salary = salary;
            Theme = theme;
        }

        public Employee()
        {
            Username = string.Empty;
            Password = string.Empty;
            RoleType = string.Empty;
            EmploymentDate = string.Empty;
            Salary = 0;
            Theme = string.Empty;
        }

        public override string ToString()
        {
            return base.ToString() + ", " + Username + ", " + RoleType + ", " + Salary + ", " + EmploymentDate;
            // return base.ToString() + $"Korisnicko ime: {Username}\nuloga: {RoleType}\nplata: {Salary}\ndatum zaposlenja: {EmploymentDate}";
        }
    }
}
