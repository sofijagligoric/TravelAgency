using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TravelAgency.DataAccess;
using TravelAgency.Models;
using TravelAgency.Views;

namespace TravelAgency.ViewModels
{
    public class HotelViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<Hotel> Hotels { get; set; }

        private Hotel _selectedHotel;

        public Hotel SelectedHotel
        {
            get => _selectedHotel;
            set
            {
                if (_selectedHotel != value)
                {
                    _selectedHotel = value;
                    OnPropertyChanged(nameof(SelectedHotel));
                }
            }
        }


        public ICommand AddHotelCommand { get; }
        public ICommand DeleteHotelCommand { get; }
        public ICommand UpdateHotelCommand { get; }
        public ICommand SearchHotelsCommand { get; }
        public ICommand AllHotelsCommand { get; }
        public ICommand DeselectHotelCommand { get; }

        public HotelViewModel()
        {
            var hotels = HotelDataAccess.GetAllHotels();

            if (hotels == null)
            {
                Console.WriteLine("Hotels list is null! Check database connection.");
                hotels = new List<Hotel>();
            }

            Hotels = new ObservableCollection<Hotel>(hotels);

            AddHotelCommand = new RelayCommand(AddHotel);
            DeleteHotelCommand = new RelayCommand(DeleteHotel);
            UpdateHotelCommand = new RelayCommand(UpdateHotel);
            SearchHotelsCommand = new RelayCommand(param => SearchHotels(param.ToString()));
            AllHotelsCommand = new RelayCommand(AllHotels);
            DeselectHotelCommand = new RelayCommand(DeselectHotel);
        }

        private void DeselectHotel()
        {
            SelectedHotel = null;
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
                        Hotels.Add(pom);
                        OnPropertyChanged(nameof(Hotels));
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

            }
        }

        private void AllHotels()
        {
            var hotels = HotelDataAccess.GetAllHotels();
            if (hotels == null)
            {
                Console.WriteLine("Hotels list is null! Check database connection.");
                hotels = new List<Hotel>();
            }
            else
            {
                Hotels = new ObservableCollection<Hotel>(hotels);
                OnPropertyChanged(nameof(Hotels));
            }
        }

        private void SearchHotels(string destName)
        {
            if (destName.Trim().Length == 0)

            {
                this.AllHotels();
                return;
            }
            var foundHotels = HotelDataAccess.GetHotelsByDestinationName(destName);
            if (foundHotels == null)
            {
                Console.WriteLine("Search is null.");
            }
            else
            {
                Hotels = new ObservableCollection<Hotel>(foundHotels);
                OnPropertyChanged(nameof(Hotels));
            }
        }

        private void DeleteHotel(object obj)
        {
            if (SelectedHotel == null)
            {
                string message = (string)Application.Current.Resources["RowNotSelected"];
                MessageWithoutOptionDialog dialog = new MessageWithoutOptionDialog(message);
                dialog.ShowDialog();

                return;
            }


            string message2 = (string)Application.Current.Resources["ConfirmDelete"] + " " + SelectedHotel.Name + "?";
            MessageDialog dialog2 = new MessageDialog(message2);
            bool? dialogResult2 = dialog2.ShowDialog();


            if ((bool)dialogResult2)
            {
                if (HotelDataAccess.DeleteHotel(SelectedHotel))
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

        private void UpdateHotel()
        {

            if (SelectedHotel != null)
            {
                UpdateHotelWindow dialog = new UpdateHotelWindow(SelectedHotel);

                bool? dialogResult = dialog.ShowDialog();

                if ((bool)dialogResult)
                {
                    Hotel pom = dialog.Hotel;
                    string message2 = (string)Application.Current.Resources["ConfirmUpdate"] + ": " + pom + "?";
                    MessageDialog dialog2 = new MessageDialog(message2);
                    bool? dialogResult2 = dialog2.ShowDialog();
                    if ((bool)dialogResult2)
                    {
                        if (HotelDataAccess.UpdateHotel(pom))
                        {
                            SelectedHotel.Name = pom.Name;
                            SelectedHotel.Address = pom.Address;
                            SelectedHotel.Email = pom.Email;
                            SelectedHotel.Destination = pom.Destination;
                            SelectedHotel.HasRestaurant = pom.HasRestaurant;
                            SelectedHotel.RoomCount = pom.RoomCount;

                            OnPropertyChanged(nameof(SelectedHotel));
                            OnPropertyChanged(nameof(Hotels));
                            string message3 = (string)Application.Current.Resources["SuccessfulUpdate"];
                            MessageWithoutOptionDialog dialog3 = new MessageWithoutOptionDialog(message3);
                            dialog3.ShowDialog();
                        }
                        else
                        {
                            string message3 = (string)Application.Current.Resources["FailedUpdate"];
                            MessageWithoutOptionDialog dialog3 = new MessageWithoutOptionDialog(message3);
                            dialog3.ShowDialog();
                        }
                    }

                }
            }
            else
            {
                string message = (string)Application.Current.Resources["RowNotSelected"];
                MessageWithoutOptionDialog dialog = new MessageWithoutOptionDialog(message);
                dialog.ShowDialog();
            }
        }

        private bool CanModify()
        {
            return SelectedHotel != null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
