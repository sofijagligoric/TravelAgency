using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Models
{
    public class Hotel
    {
        public int HotelId {  get; set; }
        public int RoomCount {  get; set; } 
        public string Name {  get; set; }
        public string Address {  get; set; }
        public string Email {  get; set; }
        public byte HasRestaurant {  get; set; }
        public Destination Destination {  get; set; }

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
            /*
            return "Hotel: " + Name +
                   "\nID: " + HotelId +
                   "\nbroj slobodnih soba: " + RoomCount +
                   "\nadresa: " + Address +
                   "\nemail: " + Email +
                   "\nima restoran: " + (hasRestaurant != 0 ? "da" : "ne") +
                   "\nmjesto: " + Destination.DestinationName + ", " + Destination.Country.CountryName;
            */
        }

    }
}
