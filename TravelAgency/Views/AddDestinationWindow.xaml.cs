using System;
using System.Collections.Generic;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using TravelAgency.DataAccess;
using TravelAgency.Models;

namespace TravelAgency.Views
{
    /// <summary>
    /// Interaction logic for AddDestinationWindow.xaml
    /// </summary>
    public partial class AddDestinationWindow : Window
    {
        public AddDestinationWindow()
        {
            InitializeComponent();
        }

        public Destination Destination { get; set; } = null;

        

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(DestinationName.Text) || string.IsNullOrEmpty(About.Text) || string.IsNullOrEmpty(Postcode.Text) ||
               string.IsNullOrEmpty(Country.Text) || string.IsNullOrEmpty(Distance.Text) || string.IsNullOrEmpty(LocalLanguage.Text))
            {
                string message = (string)Application.Current.Resources["EmptyField"];
                MessageWithoutOptionDialog dialog = new MessageWithoutOptionDialog(message);
                dialog.ShowDialog();
            }
            else
            {
                Country country = DestinationDataAccess.GetCountryByName(Country.Text);
                if (country == null)
                {
                    DestinationDataAccess.AddCountry(new Models.Country(Country.Text));
                }

                Destination = new Destination(int.Parse(Postcode.Text), DestinationName.Text, About.Text, int.Parse(Distance.Text), LocalLanguage.Text, country);
                DialogResult = true;
                Close();
                
            }
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
    }
}
