using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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
using TravelAgency.ViewModels;

namespace TravelAgency.Views
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private Employee user;

        public Employee User
        {
            get { return user; }
        }



        public AdminWindow(Employee emp)
        {
            InitializeComponent();
            this.user = emp;
            if (emp.Theme == "tamna")
            {
                ThemeComboBox.SelectedIndex = 1;
                SetTheme("Themes/DarkTheme.xaml");
            }
            else if (emp.Theme == "bordo")
            {
                ThemeComboBox.SelectedIndex = 2;
                SetTheme("Themes/BurgundyTheme.xaml");
            }
            else
            {
                ThemeComboBox.SelectedIndex = 0;
                SetTheme("Themes/BlueTheme.xaml");
            }
            var navigationService = new NavigationService(MainContent);
            DataContext = new AdminViewModel(navigationService);
        }


        private void btnMinimise_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnMaximise_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else
                this.WindowState = WindowState.Normal;
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

        private void Theme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string theme = selectedItem.Tag.ToString();
                switch (theme)
                {
                    case "Blue":
                        Blue_Click(sender, e);
                        break;
                    case "Dark":
                        Dark_Click(sender, e);
                        break;
                    case "Burgundy":
                        Burgundy_Click(sender, e);
                        break;
                }
            }
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
            {
                EmployeeDataAccess.ChangeTheme(user, "default");
                SetTheme("Themes/BlueTheme.xaml");
            }    
        }

        private void Dark_Click(object sender, RoutedEventArgs e)
        {
            var currentTheme = Application.Current.Resources.MergedDictionaries[0].Source.ToString();
            if (!currentTheme.EndsWith("Themes/DarkTheme.xaml"))
            {
                EmployeeDataAccess.ChangeTheme(user, "tamna");
                SetTheme("Themes/DarkTheme.xaml");
            }
        }

        private void Burgundy_Click(object sender, RoutedEventArgs e)
        {
            var currentTheme = Application.Current.Resources.MergedDictionaries[0].Source.ToString();
            if (!currentTheme.EndsWith("Themes/BurgundyTheme.xaml"))
            {
                EmployeeDataAccess.ChangeTheme(user, "bordo");
                SetTheme("Themes/BurgundyTheme.xaml");
            }
        }

        private void SetTheme(string theme)
        {
            var uri = new Uri(theme, UriKind.Relative);
            var resourceDictionary = new ResourceDictionary { Source = uri };

            var oldTheme = Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source != null && d.Source.ToString().EndsWith("Theme.xaml"));

            if (oldTheme != null)
            {
                Application.Current.Resources.MergedDictionaries.Remove(oldTheme);
            }

            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);

            /*
            var adminWindow = Application.Current.MainWindow as AdminWindow;
            Console.WriteLine("------------------------>  MAIN WINDOW JE NULL" );
            
            if (adminWindow != null)
            {
                Console.WriteLine("------------------------>  MAIN WINDOW NIJE NULL");
                var themeComboBox = adminWindow.FindName("ThemeComboBox") as ComboBox;
                Console.WriteLine("------------------------>  THEMEBOX JE NULL");
                if (themeComboBox != null)
                {
                    Console.WriteLine("------------------------>  THEMEBOX NIJE NULL");
                    string themeName = theme.Split('/')[1].Replace("Theme.xaml", "");
                    Console.WriteLine("------------------------>" + themeName);
                    foreach (ComboBoxItem item in themeComboBox.Items)
                    {
                        if (item.Tag != null && item.Tag.ToString() == themeName)
                        {
                            themeComboBox.SelectedItem = item;
                            break;
                        }
                    }
                }
            }
            */
        }

        private void EmployeesRadioButton_Checked(object sender, RoutedEventArgs e)
        {
                AdminEmployeesPage adminHome = new AdminEmployeesPage();    
        }

        private void Set_English_Lang(object sender, RoutedEventArgs e)
        {
            var oldLang = Application.Current.Resources.MergedDictionaries
                 .FirstOrDefault(d => d.Source != null && d.Source.ToString().EndsWith("Language.xaml")).ToString();
            if (!oldLang.EndsWith("Languages/EnglishLanguage.xaml"))
                SetLang("Languages/EnglishLanguage.xaml");
        }

        private void Set_Serbian_Lang(object sender, RoutedEventArgs e)
        {
            var oldLang = Application.Current.Resources.MergedDictionaries
               .FirstOrDefault(d => d.Source != null && d.Source.ToString().EndsWith("Language.xaml")).ToString();
            if (!oldLang.EndsWith("Languages/SerbianLanguage.xaml"))
                 SetLang("Languages/SerbianLanguage.xaml");
        }

        private void SetLang(string lang)
        {
          
            var uri = new Uri(lang, UriKind.Relative);
            var resourceDictionary = new ResourceDictionary { Source = uri };
            var oldLang = Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source != null && d.Source.ToString().EndsWith("Language.xaml"));

            if (oldLang != null)
            {
                 Application.Current.Resources.MergedDictionaries.Remove(oldLang);
            }

            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
           
        }
    }
}
