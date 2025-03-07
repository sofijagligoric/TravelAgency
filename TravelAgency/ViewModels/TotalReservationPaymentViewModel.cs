using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TravelAgency.DataAccess;
using TravelAgency.Models;
using TravelAgency.Views;

namespace TravelAgency.ViewModels
{
    public class TotalReservationPaymentViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TotalReservationPayment> TotalReservationPayments { get; set; }

        private TotalReservationPayment _selectedTotalReservationPayment;
        public TotalReservationPayment SelectedTotalReservationPayment
        {
            get => _selectedTotalReservationPayment;
            set
            {
                _selectedTotalReservationPayment = value;
                OnPropertyChanged(nameof(SelectedTotalReservationPayment));
            }
        }

        public ICommand SearchTotalReservationPaymentsCommand { get; }
        public ICommand AllTotalReservationPaymentsCommand { get; }

        public TotalReservationPaymentViewModel()
        {
            var totalReservationPayments = PaymentDataAccess.GetPaymentInfoForReservations();
            if (totalReservationPayments == null)
            {
                Console.WriteLine("TotalReservationPayments list is null! Check database connection.");
                totalReservationPayments = new List<TotalReservationPayment>();
            }

            TotalReservationPayments = new ObservableCollection<TotalReservationPayment>(totalReservationPayments);
            SearchTotalReservationPaymentsCommand = new RelayCommand(param => SearchTotalReservationPayments(param.ToString()));
            AllTotalReservationPaymentsCommand = new RelayCommand(AllTotalReservationPayments);
        }


        private void AllTotalReservationPayments()
        {
            var hotels = PaymentDataAccess.GetPaymentInfoForReservations();
            if (hotels == null)
            {
                Console.WriteLine("TotalReservationPayments list is null! Check database connection.");
                hotels = new List<TotalReservationPayment>();
            }
            else
            {
                TotalReservationPayments = new ObservableCollection<TotalReservationPayment>(hotels);
                OnPropertyChanged(nameof(TotalReservationPayments));
            }
        }

        private void SearchTotalReservationPayments(string destName)
        {
            if (destName.Trim().Length == 0)

            {
                this.AllTotalReservationPayments();
                return;
            }
            var foundTotalReservationPayments = PaymentDataAccess.GetPaymentInfoForReservationsByCustomerName(destName);
            if (foundTotalReservationPayments == null)
            {
                Console.WriteLine("Search is null.");
            }
            else
            {
                TotalReservationPayments = new ObservableCollection<TotalReservationPayment>(foundTotalReservationPayments);
                OnPropertyChanged(nameof(TotalReservationPayments));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
