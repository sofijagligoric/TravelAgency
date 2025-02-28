using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Models;

namespace TravelAgency.Models
{
    public class PackageOffersHotel : INotifyPropertyChanged
    {
        private int _packageId;
        private int _hotelId;

        public int Package
        {
            get => _packageId;
            set
            {
                if (_packageId != value)
                {
                    _packageId = value;
                    OnPropertyChanged(nameof(Package));
                }
            }
        }

        public int Hotel
        {
            get => _hotelId;
            set
            {
                if (_hotelId != value)
                {
                    _hotelId = value;
                    OnPropertyChanged(nameof(Hotel));
                }
            }
        }

        public PackageOffersHotel()
        {
            Package = 0;
            Hotel = 0;
        }

        public PackageOffersHotel(int package, int hotel)
        {
            Package = package;
            Hotel = hotel;
        }

        public override bool Equals(object obj)
        {
            return obj is PackageOffersHotel poh &&
                    Hotel == poh.Hotel;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Package, Hotel);
        }

        public override string ToString()
        {
            return $"{Package} - {Hotel}";
        }
        /*
        private int _packageId;
        private Hotel _hotelId;

        public int int
        {
            get => _packageId;
            set
            {
                if (_packageId != value)
                {
                    _packageId = value;
                    OnPropertyChanged(nameof(int));
                }
            }
        }

        public Hotel Hotel
        {
            get => _hotelId;
            set
            {
                if (_hotelId != value)
                {
                    _hotelId = value;
                    OnPropertyChanged(nameof(Hotel));
                }
            }
        }

        public intOffersHotel()
        {
            int = new int();
            Hotel = new Hotel();
        }

        public intOffersHotel(int package, Hotel hotel)
        {
            int = package;
            Hotel = hotel;
        }

        public override bool Equals(object obj)
        {
            return obj is intOffersHotel other &&
                   EqualityComparer<int>.Default.Equals(int, other.int) &&
                   EqualityComparer<Hotel>.Default.Equals(Hotel, other.Hotel);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(int, Hotel);
        }

        public override string ToString()
        {
            return $"{int.intId} - {Hotel.Name}";
        }
        */

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
