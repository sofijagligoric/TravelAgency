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
    public class ReservationViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Reservation> Reservations { get; set; }
        //  public Reservation SelectedReservation { get; set; } = new Reservation();
        private Reservation _selectedReservation;
        public Reservation NewReservation { get; set; }
        public Reservation SelectedReservation
        {
            get => _selectedReservation;
            set
            {
                if (_selectedReservation != value)
                {
                    _selectedReservation = value;
                    OnPropertyChanged(nameof(SelectedReservation));
                }
            }
        }

        public ICommand SearchReservationsCommand { get; }
        public ICommand AllReservationsCommand { get; }
        public ICommand DeleteReservationCommand { get; }
        public ICommand PayReservationCommand { get; }

        public ReservationViewModel()
        {
            var reservations = ReservationDataAccess.GetReservations();
            if (reservations == null)
            {
                Console.WriteLine("Reservations list is null! Check database connection.");
                MessageBox.Show("Reservations list is null!");
                reservations = new List<Reservation>();
            }

            Reservations = new ObservableCollection<Reservation>(reservations);
            SearchReservationsCommand = new RelayCommand(param => SearchReservations(param.ToString()));
            DeleteReservationCommand = new RelayCommand(DeleteReservation);
            AllReservationsCommand = new RelayCommand(AllReservations);
            PayReservationCommand = new RelayCommand(PayReservation);
        }

      
        private void AllReservations()
        {
            var reservations = ReservationDataAccess.GetReservations();
            if (reservations == null)
            {
                Console.WriteLine("Reservations list is null! Check database connection.");
                reservations = new List<Reservation>();
            }
            else
            {
                Reservations = new ObservableCollection<Reservation>(reservations);
                OnPropertyChanged(nameof(Reservations));
            }
        }

        private void SearchReservations(string customerName)
        {
            var foundReservations = ReservationDataAccess.GetReservationsByCustomer(customerName);
            if (foundReservations == null)
            {
                string message = (string)Application.Current.Resources["NoMatches"];
                MessageWithoutOptionDialog dialog = new MessageWithoutOptionDialog(message);
                dialog.ShowDialog();
            }
            else
            {
                Reservations = new ObservableCollection<Reservation>(foundReservations);
                OnPropertyChanged(nameof(Reservations));
            }
        }

        private void DeleteReservation(object obj)
        {
            if (SelectedReservation == null)
            {
                string message = (string)Application.Current.Resources["RowNotSelected"];
                MessageWithoutOptionDialog dialog = new MessageWithoutOptionDialog(message);
                dialog.ShowDialog();

                return;
            }


            string message2 = (string)Application.Current.Resources["ConfirmDelete"] + " " + SelectedReservation + "?";
            MessageDialog dialog2 = new MessageDialog(message2);
            bool? dialogResult2 = dialog2.ShowDialog();


            if ((bool)dialogResult2)
            {
                if (ReservationDataAccess.DeleteReservation(SelectedReservation.ReservationId))
                {
                    Reservations.Remove(SelectedReservation);
                    OnPropertyChanged(nameof(Reservations));
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

        private void PayReservation()
        {

            if (SelectedReservation != null)
            {
                if(SelectedReservation.AllPayed != 0)
                {
                    string message3 = (string)Application.Current.Resources["ReservationIsPayed"];
                    MessageWithoutOptionDialog dialog3 = new MessageWithoutOptionDialog(message3);
                    dialog3.ShowDialog();
                }
                else
                {
                    PayReservationWindow dialog = new PayReservationWindow(SelectedReservation);
                    bool? dialogResult = dialog.ShowDialog();

                    if ((bool)dialogResult)
                    {
                        Reservation pom = dialog.Reservation;
                        string message2 = (string)Application.Current.Resources["ConfirmPayment"] + "\n" +
                          (string)Application.Current.Resources["ReservationId"]+ " " + dialog.Reservation.ReservationId + ", " + 
                          dialog.Reservation.Customer.FullName +", "+ (string)Application.Current.Resources["Amount"] + " "+ dialog.AmountPayed + "?";
                        MessageDialog dialog2 = new MessageDialog(message2);
                        bool? dialogResult2 = dialog2.ShowDialog();
                        if ((bool)dialogResult2)
                        {
                            if (PaymentDataAccess.PayReservation(SelectedReservation.ReservationId, dialog.AmountPayed))
                            {
                                SelectedReservation.AllPayed = pom.AllPayed;
                                OnPropertyChanged(nameof(SelectedReservation));
                                OnPropertyChanged(nameof(Reservations));
                                string message3 = (string)Application.Current.Resources["PaymentSuccessful"];
                                MessageWithoutOptionDialog dialog3 = new MessageWithoutOptionDialog(message3);
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
            else
            {
                string message = (string)Application.Current.Resources["RowNotSelected"];
                MessageWithoutOptionDialog dialog = new MessageWithoutOptionDialog(message);
                dialog.ShowDialog();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
