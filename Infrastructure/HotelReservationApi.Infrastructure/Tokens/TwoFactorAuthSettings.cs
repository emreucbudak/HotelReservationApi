namespace HotelReservationApi.Infrastructure.Tokens
{
    public class TwoFactorAuthSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int TempTokenExpirationMinutes { get; set; }
        public int OTPCodeExpirationSeconds { get; set; }
        public int ResendCooldownSeconds { get; set; }
    }
}
