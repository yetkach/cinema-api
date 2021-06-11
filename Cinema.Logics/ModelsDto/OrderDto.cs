using System;
using System.Collections.Generic;

namespace Cinema.Logics.ModelsDto
{
    public class OrderDto
    {
        public string GuidId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserPhoneNumber { get; set; }
        public DateTime OrderTime { get; set; }
        public decimal Price { get; set; }
        public List<SeatDto> Seats { get; set; }
    }
}
