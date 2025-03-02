using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using TravelAgency.Views;

namespace TravelAgency.ViewModels
{
    public class NavigationService : INavigationService
    {
        private readonly ContentControl _contentControl;

        public NavigationService(ContentControl contentControl)
        {
            _contentControl = contentControl;
        }

        public void NavigateTo(UserControl view)
        {
            _contentControl.Content = view;
           
        }
    }

}
