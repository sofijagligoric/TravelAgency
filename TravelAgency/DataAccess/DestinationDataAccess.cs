using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using TravelAgency.Models;

namespace TravelAgency.DataAccess
{
    public class DestinationDataAccess
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["TravelAgencyConnection"].ConnectionString;

        public static bool AddCountry(Country country)
        {
            bool retVal = false;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"INSERT INTO country(CountryName) VALUES (@CountryName) ";
                        cmd.Parameters.AddWithValue("@CountryName", country.CountryName);
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

        public static List<Country> GetCountries()
        {
            List<Country> countries = new List<Country>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT CountryName from country";
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                countries.Add(new Country(
                                    reader["CountryName"]?.ToString() ?? string.Empty
                                ));
                            }
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Error occurred: " + e.Message);
            }
            return countries;
        }


        public static List<Destination> GetDestinationByName(string name)
        {
            List<Destination> destinations = new List<Destination>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT PostCode, DestinationName, About, Distance, LocalLanguage, CountryName 
                        FROM destination WHERE DestinationName = @DName";
                        cmd.Parameters.AddWithValue("@DName", name);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var country = new Country(reader["CountryName"]?.ToString() ?? string.Empty);
                                destinations.Add(new Destination(
                                    reader["PostCode"] != DBNull.Value ? Convert.ToInt32(reader["PostCode"]) : 0,
                                    reader["DestinationName"]?.ToString() ?? string.Empty,
                                    reader["About"]?.ToString() ?? string.Empty,
                                    reader["Distance"] != DBNull.Value ? Convert.ToInt32(reader["Distance"]) : 0,
                                    reader["LocalLanguage"]?.ToString() ?? string.Empty,
                                    country
                                ));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching destination by name: {ex.Message}");
            }
            return destinations;
        }

        public static bool AddDestination(Destination dest)
        {
            bool retVal = false;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"INSERT INTO destination (PostCode, DestinationName, About, Distance, LocalLanguage, CountryName)
                                            values (@PostCode, @DestinationName, @About, @Distance, @LocalLanguage, @CountryName)";
                        cmd.Parameters.AddWithValue("@PostCode", dest.PostCode);
                        cmd.Parameters.AddWithValue("@DestinationName", dest.DestinationName);
                        cmd.Parameters.AddWithValue("@About", dest.About);
                        cmd.Parameters.AddWithValue("@Distance", dest.Distance);
                        cmd.Parameters.AddWithValue("@LocalLanguage", dest.LocalLanguage);
                        cmd.Parameters.AddWithValue("@CountryName", dest.Country.CountryName);
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

        public static List<Destination> GetDestinationsByCountry(string country)
        {
            List<Destination> destinations = new List<Destination>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT PostCode, DestinationName, About, Distance, LocalLanguage, CountryName 
                        FROM destination WHERE CountryName LIKE @CName";
                        cmd.Parameters.AddWithValue("@CName", country + "%");
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var cntr = new Country(reader["CountryName"]?.ToString() ?? string.Empty);
                                destinations.Add(new Destination(
                                    reader["PostCode"] != DBNull.Value ? Convert.ToInt32(reader["PostCode"]) : 0,
                                    reader["DestinationName"]?.ToString() ?? string.Empty,
                                    reader["About"]?.ToString() ?? string.Empty,
                                    reader["Distance"] != DBNull.Value ? Convert.ToInt32(reader["Distance"]) : 0,
                                    reader["LocalLanguage"]?.ToString() ?? string.Empty,
                                    cntr
                                ));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching destination by country: {ex.Message}");
            }
            return destinations;
        }

        public static List<Destination> GetAllDestinations()
        {
            List<Destination> destinations = new List<Destination>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT PostCode, DestinationName, About, Distance, LocalLanguage, CountryName 
                        FROM destination";
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var cntr = new Country(reader["CountryName"]?.ToString() ?? string.Empty);
                                destinations.Add(new Destination(
                                    reader["PostCode"] != DBNull.Value ? Convert.ToInt32(reader["PostCode"]) : 0,
                                    reader["DestinationName"]?.ToString() ?? string.Empty,
                                    reader["About"]?.ToString() ?? string.Empty,
                                    reader["Distance"] != DBNull.Value ? Convert.ToInt32(reader["Distance"]) : 0,
                                    reader["LocalLanguage"]?.ToString() ?? string.Empty,
                                    cntr
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
            return destinations;
        }

        public static bool UpdateDestination(Destination dest)
        {
            bool retVal = false;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE destination SET About = @About, Distance = @Distance, LocalLanguage = @LocalLanguage, CountryName = @CountryName
                                          WHERE PostCode = @PostCode AND DestinationName = @DestinationName;";
                        cmd.Parameters.AddWithValue("@PostCode", dest.PostCode);
                        cmd.Parameters.AddWithValue("@DestinationName", dest.DestinationName);
                        cmd.Parameters.AddWithValue("@About", dest.About);
                        cmd.Parameters.AddWithValue("@Distance", dest.Distance);
                        cmd.Parameters.AddWithValue("@LocalLanguage", dest.LocalLanguage);
                        cmd.Parameters.AddWithValue("@CountryName", dest.Country.CountryName);
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

        public static bool DeleteDestination(Destination dest)
        {
            bool retVal = false;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"DELETE FROM destination WHERE PostCode = @PostCode AND DestinationName = @DestinationName;";
                        cmd.Parameters.AddWithValue("@PostCode", dest.PostCode);
                        cmd.Parameters.AddWithValue("@DestinationName", dest.DestinationName);
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
