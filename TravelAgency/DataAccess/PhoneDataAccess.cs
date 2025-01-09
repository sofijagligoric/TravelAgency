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

namespace TravelAgency.DataAccess
{
    public class PhoneDataAccess
    {

        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["TravelAgencyConnection"].ConnectionString;

        public static List<Phone> GetAllPhones()
        {
            List<Phone> result = new List<Phone>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT PhoneNumber, JMB, LastName, FirstName, Address, DateOfBirth, Email FROM phone_p ph INNER JOIN person p on ph.PersonJMB=p.JMB";
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var person = new Person(reader["FirstName"]?.ToString() ?? string.Empty,
                                    reader["LastName"]?.ToString() ?? string.Empty,
                                    reader["JMB"]?.ToString() ?? string.Empty,
                                    reader["Address"]?.ToString() ?? string.Empty,
                                    reader["Email"]?.ToString() ?? string.Empty,
                                    reader["DateOfBirth"]?.ToString() ?? string.Empty);

                                result.Add(new Phone(reader["PhoneNumber"]?.ToString() ?? string.Empty, person));
                            }
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Error occurred: " + e.Message);
            }
            return result;
        }

        public static List<Phone> GetAllPhonesFromPerson(string jmb)
        {
            List<Phone> result = new List<Phone>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT PhoneNumber, JMB, LastName, FirstName, Address, DateOfBirth, Email
                                            FROM phone_p ph INNER JOIN person p on ph.PersonJMB=p.JMB
                                            WHERE PersonJMB LIKE @JMBSearch";
                        cmd.Parameters.AddWithValue("@searchTerm", jmb + "%");
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var person = new Person(reader["FirstName"]?.ToString() ?? string.Empty,
                                    reader["LastName"]?.ToString() ?? string.Empty,
                                    reader["JMB"]?.ToString() ?? string.Empty,
                                    reader["Address"]?.ToString() ?? string.Empty,
                                    reader["Email"]?.ToString() ?? string.Empty,
                                    reader["DateOfBirth"]?.ToString() ?? string.Empty);

                                result.Add(new Phone(reader["PhoneNumber"]?.ToString() ?? string.Empty, person));
                            }
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Error occurred: " + e.Message);
            }
            return result;
        }

        public static bool AddPhone(Phone p)
        {
            bool retVal = false;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"INSERT INTO phone_p (PhoneNumber, PersonJMB) values (@PhoneNumber, @PersonJMB)";
                        cmd.Parameters.AddWithValue("@PhoneNumber", p.PhoneNumber);
                        cmd.Parameters.AddWithValue("@PersonJMB", p.Person.Jmb);
                        retVal = cmd.ExecuteNonQuery() == 1;
                    }
                }
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Error occurred: " + e.Message);
            }
            return retVal;
        }


        public static bool DeletePhone(string jmb)
        {
            bool retVal = false;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"DELETE FROM phone_p WHERE PersonJMB = @PersonJMB;";
                        cmd.Parameters.AddWithValue("@PersonJMB", jmb);
                        retVal = cmd.ExecuteNonQuery() == 1;
                    }
                }
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Error occurred: " + e.Message);
            }
            return retVal;
        }
    }
}
