﻿using FontAwesome.Sharp;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace TravelAgency.ViewModels
{
    public class AdminViewModel : ViewModelBase, INotifyPropertyChanged
    {
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
                OnPropertyChanged(nameof(Employee));
            }
        }

        private readonly INavigationService _navigationService;
        /*
        private readonly AdminHotelView _adminHotelView;
        private readonly AdminReservationsView _adminReservationsView;
        private readonly AdminDestinationView _adminDestinationView;
        private readonly EmployeeView _employeeView;
        private readonly AdminPackageView _adminPackageView;
        */

        public ICommand ShowHotelsCommand { get; }
        public ICommand ShowReservationsCommand { get; }
        public ICommand ShowDestinationsCommand { get; }
        public ICommand ShowEmployeesCommand { get; }
        public ICommand ShowPackagesCommand { get; }
        public ICommand ChangePasswordCommand { get; }

        public AdminViewModel(INavigationService navigationService, Employee e)
        {
            _navigationService = navigationService;
            /*
            _adminHotelView = new AdminHotelView();
            _adminReservationsView = new AdminReservationsView();
            _adminDestinationView = new AdminDestinationView();
            _employeeView = new EmployeeView();
            _adminPackageView = new AdminPackageView();
            */
            Employee = e;

            ShowHotelsCommand = new RelayCommand(ShowHotels);
            ShowReservationsCommand = new RelayCommand(ShowReservations);
            ShowDestinationsCommand = new RelayCommand(ShowDestinations);
            ShowEmployeesCommand = new RelayCommand(ShowEmployees);
            ChangePasswordCommand = new RelayCommand(ChangePassword);
            ShowPackagesCommand = new RelayCommand(ShowPackages);


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
                else
                {
                    string message3 = (string)Application.Current.Resources["ActionCanceled"];
                    MessageWithoutOptionDialog dialog3 = new MessageWithoutOptionDialog(message3);
                    dialog3.ShowDialog();
                }

            }
        }

        private void ShowHotels()
        {
            Caption = (string)Application.Current.Resources["Hotels"];
            Icon = IconChar.Hotel;
            _navigationService.NavigateTo(new AdminHotelView());

            // _navigationService.NavigateTo(_adminHotelView);
        }

        private void ShowPackages()
        {

            Caption = (string)Application.Current.Resources["Packages"];
            Icon = IconChar.PlaneDeparture;
            _navigationService.NavigateTo(new AdminPackageView());
            //   _navigationService.NavigateTo(_adminPackageView);
        }

        private void ShowReservations()
        {

            Caption = (string)Application.Current.Resources["Reservations"];
            Icon = IconChar.CalendarCheck;
            _navigationService.NavigateTo(new AdminReservationsView());
            // _navigationService.NavigateTo(_adminReservationsView);
        }

        private void ShowDestinations()
        {
            Caption = (string)Application.Current.Resources["Destinations"];
            Icon = IconChar.LocationDot;
            _navigationService.NavigateTo(new AdminDestinationView());
            // _navigationService.NavigateTo(_adminDestinationView);
        }

        private void ShowEmployees()
        {

            Caption = (string)Application.Current.Resources["Employees"];
            Icon = IconChar.User;
            _navigationService.NavigateTo(new EmployeeView());
            //  _navigationService.NavigateTo(_employeeView);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
