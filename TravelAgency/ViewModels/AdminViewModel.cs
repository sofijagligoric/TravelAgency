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

        public ICommand ShowHotelsCommand { get; }

        public AdminViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ShowHotelsCommand = new RelayCommand(ShowHotels);
        }

        private void ShowHotels()
        {
            _navigationService.NavigateTo(new AdminHotelView());
        }
    }
}
