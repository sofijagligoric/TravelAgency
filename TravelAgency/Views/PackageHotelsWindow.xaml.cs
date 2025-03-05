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
    /// Interaction logic for PackageHotels.xaml
    /// </summary>
    public partial class PackageHotelsWindow : Window
    {
        public Package Package { get; set; }
      


        public PackageHotelsWindow(Package package)
        {
            InitializeComponent();
            Package = package;
            MainFrame.Content = new PackageHotelsPage(this);

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
