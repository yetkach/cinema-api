using System;
using System.Collections.Generic;

namespace Cinema.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string GuidId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserPhoneNumber { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderTime { get; set; }
        public List<Seat> Seats { get; set; } 
    }
}
