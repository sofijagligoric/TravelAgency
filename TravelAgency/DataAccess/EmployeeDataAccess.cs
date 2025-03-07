using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
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
    public class EmployeeDataAccess
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["TravelAgencyConnection"].ConnectionString;

        public static Employee CheckLoginInfo(string username, string password)
        {
            Employee retVal = null;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT e.JMB, LastName, FirstName, Address, DateOfBirth, Email, PhoneNumber, Salary, Username, PasswordString, EmploymentDate, RoleType, PreferredTheme FROM employee e NATURAL JOIN person WHERE BINARY Username = @UsrName AND PasswordString = @Passwd";
                        cmd.Parameters.AddWithValue("@UsrName", username);
                        cmd.Parameters.AddWithValue("@Passwd", password);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                retVal = new Employee(
                                    reader["FirstName"]?.ToString() ?? string.Empty,
                                    reader["LastName"]?.ToString() ?? string.Empty,
                                    reader["JMB"]?.ToString() ?? string.Empty,
                                    reader["Address"]?.ToString() ?? string.Empty,
                                    reader["Email"]?.ToString() ?? string.Empty,
                                    General.DateFromBase(reader["DateOfBirth"]?.ToString() ?? string.Empty),
                                    reader["PhoneNumber"]?.ToString() ?? string.Empty,
                                    reader["Username"]?.ToString() ?? string.Empty,
                                    reader["PasswordString"]?.ToString() ?? string.Empty,
                                    reader["RoleType"]?.ToString() ?? string.Empty,
                                    General.DateFromBase(reader["EmploymentDate"]?.ToString() ?? string.Empty),
                                    reader["Salary"] != DBNull.Value ? Convert.ToDecimal(reader["Salary"]) : 0m,
                                    reader["PreferredTheme"]?.ToString() ?? string.Empty
                                );
                            }
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            return retVal;
        }

        public static Employee GetEmployeeByJMB(string jmbFind)
        {
            Employee retVal = null;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT e.JMB, LastName, FirstName, Address, DateOfBirth, Email, PhoneNumber, Salary, Username, PasswordString, EmploymentDate, RoleType, PreferredTheme FROM employee e NATURAL JOIN person WHERE JMB LIKE @JMB";
                        cmd.Parameters.AddWithValue("@JMB", jmbFind + "%");

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                retVal = new Employee(
                                    reader["FirstName"]?.ToString() ?? string.Empty,
                                    reader["LastName"]?.ToString() ?? string.Empty,
                                    reader["JMB"]?.ToString() ?? string.Empty,
                                    reader["Address"]?.ToString() ?? string.Empty,
                                    reader["Email"]?.ToString() ?? string.Empty,
                                    General.DateFromBase(reader["DateOfBirth"]?.ToString() ?? string.Empty),
                                     reader["PhoneNumber"]?.ToString() ?? string.Empty,
                                    reader["Username"]?.ToString() ?? string.Empty,
                                    reader["PasswordString"]?.ToString() ?? string.Empty,
                                    reader["RoleType"]?.ToString() ?? string.Empty,
                                   General.DateFromBase(reader["EmploymentDate"]?.ToString() ?? string.Empty),
                                    reader["Salary"] != DBNull.Value ? Convert.ToDecimal(reader["Salary"]) : 0m,
                                    reader["PreferredTheme"]?.ToString() ?? string.Empty
                                );
                            }
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            return retVal;
        }

        public static List<Employee> GetEmployeesByFirstOrLastName(string searchString)
        {
            List<Employee> result = new List<Employee>();
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
                            cmd.CommandText = "SELECT e.JMB, LastName, FirstName, Address, DateOfBirth, Email, PhoneNumber, Salary, Username, PasswordString, EmploymentDate, RoleType, PreferredTheme FROM employee e NATURAL JOIN person WHERE FirstName LIKE @searchTerm OR LastName LIKE @searchTerm";
                            cmd.Parameters.AddWithValue("@searchTerm", searchString + "%");
                        }
                        else
                        {
                            string firstName = parts.Length > 0 ? parts[0] : "";
                            string lastName = parts.Length > 1 ? parts[1] : "";

                            cmd.CommandText = @"
                                            SELECT e.JMB, LastName, FirstName, Address, DateOfBirth, Email, PhoneNumber, Salary, Username, PasswordString, EmploymentDate, RoleType, PreferredTheme FROM employee e NATURAL JOIN person 
                                            WHERE (FirstName LIKE @firstName AND LastName LIKE @lastName) 
                                            OR (FirstName LIKE @lastName AND LastName LIKE @firstName)";

                            cmd.Parameters.AddWithValue("@firstName", firstName + "%");
                            cmd.Parameters.AddWithValue("@lastName", lastName + "%");
                        }

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(new Employee(
                                    reader["FirstName"]?.ToString() ?? string.Empty,
                                    reader["LastName"]?.ToString() ?? string.Empty,
                                    reader["JMB"]?.ToString() ?? string.Empty,
                                    reader["Address"]?.ToString() ?? string.Empty,
                                    reader["Email"]?.ToString() ?? string.Empty,
                                     General.DateFromBase(reader["DateOfBirth"]?.ToString() ?? string.Empty),
                                     reader["PhoneNumber"]?.ToString() ?? string.Empty,
                                    reader["Username"]?.ToString() ?? string.Empty,
                                    reader["PasswordString"]?.ToString() ?? string.Empty,
                                    reader["RoleType"]?.ToString() ?? string.Empty,
                                   General.DateFromBase(reader["EmploymentDate"]?.ToString() ?? string.Empty),
                                    reader["Salary"] != DBNull.Value ? Convert.ToDecimal(reader["Salary"]) : 0m,
                                    reader["PreferredTheme"]?.ToString() ?? string.Empty
                                ));
                            }
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            return result;
        }

        public static List<Employee> GetAllEmployees()
        {
            List<Employee> result = new List<Employee>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT e.JMB, LastName, FirstName, Address, DateOfBirth, Email, PhoneNumber, Salary, Username, PasswordString, EmploymentDate, RoleType, PreferredTheme FROM employee e NATURAL JOIN person";
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(new Employee(
                                    reader["FirstName"]?.ToString() ?? string.Empty,
                                    reader["LastName"]?.ToString() ?? string.Empty,
                                    reader["JMB"]?.ToString() ?? string.Empty,
                                    reader["Address"]?.ToString() ?? string.Empty,
                                    reader["Email"]?.ToString() ?? string.Empty,
                                    General.DateFromBase(reader["DateOfBirth"]?.ToString() ?? string.Empty),
                                     reader["PhoneNumber"]?.ToString() ?? string.Empty,
                                    reader["Username"]?.ToString() ?? string.Empty,
                                    reader["PasswordString"]?.ToString() ?? string.Empty,
                                    reader["RoleType"]?.ToString() ?? string.Empty,
                                    General.DateFromBase(reader["EmploymentDate"]?.ToString() ?? string.Empty),
                                    reader["Salary"] != DBNull.Value ? Convert.ToDecimal(reader["Salary"]) : 0m,
                                    reader["PreferredTheme"]?.ToString() ?? string.Empty
                                ));
                            }
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            return result;
        }

        public static bool ChangeTheme(Employee emp, string theme)
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
                        cmd.CommandText = "change_theme";
                        cmd.Parameters.AddWithValue("@p_EmplyeeJMB", emp.Jmb);
                        cmd.Parameters["@p_EmplyeeJMB"].Direction = ParameterDirection.Input;
                        cmd.Parameters.AddWithValue("@p_NewTheme", theme);
                        cmd.Parameters["@p_NewTheme"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add("@successful", MySqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@message", MySqlDbType.VarChar, 255).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        successful = Convert.ToBoolean(cmd.Parameters["@successful"].Value);
                        string message = cmd.Parameters["@message"].Value?.ToString() ?? string.Empty;
                        /*
                        if (successful)
                        {
                           // MessageBox.Show("Theme changed successfully.");
                            emp.Theme = theme;
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

        public static bool AddEmployee(Employee employee, string phone)
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
                        cmd.CommandText = "add_sales_agent";
                        cmd.Parameters.AddWithValue("@pJMB", employee.Jmb);
                        cmd.Parameters["@pJMB"].Direction = ParameterDirection.Input;
                        cmd.Parameters.AddWithValue("@pFirstName", employee.FirstName);
                        cmd.Parameters["@pFirstName"].Direction = ParameterDirection.Input;
                        cmd.Parameters.AddWithValue("@pLastName", employee.LastName);
                        cmd.Parameters["@pLastName"].Direction = ParameterDirection.Input;
                        cmd.Parameters.AddWithValue("@pAddress", employee.Address);
                        cmd.Parameters["@pAddress"].Direction = ParameterDirection.Input;
                        cmd.Parameters.AddWithValue("@pDateOfBirth", General.DateForBase(employee.DateOfBirth));
                        cmd.Parameters["@pDateOfBirth"].Direction = ParameterDirection.Input;
                        cmd.Parameters.AddWithValue("@pEmail", employee.Email);
                        cmd.Parameters["@pEmail"].Direction = ParameterDirection.Input;
                        cmd.Parameters.AddWithValue("@pphone", phone);
                        cmd.Parameters["@pphone"].Direction = ParameterDirection.Input;
                        cmd.Parameters.AddWithValue("@pUsername", employee.Username);
                        cmd.Parameters["@pUsername"].Direction = ParameterDirection.Input;
                        cmd.Parameters.AddWithValue("@pPasswordString", employee.Password);
                        cmd.Parameters["@pPasswordString"].Direction = ParameterDirection.Input;
                        cmd.Parameters.AddWithValue("@pEmploymentDate", General.DateForBase(employee.EmploymentDate));
                        cmd.Parameters["@pEmploymentDate"].Direction = ParameterDirection.Input;
                        cmd.Parameters.AddWithValue("@pSalary", employee.Salary);
                        cmd.Parameters["@pSalary"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add("@successful", MySqlDbType.Bit);
                        cmd.Parameters["@successful"].Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add("@message", MySqlDbType.VarChar, 255);
                        cmd.Parameters["@message"].Direction = System.Data.ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        successful = Convert.ToBoolean(cmd.Parameters["@successful"].Value);
                        string message = cmd.Parameters["@message"].Value?.ToString() ?? string.Empty;
                        /*
                        if (successful)
                        {
                            MessageBox.Show("Employee added successfully.");
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

        public static bool UpdateEmployee(Employee employee, string jmb)
        {
            bool retVal = false;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE employee e 
                                            NATURAL JOIN person p
                                            SET 
                                                p.JMB = @NewJMB,
                                                p.FirstName = @FName, 
                                                p.LastName = @LName, 
                                                p.Address = @Address, 
                                                p.DateOfBirth = @DateOfBirth, 
                                                p.Email = @Email,
                                                p.PhoneNumber = @PhoneNumber,
                                                e.Salary = @Salary, 
                                                e.Username = @UName, 
                                                e.PasswordString = @Password, 
                                                e.EmploymentDate = @EmploymentDate, 
                                                e.RoleType = @RoleType, 
                                                e.PreferredTheme = @Theme
                                            WHERE e.JMB = @JMB";

                        cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                        cmd.Parameters.AddWithValue("@UName", employee.Username);
                        cmd.Parameters.AddWithValue("@Password", employee.Password);
                        cmd.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                        cmd.Parameters.AddWithValue("@EmploymentDate", General.DateForBase(employee.EmploymentDate));
                        cmd.Parameters.AddWithValue("@RoleType", employee.RoleType);
                        cmd.Parameters.AddWithValue("@Theme", employee.Theme);
                        cmd.Parameters.AddWithValue("@JMB", jmb);
                        cmd.Parameters.AddWithValue("@NewJMB", employee.Jmb);
                        cmd.Parameters.AddWithValue("@FName", employee.FirstName);
                        cmd.Parameters.AddWithValue("@LName", employee.LastName);
                        cmd.Parameters.AddWithValue("@Address", employee.Address);
                        cmd.Parameters.AddWithValue("@DateOfBirth", General.DateForBase(employee.DateOfBirth));
                        cmd.Parameters.AddWithValue("@Email", employee.Email);
                        retVal = cmd.ExecuteNonQuery() >= 1;
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine($"Error: {e.Message}");

            }
            return retVal;
        }

        public static bool DeleteEmployee(string jmb)
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
                        cmd.CommandText = "delete_employee";
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
                Console.WriteLine($"Error: {e.Message}");
            }
            return successful;
        }

        public static bool ChangePassword(string jmb, string password)
        {
            bool retVal = false;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE employee
                                            SET PasswordString = @Password
                                            WHERE JMB = @JMB";

                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.Parameters.AddWithValue("@JMB", jmb);
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

    }
}
