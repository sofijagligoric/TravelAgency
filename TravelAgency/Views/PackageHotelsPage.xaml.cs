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
    /// Interaction logic for PackageHotelsPage.xaml
    /// </summary>
    public partial class PackageHotelsPage : Page
    {
        private PackageHotelsWindow _mainWindow;
        public Package Package { get; set; }
        public ObservableCollection<Hotel> Hotels { get; set; }

        private Hotel _selectedHotel;
        public Hotel SelectedHotel
        {
            get => _selectedHotel;
            set
            {
                _selectedHotel = value;
                OnPropertyChanged(nameof(SelectedHotel));
            }
        }

        public PackageHotelsPage(PackageHotelsWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            DataContext = this;
            Package = _mainWindow.Package;
            List<Hotel> HotelsList = PackageOffersHotelDataAccess.GetHotelsByPackage(Package.PackageId);
            Hotels = new ObservableCollection<Hotel>(HotelsList);
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedHotel == null)
            {
                string message = (string)Application.Current.Resources["RowNotSelected"];
                MessageWithoutOptionDialog dialog = new MessageWithoutOptionDialog(message);
                dialog.ShowDialog();

            }

            else
            {
                string message2 = (string)Application.Current.Resources["ConfirmDelete"] + " " + SelectedHotel.Name + "?";
                MessageDialog dialog2 = new MessageDialog(message2);
                bool? dialogResult2 = dialog2.ShowDialog();


                if ((bool)dialogResult2)
                {
                    if (PackageOffersHotelDataAccess.DeletePackageOffersHotel(Package.PackageId, SelectedHotel.HotelId))
                    {
                        Hotels.Remove(SelectedHotel);
                        OnPropertyChanged(nameof(Hotels));
                        string message = (string)Application.Current.Resources["SuccessfulDelete"];
                        MessageWithoutOptionDialog dialog = new MessageWithoutOptionDialog(message);
                        dialog.ShowDialog();

                    }
                    else
                    {

                        string message = (string)Application.Current.Resources["FailedDelete"];
                        MessageWithoutOptionDialog dialog = new MessageWithoutOptionDialog(message);
                        dialog.ShowDialog();
                    }
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.MainFrame.Content = new AddHotelToPackagePage(_mainWindow);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
