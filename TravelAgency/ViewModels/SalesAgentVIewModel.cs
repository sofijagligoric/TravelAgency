using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
        private readonly CustomerView _customersView;

        public ICommand ShowCustomersCommand { get; }
        public ICommand ShowHotelsCommand { get; }
        public ICommand ShowReservationsCommand { get; }
        public ICommand ShowTotalPaymentsCommand { get; }



        public SalesAgentViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _customersView = new CustomerView();
            _saHotelView = new SalesAgentHotelView();
            _saReservationView = new SalesAgentReservationView();
            _saTotalPayments = new TotalReservationPayments();
            ShowCustomersCommand = new RelayCommand(ShowCustomers);
            ShowHotelsCommand = new RelayCommand(ShowHotels);
            ShowReservationsCommand = new RelayCommand(ShowReservations);
            ShowTotalPaymentsCommand = new RelayCommand(ShowTotalPayments);

            ShowCustomers();
        }

        private void ShowCustomers()
        {
            _navigationService.NavigateTo(_customersView);
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

    }
}
