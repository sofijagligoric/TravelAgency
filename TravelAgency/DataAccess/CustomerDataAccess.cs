using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using TravelAgency.Models;
using TravelAgency.Util;

namespace TravelAgency.DataAccess
{
    public class CustomerDataAccess
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["TravelAgencyConnection"].ConnectionString;


        public static Customer GetCustomerByJMB(string jmbFind)
        {
            Customer retVal = null;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT p.JMB, LastName, FirstName, Address, DateOfBirth, Email, PhoneNumber FROM customer p NATURAL JOIN person WHERE JMB = @JMB";
                        cmd.Parameters.AddWithValue("@JMB", jmbFind);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                retVal = new Customer(
                                    reader["FirstName"]?.ToString() ?? string.Empty,
                                    reader["LastName"]?.ToString() ?? string.Empty,
                                    reader["JMB"]?.ToString() ?? string.Empty,
                                    reader["Address"]?.ToString() ?? string.Empty,
                                    reader["Email"]?.ToString() ?? string.Empty,
                                    General.DateFromBase(reader["DateOfBirth"]?.ToString() ?? string.Empty),
                                    reader["PhoneNumber"]?.ToString() ?? string.Empty
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching customer by JMB: {ex.Message}");
            }
            return retVal;
        }

        public static List<Customer> GetCustomersByFirstOrLastName(string searchString)
        {
            List<Customer> result = new List<Customer>();
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
                            cmd.CommandText = "SELECT p.JMB, LastName, FirstName, Address, DateOfBirth, Email, PhoneNumber FROM customer p NATURAL JOIN person WHERE FirstName LIKE @searchTerm OR LastName LIKE @searchTerm";
                            cmd.Parameters.AddWithValue("@searchTerm", searchString + "%");
                        }
                        else
                        {
                            string firstName = parts.Length > 0 ? parts[0] : "";
                            string lastName = parts.Length > 1 ? parts[1] : "";

                            cmd.CommandText = @"
                                            SELECT p.JMB, LastName, FirstName, Address, DateOfBirth, Email, PhoneNumber 
                                            FROM customer p NATURAL JOIN person 
                                            WHERE (FirstName LIKE @firstName AND LastName LIKE @lastName) 
                                            OR (FirstName LIKE @lastName AND LastName LIKE @firstName)";

                            cmd.Parameters.AddWithValue("@firstName", firstName + "%");
                            cmd.Parameters.AddWithValue("@lastName", lastName + "%");
                        }

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(new Customer(
                                    reader["FirstName"]?.ToString() ?? string.Empty,
                                    reader["LastName"]?.ToString() ?? string.Empty,
                                    reader["JMB"]?.ToString() ?? string.Empty,
                                    reader["Address"]?.ToString() ?? string.Empty,
                                    reader["Email"]?.ToString() ?? string.Empty,
                                    General.DateFromBase(reader["DateOfBirth"]?.ToString() ?? string.Empty),
                                    reader["PhoneNumber"]?.ToString() ?? string.Empty
                                ));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching customers by name: {ex.Message}");
            }
            return result;
        }

        public static List<Customer> GetAllCustomers()
        {
            List<Customer> result = new List<Customer>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT p.JMB, LastName, FirstName, Address, DateOfBirth, Email, PhoneNumber FROM customer p NATURAL JOIN person";
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(new Customer(
                                    reader["FirstName"]?.ToString() ?? string.Empty,
                                    reader["LastName"]?.ToString() ?? string.Empty,
                                    reader["JMB"]?.ToString() ?? string.Empty,
                                    reader["Address"]?.ToString() ?? string.Empty,
                                    reader["Email"]?.ToString() ?? string.Empty,
                                     General.DateFromBase(reader["DateOfBirth"]?.ToString() ?? string.Empty),
                                    reader["PhoneNumber"]?.ToString() ?? string.Empty
                                ));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching all customers: {ex.Message}");
            }
            return result;
        }

        public static bool AddCustomer(Customer customer, string phone)
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
                        cmd.CommandText = "add_customer";
                        cmd.Parameters.AddWithValue("@pJMB", customer.Jmb);
                        cmd.Parameters["@pJMB"].Direction = ParameterDirection.Input;
                        cmd.Parameters.AddWithValue("@pFirstName", customer.FirstName);
                        cmd.Parameters["@pFirstName"].Direction = ParameterDirection.Input;
                        cmd.Parameters.AddWithValue("@pLastName", customer.LastName);
                        cmd.Parameters["@pLastName"].Direction = ParameterDirection.Input;
                        cmd.Parameters.AddWithValue("@pAddress", customer.Address);
                        cmd.Parameters["@pAddress"].Direction = ParameterDirection.Input;
                        cmd.Parameters.AddWithValue("@pDateOfBirth", General.DateForBase(customer.DateOfBirth));
                        cmd.Parameters["@pDateOfBirth"].Direction = ParameterDirection.Input;
                        cmd.Parameters.AddWithValue("@pEmail", customer.Email);
                        cmd.Parameters["@pEmail"].Direction = ParameterDirection.Input;
                        cmd.Parameters.AddWithValue("@pphone", phone);
                        cmd.Parameters["@pphone"].Direction = ParameterDirection.Input;
                        cmd.Parameters.Add("@successful", MySqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@message", MySqlDbType.VarChar, 255).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        successful = Convert.ToBoolean(cmd.Parameters["@successful"].Value);
                        string message = cmd.Parameters["@message"].Value?.ToString() ?? string.Empty;
                        /*
                        if (successful)
                        {
                            MessageBox.Show("Customer added successfully.");
                        }
                        else
                        {
                            MessageBox.Show($"Err
                        */
                    }
                }
            }
            catch (MySqlException e)
            {
                // MessageBox.Show("Error occurred: " + e.Message);
                Console.WriteLine($"An error occurred: {e.Message}");
            }
            return successful;
        }

        public static bool DeleteCustomer(string jmb)
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
                        cmd.CommandText = "delete_customer";
                        cmd.Parameters.AddWithValue("@pJMB", jmb).Direction = ParameterDirection.Input;
                        cmd.Parameters.Add("@successful", MySqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@message", MySqlDbType.VarChar, 255).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        successful = Convert.ToBoolean(cmd.Parameters["@successful"].Value);
                        string message = cmd.Parameters["@message"].Value?.ToString() ?? string.Empty;
                        /*
                        if (successful)
                        {
                            MessageBox.Show("Customer deleted successfully.");
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
                Console.WriteLine($"An error occurred: {e.Message}");
            }
            return successful;
        }

        public static bool UpdateCustomer(Customer customer, string jmb)
        {
            bool retVal = false;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE person SET JMB = @NewJMB, FirstName = @FName, LastName = @LName, Address = @Address, DateOfBirth = @DateOfBirth, Email = @Email, PhoneNumber = @PhoneNumber WHERE JMB = @JMB;";
                        cmd.Parameters.AddWithValue("@JMB", jmb);
                        cmd.Parameters.AddWithValue("@NewJMB", customer.Jmb);
                        cmd.Parameters.AddWithValue("@FName", customer.FirstName);
                        cmd.Parameters.AddWithValue("@LName", customer.LastName);
                        cmd.Parameters.AddWithValue("@Address", customer.Address);
                        cmd.Parameters.AddWithValue("@DateOfBirth", General.DateForBase(customer.DateOfBirth));
                        cmd.Parameters.AddWithValue("@Email", customer.Email);
                        cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);

                        retVal = cmd.ExecuteNonQuery() >= 1;
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }
            return retVal;
        }


    }

}
