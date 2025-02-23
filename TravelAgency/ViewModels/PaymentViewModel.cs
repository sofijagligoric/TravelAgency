using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelAgency.DataAccess;
using TravelAgency.Models;

namespace TravelAgency.ViewModels
{
    public class PaymentViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Payment> Payments { get; set; }

        private Payment _selectedPayment;
        public Payment SelectedPayment
        {
            get => _selectedPayment;
            set
            {
                _selectedPayment = value;
                OnPropertyChanged(nameof(SelectedPayment));
            }
        }

        public ICommand SearchPaymentsCommand { get; }
        public ICommand AllPaymentsCommand { get; }

        public PaymentViewModel()
        {
            var payments = PaymentDataAccess.GetAllPayments();
            if (payments == null)
            {
                Console.WriteLine("Payments list is null! Check database connection.");
                payments = new List<Payment>();
            }

            Payments = new ObservableCollection<Payment>(payments);
            SearchPaymentsCommand = new RelayCommand(param => SearchPayments(param.ToString()));
            AllPaymentsCommand = new RelayCommand(AllPayments);
        }



        private void AllPayments()
        {
            var hotels = PaymentDataAccess.GetAllPayments();
            if (hotels == null)
            {
                Console.WriteLine("Payments list is null! Check database connection.");
                hotels = new List<Payment>();
            }
            else
            {
                Payments = new ObservableCollection<Payment>(hotels);
                OnPropertyChanged(nameof(Payments));
            }
        }

        private void SearchPayments(string destName)
        {
            if (destName.Trim().Length == 0)

            {
                this.AllPayments();
                return;
            }
            var foundPayments = PaymentDataAccess.GetAllPaymentsByCustomerName(destName);
            if (foundPayments == null)
            {
                Console.WriteLine("Search is null.");
            }
            else
            {
                Payments = new ObservableCollection<Payment>(foundPayments);
                OnPropertyChanged(nameof(Payments));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

