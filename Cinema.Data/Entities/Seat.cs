namespace Cinema.Data.Entities
{
    public class Seat
    {
        public int Id { get; set; }
        public string OrderGuid { get; set; }
        public int MovieId { get; set; }
        public int? Row { get; set; }
        public int? Number { get; set; }
    }
}
