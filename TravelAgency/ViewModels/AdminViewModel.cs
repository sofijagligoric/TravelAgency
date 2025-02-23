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
    public class AdminViewModel : ViewModelBase
    {
        private string _caption;
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

        private string _icon;
        public string Icon
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
                OnPropertyChanged(nameof(Employee));
            }
        }

        private readonly INavigationService _navigationService;
        private readonly AdminHotelView _adminHotelView;
        private readonly AdminReservationsView _adminReservationsView;
        private readonly AdminDestinationView _adminDestinationView;
        private readonly EmployeeView _employeeView;

        public ICommand ShowHotelsCommand { get; }
        public ICommand ShowReservationsCommand { get; }
        public ICommand ShowDestinationsCommand { get; }
        public ICommand ShowEmployeesCommand { get; }
        public ICommand ChangePasswordCommand { get; }

        public AdminViewModel(INavigationService navigationService, Employee e)
        {
            _navigationService = navigationService;
            _adminHotelView = new AdminHotelView();
            _adminReservationsView = new AdminReservationsView();
            _adminDestinationView = new AdminDestinationView();
            _employeeView = new EmployeeView();
            Employee = e;

            ShowHotelsCommand = new RelayCommand(ShowHotels);
            ShowReservationsCommand = new RelayCommand(ShowReservations);
            ShowDestinationsCommand = new RelayCommand(ShowDestinations);
            ShowEmployeesCommand = new RelayCommand(ShowEmployees);
            ChangePasswordCommand = new RelayCommand(ChangePassword);

            ShowEmployees();
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

        private void ShowHotels()
        {
            //_navigationService.NavigateTo(new AdminHotelView());
            Caption = (string)Application.Current.Resources["Hotels"];
            Icon = "Hotel";
            _navigationService.NavigateTo(_adminHotelView);
        }

        private void ShowReservations()
        {
            // _navigationService.NavigateTo(new AdminReservationsView());
            Caption = (string)Application.Current.Resources["Reservations"];
            Icon = "CalendarCheck";
            _navigationService.NavigateTo(_adminReservationsView);
        }

        private void ShowDestinations()
        {
            //_navigationService.NavigateTo(new AdminHotelView());
            
            Caption = (string)Application.Current.Resources["Destinations"];
            Icon = "LocationDot";
            _navigationService.NavigateTo(_adminDestinationView);
        }

        private void ShowEmployees()
        {
            //_navigationService.NavigateTo(new AdminHotelView());
            Caption = (string)Application.Current.Resources["Employees"];
            Icon = "User";
            _navigationService.NavigateTo(_employeeView);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
