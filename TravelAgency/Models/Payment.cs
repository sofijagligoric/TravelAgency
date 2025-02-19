using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Models
{
    public class Payment : INotifyPropertyChanged
    {
        private string _customerName;
        private int _reservationId;
        private int _paymentId;
        private decimal _amount;
        private decimal _payedToHotel;
        private decimal _payedToAgency;
        private string _phoneNumber;
        private string _date;
        private string _customerJMB;

        public string CustomerName
        {
            get => _customerName;
            set
            {
                if (_customerName != value)
                {
                    _customerName = value;
                    OnPropertyChanged(nameof(CustomerName));
                }
            }
        }

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

        public int PaymentId
        {
            get => _paymentId;
            set
            {
                if (_paymentId != value)
                {
                    _paymentId = value;
                    OnPropertyChanged(nameof(PaymentId));
                }
            }
        }

        public decimal Amount
        {
            get => _amount;
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                    OnPropertyChanged(nameof(Amount));
                }
            }
        }

        public decimal PayedToHotel
        {
            get => _payedToHotel;
            set
            {
                if (_payedToHotel != value)
                {
                    _payedToHotel = value;
                    OnPropertyChanged(nameof(PayedToHotel));
                }
            }
        }

        public decimal PayedToAgency
        {
            get => _payedToAgency;
            set
            {
                if (_payedToAgency != value)
                {
                    _payedToAgency = value;
                    OnPropertyChanged(nameof(PayedToAgency));
                }
            }
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    OnPropertyChanged(nameof(PhoneNumber));
                }
            }
        }

        public string Date
        {
            get => _date;
            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }

        public string CustomerJMB
        {
            get => _customerJMB;
            set
            {
                if (_customerJMB != value)
                {
                    _customerJMB = value;
                    OnPropertyChanged(nameof(CustomerJMB));
                }
            }
        }

        public Payment(string customerName, int reservationId, int paymentId, decimal amount, decimal payedToHotel, decimal payedToAgency, string phoneNumber, string date, string customerJMB)
        {
            CustomerName = customerName;
            ReservationId = reservationId;
            PaymentId = paymentId;
            Amount = amount;
            PayedToHotel = payedToHotel;
            PayedToAgency = payedToAgency;
            PhoneNumber = phoneNumber;
            Date = date;
            CustomerJMB = customerJMB;
        }

        public Payment(Payment other)
        {
            PaymentId = other.PaymentId;
            Amount = other.Amount;
            PayedToHotel = other.PayedToHotel;
            PayedToAgency = other.PayedToAgency;
            ReservationId = other.ReservationId;
            CustomerName = other.CustomerName;
            PhoneNumber = other.PhoneNumber;
            Date = other.Date;
            CustomerJMB = other.CustomerJMB;
        }

        public Payment()
        {
            PaymentId = 0;
            Amount = 0;
            PayedToHotel = 0;
            PayedToAgency = 0;
            ReservationId = 0;
            CustomerName = string.Empty;
            PhoneNumber = string.Empty;
            Date = string.Empty;
            CustomerJMB = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override bool Equals(object obj)
        {
            return obj is Payment payment &&
                   PaymentId == payment.PaymentId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PaymentId);
        }
    }
}
