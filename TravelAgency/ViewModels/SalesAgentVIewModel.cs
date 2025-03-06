using FontAwesome.Sharp;
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
    public class SalesAgentViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService;
        /*
        private readonly SalesAgentHotelView _saHotelView;
        private readonly SalesAgentReservationView _saReservationView;
        private readonly TotalReservationPayments _saTotalPayments;
        private readonly SalesAgentPackageView _saPackages;
        private readonly CustomerView _customersView;
        private readonly PaymentView _paymentsView;
        */

        public ICommand ShowCustomersCommand { get; }
        public ICommand ShowHotelsCommand { get; }
        public ICommand ShowReservationsCommand { get; }
        public ICommand ShowTotalPaymentsCommand { get; }
        public ICommand ShowAllPaymentsCommand { get; }
        public ICommand ShowPackagesCommand { get; }
        public ICommand ChangePasswordCommand { get; }


        private string _caption;
        private IconChar _icon;
        public string Caption
        {
            get => _caption;
            set
            {
                if (_caption != value)
                {
                    _caption = value;
                    OnPropertyChanged(nameof(Caption));
                }
            }
        }

        public IconChar Icon
        {
            get => _icon;
            set
            {
                if (_icon != value)
                {
                    _icon = value;
                    OnPropertyChanged(nameof(Icon));
                }
            }
        }

        private Employee _employee;
        public Employee Employee
        {
            get => _employee;
            set
            {
                _employee = value;
            }
        }


        public SalesAgentViewModel(INavigationService navigationService, Employee e)
        {
            _navigationService = navigationService;
            /*
            _customersView = new CustomerView();
            _saHotelView = new SalesAgentHotelView();
            _saReservationView = new SalesAgentReservationView();
            _saTotalPayments = new TotalReservationPayments();
            _paymentsView = new PaymentView();
            _saPackages = new SalesAgentPackageView();
            */
            Employee = e;

            ShowCustomersCommand = new RelayCommand(ShowCustomers);
            ShowHotelsCommand = new RelayCommand(ShowHotels);
            ShowReservationsCommand = new RelayCommand(ShowReservations);
            ShowTotalPaymentsCommand = new RelayCommand(ShowTotalPayments);
            ShowAllPaymentsCommand = new RelayCommand(ShowAllPayments);
            ChangePasswordCommand = new RelayCommand(ChangePassword);
            ShowPackagesCommand = new RelayCommand(ShowPackages);

            Caption = (string)Application.Current.Resources["Customers"];
            Icon = IconChar.User;
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
            //   _navigationService.NavigateTo(_customersView);
            Caption = Caption = (string)Application.Current.Resources["Customers"];
            Icon = IconChar.User;
            _navigationService.NavigateTo(new CustomerView());
        }

        private void ShowPackages()
        {
            // _navigationService.NavigateTo(_saPackages);
            
            Caption = Caption = (string)Application.Current.Resources["Packages"];
            Icon = IconChar.PlaneDeparture;
            _navigationService.NavigateTo(new SalesAgentPackageView());

        }

        private void ShowHotels()
        {
            // _navigationService.NavigateTo(_saHotelView);
           
            Caption = Caption = (string)Application.Current.Resources["Hotels"];
            Icon = IconChar.Hotel;
            _navigationService.NavigateTo(new SalesAgentHotelView());

        }

        private void ShowReservations()
        {
            // _navigationService.NavigateTo(_saReservationView);
            Caption = Caption = (string)Application.Current.Resources["Reservations"];
            Icon = IconChar.CalendarCheck;
            _navigationService.NavigateTo(new SalesAgentReservationView());
        }
        private void ShowTotalPayments()
        {
            // _navigationService.NavigateTo(_saTotalPayments);
            Caption = Caption = (string)Application.Current.Resources["PaymentsByReservation"];
            Icon = IconChar.MoneyBills;
            _navigationService.NavigateTo(new TotalReservationPayments());
        }

        private void ShowAllPayments()
        {
            //   _navigationService.NavigateTo(_paymentsView);
            Caption = Caption = (string)Application.Current.Resources["AllPayments"];
            Icon = IconChar.CashRegister;
            _navigationService.NavigateTo(new PaymentView());
        }

        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}
