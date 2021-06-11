using System.Collections.Generic;

namespace Cinema.Web.Models
{
    public class OrderModel
    {
        public string GuidId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserPhoneNumber { get; set; }
        public decimal Price { get; set; }
        public List<SeatModel> Seats { get; set; }
    }
}
