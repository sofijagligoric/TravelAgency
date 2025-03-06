using Mysqlx.Datatypes;
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
using TravelAgency.DataAccess;
using TravelAgency.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace TravelAgency.Views
{
    /// <summary>
    /// Interaction logic for AddReservationWindow.xaml
    /// </summary>
    public partial class AddReservationWindow : Window
    {
        public Package Package { get; set; }
        public Reservation Reservation { get; set; }
        public List<Hotel> Hotels { get; set; }
       

        private bool _isOptionYes;

        public bool IsOptionYes
        {
            get => _isOptionYes;
            set
            {
                _isOptionYes = value;
                OnPropertyChanged(nameof(IsOptionYes));
            }
        }

        public AddReservationWindow(Package package)
        {
            InitializeComponent();
            DataContext = this;
            Package = new Package(package);
            Reservation = new Reservation();
            //   Hotels = PackageOffersHotelDataAccess.GetHotelsByPackage(Package.PackageId);
            Hotels = PackageOffersHotelDataAccess.GetHotelsByPackage(Package.PackageId)
                .Where(hotel => hotel.RoomCount > 0)
                .ToList();

        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {

            AddCustomer();
        }

        private void AddCustomer()
        {
            AddCustomerWindow dialog = new AddCustomerWindow();

            bool? dialogResult = dialog.ShowDialog();

            if ((bool)dialogResult)
            {
                Customer pom = dialog.Customer;
                string message2 = (string)Application.Current.Resources["ConfirmAdd"] + ": " + pom + "?";
                MessageDialog dialog2 = new MessageDialog(message2);
                bool? dialogResult2 = dialog2.ShowDialog();
                if ((bool)dialogResult2)
                {
                    if (CustomerDataAccess.AddCustomer(pom, dialog.Customer.PhoneNumber))
                    {
                        
                        string message = (string)Application.Current.Resources["SuccessfullyAdded"];
                        MessageWithoutOptionDialog dialog3 = new MessageWithoutOptionDialog(message + " " + pom);
                        dialog3.ShowDialog();
                    }
                }

            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (HotelComboBox.SelectedItem == null)
            {
                string message = (string)Application.Current.Resources["RowNotSelected"];
                MessageWithoutOptionDialog dialog = new MessageWithoutOptionDialog(message);
                dialog.ShowDialog();
            }
            else
            {
                if (string.IsNullOrEmpty(PackageId.Text) || string.IsNullOrEmpty(Price.Text) || string.IsNullOrEmpty(CustomerJmb.Text))
                {
                    string message = (string)Application.Current.Resources["EmptyField"];
                    MessageWithoutOptionDialog dialog = new MessageWithoutOptionDialog(message);
                    dialog.ShowDialog();
                }
                else
                {
                    if (HasValidationError(Price) || HasValidationError(CustomerJmb))
                    {
                        string message = (string)Application.Current.Resources["InvalidInput"];
                        MessageWithoutOptionDialog dialog = new MessageWithoutOptionDialog(message);
                        dialog.ShowDialog();
                    }
                    else
                    {
                        Customer customer = CustomerDataAccess.GetCustomerByJMB(CustomerJmb.Text);
                        if (customer == null)
                        {
                            string message = (string)Application.Current.Resources["UnknownCustomer"];
                            MessageDialog dialog = new MessageDialog(message);
                            bool? dialogResult = dialog.ShowDialog();
                            if ((bool)dialogResult)
                            {
                                AddCustomer();
                            }
                        }
                        else
                        {

                            Reservation = new Reservation(1, decimal.Parse(Price.Text, System.Globalization.CultureInfo.InvariantCulture),
                               (HotelComboBox.SelectedItem as Hotel), Package, customer, (byte)(IsOptionYes ? 1 : 0), SalesAgentWindow.EmployeeJmb);
                            DialogResult = true;
                            Close();
                        }

                    }
                }
            }
        }

        private bool HasValidationError(Control control)
        {
            return Validation.GetHasError(control);
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
