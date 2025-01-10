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

namespace TravelAgency.Windows
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            AdminEmployeesPage adminHome = new AdminEmployeesPage();
            MainFrame.Navigate(adminHome);
        }

        private void MainFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }

        private void btnMinimise_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginView loginWindow = new LoginView();
            loginWindow.Show();
            this.Close();
        }

        private void ThemeMenu_Click(object sender, RoutedEventArgs e)
        {
            var currentTheme = Application.Current.Resources.MergedDictionaries[0].Source.ToString();
            if (currentTheme.EndsWith("Themes/DarkTheme.xaml"))
                SetTheme("Themes/BlueTheme.xaml");
            else
                SetTheme("Themes/DarkTheme.xaml");
        }

        private void Blue_Click(object sender, RoutedEventArgs e)
        {
            var currentTheme = Application.Current.Resources.MergedDictionaries[0].Source.ToString();
            if (!currentTheme.EndsWith("Themes/BlueTheme.xaml"))
                SetTheme("Themes/BlueTheme.xaml");
        }

        private void Dark_Click(object sender, RoutedEventArgs e)
        {
            var currentTheme = Application.Current.Resources.MergedDictionaries[0].Source.ToString();
            if (!currentTheme.EndsWith("Themes/DarkTheme.xaml"))
                SetTheme("Themes/DarkTheme.xaml");
        }

        private void Burgundy_Click(object sender, RoutedEventArgs e)
        {
            var currentTheme = Application.Current.Resources.MergedDictionaries[0].Source.ToString();
            if (!currentTheme.EndsWith("Themes/BurgundyTheme.xaml"))
                SetTheme("Themes/BurgundyTheme.xaml");
        }

        private void SetTheme(string theme)
        {
            /*
            var uri = new Uri(theme, UriKind.Relative);
            var resourceDictionary = new ResourceDictionary { Source = uri };
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
            */
            var uri = new Uri(theme, UriKind.Relative);
            var resourceDictionary = new ResourceDictionary { Source = uri };

            // Ukloni prethodne teme samo ako su iste vrste (npr. završavaju se sa "Theme.xaml")
            var oldTheme = Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source != null && d.Source.ToString().EndsWith("Theme.xaml"));

            if (oldTheme != null)
            {
                Application.Current.Resources.MergedDictionaries.Remove(oldTheme);
            }

            // Dodaj novu temu na kraj
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
        }

        private void EmployeesRadioButton_Checked(object sender, RoutedEventArgs e)
        {
                AdminEmployeesPage adminHome = new AdminEmployeesPage();
                MainFrame.Navigate(adminHome);
          
        }

        private void ReservationsRadioButton_Checked(object sender, RoutedEventArgs e)
        {
           
                AdminReservationsPage adminRes = new AdminReservationsPage();
                MainFrame.Navigate(adminRes);
         
        }

    }
}
