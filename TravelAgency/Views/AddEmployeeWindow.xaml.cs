using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using TravelAgency.Models;
using TravelAgency.Util;

namespace TravelAgency.Views
{
    /// <summary>
    /// Interaction logic for AddEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        public Employee Employee { get; set; }


        public AddEmployeeWindow()
        {
            InitializeComponent();
            Employee = new Employee();
            DataContext = this;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(FirstName.Text) || string.IsNullOrEmpty(Address.Text) || string.IsNullOrEmpty(LastName.Text) ||
               string.IsNullOrEmpty(Email.Text) || string.IsNullOrEmpty(PhoneNumber.Text) || string.IsNullOrEmpty(JMB.Text)
                || string.IsNullOrEmpty(Salary.Text) || string.IsNullOrEmpty(Username.Text) || string.IsNullOrEmpty(DateOfBirth.Text))

            {
                string message = (string)Application.Current.Resources["EmptyField"];
                MessageWithoutOptionDialog dialog = new MessageWithoutOptionDialog(message);
                dialog.ShowDialog();
            }
            else
            {
                Employee = new Employee(FirstName.Text, LastName.Text, JMB.Text, Address.Text, Email.Text, DateOfBirth.Text, PhoneNumber.Text,
                    Username.Text, General.HashPassword("pass123"), "salesAgent", DateTime.Now.ToString("dd.MM.yyyy."), decimal.Parse(Salary.Text, System.Globalization.CultureInfo.InvariantCulture), "default");
                DialogResult = true;
                Close();

            }


        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
