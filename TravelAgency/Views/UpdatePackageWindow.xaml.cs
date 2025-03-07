using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TravelAgency.DataAccess;
using TravelAgency.Models;

namespace TravelAgency.Views
{
    /// <summary>
    /// Interaction logic for UpdatePackageWindow.xaml
    /// </summary>
    public partial class UpdatePackageWindow : Window
    {
        public Package Package { get; set; }

        public UpdatePackageWindow(Package package)
        {
            InitializeComponent();
            DataContext = this;
            Package = new Package(package);

        }


        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(StartDate.Text) || string.IsNullOrEmpty(Price.Text) || string.IsNullOrEmpty(EndDate.Text) || string.IsNullOrEmpty(About.Text))
            {
                string message = (string)Application.Current.Resources["EmptyField"];
                MessageWithoutOptionDialog dialog = new MessageWithoutOptionDialog(message);
                dialog.ShowDialog();
            }
            else
            {
                if (HasValidationError(StartDate) || HasValidationError(Price) ||
                       HasValidationError(EndDate))
                {
                    string message = (string)Application.Current.Resources["InvalidInput"];
                    MessageWithoutOptionDialog dialog = new MessageWithoutOptionDialog(message);
                    dialog.ShowDialog();
                }
                else
                {
                    Package.About = About.Text;
                    Package.EndDate = EndDate.Text;
                    Package.StartDate = StartDate.Text;
                    Package.Price = decimal.Parse(Price.Text, System.Globalization.CultureInfo.InvariantCulture);

                    DialogResult = true;
                    Close();

                }
            }
        }


        private bool HasValidationError(Control control)
        {
            return Validation.GetHasError(control);
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
