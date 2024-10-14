namespace authservice.Models
{
    public class AllegroSettingsModel
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
        public string DeviceCode { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public DateTime? AccessTokenExpiredDate { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public string VerificationURIComplete { get; set; } = string.Empty ;
    }
}
