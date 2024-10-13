using authservice.Interfaces;
using authservice.Models;
using Newtonsoft.Json.Linq;

namespace authservice.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly AllegroSettingsModel _allegroSetttings;
        private readonly IConfiguration _configuration;
        public SettingsService(IConfiguration configuration)
        {
            _configuration = configuration;
            _allegroSetttings = new AllegroSettingsModel()
            {
                BaseUrl = _configuration["Allegro:BaseUrl"].ToString(),
                ClientId = _configuration["Allegro:ClientId"].ToString(),
                ClientSecret = _configuration["Allegro:ClientSecret"].ToString(),
                DeviceCode = _configuration["Allegro:DeviceCode"].ToString(),
                AccessTokenExpiredDate = !String.IsNullOrEmpty(_configuration["Allegro:AccessTokenExpiredDate"]) ? Convert.ToDateTime(_configuration["Allegro:AccessTokenExpiredDate"]) : null,
                AccessToken = _configuration["Allegro:AccessToken"],
                RefreshToken = _configuration["Allegro:RefreshToken"],
                VerificationURIComplete = _configuration["Allegro:VerificationURIComplete"]
            };
        }

        public string GetAccessToken() => _allegroSetttings.AccessToken;

        public string GetBaseUrl() => _allegroSetttings.BaseUrl;
        public string GetClientId() => _allegroSetttings.ClientId;
        public string GetClientSecret() => _allegroSetttings.ClientSecret;

        public string GetDeviceCode() => _allegroSetttings.DeviceCode;

        public string GetRefreshToken() => _allegroSetttings.RefreshToken;

        public string GetVerificationURIComplete() => _allegroSetttings.VerificationURIComplete;

        public void SetAccessToken(string accessToken)
        {
            _configuration["Allegro:AccessToken"] = accessToken;
            _allegroSetttings.AccessToken = accessToken;
        }

        public void SetAccessTokenExpiredDate(DateTime accessTokenExpiredDate)
        {
            _configuration["Allegro:AccessTokenExpiredDate"] = accessTokenExpiredDate.ToString();
            _allegroSetttings.AccessTokenExpiredDate = accessTokenExpiredDate;
        }

        public void SetDeviceCode(string deviceCode)
        {
            _configuration["Allegro:DeviceCode"] = deviceCode;
            _allegroSetttings.DeviceCode = deviceCode;
        }

        public void SetRefreshToken(string refreshToken)
        {
            _configuration["Allegro:RefreshToken"] = refreshToken;
            _allegroSetttings.RefreshToken = refreshToken;
        }

        public void SetVerificationURIComplete(string verificationURIComplete)
        {
            _configuration["Allegro:VerificationURIComplete"] = verificationURIComplete;
            _allegroSetttings.VerificationURIComplete = verificationURIComplete;
        }
    }
}
