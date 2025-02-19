using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Models
{
    public class TotalReservationPayment : INotifyPropertyChanged
    {
        private string _customerName;
        private int _reservationId;
        private decimal _totalPrice;
        private decimal _payed;
        private decimal _owed;
        private string _fullyPayed;

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

        public decimal TotalPrice
        {
            get => _totalPrice;
            set
            {
                if (_totalPrice != value)
                {
                    _totalPrice = value;
                    OnPropertyChanged(nameof(TotalPrice));
                }
            }
        }

        public decimal Payed
        {
            get => _payed;
            set
            {
                if (_payed != value)
                {
                    _payed = value;
                    OnPropertyChanged(nameof(Payed));
                }
            }
        }

        public decimal Owed
        {
            get => _owed;
            set
            {
                if (_owed != value)
                {
                    _owed = value;
                    OnPropertyChanged(nameof(Owed));
                }
            }
        }

        public string FullyPayed
        {
            get => _fullyPayed;
            set
            {
                if (_fullyPayed != value)
                {
                    _fullyPayed = value;
                    OnPropertyChanged(nameof(FullyPayed));
                }
            }
        }

        public TotalReservationPayment(TotalReservationPayment other)
        {
            CustomerName = other.CustomerName;
            ReservationId = other.ReservationId;
            TotalPrice = other.TotalPrice;
            Payed = other.Payed;
            Owed = other.Owed;
            FullyPayed = other.FullyPayed;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TotalReservationPayment(string customerName, int reservationId, decimal totalPrice, decimal payed, decimal owed, string fullyPayed)
        {
            CustomerName = customerName;
            ReservationId = reservationId;
            TotalPrice = totalPrice;
            Payed = payed;
            Owed = owed;
            FullyPayed = fullyPayed;
        }

        public TotalReservationPayment()
        {
            CustomerName = string.Empty;
            ReservationId = 0;
            TotalPrice = 0;
            Payed = 0;
            Owed = 0;
            FullyPayed = string.Empty ;
        }

        public override bool Equals(object obj)
        {
            return obj is TotalReservationPayment payment &&
                   CustomerName == payment.CustomerName &&
                   ReservationId == payment.ReservationId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CustomerName, ReservationId);
        }
    }
}
