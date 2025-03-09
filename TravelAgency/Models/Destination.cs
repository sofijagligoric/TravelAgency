using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TravelAgency.Models
{
    public class Destination : INotifyPropertyChanged
    {
        private int _postCode;
        private string _destinationName;
        private string _about;
        private int _distance;
        private string _localLanguage;
        private Country _country;


        public int PostCode
        {
            get => _postCode;
            set
            {
                _postCode = value;
                OnPropertyChanged(nameof(PostCode));
            }
        }

        public string DestinationName
        {
            get => _destinationName;
            set
            {
                _destinationName = value;
                OnPropertyChanged(nameof(DestinationName));
            }
        }

        public string About
        {
            get => _about;
            set
            {
                _about = value;
                OnPropertyChanged(nameof(About));
            }
        }

        public int Distance
        {
            get => _distance;
            set
            {
                _distance = value;
                OnPropertyChanged(nameof(Distance));
            }
        }

        public string LocalLanguage
        {
            get => _localLanguage;
            set
            {
                _localLanguage = value;
                OnPropertyChanged(nameof(LocalLanguage));
            }
        }

        public Country Country
        {
            get => _country;
            set
            {
                _country = value;
                OnPropertyChanged(nameof(Country));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Destination()
        {
            PostCode = 0;
            DestinationName = string.Empty;
            About = string.Empty;
            Distance = 0;
            LocalLanguage = string.Empty;
            Country = new Country();
        }

        public Destination(Destination other)
        {
            PostCode = other.PostCode;
            DestinationName = other.DestinationName;
            About = other.About;
            Distance = other.Distance;
            LocalLanguage = other.LocalLanguage;
            Country = new Country(other.Country);
        }

        public Destination(int postCode, string destinationName, string about, int distance, string localLanguage, Country country)
        {
            PostCode = postCode;
            DestinationName = destinationName;
            About = about;
            Distance = distance;
            LocalLanguage = localLanguage;
            Country = country;
        }

        public override string ToString()
        {
            return "\n" + (string)Application.Current.Resources["Name"] + " " + DestinationName + ",\n" + (string)Application.Current.Resources["Postcode"] + " " + PostCode + ",\n" +
                (string)Application.Current.Resources["About"] + " " + About + ", \n" + (string)Application.Current.Resources["Distance"] + " " + Distance +
                ", \n" + (string)Application.Current.Resources["LocalLanguage"] + " " + LocalLanguage + ", \n" + (string)Application.Current.Resources["Country"] + " " + Country?.CountryName;

        }

        public override bool Equals(object obj)
        {
            return obj is Destination destination &&
                   PostCode == destination.PostCode &&
                   DestinationName.Equals(destination.DestinationName);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PostCode, DestinationName);
        }
    }
}
