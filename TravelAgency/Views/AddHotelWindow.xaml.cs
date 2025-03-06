using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using TravelAgency.DataAccess;
using TravelAgency.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace TravelAgency.Views
{
    /// <summary>
    /// Interaction logic for AddHotelWindow.xaml
    /// </summary>
    public partial class AddHotelWindow : Window
    {

        //public Hotel Hotel { get; set; } = null;
        public Hotel Hotel { get; set; }
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

        public AddHotelWindow()
        {
            InitializeComponent();
            Hotel = new Hotel();
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
                        string message = (string)Application.Current.Resources["UnknownDestination"];
                        MessageDialog dialog = new MessageDialog(message);
                        bool? dialogResult = dialog.ShowDialog();
                        if ((bool)dialogResult)
                        {
                            AddDestination();
                        }
                    }
                    else
                    {
                        Hotel = new Hotel(1, int.Parse(RoomCount.Text), Name.Text, Address.Text, Email.Text, (byte)(IsOptionYes ? 1 : 0), dest);
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
        private void AddDestination()
        {
            AddDestinationWindow dialog = new AddDestinationWindow();

            bool? dialogResult = dialog.ShowDialog();

            if ((bool)dialogResult)
            {
                Destination pom = dialog.Destination;
                string message2 = (string)Application.Current.Resources["ConfirmAdd"] + ": " + pom + "?";
                MessageDialog dialog2 = new MessageDialog(message2);
                bool? dialogResult2 = dialog2.ShowDialog();
                if ((bool)dialogResult2)
                {
                    if (DestinationDataAccess.AddDestination(pom))
                    {
                        string message = (string)Application.Current.Resources["SuccessfullyAdded"];
                        MessageWithoutOptionDialog dialog3 = new MessageWithoutOptionDialog(message + " " + pom);
                        dialog3.ShowDialog();
                    }
                }
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

        /*
        private void LetterNumberCheck(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                string input = textBox.Text;
                if (!Regex.IsMatch(input, "^[a-zA-Z0-9 ]*$"))
                {
                    textBox.Background = Brushes.LightCoral;
                }
            }
        }
        */
    }
}
