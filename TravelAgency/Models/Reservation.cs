using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TravelAgency.DataAccess;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace TravelAgency.Models
{
    public class Reservation : INotifyPropertyChanged
    {
        private int _reservationId;
        private decimal _price;
        private Hotel _hotel;
        private Package _package;
        private Customer _customer;
        private byte _allPayed;
        private string _employeeJMB;
        private Employee _employee;

        public int ReservationId
        {
            get => _reservationId;
            set
            {
                if (_reservationId != value)
                {
                    _reservationId = value;
                    OnPropertyChanged(nameof(ReservationId));
                }
            }
        }

        public decimal Price
        {
            get => _price;
            set
            {
                if (_price != value)
                {
                    _price = value;
                    OnPropertyChanged(nameof(Price));
                }
            }
        }

        public Hotel Hotel
        {
            get => _hotel;
            set
            {
                if (_hotel != value)
                {
                    _hotel = value;
                    OnPropertyChanged(nameof(Hotel));
                }
            }
        }

        public Package Package
        {
            get => _package;
            set
            {
                if (_package != value)
                {
                    _package = value;
                    OnPropertyChanged(nameof(Package));
                }
            }
        }

        public Customer Customer
        {
            get => _customer;
            set
            {
                if (_customer != value)
                {
                    _customer = value;
                    OnPropertyChanged(nameof(Customer));
                }
            }
        }

        public byte AllPayed
        {
            get => _allPayed;
            set
            {
                if (_allPayed != value)
                {
                    _allPayed = value;
                    OnPropertyChanged(nameof(AllPayed));
                    OnPropertyChanged(nameof(AllPayedString));
                }
            }
        }

        public string EmployeeJMB
        {
            get => _employeeJMB;
            set
            {
                if (_employeeJMB != value)
                {
                    _employeeJMB = value;
                    OnPropertyChanged(nameof(EmployeeJMB));
                }
            }
        }

        public Employee Employee
        {
            get => _employee;
            set
            {
                if (_employee != value)
                {
                    _employee = value;
                    OnPropertyChanged(nameof(Employee));
                }
            }
        }

        public string AllPayedString
        {
            get
            {
                if (AllPayed == 0)
                    return (string)Application.Current.Resources["No"];
                else return (string)Application.Current.Resources["Yes"]; ;
            }
            set
            {
                if (AllPayedString != value)
                {
                    AllPayedString = value;
                    OnPropertyChanged(nameof(AllPayedString));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Reservation(int reservationId, decimal price, Hotel hotel, Package package, Customer customer, byte allPayed, string employeeJMB)
        {
            ReservationId = reservationId;
            Price = price;
            Hotel = hotel;
            Package = package;
            Customer = customer;
            AllPayed = allPayed;
            EmployeeJMB = employeeJMB;
            Employee = EmployeeDataAccess.GetEmployeeByJMB(employeeJMB);
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
            Employee = new Employee();
        }

        public Reservation(Reservation other)
        {
            this.ReservationId = other.ReservationId;
            this.Employee = other.Employee;
            this.Price = other.Price;
            this.Hotel = other._hotel;
            this.Package = other.Package;
            this.Customer = other.Customer;
            this.AllPayed = other.AllPayed;
            this.EmployeeJMB = other.EmployeeJMB;
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
            return "\n" + (string)Application.Current.Resources["ReservationId"] + " " +  ReservationId + ", \n" +
                 (string)Application.Current.Resources["PackageId"] + " " + Package.PackageId + ", \n" +
                  (string)Application.Current.Resources["CustomerFullName"] + " " + Customer.LastName + " " + Customer.FirstName + ", \n" +
                   (string)Application.Current.Resources["Hotel"] + " " + Hotel.Name + ", \n" +
                    (string)Application.Current.Resources["Price"] + " " + Price + ", \n" + (string)Application.Current.Resources["IsPayed"] + " " + AllPayedString;
        }
    }

}

