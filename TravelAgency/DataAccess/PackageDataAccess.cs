using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using Org.BouncyCastle.Bcpg;
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
    public class PackageDataAccess
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["TravelAgencyConnection"].ConnectionString;

        public List<Package> GetPackages()
        {
            List<Package> packages = new List<Package>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT a.PackageId, a.Price, a.StartDate, a.EndDate, a.About, d.DestinationName,
                        d.PostCode, d.About, d.Distance, d.LocalLanguage, d.CountryName 
                        FROM package a INNER JOIN destination d on a.PostCode=d.PostCode AND a.DestinationName=d.DestinationName";
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
                                packages.Add(new Package(
                                    reader["PackageId"] != DBNull.Value ? Convert.ToInt32(reader["PackageId"]) : 0,
                                    reader["StartDate"]?.ToString() ?? string.Empty,
                                    reader["EndDate"]?.ToString() ?? string.Empty,
                                    reader["Price"] != DBNull.Value ? Convert.ToDecimal(reader["Price"]) : 0m,
                                    reader["About"]?.ToString() ?? string.Empty,
                                    destination
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

        public List<Package> GetPackagesByDestination(string destinationName)
        {
            List<Package> packages = new List<Package>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT a.PackageId, a.Price, a.StartDate, a.EndDate, a.About, d.DestinationName,
                        d.PostCode, d.About, d.Distance, d.LocalLanguage, d.CountryName 
                        FROM package a INNER JOIN destination d on a.PostCode=d.PostCode AND a.DestinationName=d.DestinationName
                        WHERE NazivDestinacije DestinationName LIKE @DestName;";
                        cmd.Parameters.AddWithValue("@DestName", destinationName + "%");
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
                                packages.Add(new Package(
                                    reader["PackageId"] != DBNull.Value ? Convert.ToInt32(reader["PackageId"]) : 0,
                                    reader["StartDate"]?.ToString() ?? string.Empty,
                                    reader["EndDate"]?.ToString() ?? string.Empty,
                                    reader["Price"] != DBNull.Value ? Convert.ToDecimal(reader["Price"]) : 0m,
                                    reader["About"]?.ToString() ?? string.Empty,
                                    destination
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

        public bool AddPackage(Package package)
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
                        cmd.CommandText = "add_package";
                        cmd.Parameters.AddWithValue("@aStartDate", package.StartDate);
                        cmd.Parameters["@aStartDate"].Direction = ParameterDirection.Input;
                        cmd.Parameters.AddWithValue("@aEndDate", package.EndDate);
                        cmd.Parameters["@aEndDate"].Direction = ParameterDirection.Input;
                        cmd.Parameters.AddWithValue("@aPrice", package.Price);
                        cmd.Parameters["@aPrice"].Direction = ParameterDirection.Input;
                        cmd.Parameters.AddWithValue("@aAbout", package.About);
                        cmd.Parameters["@aAbout"].Direction = ParameterDirection.Input;
                        cmd.Parameters.AddWithValue("@aPostCode", package.Destination.PostCode);
                        cmd.Parameters["@aPostCode"].Direction = ParameterDirection.Input;
                        cmd.Parameters.AddWithValue("@aDestinationName", package.Destination.DestinationName);
                        cmd.Parameters["@aDestinationName"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add("@PackId", MySqlDbType.Int32).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@successful", MySqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@message", MySqlDbType.VarChar, 255).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        package.PackageId = Convert.ToInt32(cmd.Parameters["@PackId"].Value);
                        successful = Convert.ToBoolean(cmd.Parameters["@successful"].Value);
                        string message = cmd.Parameters["@message"].Value?.ToString() ?? string.Empty;
                        if (successful)
                        {
                            MessageBox.Show("Package added successfully.");
                        }
                        else
                        {
                            MessageBox.Show($"Error: {message}");
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Error occurred: " + e.Message);
            }
            return successful;
        }

        public bool DeletePackage(int packageID)
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
                        cmd.CommandText = "delete_package";
                        cmd.Parameters.AddWithValue("@aID", packageID).Direction = ParameterDirection.Input;
                        cmd.Parameters.Add("@successful", MySqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@message", MySqlDbType.VarChar, 255).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        successful = Convert.ToBoolean(cmd.Parameters["@successful"].Value);
                        string message = cmd.Parameters["@message"].Value?.ToString() ?? string.Empty;
                        if (successful)
                        {
                            MessageBox.Show("Package deleted successfully.");
                        }
                        else
                        {
                            MessageBox.Show($"Error: {message}");
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Error occurred: " + e.Message);
            }
            return successful;
        }

        public bool UpdatePackage(Package package)
        {
            bool retVal = false;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE package SET StartDate = @StartDate, EndDate = @EndDate, Price = @Price, About = @About, 
                                          PostCode = @PostCode, DestinationName = @DestinationName
                                          WHERE PackageId = @PackageId;";
                        cmd.Parameters.AddWithValue("@StartDate", package.StartDate);
                        cmd.Parameters.AddWithValue("@EndDate", package.EndDate);
                        cmd.Parameters.AddWithValue("@Price", package.Price);
                        cmd.Parameters.AddWithValue("@About", package.About);
                        cmd.Parameters.AddWithValue("@PostCode", package.Destination.PostCode);
                        cmd.Parameters.AddWithValue("@DestinationName", package.Destination.DestinationName);
                        cmd.Parameters.AddWithValue("@PackageId", package.PackageId);

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
