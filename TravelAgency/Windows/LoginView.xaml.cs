using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TravelAgency.DataAccess;
using TravelAgency.Models;

namespace TravelAgency.Windows
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            {

                Employee? emp = EmployeeDataAccess.CheckLoginInfo(username, password);

                if (emp != null)
                {
                    
                    if("admin".Equals(emp.RoleType))
                    {
                        AdminWindow adminWindow = new AdminWindow(emp);
                        adminWindow.Show();
                    }
                    else if ("salesAgent".Equals(emp.RoleType))
                    {
                        SalesAgentWindow saWindow = new SalesAgentWindow();
                        saWindow.Show();
                    }
                    else
                    {
                        MessageBox.Show("Nepoznat tip naloga.", "Greška", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    this.Close();


                }
                else
                {
                       incorrectData.SetResourceReference(TextBlock.TextProperty, "IncorrectPassword");
                }

            }
            else
            {
                incorrectData.SetResourceReference(TextBlock.TextProperty, "EmptyPasswordOrUsernameField");
            }
        }

        private void Window_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimise_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }

}
