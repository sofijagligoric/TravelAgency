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
        //  public Hotel SelectedHotel { get; set; } = new Hotel();
        public Hotel NewHotel { get; set; }
        private Hotel _selectedHotel;
        private Hotel _backupHotel; 

        public Hotel SelectedHotel
        {
            get => _selectedHotel;
            set
            {
                if (_selectedHotel != value)
                {
                    _backupHotel = value != null ? new Hotel(value) : null;  
                    _selectedHotel = value;
                    OnPropertyChanged(nameof(SelectedHotel));
                }
            }
        }

        /*
        private string _messageLabel;
        public string MessageLabel
        {
            get { return _messageLabel; }
            set
            {
                _messageLabel = value;
                OnPropertyChanged(nameof(MessageLabel)); 
            }
        }

        private Brush _messageTextColor = Brushes.Red;  
        public Brush MessageLabelTextColor
        {
            get { return _messageTextColor; }
            set
            {
                _messageTextColor = value;
                OnPropertyChanged(nameof(MessageLabelTextColor));
            }
        }
        */

        public ICommand AddHotelCommand { get; }
        public ICommand DeleteHotelCommand { get; }
        public ICommand UpdateHotelCommand { get; }
        public ICommand SearchHotelsCommand { get; }
        public ICommand AllHotelsCommand { get; }

        public HotelViewModel()
        {
            var hotels = HotelDataAccess.GetAllHotels();
            if (hotels == null)
            {
                Console.WriteLine("Hotels list is null! Check database connection.");
                hotels = new List<Hotel>();  
            }

            Hotels = new ObservableCollection<Hotel>(hotels);
            NewHotel = new Hotel();
            AddHotelCommand = new RelayCommand(AddHotel);
            DeleteHotelCommand = new RelayCommand(DeleteHotel);
            UpdateHotelCommand = new RelayCommand(UpdateHotel);
            SearchHotelsCommand = new RelayCommand(param => SearchHotels(param.ToString()));
            AllHotelsCommand = new RelayCommand(AllHotels);
        }

        private void AddHotel()
        {
            AddHotelWindow dialog = new AddHotelWindow();

            bool? dialogResult =dialog.ShowDialog();

            if ((bool)dialogResult)
            {
                Hotel pom = dialog.Hotel;
                if (HotelDataAccess.AddHotel(pom))
                {
                    Hotels.Add(pom);
                    OnPropertyChanged(nameof(Hotels));
                    string message = (string)Application.Current.Resources["SuccessfullyAdded"];
                    MessageWithoutOptionDialog dialog2 = new MessageWithoutOptionDialog(message + " " + pom);
                    dialog2.ShowDialog();
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
            var foundHotels = HotelDataAccess.GetHotelsByDestinationName(destName);
            if (foundHotels == null)
            {
                MessageBox.Show("No result");
              //  hotels = new List<Hotel>(); 
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
                //  MessageBox.Show("Please select a row you want to delete.");
                /*
                MessageLabel = "Please select a row you want to delete.";
                MessageLabelTextColor = Brushes.Red;
                */
                string message = (string)Application.Current.Resources["RowNotSelected"];
                MessageWithoutOptionDialog dialog = new MessageWithoutOptionDialog(message);   
                dialog.ShowDialog();
               
                return;
            }
            /*
            var result = MessageBox.Show($"Are you sure you want to delete {SelectedHotel.Name}?",
                                         "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                if (HotelDataAccess.DeleteHotel(SelectedHotel))
                {
                    Hotels.Remove(SelectedHotel); 
                    OnPropertyChanged(nameof(Hotels));
                    /*
                    MessageLabel = "Action completed successfully.";
                    MessageLabelTextColor = Brushes.Green;
                    */
     /*   }
                else
                {
                    MessageBox.Show("Failed to delete hotel.");
                }
}
*/

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
                    /*
                    MessageLabel = "Action completed successfully.";
                    MessageLabelTextColor = Brushes.Green;
                    */
                }
                else
                {
                    // MessageBox.Show("Failed to delete hotel.");
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
                /*
                string message1 = (string)Application.Current.Resources["ConfirmUpdate"] + " " + SelectedHotel.Name + "?";
                MessageDialog dialog1 = new MessageDialog(message1);
                bool? dialogResult1 = dialog1.ShowDialog();

                if ((bool)dialogResult1)
                {
                    bool success = HotelDataAccess.UpdateHotel(SelectedHotel);
                    if (success)
                    {
                        string message2 = (string)Application.Current.Resources["SuccessfulUpdate"];
                        MessageWithoutOptionDialog dialog2 = new MessageWithoutOptionDialog(message2);
                        dialog2.ShowDialog();
                    }
                    else
                    {
                        string message3 = (string)Application.Current.Resources["FailedUpdate"];
                        MessageWithoutOptionDialog dialog3 = new MessageWithoutOptionDialog(message3);
                        dialog3.ShowDialog();
                    }
                }
                else
                {
                    SelectedHotel.Name = _backupHotel.Name;
                    SelectedHotel.RoomCount = _backupHotel.RoomCount;
                    SelectedHotel.Address = _backupHotel.Address;
                    SelectedHotel.Email = _backupHotel.Email;
                    SelectedHotel.HasRestaurant = _backupHotel.HasRestaurant;
                    SelectedHotel.Destination = _backupHotel.Destination;
                }
                */
                UpdateHotelWindow dialog = new UpdateHotelWindow(SelectedHotel);

                bool? dialogResult = dialog.ShowDialog();

                if ((bool)dialogResult)
                {
                    Hotel pom = dialog.Hotel;
                    if (HotelDataAccess.UpdateHotel(pom))
                    {
                        SelectedHotel = pom;
                        OnPropertyChanged(nameof(Hotels));
                        string message2 = (string)Application.Current.Resources["SuccessfulUpdate"];
                        MessageWithoutOptionDialog dialog2 = new MessageWithoutOptionDialog(message2);
                        dialog2.ShowDialog();
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
