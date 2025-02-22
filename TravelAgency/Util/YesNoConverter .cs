using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace TravelAgency.Util
{
    public class YesNoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool hasRestaurant = (bool)value;
            return hasRestaurant ? Application.Current.Resources["Yes"] : Application.Current.Resources["No"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string)value == (string)Application.Current.Resources["Yes"];
        }
    }
}
