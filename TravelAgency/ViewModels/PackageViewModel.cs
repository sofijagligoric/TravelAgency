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
    public class PackageViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<Package> Packages { get; set; }
        //  public Package SelectedPackage { get; set; } = new Package();

        private Package _selectedPackage;

        public Package SelectedPackage
        {
            get => _selectedPackage;
            set
            {
                if (_selectedPackage != value)
                {
                    //  _backupPackage = value != null ? new Package(value) : null;  
                    _selectedPackage = value;
                    OnPropertyChanged(nameof(SelectedPackage));
                }
            }
        }


        public ICommand AddPackageCommand { get; }
        public ICommand AddReservationCommand { get; }
        public ICommand DeletePackageCommand { get; }
        public ICommand UpdatePackageCommand { get; }
        public ICommand SearchPackagesCommand { get; }
        public ICommand AllPackagesCommand { get; }
        public ICommand ShowHotelsCommand { get; }

        public PackageViewModel()
        {
            var packages = PackageDataAccess.GetPackages();

            if (packages == null)
            {
                Console.WriteLine("Packages list is null! Check database connection.");
                packages = new List<Package>();
            }

            Packages = new ObservableCollection<Package>(packages);

            AddPackageCommand = new RelayCommand(AddPackage);
            AddReservationCommand = new RelayCommand(AddReservation);
            DeletePackageCommand = new RelayCommand(DeletePackage);
            UpdatePackageCommand = new RelayCommand(UpdatePackage);
            SearchPackagesCommand = new RelayCommand(param => SearchPackages(param.ToString()));
            AllPackagesCommand = new RelayCommand(AllPackages);
            ShowHotelsCommand = new RelayCommand(ShowHotels);

        }

        private void AddReservation()
        {
            if (SelectedPackage != null)
            {
                List<Hotel> availableHotels = PackageOffersHotelDataAccess.GetHotelsByPackage(SelectedPackage.PackageId);
                if (availableHotels.Count == 0)
                {
                    string message = (string)Application.Current.Resources["HotelUnavailable"];
                    MessageWithoutOptionDialog dialog = new MessageWithoutOptionDialog(message);
                    dialog.ShowDialog();
                }
                else
                {
                    if (availableHotels.Where(hotel => hotel.RoomCount > 0).ToList().Count == 0)
                    {
                        string message = (string)Application.Current.Resources["NoRooms"];
                        MessageWithoutOptionDialog dialog = new MessageWithoutOptionDialog(message);
                        dialog.ShowDialog();
                    }
                    else
                    {

                        AddReservationWindow dialog = new AddReservationWindow(SelectedPackage);

                        bool? dialogResult = dialog.ShowDialog();

                        if ((bool)dialogResult)
                        {
                            Reservation pom = dialog.Reservation;
                            string message2 = (string)Application.Current.Resources["ConfirmAdd"] + ": " + pom + "?";
                            MessageDialog dialog2 = new MessageDialog(message2);
                            bool? dialogResult2 = dialog2.ShowDialog();
                            if ((bool)dialogResult2)
                            {
                                if (ReservationDataAccess.AddReservation(pom))
                                {
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
                }
                
            }
            else
            {
                string message = (string)Application.Current.Resources["RowNotSelected"];
                MessageWithoutOptionDialog dialog = new MessageWithoutOptionDialog(message);
                dialog.ShowDialog();
            }
        }

        private void AddPackage()
        {
            
            AddPackageWindow dialog = new AddPackageWindow();

            bool? dialogResult = dialog.ShowDialog();

            if ((bool)dialogResult)
            {
                Package pom = dialog.Package;
                string message2 = (string)Application.Current.Resources["ConfirmAdd"] + ": " + pom + "?";
                MessageDialog dialog2 = new MessageDialog(message2);
                bool? dialogResult2 = dialog2.ShowDialog();
                if ((bool)dialogResult2)
                {
                    if (PackageDataAccess.AddPackage(pom))
                    {
                        Packages.Add(pom);
                        OnPropertyChanged(nameof(Packages));
                        string message = (string)Application.Current.Resources["SuccessfullyAdded"];
                        MessageWithoutOptionDialog dialog3 = new MessageWithoutOptionDialog(message);
                        dialog3.ShowDialog();
                    }
                }
                else
                {
                    string message3 = (string)Application.Current.Resources["FailedAdd"];
                    MessageWithoutOptionDialog dialog3 = new MessageWithoutOptionDialog(message3);
                    dialog3.ShowDialog();
                }
            }
           

        }

        private void AllPackages()
        {
            var packages = PackageDataAccess.GetPackages();
            if (packages == null)
            {
                Console.WriteLine("Packages list is null! Check database connection.");
                packages = new List<Package>();
            }
            else
            {
                Packages = new ObservableCollection<Package>(packages);
                OnPropertyChanged(nameof(Packages));
            }
        }

        private void SearchPackages(string destName)
        {
            if (destName.Trim().Length == 0)

            {
                this.AllPackages();
                return;
            }
            var foundPackages = PackageDataAccess.GetPackagesByDestination(destName);
            if (foundPackages == null)
            {
                Console.WriteLine("Search is null.");
            }
            else
            {
                Packages = new ObservableCollection<Package>(foundPackages);
                OnPropertyChanged(nameof(Packages));
            }
        }

        private void DeletePackage()
        {
            if (SelectedPackage == null)
            {
                string message = (string)Application.Current.Resources["RowNotSelected"];
                MessageWithoutOptionDialog dialog = new MessageWithoutOptionDialog(message);
                dialog.ShowDialog();

                return;
            }


            string message2 = (string)Application.Current.Resources["ConfirmDelete"] + " " + SelectedPackage.PackageId + "?";
            MessageDialog dialog2 = new MessageDialog(message2);
            bool? dialogResult2 = dialog2.ShowDialog();


            if ((bool)dialogResult2)
            {
                if (PackageDataAccess.DeletePackage(SelectedPackage.PackageId))
                {
                    Packages.Remove(SelectedPackage);
                    OnPropertyChanged(nameof(Packages));
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


        private void ShowHotels()
        {
            if (SelectedPackage == null)
            {
                string message = (string)Application.Current.Resources["RowNotSelected"];
                MessageWithoutOptionDialog dialog = new MessageWithoutOptionDialog(message);
                dialog.ShowDialog();

                return;
            }
            
               PackageHotelsWindow dialog2 = new PackageHotelsWindow(SelectedPackage);
            dialog2.ShowDialog();

        }

        private void UpdatePackage()
        {
          
            if (SelectedPackage != null)
            {
                UpdatePackageWindow dialog = new UpdatePackageWindow(SelectedPackage);

                bool? dialogResult = dialog.ShowDialog();

                if ((bool)dialogResult)
                {
                    Package pom = dialog.Package;
                    string message2 = (string)Application.Current.Resources["ConfirmUpdate"] + ": " + pom + "?";
                    MessageDialog dialog2 = new MessageDialog(message2);
                    bool? dialogResult2 = dialog2.ShowDialog();
                    if ((bool)dialogResult2)
                    {
                        if (PackageDataAccess.UpdatePackage(pom))
                        {
                            SelectedPackage.About = pom.About;
                            SelectedPackage.EndDate = pom.EndDate;
                            SelectedPackage.StartDate = pom.StartDate;
                            SelectedPackage.Destination = pom.Destination;
                            SelectedPackage.Price = pom.Price;

                            OnPropertyChanged(nameof(SelectedPackage));
                            OnPropertyChanged(nameof(Packages));
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
            return SelectedPackage != null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
