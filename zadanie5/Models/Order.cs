using System;

namespace zadanie5.Models
{
    public class Order
    {
        //client
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Pesel { get; set; }
        //trip
        public int IdTrip { get; set; }
        public string Name { get; set; }
        //client trip
        public DateTime PaymentDate { get; set; }






    }
}
