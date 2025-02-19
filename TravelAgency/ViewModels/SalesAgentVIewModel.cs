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
    public class SalesAgentViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly SalesAgentHotelView _saHotelView;
        private readonly SalesAgentReservationView _saReservationView;


        public ICommand ShowHotelsCommand { get; }
        public ICommand ShowReservationsCommand { get; }


        public SalesAgentViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _saHotelView = new SalesAgentHotelView();
            _saReservationView = new SalesAgentReservationView();
            ShowHotelsCommand = new RelayCommand(ShowHotels);
            ShowReservationsCommand = new RelayCommand(ShowReservations);
        }

        private void ShowHotels()
        {
            _navigationService.NavigateTo(_saHotelView);
        }

        private void ShowReservations()
        {
            _navigationService.NavigateTo(_saReservationView);
        }
    }
}
