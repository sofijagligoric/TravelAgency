using Google.Protobuf.Compiler;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TravelAgency.Models;

namespace TravelAgency.DataAccess
{
    public class HotelDataAccess
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["TravelAgencyConnection"].ConnectionString;

        public static bool AddHotel(Hotel hotel)
        {
            bool retVal = false;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"INSERT INTO hotel (Name_of, RoomCount, Address, Email, ContainsRestaurant, PostCode, DestinationName) values (@Name_of, @RoomCount, @Address, @Email, @ContainsRestaurant, @PostCode, @DestinationName)";
                        cmd.Parameters.AddWithValue("@Name_of", hotel.Name);
                        cmd.Parameters.AddWithValue("@RoomCount", hotel.RoomCount);
                        cmd.Parameters.AddWithValue("@Address", hotel.Address);
                        cmd.Parameters.AddWithValue("@Email", hotel.Email);
                        cmd.Parameters.AddWithValue("@ContainsRestaurant", hotel.HasRestaurant);
                        cmd.Parameters.AddWithValue("@PostCode", hotel.Destination.PostCode);
                        cmd.Parameters.AddWithValue("@DestinationName", hotel.Destination.DestinationName);
                        retVal = cmd.ExecuteNonQuery() == 1;
                        hotel.HotelId = (int)cmd.LastInsertedId;
                    }
                }
            }
            catch (MySqlException e)
            {
                //  MessageBox.Show("Error occurred: " + e.Message);
                return false;
            }
            return retVal;
        }

        public static List<Hotel> GetAllHotels()
        {
            List<Hotel> hotels = new List<Hotel>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT 
                                        h.HotelId, 
                                        h.Name_of AS HotelName, 
                                        h.RoomCount, 
                                        h.Address, 
                                        h.Email, 
                                        h.ContainsRestaurant, 
                                        h.PostCode AS HotelPostCode, 
                                        d.PostCode AS DestinationPostCode, 
                                        d.DestinationName, 
                                        d.About, 
                                        d.Distance, 
                                        d.LocalLanguage, 
                                        d.CountryName
                                    FROM hotel h
                                    JOIN destination d ON h.PostCode = d.PostCode AND h.DestinationName = d.DestinationName;";
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                var country = new Country(reader["CountryName"]?.ToString() ?? string.Empty);

                                var destination = new Destination(
                                    reader["DestinationPostCode"] != DBNull.Value ? Convert.ToInt32(reader["DestinationPostCode"]) : 0,
                                    reader["DestinationName"]?.ToString() ?? string.Empty,
                                    reader["About"]?.ToString() ?? string.Empty,
                                    reader["Distance"] != DBNull.Value ? Convert.ToInt32(reader["Distance"]) : 0,
                                    reader["LocalLanguage"]?.ToString() ?? string.Empty,
                                    country
                                );

                                var hotel = new Hotel(
                                    reader["HotelId"] != DBNull.Value ? Convert.ToInt32(reader["HotelId"]) : 0,
                                    reader["RoomCount"] != DBNull.Value ? Convert.ToInt32(reader["RoomCount"]) : 0,
                                    reader["HotelName"]?.ToString() ?? string.Empty,
                                    reader["Address"]?.ToString() ?? string.Empty,
                                    reader["Email"]?.ToString() ?? string.Empty,
                                    reader["ContainsRestaurant"] != DBNull.Value ? Convert.ToByte(reader["ContainsRestaurant"]) : (byte)0,
                                    destination
                                );

                                hotels.Add(hotel);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return hotels;
        }

        public static List<Hotel> GetHotelsByDestinationName(string destinationName)
        {
            List<Hotel> hotels = new List<Hotel>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT 
                                        h.HotelId, 
                                        h.Name_of AS HotelName, 
                                        h.RoomCount, 
                                        h.Address, 
                                        h.Email, 
                                        h.ContainsRestaurant, 
                                        h.PostCode AS HotelPostCode, 
                                        d.PostCode AS DestinationPostCode, 
                                        d.DestinationName, 
                                        d.About, 
                                        d.Distance, 
                                        d.LocalLanguage, 
                                        d.CountryName
                                    FROM hotel h
                                    JOIN destination d ON h.PostCode = d.PostCode AND h.DestinationName = d.DestinationName
                                    WHERE d.DestinationName LIKE @DestName;";
                        cmd.Parameters.AddWithValue("@DestName", destinationName + "%");
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while(reader.Read())
                            {

                                var country = new Country(reader["CountryName"]?.ToString() ?? string.Empty);

                                var destination = new Destination(
                                    reader["DestinationPostCode"] != DBNull.Value ? Convert.ToInt32(reader["DestinationPostCode"]) : 0,
                                    reader["DestinationName"]?.ToString() ?? string.Empty,
                                    reader["About"]?.ToString() ?? string.Empty,
                                    reader["Distance"] != DBNull.Value ? Convert.ToInt32(reader["Distance"]) : 0,
                                    reader["LocalLanguage"]?.ToString() ?? string.Empty,
                                    country
                                );

                                var hotel = new Hotel(
                                    reader["HotelId"] != DBNull.Value ? Convert.ToInt32(reader["HotelId"]) : 0,
                                    reader["RoomCount"] != DBNull.Value ? Convert.ToInt32(reader["RoomCount"]) : 0,
                                    reader["HotelName"]?.ToString() ?? string.Empty,
                                    reader["Address"]?.ToString() ?? string.Empty,
                                    reader["Email"]?.ToString() ?? string.Empty,
                                    reader["ContainsRestaurant"] != DBNull.Value ? Convert.ToByte(reader["ContainsRestaurant"]) : (byte)0,
                                    destination
                                );

                                hotels.Add(hotel);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return hotels;
        }

        public static bool UpdateHotel(Hotel hotel)
        {
            bool retVal = false;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE hotel SET Name_of = @Name_of, RoomCount = @RoomCount, Address = @Address, Email = @Email, 
                                          ContainsRestaurant = @ContainsRestaurant, PostCode = @PostCode, DestinationName = @DestinationName
                                          WHERE HotelId = @HotelId;";
                        cmd.Parameters.AddWithValue("@Name_of", hotel.Name);
                        cmd.Parameters.AddWithValue("@HotelId", hotel.HotelId);
                        cmd.Parameters.AddWithValue("@RoomCount", hotel.RoomCount);
                        cmd.Parameters.AddWithValue("@Address", hotel.Address);
                        cmd.Parameters.AddWithValue("@Email", hotel.Email);
                        cmd.Parameters.AddWithValue("@ContainsRestaurant", hotel.HasRestaurant);
                        cmd.Parameters.AddWithValue("@PostCode", hotel.Destination.PostCode);
                        cmd.Parameters.AddWithValue("@DestinationName", hotel.Destination.DestinationName);
                        retVal = cmd.ExecuteNonQuery() >= 1;
                    }
                }
            }
            catch (MySqlException e)
            {
                //  MessageBox.Show("Error occurred: " + e.Message);
                return false;
            }
            return retVal;
        }

        public static bool DeleteHotel(Hotel hotel)
        {
            bool retVal = false;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"DELETE FROM hotel WHERE HotelId = @HotelId;";
                        cmd.Parameters.AddWithValue("@HotelId", hotel.HotelId);
                        retVal = cmd.ExecuteNonQuery() == 1;
                    }
                }
            }
            catch (MySqlException e)
            {
               // MessageBox.Show("Error occurred: " + e.Message);
                return false;
            }
            return retVal;
        }
    }
}
