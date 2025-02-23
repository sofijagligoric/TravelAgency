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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using TravelAgency.Models;

namespace TravelAgency.Views
{
    /// <summary>
    /// Interaction logic for UpdateEmployeeWindow.xaml
    /// </summary>
    public partial class UpdateEmployeeWindow : Window
    {
        public Employee Employee { get; set; }


        public UpdateEmployeeWindow(Employee employee1)
        {
            InitializeComponent();
            Employee = new Employee(employee1);
            DataContext = this;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(FirstName.Text) || string.IsNullOrEmpty(Address.Text) || string.IsNullOrEmpty(LastName.Text) ||
               string.IsNullOrEmpty(Email.Text) || string.IsNullOrEmpty(PhoneNumber.Text) || string.IsNullOrEmpty(Username.Text) || string.IsNullOrEmpty(Salary.Text) || string.IsNullOrEmpty(JMB.Text) || string.IsNullOrEmpty(DateOfBirth.Text))
            // || string.IsNullOrEmpty(HasRestaurant.Text))
            {
                string message = (string)Application.Current.Resources["EmptyField"];
                MessageWithoutOptionDialog dialog = new MessageWithoutOptionDialog(message);
                dialog.ShowDialog();
            }
            else
            {
                Employee.FirstName = FirstName.Text;
                Employee.LastName = LastName.Text;
                Employee.Address = Address.Text;
                Employee.PhoneNumber = PhoneNumber.Text;
                Employee.Email = Email.Text;
                Employee.DateOfBirth = DateOfBirth.Text;
                Employee.Salary = decimal.Parse(Salary.Text, System.Globalization.CultureInfo.InvariantCulture);
                Employee.Username = Username.Text;
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
