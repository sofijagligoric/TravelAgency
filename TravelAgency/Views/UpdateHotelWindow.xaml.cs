using Org.BouncyCastle.Utilities;
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

namespace TravelAgency.Views
{
    /// <summary>
    /// Interaction logic for UpdateHotelWindow.xaml
    /// </summary>
    /// 

    public partial class UpdateHotelWindow : Window
    {
        public Hotel Hotel { get; set; }

        /*
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
        */
        public UpdateHotelWindow(Hotel hotel1)
        {
            InitializeComponent();
            Hotel = new Hotel(hotel1);
            DataContext = this;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Name.Text) || string.IsNullOrEmpty(Address.Text) || string.IsNullOrEmpty(Postcode.Text) ||
               string.IsNullOrEmpty(Email.Text) || string.IsNullOrEmpty(RoomCount.Text) || string.IsNullOrEmpty(DestinationName.Text))
            {
                string message = (string)Application.Current.Resources["EmptyField"];
                MessageWithoutOptionDialog dialog = new MessageWithoutOptionDialog(message);
                dialog.ShowDialog();
            }
            else
            {
                if (HasValidationError(Name) || HasValidationError(Postcode) ||
                       HasValidationError(Email) || HasValidationError(RoomCount) ||
                       HasValidationError(DestinationName))
                {
                    string message = (string)Application.Current.Resources["InvalidInput"];
                    MessageWithoutOptionDialog dialog = new MessageWithoutOptionDialog(message);
                    dialog.ShowDialog();
                }
                else
                {
                    Destination dest = DestinationDataAccess.GetDestinationByNameAndPostcode(DestinationName.Text, Postcode.Text);
                    if (dest == null)
                    {
                        string message = (string)Application.Current.Resources["UnknownEntry"];
                        MessageWithoutOptionDialog dialog = new MessageWithoutOptionDialog(message + " " + DestinationName.Text + ", " + Postcode.Text);
                        dialog.ShowDialog();
                    }
                    else
                    {
                        // Hotel = new Hotel(int.Parse(HotelId.Text), int.Parse(RoomCount.Text), Name.Text, Address.Text, Email.Text, (byte)(HasRestaurant.Text.Equals("yes") ? 1 : 0), dest);
                        Hotel.Name = Name.Text;
                        Hotel.Address = Address.Text;
                        Hotel.Email = Email.Text;
                        Hotel.HasRestaurant = (byte)((bool)rbYes.IsChecked ? 1 : 0);
                        Hotel.Destination = dest;
                        Hotel.RoomCount = int.Parse(RoomCount.Text);
                        DialogResult = true;
                        Close();
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
