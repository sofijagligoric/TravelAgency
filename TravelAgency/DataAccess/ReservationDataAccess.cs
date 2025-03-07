using MySql.Data.MySqlClient;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using TravelAgency.Models;
using TravelAgency.Util;

namespace TravelAgency.DataAccess
{
    public class ReservationDataAccess
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["TravelAgencyConnection"].ConnectionString;

        public static bool AddReservation(Reservation reservation)
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
                        cmd.CommandText = "add_reservation";
                        cmd.Parameters.AddWithValue("@rPrice", reservation.Price);
                        cmd.Parameters["@rPrice"].Direction = ParameterDirection.Input;
                        cmd.Parameters.AddWithValue("@rPackageId", reservation.Package.PackageId);
                        cmd.Parameters["@rPackageId"].Direction = ParameterDirection.Input;
                        cmd.Parameters.AddWithValue("@rHotelId", reservation.Hotel.HotelId);
                        cmd.Parameters["@rHotelId"].Direction = ParameterDirection.Input;
                        cmd.Parameters.AddWithValue("@CustomerJMB", reservation.Customer.Jmb);
                        cmd.Parameters["@CustomerJMB"].Direction = ParameterDirection.Input;
                        cmd.Parameters.AddWithValue("@ReservationPayed", reservation.AllPayed);
                        cmd.Parameters["@ReservationPayed"].Direction = ParameterDirection.Input;
                        cmd.Parameters.AddWithValue("@rEmployeeJMB", reservation.EmployeeJMB);
                        cmd.Parameters["@rEmployeeJMB"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add("@successful", MySqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@message", MySqlDbType.VarChar, 255).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@ResId", MySqlDbType.Int32).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        reservation.ReservationId = (int)cmd.LastInsertedId;
                        successful = Convert.ToBoolean(cmd.Parameters["@successful"].Value);
                        string message = cmd.Parameters["@message"].Value?.ToString() ?? string.Empty;

                        /*
                        if (successful)
                        {
                            MessageBox.Show("Reservation added successfully.");
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

        public static bool DeleteReservation(int reservationID)
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
                        cmd.CommandText = "delete_reservation";
                        cmd.Parameters.AddWithValue("@rID", reservationID).Direction = ParameterDirection.Input;
                        cmd.Parameters.Add("@successful", MySqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@message", MySqlDbType.VarChar, 255).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        successful = Convert.ToBoolean(cmd.Parameters["@successful"].Value);
                        string message = cmd.Parameters["@message"].Value?.ToString() ?? string.Empty;
                        /*
                        if (successful)
                        {
                            MessageBox.Show("Reservation deleted successfully.");
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

        public static bool UpdateReservation(Reservation reservation)
        {
            bool retVal = false;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE reservation SET Price = @Price, IsPayed = @IsPayed, PackageId = @PackageId,
                                          HotelId = @HotelId, CustomerJMB = @CustomerJMB, EmployeeJMB = @EmployeeJMB
                                          WHERE ReservationId = @ReservationId;";
                        cmd.Parameters.AddWithValue("@Price", reservation.Price);
                        cmd.Parameters.AddWithValue("@EmployeeJMB", reservation.EmployeeJMB);
                        cmd.Parameters.AddWithValue("@IsPayed", reservation.AllPayed);
                        cmd.Parameters.AddWithValue("@PackageId", reservation.Package.PackageId);
                        cmd.Parameters.AddWithValue("@HotelId", reservation.Hotel.HotelId);
                        cmd.Parameters.AddWithValue("@CustomerJMB", reservation.Customer.Jmb);
                        cmd.Parameters.AddWithValue("@ReservationId", reservation.ReservationId);

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

        public static List<Reservation> GetReservations()
        {
            List<Reservation> packages = new List<Reservation>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT ReservationId, r.Price AS ReservationPrice, r.PackageId, r.HotelId, CustomerJMB, IsPayed, r.EmployeeJMB, 
                                          a.StartDate, a.EndDate, a.Price, a.About, a.PostCode, 
                                          h.Name_of AS HotelName, h.RoomCount, h.Address, h.Email, h.ContainsRestaurant, h.PostCode,
                                          d.DestinationName, d.About, d.Distance, d.LocalLanguage, d.CountryName, 
                                          o.LastName, o.FirstName, o.Address, o.DateOfBirth, o.Email, o.PhoneNumber 
                                          FROM reservation r INNER JOIN package a ON a.PackageId=r.PackageId 
                                          INNER JOIN hotel h ON h.HotelId=r.HotelId 
                                          INNER JOIN destination d ON d.PostCode=h.PostCode AND d.DestinationName = h.DestinationName
                                          INNER JOIN customer p ON p.JMB = r.CustomerJMB 
                                          INNER JOIN person o ON p.JMB=o.JMB";
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                var country = new Country(reader["CountryName"]?.ToString() ?? string.Empty);

                                var destination = new Destination(
                                    reader["PostCode"] != DBNull.Value ? Convert.ToInt32(reader["PostCode"]) : 0,
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

                                var package = new Package(
                                    reader["PackageId"] != DBNull.Value ? Convert.ToInt32(reader["PackageId"]) : 0,
                                    reader["StartDate"]?.ToString() ?? string.Empty,
                                    reader["EndDate"]?.ToString() ?? string.Empty,
                                    reader["Price"] != DBNull.Value ? Convert.ToDecimal(reader["Price"]) : 0m,
                                    reader["About"]?.ToString() ?? string.Empty,
                                    destination
                                );

                                var customer = new Customer(
                                    reader["FirstName"]?.ToString() ?? string.Empty,
                                    reader["LastName"]?.ToString() ?? string.Empty,
                                    reader["CustomerJMB"]?.ToString() ?? string.Empty,
                                    reader["Address"]?.ToString() ?? string.Empty,
                                    reader["Email"]?.ToString() ?? string.Empty,
                                     General.DateFromBase(reader["DateOfBirth"]?.ToString() ?? string.Empty),
                                    reader["PhoneNumber"]?.ToString() ?? string.Empty
                                );

                                packages.Add(new Reservation(
                                    reader["ReservationId"] != DBNull.Value ? Convert.ToInt32(reader["ReservationId"]) : 0,
                                    reader["ReservationPrice"] != DBNull.Value ? Convert.ToDecimal(reader["ReservationPrice"]) : 0m,
                                    hotel,
                                    package,
                                    customer,
                                    reader["IsPayed"] != DBNull.Value ? Convert.ToByte(reader["IsPayed"]) : (byte)0,
                                    reader["EmployeeJMB"]?.ToString() ?? string.Empty
                                ));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching all packets: {ex.Message}");
            }
            return packages;
        }

        public static List<Reservation> GetReservationsByCustomer(string searchString)
        {
            List<Reservation> packages = new List<Reservation>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        string[] parts = searchString.Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length == 1)
                        {
                            cmd.CommandText = @"SELECT ReservationId, r.Price AS ReservationPrice, r.PackageId, r.HotelId, CustomerJMB, IsPayed, r.EmployeeJMB, 
                                          a.StartDate, a.EndDate, a.Price, a.About, a.PostCode, 
                                          h.Name_of as HotelName, h.RoomCount, h.Address, h.Email, h.ContainsRestaurant, h.PostCode,
                                          d.DestinationName, d.About, d.Distance, d.LocalLanguage, d.CountryName, 
                                          o.LastName, o.FirstName, o.Address, o.DateOfBirth, o.Email, o.PhoneNumber 
                                          FROM reservation r INNER JOIN package a ON a.PackageId=r.PackageId 
                                          INNER JOIN hotel h ON h.HotelId=r.HotelId 
                                          INNER JOIN destination d ON d.PostCode=h.PostCode  AND d.DestinationName = h.DestinationName
                                          INNER JOIN customer p ON p.JMB = r.CustomerJMB 
                                          INNER JOIN person o ON p.JMB=o.JMB
                                          WHERE o.FirstName LIKE @searchTerm OR o.LastName LIKE @searchTerm";
                            cmd.Parameters.AddWithValue("@searchTerm", searchString + "%");
                        }
                        else
                        {
                            string firstName = parts.Length > 0 ? parts[0] : "";
                            string lastName = parts.Length > 1 ? parts[1] : "";

                            cmd.CommandText = @"SELECT ReservationId, r.Price AS ReservationPrice, r.PackageId, r.HotelId, CustomerJMB, IsPayed, r.EmployeeJMB, 
                                          a.StartDate, a.EndDate, a.Price, a.About, a.PostCode, 
                                          h.Name_of as HotelName, h.RoomCount, h.Address, h.Email, h.ContainsRestaurant, h.PostCode,
                                          d.DestinationName, d.About, d.Distance, d.LocalLanguage, d.CountryName, 
                                          o.LastName, o.FirstName, o.Address, o.DateOfBirth, o.Email, o.PhoneNumber 
                                          FROM reservation r INNER JOIN package a ON a.PackageId=r.PackageId 
                                          INNER JOIN hotel h ON h.HotelId=r.HotelId 
                                          INNER JOIN destination d ON d.PostCode=h.PostCode  AND d.DestinationName = h.DestinationName
                                          INNER JOIN customer p ON p.JMB = r.CustomerJMB 
                                          INNER JOIN person o ON p.JMB=o.JMB
                                          WHERE(o.FirstName LIKE @firstName AND o.LastName LIKE @lastName)
                                             OR(o.FirstName LIKE @lastName AND o.LastName LIKE @firstName)";


                            cmd.Parameters.AddWithValue("@firstName", firstName + "%");
                            cmd.Parameters.AddWithValue("@lastName", lastName + "%");
                        }

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                var country = new Country(reader["CountryName"]?.ToString() ?? string.Empty);

                                var destination = new Destination(
                                    reader["PostCode"] != DBNull.Value ? Convert.ToInt32(reader["PostCode"]) : 0,
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

                                var package = new Package(
                                    reader["PackageId"] != DBNull.Value ? Convert.ToInt32(reader["PackageId"]) : 0,
                                    reader["StartDate"]?.ToString() ?? string.Empty,
                                    reader["EndDate"]?.ToString() ?? string.Empty,
                                    reader["Price"] != DBNull.Value ? Convert.ToDecimal(reader["Price"]) : 0m,
                                    reader["About"]?.ToString() ?? string.Empty,
                                    destination
                                );

                                var customer = new Customer(
                                    reader["FirstName"]?.ToString() ?? string.Empty,
                                    reader["LastName"]?.ToString() ?? string.Empty,
                                    reader["CustomerJMB"]?.ToString() ?? string.Empty,
                                    reader["Address"]?.ToString() ?? string.Empty,
                                    reader["Email"]?.ToString() ?? string.Empty,
                                     General.DateFromBase(reader["DateOfBirth"]?.ToString() ?? string.Empty),
                                    reader["PhoneNumber"]?.ToString() ?? string.Empty
                                );

                                packages.Add(new Reservation(
                                    reader["ReservationId"] != DBNull.Value ? Convert.ToInt32(reader["ReservationId"]) : 0,
                                    reader["ReservationPrice"] != DBNull.Value ? Convert.ToDecimal(reader["ReservationPrice"]) : 0m,
                                    hotel,
                                    package,
                                    customer,
                                    reader["IsPayed"] != DBNull.Value ? Convert.ToByte(reader["IsPayed"]) : (byte)0,
                                    reader["EmployeeJMB"]?.ToString() ?? string.Empty
                                ));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching all packets: {ex.Message}");
            }
            return packages;

        }
    }
}
