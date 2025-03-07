using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            return (string)Application.Current.Resources["PackageId"] + " " + Package + " - " + (string)Application.Current.Resources["HotelId"] + " " + Hotel;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
