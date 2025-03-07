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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using TravelAgency.Models;
using TravelAgency.Util;
using TravelAgency.DataAccess;

namespace TravelAgency.Views
{
    /// <summary>
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        Employee Employee { get; set; }
        public string New = null;
        public ChangePasswordWindow(Employee employee)
        {
            InitializeComponent();
            DataContext = this;
            Employee = employee;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NewPassword.Text) || string.IsNullOrEmpty(NewPassword2.Text) || string.IsNullOrEmpty(CurrentPassword.Text))

            {
                string message = (string)Application.Current.Resources["EmptyField"];
                MessageWithoutOptionDialog dialog = new MessageWithoutOptionDialog(message);
                dialog.ShowDialog();
            }
            else
            {
                if (!Employee.Password.Equals(General.HashPassword(CurrentPassword.Text)))
                {
                    string message2 = (string)Application.Current.Resources["CurrentDontMatch"];
                    MessageWithoutOptionDialog dialog2 = new MessageWithoutOptionDialog(message2);
                    dialog2.ShowDialog();
                }
                else
                {
                    if (!NewPassword.Text.Equals(NewPassword2.Text))
                    {
                        string message3 = (string)Application.Current.Resources["PasswordsDontMatch"];
                        MessageWithoutOptionDialog dialog3 = new MessageWithoutOptionDialog(message3);
                        dialog3.ShowDialog();
                    }
                    else
                    {
                        New = General.HashPassword(NewPassword2.Text);
                        DialogResult = true;
                        Close();
                    }
                }

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
