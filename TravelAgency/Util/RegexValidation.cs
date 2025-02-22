using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TravelAgency.Util
{
    public class RegexValidation : ValidationRule
    {
        public string Pattern { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            /*
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                string emptyFieldMessage = (string)Application.Current.Resources["EmptyFieldRegex"];
                return new ValidationResult(false, emptyFieldMessage);
            }
            */

            string input = value.ToString();
            Console.WriteLine($"Validating: {input}");
            Regex regex = new Regex(Pattern);
            bool isValid = regex.IsMatch(input);
            Console.WriteLine($"Regex match: {isValid}");

            if (!regex.IsMatch(input))
            {
                string invalidInputMessage = (string)Application.Current.Resources["InvalidInput"];
                return new ValidationResult(false, invalidInputMessage);
            }

            return ValidationResult.ValidResult;
        }
    }
}

