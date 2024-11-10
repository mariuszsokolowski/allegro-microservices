using authservice.Exceptions;
using authservice.Interfaces;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json.Nodes;

namespace authservice.Services
{
    internal class AllegroService : IAllegroService
    {
        private readonly HttpClient _httpClient;
        public AllegroService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> GetAccessToken(ISettingsService settingService)
        {
            string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                     .GetBytes($"{settingService.GetClientId()}:{settingService.GetClientSecret()}"));
            JObject json;
            var data = new[]
        {
    new KeyValuePair<string, string>("Content-Type", "application/x-www-form-urlencoded"),
};

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {encoded}");
            var response = await _httpClient.PostAsync($"{settingService.GetBaseUrl()}auth/oauth/token?grant_type=urn:ietf:params:oauth:grant-type:device_code&device_code={settingService.GetDeviceCode()}", new FormUrlEncodedContent(data));
            var contents = await response.Content.ReadAsStringAsync();
            json = JObject.Parse(contents);
            await settingService.SetAccessToken(json["access_token"]?.ToString() ?? "");
            await settingService.SetRefreshToken(json["refresh_token"]?.ToString() ?? "");
            await settingService.SetAccessTokenExpiredDate(DateTime.Now.AddSeconds(double.Parse(json["expires_in"]?.ToString() ?? "0") - 1));
            return json["access_token"]?.ToString() ?? "";
        }

        public async Task GetDeviceCode(ISettingsService settingService)
        {
            string baseURL = settingService.GetBaseUrl();
            JObject json;
            string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                    .GetBytes($"{settingService.GetClientId()}:{settingService.GetClientSecret()}"));
            var data = new[]
                            {
                                new KeyValuePair<string, string>("Content-Type", "application/x-www-form-urlencoded"),
                            };

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {encoded}");
            var response = await _httpClient.PostAsync($"{baseURL}auth/oauth/device?client_id={settingService.GetClientId()}", new FormUrlEncodedContent(data));
            var contents = await response.Content.ReadAsStringAsync();
            json = JObject.Parse(contents);
            await settingService.SetVerificationURIComplete(CheckJsonKeyValue(json, "verification_uri_complete"));
            await settingService.SetDeviceCode(CheckJsonKeyValue(json, "device_code"));
        }
        public async Task<string> SetAccessTokenByRefreshToken(ISettingsService settingService)
        {

            string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                  .GetBytes($"{settingService.GetClientId()}:{settingService.GetClientSecret()}"));
            JObject json;
            var data = new[]
                          { new KeyValuePair<string, string>("Content-Type", "application/x-www-form-urlencoded"),};


            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {encoded}");
            var response = await _httpClient.PostAsync($"{settingService.GetBaseUrl()}auth/oauth/token?grant_type=refresh_token&refresh_token={settingService.GetRefreshToken()}", new FormUrlEncodedContent(data));


            var contents = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
                throw new FailedRefreshTokenRequestException(response.StatusCode,contents);

            json = JObject.Parse(contents);
            await settingService.SetRefreshToken(CheckJsonKeyValue(json, "refresh_token"));
            await settingService.SetAccessToken(CheckJsonKeyValue(json, "access_token"));
            if (!String.IsNullOrEmpty(CheckJsonKeyValue(json, "expires_in")))
                await settingService.SetAccessTokenExpiredDate(DateTime.Now.AddSeconds(double.Parse(CheckJsonKeyValue(json, "expires_in")) - 1));
            return CheckJsonKeyValue(json, "access_token");
        }

        //Check json key is exists
        private string CheckJsonKeyValue(JObject json, string key)
        {
            return json[key]?.ToString() ?? "";
        }

    }
}
