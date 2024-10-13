namespace authservice.Models
{
    public class AllegroSettingsModel
    {
        public string BaseUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string DeviceCode { get; set; }
        public string AccessToken { get; set; }
        public DateTime? AccessTokenExpiredDate { get; set; }
        public string RefreshToken { get; set; }
        public string VerificationURIComplete { get; set; }
    }
}
