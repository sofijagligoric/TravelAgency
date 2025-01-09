using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Models
{
    public class Payment
    {
        public string CustomerName { get; set; }
        public int ReservationId { get; set; }
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public decimal PayedToHotel { get; set; }
        public decimal PayedToAgency { get; set; }
        public string PhoneNumber { get; set; }
        public string Date { get; set; }
        public string CustomerJMB { get; set; }

        public Payment(string customerName, int reservationId, int paymentId, decimal amount, decimal payedToHotel, decimal payedToAgency, string phoneNumber, string date, string customerJMB)
        {
            CustomerName = customerName;
            ReservationId = reservationId;
            PaymentId = paymentId;
            Amount = amount;
            PayedToHotel = payedToHotel;
            PayedToAgency = payedToAgency;
            PhoneNumber = phoneNumber;
            Date = date;
            CustomerJMB = customerJMB;
        }

        public Payment()
        {
            PaymentId = 0;
            Amount = 0;
            PayedToHotel = 0;
            PayedToAgency = 0;
            ReservationId = 0;
            CustomerName = string.Empty;
            PhoneNumber = string.Empty;
            Date = string.Empty;
            CustomerJMB = string.Empty;
        }

        public override bool Equals(object obj)
        {
            return obj is Payment payment &&
                   PaymentId == payment.PaymentId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PaymentId);
        }
    }
}
