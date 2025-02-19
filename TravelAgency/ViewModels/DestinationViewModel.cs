using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TravelAgency.DataAccess;
using TravelAgency.Models;
using TravelAgency.Views;

namespace TravelAgency.ViewModels
{
    public class DestinationViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Destination> Destinations { get; set; }

        private Destination _selectedDestination;
        public Destination SelectedDestination
        {
            get => _selectedDestination;
            set
            {
                _selectedDestination = value;
                OnPropertyChanged(nameof(SelectedDestination));
            }
        }

        public ICommand AddDestinationCommand { get; }
        public ICommand DeleteDestinationCommand { get; }
        public ICommand SearchDestinationsCommand { get; }
        public ICommand AllDestinationsCommand { get; }

        public DestinationViewModel()
        {
            var destinations = DestinationDataAccess.GetAllDestinations();
            if (destinations == null)
            {
                Console.WriteLine("Destinations list is null! Check database connection.");
                destinations = new List<Destination>();
            }

            Destinations = new ObservableCollection<Destination>(destinations);
            AddDestinationCommand = new RelayCommand(AddDestination);
            DeleteDestinationCommand = new RelayCommand(DeleteDestination);
            SearchDestinationsCommand = new RelayCommand(param => SearchDestinations(param.ToString()));
            AllDestinationsCommand = new RelayCommand(AllDestinations);
        }

        private void AddDestination()
        {
            AddDestinationWindow dialog = new AddDestinationWindow();

            bool? dialogResult = dialog.ShowDialog();

            if ((bool)dialogResult)
            {
                Destination pom = dialog.Destination;
                if (DestinationDataAccess.AddDestination(pom))
                {
                    Destinations.Add(pom);
                    OnPropertyChanged(nameof(Destinations));
                    string message = (string)Application.Current.Resources["SuccessfullyAdded"];
                    MessageWithoutOptionDialog dialog2 = new MessageWithoutOptionDialog(message + " " + pom);
                    dialog2.ShowDialog();
                }

            }
        }

        private void AllDestinations()
        {
            var hotels = DestinationDataAccess.GetAllDestinations();
            if (hotels == null)
            {
                Console.WriteLine("Destinations list is null! Check database connection.");
                hotels = new List<Destination>();
            }
            else
            {
                Destinations = new ObservableCollection<Destination>(hotels);
                OnPropertyChanged(nameof(Destinations));
            }
        }

        private void SearchDestinations(string destName)
        {
            var foundDestinations = DestinationDataAccess.GetDestinationByName(destName);
            if (foundDestinations == null)
            {
                string message = (string)Application.Current.Resources["NoMatches"];
                MessageWithoutOptionDialog dialog = new MessageWithoutOptionDialog(message);
                dialog.ShowDialog();
            }
            else
            {
                Destinations = new ObservableCollection<Destination>(foundDestinations);
                OnPropertyChanged(nameof(Destinations));
            }
        }

        private void DeleteDestination(object obj)
        {
            if (SelectedDestination == null)
            {
                string message = (string)Application.Current.Resources["RowNotSelected"];
                MessageWithoutOptionDialog dialog = new MessageWithoutOptionDialog(message);
                dialog.ShowDialog();

                return;
            }


            string message2 = (string)Application.Current.Resources["ConfirmDelete"] + " " + SelectedDestination.DestinationName + "?";
            MessageDialog dialog2 = new MessageDialog(message2);
            bool? dialogResult2 = dialog2.ShowDialog();


            if ((bool)dialogResult2)
            {
                if (DestinationDataAccess.DeleteDestination(SelectedDestination))
                {
                    Destinations.Remove(SelectedDestination);
                    OnPropertyChanged(nameof(Destinations));
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


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
    }
