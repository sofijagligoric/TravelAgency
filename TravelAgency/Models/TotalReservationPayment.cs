using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Models
{
    public class TotalReservationPayment
    {
        public string CustomerName {  get; set; }
        public int ReservationId { get; set; }
        public decimal TotalPrice {  get; set; }
        public decimal Payed {  get; set; }
        public decimal Owed {  get; set; }
        public string FullyPayed {  get; set; }

        public TotalReservationPayment(string customerName, int reservationId, decimal totalPrice, decimal payed, decimal owed, string fullyPayed)
        {
            CustomerName = customerName;
            ReservationId = reservationId;
            TotalPrice = totalPrice;
            Payed = payed;
            Owed = owed;
            FullyPayed = fullyPayed;
        }

        public TotalReservationPayment()
        {
            CustomerName = string.Empty;
            ReservationId = 0;
            TotalPrice = 0;
            Payed = 0;
            Owed = 0;
            FullyPayed = string.Empty ;
        }

        public override bool Equals(object obj)
        {
            return obj is TotalReservationPayment payment &&
                   CustomerName == payment.CustomerName &&
                   ReservationId == payment.ReservationId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CustomerName, ReservationId);
        }
    }
}
