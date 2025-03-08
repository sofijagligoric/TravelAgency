using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TravelAgency.Util
{
    public class DateValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult(true, null);
            }

            if (DateTime.TryParseExact(value.ToString(), "dd.MM.yyyy.", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                return new ValidationResult(true, null); 
            }
            return new ValidationResult(false, (string)Application.Current.Resources["DateNotValid"]);
        }
    }
}
