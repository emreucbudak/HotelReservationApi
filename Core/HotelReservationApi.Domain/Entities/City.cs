namespace HotelReservationApi.Domain.Entities
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PostalCode { get; set; }
        public ICollection<District>? Districts { get; set; }
    }
}
