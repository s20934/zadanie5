using System;
using System.Collections.Generic;

#nullable disable

namespace zadanie5.Models
{
    public partial class ClientTrip
    {
      

        public int IdClient { get; set; }
        public int IdTrip { get; set; }
        public DateTime RegisteredAt { get; set; }
        public DateTime PaymentDate { get; set; }

        public ClientTrip(int idClient, int idTrip, DateTime paymentDate)
        {
            IdClient = idClient;
            IdTrip = idTrip;
            PaymentDate = paymentDate;
            RegisteredAt = DateTime.UtcNow;
        }

        public virtual Client IdClientNavigation { get; set; }
        public virtual Trip IdTripNavigation { get; set; }
     
    }
}
