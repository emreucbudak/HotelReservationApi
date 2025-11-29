namespace HotelReservationApi.Domain.Entities
{
    public class EmailModel
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public int? VerificationCode { get; set; }
        public Stream? AttachmentStream { get; set; }
        public string? AttachmentFileName { get; set; }
    }
}
