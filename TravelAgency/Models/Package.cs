using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TravelAgency.Models
{
 

public class Package : INotifyPropertyChanged
    {
        private int _packageId;
        private string _startDate;
        private string _endDate;
        private decimal _price;
        private string _about;
        private Destination _destination;

        public Package(Package other)
        {
            PackageId = other.PackageId;
            StartDate = other.StartDate;
            EndDate = other.EndDate;
            Price = other.Price;
            About = other.About;
            Destination = other.Destination; 
        }

        public int PackageId
        {
            get { return _packageId; }
            set
            {
                if (_packageId != value)
                {
                    _packageId = value;
                    OnPropertyChanged(nameof(PackageId));
                }
            }
        }

        public string StartDate
        {
            get { return _startDate; }
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    OnPropertyChanged(nameof(StartDate));
                }
            }
        }

        public string EndDate
        {
            get { return _endDate; }
            set
            {
                if (_endDate != value)
                {
                    _endDate = value;
                    OnPropertyChanged(nameof(EndDate));
                }
            }
        }

        public decimal Price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    _price = value;
                    OnPropertyChanged(nameof(Price));
                }
            }
        }

        public string About
        {
            get { return _about; }
            set
            {
                if (_about != value)
                {
                    _about = value;
                    OnPropertyChanged(nameof(About));
                }
            }
        }

        public Destination Destination
        {
            get { return _destination; }
            set
            {
                if (_destination != value)
                {
                    _destination = value;
                    OnPropertyChanged(nameof(Destination));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Package(int packageId, string startDate, string endDate, decimal price, string about, Destination destination)
        {
            PackageId = packageId;
            StartDate = startDate;
            EndDate = endDate;
            Price = price;
            About = about;
            Destination = destination;
        }

        public Package()
        {
            PackageId = 0;
            StartDate = string.Empty;
            EndDate = string.Empty; ;
            Price = 0;
            About = string.Empty;
            Destination = new Destination();
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PackageId);
        }

        public override bool Equals(object obj)
        {
            return obj is Package package &&
                   PackageId == package.PackageId;
        }

        public override string ToString()
        {
            return "\n" + (string)Application.Current.Resources["PackageId"] + " " + PackageId + ", \n" +
                 (string)Application.Current.Resources["StartDate"] + " " + StartDate + ", \n" +
                 (string)Application.Current.Resources["EndDate"] + " " + EndDate + ", \n" +
                  (string)Application.Current.Resources["Price"] + " " + Price + ", \n" +
                   (string)Application.Current.Resources["DestinationNameHint"] + " " + Destination.DestinationName + ", \n" +
                    (string)Application.Current.Resources["About"] + " " + About;
            /*
            return "Aranžman ID: " + PackageId +
                   "\nDatum polaska: " + StartDate +
                   ", Datum povratka: " + EndDate +
                   "\nCijena: " + Price + " KM" +
                   "\nDestinacija: " + Destination.DestinationName +
                   "\nOpis: " + About;
            */
        }

    }
}
