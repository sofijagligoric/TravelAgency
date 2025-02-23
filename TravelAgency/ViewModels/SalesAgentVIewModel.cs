using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
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
    public class SalesAgentViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly SalesAgentHotelView _saHotelView;
        private readonly SalesAgentReservationView _saReservationView;
        private readonly TotalReservationPayments _saTotalPayments;
        private readonly SalesAgentPackageView _saPackages;
        private readonly CustomerView _customersView;
        private readonly PaymentView _paymentsView;

        public ICommand ShowCustomersCommand { get; }
        public ICommand ShowHotelsCommand { get; }
        public ICommand ShowReservationsCommand { get; }
        public ICommand ShowTotalPaymentsCommand { get; }
        public ICommand ShowAllPaymentsCommand { get; }
        public ICommand ShowPackagesCommand { get; }
        public ICommand ChangePasswordCommand { get; }


        private Employee _employee;
        public Employee Employee
        {
            get => _employee;
            set
            {
                _employee = value;
                OnPropertyChanged(nameof(Employee));
            }
        }


        public SalesAgentViewModel(INavigationService navigationService, Employee e)
        {
            _navigationService = navigationService;
            _customersView = new CustomerView();
            _saHotelView = new SalesAgentHotelView();
            _saReservationView = new SalesAgentReservationView();
            _saTotalPayments = new TotalReservationPayments();
            _paymentsView = new PaymentView();
            _saPackages = new SalesAgentPackageView();
            Employee = e;

            ShowCustomersCommand = new RelayCommand(ShowCustomers);
            ShowHotelsCommand = new RelayCommand(ShowHotels);
            ShowReservationsCommand = new RelayCommand(ShowReservations);
            ShowTotalPaymentsCommand = new RelayCommand(ShowTotalPayments);
            ShowAllPaymentsCommand = new RelayCommand(ShowAllPayments);
            ChangePasswordCommand = new RelayCommand(ChangePassword);
            ShowPackagesCommand = new RelayCommand(ShowPackages);

            ShowCustomers();
        }

        private void ChangePassword()
        {
            ChangePasswordWindow dialog = new ChangePasswordWindow(Employee);

            bool? dialogResult = dialog.ShowDialog();

            if ((bool)dialogResult)
            {
                string message2 = (string)Application.Current.Resources["PasswordChangeConfirm"];
                MessageDialog dialog2 = new MessageDialog(message2);
                bool? dialogResult2 = dialog2.ShowDialog();
                if ((bool)dialogResult2)
                {
                    if (EmployeeDataAccess.ChangePassword(Employee.Jmb, dialog.New))
                    {

                        string message3 = (string)Application.Current.Resources["PasswordChangedSuccessfully"];
                        MessageWithoutOptionDialog dialog3 = new MessageWithoutOptionDialog(message3);
                        dialog3.ShowDialog();
                    }
                    else
                    {
                        string message3 = (string)Application.Current.Resources["PasswordChangeFailed"];
                        MessageWithoutOptionDialog dialog3 = new MessageWithoutOptionDialog(message3);
                        dialog3.ShowDialog();
                    }

                }

            }
        }

        private void ShowCustomers()
        {
            _navigationService.NavigateTo(_customersView);
        }

        private void ShowPackages()
        {
            _navigationService.NavigateTo(_saPackages);
        }

        private void ShowHotels()
        {
            _navigationService.NavigateTo(_saHotelView);
        }

        private void ShowReservations()
        {
            _navigationService.NavigateTo(_saReservationView);
        }
        private void ShowTotalPayments()
        {
            _navigationService.NavigateTo(_saTotalPayments);
        }

        private void ShowAllPayments()
        {
            _navigationService.NavigateTo(_paymentsView);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
