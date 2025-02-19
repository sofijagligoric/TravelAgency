using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelAgency.Views;

namespace TravelAgency.ViewModels
{
    public class AdminViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly AdminHotelView _adminHotelView;
        private readonly AdminReservationsView _adminReservationsView;
        private readonly AdminDestinationView _adminDestinationView;

        public ICommand ShowHotelsCommand { get; }
        public ICommand ShowReservationsCommand { get; }
        public ICommand ShowDestinationsCommand { get; }

        public AdminViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _adminHotelView = new AdminHotelView();
            _adminReservationsView = new AdminReservationsView();
            _adminDestinationView = new AdminDestinationView();
            ShowHotelsCommand = new RelayCommand(ShowHotels);
            ShowReservationsCommand = new RelayCommand(ShowReservations);
            ShowDestinationsCommand = new RelayCommand(ShowDestinations);
        }

     
        private void ShowHotels()
        {
            //_navigationService.NavigateTo(new AdminHotelView());
            _navigationService.NavigateTo(_adminHotelView);
        }

        private void ShowReservations()
        {
            // _navigationService.NavigateTo(new AdminReservationsView());
            _navigationService.NavigateTo(_adminReservationsView);
        }

        private void ShowDestinations()
        {
            //_navigationService.NavigateTo(new AdminHotelView());
            _navigationService.NavigateTo(_adminDestinationView);
        }
    }
}
