using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities.IO.Pem;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TravelAgency.Models;
using TravelAgency.Util;

namespace TravelAgency.DataAccess
{
    public class PackageOffersHotelDataAccess
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["TravelAgencyConnection"].ConnectionString;

        public static bool AddPackageOffersHotel(PackageOffersHotel poh)
        {
            bool successful = false;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "add_package_offers_hotel";
                        cmd.Parameters.AddWithValue("@rPackageId", poh.Package);
                        cmd.Parameters["@rPackageId"].Direction = ParameterDirection.Input;
                        cmd.Parameters.AddWithValue("@rHotelId", poh.Hotel);
                        cmd.Parameters["@rHotelId"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add("@successful", MySqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@message", MySqlDbType.VarChar, 255).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        successful = Convert.ToBoolean(cmd.Parameters["@successful"].Value);
                        string message = cmd.Parameters["@message"].Value?.ToString() ?? string.Empty;
                        /*
                        if (successful)
                        {
                            MessageBox.Show(message);
                        }
                        else
                        {
                            MessageBox.Show($"Error: {message}");
                        }
                        */
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            return successful;
        }

        public static bool DeletePackageOffersHotel(int packageId, int hotelId)
        {
            bool retVal = false;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"DELETE FROM package_offers_hotel WHERE HotelId = @HotelId AND PackageId = @PackageId;";
                        cmd.Parameters.AddWithValue("@HotelId", hotelId);
                        cmd.Parameters.AddWithValue("@PackageId", packageId);
                        retVal = cmd.ExecuteNonQuery() == 1;
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            return retVal;
        }

        public static List<PackageOffersHotel> GetAllPackageOffersHotel()
        {
            List<PackageOffersHotel> packages = new List<PackageOffersHotel>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT PackageId, HotelId
				        FROM package_offers_hotel anh";
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                packages.Add(new PackageOffersHotel(reader["PackageId"] != DBNull.Value ? Convert.ToInt32(reader["PackageId"]) : 0,
                                              reader["HotelId"] != DBNull.Value ? Convert.ToInt32(reader["HotelId"]) : 0));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return packages;
        }

        public static List<PackageOffersHotel> GetPackageOffersHotelByPackage(int packageId)
        {
            List<PackageOffersHotel> packages = new List<PackageOffersHotel>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT PackageId, HotelId
				        FROM package_offers_hotel anh 
                        WHERE PackageId = @PId";
                        cmd.Parameters.AddWithValue("@PId", packageId);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                packages.Add(new PackageOffersHotel(reader["PackageId"] != DBNull.Value ? Convert.ToInt32(reader["PackageId"]) : 0,
                                                                             reader["HotelId"] != DBNull.Value ? Convert.ToInt32(reader["HotelId"]) : 0));
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return packages;
        }

        public static List<Hotel> GetHotelsByPackage(int packageId)
        {
            List<Hotel> hotels = new List<Hotel>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT  d.DestinationName,
                        d.PostCode as DestinationPostCode, d.About, d.Distance, d.LocalLanguage, d.CountryName,
				        h.HotelId, h.RoomCount, h.Name_of as HotelName, h.Address, h.Email, h.ContainsRestaurant
				        FROM package_offers_hotel anh
				        INNER JOIN hotel h on h.HotelId=anh.HotelId
				        INNER JOIN destination d on h.PostCode=d.PostCode AND h.DestinationName=d.DestinationName 
                        WHERE PackageId = @PId";
                        cmd.Parameters.AddWithValue("@PId", packageId);
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
    }
}
