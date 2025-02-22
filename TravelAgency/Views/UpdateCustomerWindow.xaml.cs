﻿using Org.BouncyCastle.Utilities;
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
using System.Net.Sockets;
using System.ComponentModel;

namespace TravelAgency.Views
{
    /// <summary>
    /// Interaction logic for UpdateCustomerWindow.xaml
    /// </summary>
    public partial class UpdateCustomerWindow : Window
    {
        public Customer Customer { get; set; }


        public UpdateCustomerWindow(Customer hotel1)
        {
            InitializeComponent();
            Customer = new Customer(hotel1);
            DataContext = this;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(FirstName.Text) || string.IsNullOrEmpty(Address.Text) || string.IsNullOrEmpty(LastName.Text) ||
               string.IsNullOrEmpty(Email.Text) || string.IsNullOrEmpty(PhoneNumber.Text) || string.IsNullOrEmpty(JMB.Text) || string.IsNullOrEmpty(DateOfBirth.Text))
            // || string.IsNullOrEmpty(HasRestaurant.Text))
            {
                string message = (string)Application.Current.Resources["EmptyField"];
                MessageWithoutOptionDialog dialog = new MessageWithoutOptionDialog(message);
                dialog.ShowDialog();
            }
            else
            {
                Customer.FirstName = FirstName.Text;
                Customer.LastName = LastName.Text;
                Customer.Address = Address.Text;
                Customer.PhoneNumber = PhoneNumber.Text;
                Customer.Email = Email.Text;
                Customer.DateOfBirth = DateOfBirth.Text;
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
