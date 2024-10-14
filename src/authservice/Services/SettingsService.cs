using authservice.Interfaces;
using authservice.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using authservice.Extensions;

namespace authservice.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly AllegroSettingsModel _allegroSetttings;
        private readonly IConfiguration _configuration;
        private readonly string _appsettingsPath;
        public SettingsService(IConfiguration configuration)
        {
            _configuration = configuration;
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
            if (environment == "Production")
            {
                _appsettingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
            }
            else
            {
                _appsettingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"appsettings.{environment}.json");
            }

            _allegroSetttings = new AllegroSettingsModel()
            {
                BaseUrl = _configuration.GetValueOrDefault("Allegro:BaseUrl"),
                ClientId = _configuration.GetValueOrDefault("Allegro:ClientId"),
                ClientSecret = _configuration.GetValueOrDefault("Allegro:ClientSecret"),
                DeviceCode = _configuration.GetValueOrDefault("Allegro:DeviceCode"),
                AccessTokenExpiredDate = !String.IsNullOrEmpty(_configuration.GetValueOrDefault("Allegro:AccessTokenExpiredDate")) ? Convert.ToDateTime(_configuration["Allegro:AccessTokenExpiredDate"]) : null,
                AccessToken = _configuration.GetValueOrDefault("Allegro:AccessToken"),
                RefreshToken = _configuration.GetValueOrDefault("Allegro:RefreshToken"),
                VerificationURIComplete = _configuration.GetValueOrDefault("Allegro:VerificationURIComplete")
            };
        }
        public string GetAccessToken() => _allegroSetttings.AccessToken;
        public string GetBaseUrl() => _allegroSetttings.BaseUrl;
        public string GetClientId() => _allegroSetttings.ClientId;
        public string GetClientSecret() => _allegroSetttings.ClientSecret;
        public DateTime? GetAccessTokenExpiredDate() => _allegroSetttings.AccessTokenExpiredDate;


        public string GetDeviceCode() => _allegroSetttings.DeviceCode;

        public string GetRefreshToken() => _allegroSetttings.RefreshToken;

        public string GetVerificationURIComplete() => _allegroSetttings.VerificationURIComplete;

        public async Task SetAccessToken(string accessToken)
        {
            await SaveAsync("Allegro.AccessToken", accessToken);
            _allegroSetttings.AccessToken = accessToken;
        }

        public async Task SetAccessTokenExpiredDate(DateTime? accessTokenExpiredDate)
        {
            await SaveAsync("Allegro.AccessTokenExpiredDate", accessTokenExpiredDate?.ToString() ?? "");
            _allegroSetttings.AccessTokenExpiredDate = accessTokenExpiredDate;
        }

        public async Task SetDeviceCode(string deviceCode)
        {
            await SaveAsync("Allegro.DeviceCode", deviceCode);
            _allegroSetttings.DeviceCode = deviceCode;
        }

        public async Task SetRefreshToken(string refreshToken)
        {
            await SaveAsync("Allegro.RefreshToken", refreshToken);
            _allegroSetttings.RefreshToken = refreshToken;
        }

        public async Task SetVerificationURIComplete(string verificationURIComplete)
        {
            await SaveAsync("Allegro.VerificationURIComplete", verificationURIComplete);
            _allegroSetttings.VerificationURIComplete = verificationURIComplete;
        }

       /// <summary>
       /// Save new value to object in appsettings.json
       /// </summary>
       /// <param name="key"></param>
       /// <param name="value"></param>
        private async Task SaveAsync(string key, string value)
        {
            var jsonObject = await GetJson();
            var selectToken = jsonObject.SelectToken(key);

            if (selectToken != null)
            {
                selectToken.Replace(JToken.FromObject(value));
                await File.WriteAllTextAsync($"{_appsettingsPath}", jsonObject.ToString());
            }
        }
        /// <summary>
        /// Get appsettings.json
        /// </summary>
        /// <returns></returns>
        private async Task<JObject> GetJson()
        {
            var json = await File.ReadAllTextAsync($"{_appsettingsPath}");
            return JObject.Parse(json);
        }
    }
}
