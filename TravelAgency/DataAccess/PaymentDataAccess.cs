using MySql.Data.MySqlClient;
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
    public class PaymentDataAccess
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["TravelAgencyConnection"].ConnectionString;

        public static bool PayReservation(int resId, int amount)
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
                        cmd.CommandText = "pay_reservation";
                        cmd.Parameters.AddWithValue("@uAmount", amount);
                        cmd.Parameters["@uAmount"].Direction = ParameterDirection.Input;
                        cmd.Parameters.AddWithValue("@uReservationId", resId);
                        cmd.Parameters["@uReservationId"].Direction = ParameterDirection.Input;   cmd.Parameters.Add("@successful", MySqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@message", MySqlDbType.VarChar, 255).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@successfulPayment", MySqlDbType.Int32).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        successful = Convert.ToBoolean(cmd.Parameters["@successfulPayment"].Value);
                        string message = cmd.Parameters["@message"].Value?.ToString() ?? string.Empty;
                       
                    }
                }
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Error occurred: " + e.Message);
            }
            return successful;
        }

        public static List<TotalReservationPayment> GetPaymentInfoForReservations()
        {
            List<TotalReservationPayment> result = new List<TotalReservationPayment>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM total_payment_for_reservation";
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(new TotalReservationPayment(
                                    reader["CustomerName"]?.ToString() ?? string.Empty,
                                    reader["ReservationId"] != DBNull.Value ? Convert.ToInt32(reader["ReservationId"]) : 0,
                                    reader["FinalPrice"] != DBNull.Value ? Convert.ToDecimal(reader["FinalPrice"]) : 0m,
                                    reader["Payed"] != DBNull.Value ? Convert.ToDecimal(reader["Payed"]) : 0m,
                                    reader["Debt"] != DBNull.Value ? Convert.ToDecimal(reader["Debt"]) : 0m,
                                    //   reader["IsPayed"]?.ToString() ?? string.Empty
                                    reader["IsPayed"] != DBNull.Value && reader["IsPayed"].ToString().Equals("Jeste", StringComparison.OrdinalIgnoreCase)
                                ));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                MessageBox.Show("Error occurred: " + ex.Message);
            }
            return result;
        }

        public static List<TotalReservationPayment> GetPaymentInfoForReservationsByCustomerName(string searchString)
        {
            List<TotalReservationPayment> result = new List<TotalReservationPayment>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        string[] parts = searchString.Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);
                        string firstName = parts.Length > 0 ? parts[0] : "";
                        string lastName = parts.Length > 1 ? parts[1] : "";
                        cmd.CommandText = @"
                                            SELECT * FROM total_payment_for_reservation 
                                            WHERE (CustomerName LIKE @firstName AND CustomerName LIKE @lastName) 
                                            OR (CustomerName LIKE @lastName AND CustomerName LIKE @firstName)";

                        cmd.Parameters.AddWithValue("@firstName", "%" + firstName + "%");
                        cmd.Parameters.AddWithValue("@lastName", "%" + lastName + "%");
                        /*
                        cmd.CommandText = "SELECT * FROM total_payment_for_reservation WHERE  CustomerName LIKE @CustomerName";
                        cmd.Parameters.AddWithValue("@CustomerName", "%" + searchString + "%");
                        */

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(new TotalReservationPayment(
                                    reader["CustomerName"]?.ToString() ?? string.Empty,
                                    reader["ReservationId"] != DBNull.Value ? Convert.ToInt32(reader["ReservationId"]) : 0,
                                    reader["FinalPrice"] != DBNull.Value ? Convert.ToDecimal(reader["FinalPrice"]) : 0m,
                                    reader["Payed"] != DBNull.Value ? Convert.ToDecimal(reader["Payed"]) : 0m,
                                    reader["Debt"] != DBNull.Value ? Convert.ToDecimal(reader["Debt"]) : 0m,
                                    reader["IsPayed"] != DBNull.Value && reader["IsPayed"].ToString().Equals("Jeste", StringComparison.OrdinalIgnoreCase)
                                ));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                
            }
            return result;
        }

        public static TotalReservationPayment GetTotalPaymentForReservation(int resId)
        {
            TotalReservationPayment result = null;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM total_payment_for_reservation WHERE  ReservationId LIKE @ResId";
                        cmd.Parameters.AddWithValue("@ResId", resId);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                result= new TotalReservationPayment(
                                    reader["CustomerName"]?.ToString() ?? string.Empty,
                                    reader["ReservationId"] != DBNull.Value ? Convert.ToInt32(reader["ReservationId"]) : 0,
                                    reader["FinalPrice"] != DBNull.Value ? Convert.ToDecimal(reader["FinalPrice"]) : 0m,
                                    reader["Payed"] != DBNull.Value ? Convert.ToDecimal(reader["Payed"]) : 0m,
                                    reader["Debt"] != DBNull.Value ? Convert.ToDecimal(reader["Debt"]) : 0m,
                                    reader["IsPayed"] != DBNull.Value && reader["IsPayed"].ToString().Equals("Jeste", StringComparison.OrdinalIgnoreCase)
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return result;
        }


        public static List<Payment> GetAllPayments()
        {
            List<Payment> result = new List<Payment>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM reservation_payment_info";
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(new Payment(
                                    reader["CustomerFirstName"]?.ToString() ?? string.Empty,
                                    reader["ReservationId"] != DBNull.Value ? Convert.ToInt32(reader["ReservationId"]) : 0,
                                    reader["PaymentId"] != DBNull.Value ? Convert.ToInt32(reader["PaymentId"]) : 0,
                                    reader["Amount"] != DBNull.Value ? Convert.ToDecimal(reader["Amount"]) : 0m,
                                    reader["PayedToHotel"] != DBNull.Value ? Convert.ToDecimal(reader["PayedToHotel"]) : 0m,
                                    reader["PayedToAgency"] != DBNull.Value ? Convert.ToDecimal(reader["PayedToAgency"]) : 0m,
                                    reader["PhoneNumber"]?.ToString() ?? string.Empty,
                                    General.DateFromBase(reader["PaymentDate"]?.ToString() ?? string.Empty),
                                    reader["JMB"]?.ToString() ?? string.Empty
                                ));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return result;
        }

        public static List<Payment> GetAllPaymentsByCustomerName(string searchString)
        {
            List<Payment> result = new List<Payment>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        string[] parts = searchString.Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);
                        string firstName = parts.Length > 0 ? parts[0] : "";
                        string lastName = parts.Length > 1 ? parts[1] : "";
                        cmd.CommandText = @"
                                            SELECT * FROM reservation_payment_info 
                                            WHERE (CustomerFirstName LIKE @firstName AND CustomerFirstName LIKE @lastName) 
                                            OR (CustomerFirstName LIKE @lastName AND CustomerFirstName LIKE @firstName)";

                        cmd.Parameters.AddWithValue("@firstName", "%" + firstName + "%");
                        cmd.Parameters.AddWithValue("@lastName", "%" + lastName + "%");
                        /*
                        cmd.CommandText = "SELECT * FROM reservation_payment_info WHERE  CustomerFirstName LIKE @CustomerName";
                        cmd.Parameters.AddWithValue("@CustomerName", "%" + searchString + "%");
                        */
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(new Payment(
                                    reader["CustomerFirstName"]?.ToString() ?? string.Empty,
                                    reader["ReservationId"] != DBNull.Value ? Convert.ToInt32(reader["ReservationId"]) : 0,
                                    reader["PaymentId"] != DBNull.Value ? Convert.ToInt32(reader["PaymentId"]) : 0,
                                    reader["Amount"] != DBNull.Value ? Convert.ToDecimal(reader["Amount"]) : 0m,
                                    reader["PayedToHotel"] != DBNull.Value ? Convert.ToDecimal(reader["PayedToHotel"]) : 0m,
                                    reader["PayedToAgency"] != DBNull.Value ? Convert.ToDecimal(reader["PayedToAgency"]) : 0m,
                                    reader["PhoneNumber"]?.ToString() ?? string.Empty,
                                    General.DateFromBase(reader["PaymentDate"]?.ToString() ?? string.Empty),
                                    reader["JMB"]?.ToString() ?? string.Empty
                                ));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                MessageBox.Show("Error occurred: " + ex.Message);
            }
            return result;
        }
    }
}
