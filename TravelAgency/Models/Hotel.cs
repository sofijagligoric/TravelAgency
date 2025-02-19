using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TravelAgency.Models
{

    public class Hotel : INotifyPropertyChanged
    {
        private int _hotelId;
        private int _roomCount;
        private string _name;
        private string _address;
        private string _email;
        private byte _hasRestaurant;
        private Destination _destination;

        public int HotelId
        {
            get => _hotelId;
            set { _hotelId = value; OnPropertyChanged(nameof(HotelId)); }
        }

        public int RoomCount
        {
            get => _roomCount;
            set { _roomCount = value; OnPropertyChanged(nameof(RoomCount)); }
        }

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        public string Address
        {
            get => _address;
            set { _address = value; OnPropertyChanged(nameof(Address)); }
        }

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(nameof(Email)); }
        }


        public byte HasRestaurant
        {
            get => _hasRestaurant;
            set
            {
                if (_hasRestaurant != value)
                {
                    _hasRestaurant = value;
                    OnPropertyChanged(nameof(HasRestaurant));
                    OnPropertyChanged(nameof(HasRestaurantBool));
                }
            }
        }

        public bool HasRestaurantBool
        {
            get => _hasRestaurant == 1;
            set
            {
                if (HasRestaurantBool != value) 
                {
                    HasRestaurant = (byte)(value ? 1 : 0);
                    OnPropertyChanged(nameof(HasRestaurantBool));
                }
            }
        }

        public string HasRestaurantString
        {
            get
            {
                if (HasRestaurant == 0)
                    return (string)Application.Current.Resources["No"];
                else return  (string)Application.Current.Resources["Yes"]; ;
            }
        }

        public Destination Destination
        {
            get => _destination;
            set { _destination = value; OnPropertyChanged(nameof(Destination)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public Hotel(int HotelId, int roomCount, string name, string address, string email, byte hasRestaurant, Destination destination)
        {
            this.HotelId = HotelId;
            RoomCount = roomCount;
            Name = name;
            Address = address;
            Email = email;
            this.HasRestaurant = hasRestaurant;
            Destination = destination;
        }

        public Hotel(Hotel other)
        {
            this.HotelId = other.HotelId;
            this.Name = other.Name;
            this.RoomCount = other.RoomCount;
            this.Address = other.Address;
            this.Email = other.Email;
            this.HasRestaurant = other.HasRestaurant;
            this.Destination = new Destination(other.Destination); 
        }

        public Hotel()
        {
            Name = string.Empty;
            Address = string.Empty;
            Email = string.Empty;
            HotelId = 0;
            RoomCount = 0;
            HasRestaurant = 0;
            Destination = new Destination();
        }

        public override bool Equals(object obj)
        {
            return obj is Hotel hotel &&
                   HotelId == hotel.HotelId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(HotelId);
        }

        public override string ToString()
        {
            return Name + ", " + HotelId + ", " + RoomCount + ", " + Address + ", " + Email + ", " + (HasRestaurant != 0 ? "da" : "ne") +
                  ", " + Destination.DestinationName + ", " + Destination.Country.CountryName;


        }
    }
        }
