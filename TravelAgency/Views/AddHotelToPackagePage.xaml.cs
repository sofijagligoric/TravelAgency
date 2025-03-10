using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TravelAgency.DataAccess;
using TravelAgency.Models;

namespace TravelAgency.Views
{
    /// <summary>
    /// Interaction logic for AddHotelToPackagePage.xaml
    /// </summary>
    public partial class AddHotelToPackagePage : Page
    {
        private PackageHotelsWindow _mainWindow;
        public ObservableCollection<Hotel> DestinationHotels { get; set; }
        public Package Package { get; set; }

        public AddHotelToPackagePage(PackageHotelsWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            DataContext = this;
            Package = _mainWindow.Package;
            List<Hotel> hotelsInPackage = PackageOffersHotelDataAccess.GetHotelsByPackage(Package.PackageId);
            List<Hotel> dHotels = HotelDataAccess.GetHotelsByDestinationName(Package.Destination.DestinationName);
            dHotels.RemoveAll(hotel => hotelsInPackage.Contains(hotel));
            DestinationHotels = new ObservableCollection<Hotel>(dHotels);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.MainFrame.Content = new PackageHotelsPage(_mainWindow);
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
                PackageOffersHotel pom = new PackageOffersHotel(Package.PackageId, (HotelComboBox.SelectedItem as Hotel).HotelId);
                string message2 = (string)Application.Current.Resources["ConfirmAdd"] + ": " + (HotelComboBox.SelectedItem as Hotel).Name + "?";
                MessageDialog dialog2 = new MessageDialog(message2);
                bool? dialogResult2 = dialog2.ShowDialog();
                if ((bool)dialogResult2)
                {
                    if (PackageOffersHotelDataAccess.AddPackageOffersHotel(pom))
                    {
                        string message = (string)Application.Current.Resources["SuccessfullyAdded"];
                        MessageWithoutOptionDialog dialog3 = new MessageWithoutOptionDialog(message + " " + pom);
                        dialog3.ShowDialog();
                    }
                    else
                    {
                        string message3 = (string)Application.Current.Resources["FailedAdd"];
                        MessageWithoutOptionDialog dialog3 = new MessageWithoutOptionDialog(message3);
                        dialog3.ShowDialog();
                    }
                }
                else
                {
                    string message3 = (string)Application.Current.Resources["ActionCanceled"];
                    MessageWithoutOptionDialog dialog3 = new MessageWithoutOptionDialog(message3);
                    dialog3.ShowDialog();
                }
                _mainWindow.MainFrame.Content = new PackageHotelsPage(_mainWindow);
            }
        }



        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {

            AddHotel();
        }

        private void AddHotel()
        {
            AddHotelWindow dialog = new AddHotelWindow();

            bool? dialogResult = dialog.ShowDialog();

            if ((bool)dialogResult)
            {
                Hotel pom = dialog.Hotel;
                string message2 = (string)Application.Current.Resources["ConfirmAdd"] + ": " + pom + "?";
                MessageDialog dialog2 = new MessageDialog(message2);
                bool? dialogResult2 = dialog2.ShowDialog();
                if ((bool)dialogResult2)
                {
                    if (HotelDataAccess.AddHotel(pom))
                    {
                        DestinationHotels.Add(pom);
                        OnPropertyChanged(nameof(DestinationHotels));
                        string message = (string)Application.Current.Resources["SuccessfullyAdded"];
                        MessageWithoutOptionDialog dialog3 = new MessageWithoutOptionDialog(message);
                        dialog3.ShowDialog();
                    }
                    else
                    {
                        string message3 = (string)Application.Current.Resources["FailedAdd"];
                        MessageWithoutOptionDialog dialog3 = new MessageWithoutOptionDialog(message3);
                        dialog3.ShowDialog();
                    }
                }
                else
                {
                    string message3 = (string)Application.Current.Resources["ActionCanceled"];
                    MessageWithoutOptionDialog dialog3 = new MessageWithoutOptionDialog(message3);
                    dialog3.ShowDialog();
                }

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
