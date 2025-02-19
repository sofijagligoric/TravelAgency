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
    /// Interaction logic for PayReservationWindow.xaml
    /// </summary>
    public partial class PayReservationWindow : Window
    {
        public Reservation Reservation { get; set; }
        public TotalReservationPayment Payment { get; set; }
        public int AmountPayed { get; set; }

        public PayReservationWindow(Reservation reservation1)
        {
            InitializeComponent();
            Reservation = reservation1;
            Payment = PaymentDataAccess.GetTotalPaymentForReservation(Reservation.ReservationId);
            AmountPayed = 0;
            DataContext = this;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ReservationId.Text) || string.IsNullOrEmpty(Name.Text) || string.IsNullOrEmpty(Price.Text) ||
               string.IsNullOrEmpty(Amount.Text) || string.IsNullOrEmpty(Debt.Text))
            {
                string message = (string)Application.Current.Resources["EmptyField"];
                MessageWithoutOptionDialog dialog = new MessageWithoutOptionDialog(message);
                dialog.ShowDialog();
            }
            else
            {
                AmountPayed = int.Parse(Amount.Text);
                if (Payment != null)
                {
                    if(Payment.Owed < AmountPayed)
                    {
                        string message2 = (string)Application.Current.Resources["AmountTooBig"];
                        MessageWithoutOptionDialog dialog2 = new MessageWithoutOptionDialog(message2);
                        dialog2.ShowDialog();
                    }
                    else
                    {
                        if (Payment.Owed + AmountPayed == Reservation.Price)
                            Reservation.AllPayed = 1;
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
    }
}
