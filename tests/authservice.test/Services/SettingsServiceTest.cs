using authservice.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace authservice.test.Services
{
    public class SettingsServiceTest
    {

        [Fact]
        public void get_base_url_should_return_correct_value()
        {
            // Act
            var result = _settingsService.GetBaseUrl();

            // Assert
            Assert.Equal("https://allegro.pl.allegrosandbox.pl/", result);
        }
        [Fact]
        public void get_client_secret_id_should_return_correct_value()
        {
            // Act
            var result = _settingsService.GetClientSecret();

            // Assert
            Assert.Equal("sampleClientSecret", result);
        }
        [Fact]
        public void get_device_code_should_return_correct_value()
        {
            // Act
            var result = _settingsService.GetDeviceCode();

            // Assert
            Assert.Equal("1234567890", result);
        }
        [Fact]
        public void get_refresh_token_should_return_correct_value()
        {
            // Act
            var result = _settingsService.GetRefreshToken();

            // Assert
            Assert.Equal("sampleRefreshToken", result);
        }
        [Fact]
        public void get_acess_token_should_return_correct_value()
        {
            // Act
            var result = _settingsService.GetAccessToken();

            // Assert
            Assert.Equal("sampleAccessToken", result);
        }
        [Fact]
        public void get_verification_uri_complete_should_return_correct_value()
        {
            // Act
            var result = _settingsService.GetVerificationURIComplete();

            // Assert
            Assert.Equal("https://allegro.pl.allegrosandbox.pl/uzytkownik/bezpieczenstwo/skojarz-aplikacje?code=xxxxxxx", result);
        }
        [Fact]
        public void get_client_id_should_return_correct_value()
        {

            // Act
            var result = _settingsService.GetAccessTokenExpiredDate();

            // Assert
            Assert.Equal(_configurationDate, result);
        }

        #region Arrange
        private readonly SettingsService _settingsService;
        private readonly DateTime _configurationDate;
        public SettingsServiceTest()
        {
            _configurationDate = DateTime.ParseExact("18.10.2024 02:01:42", "dd.MM.yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            var inMemorySettings = new Dictionary<string, string> {
                                    {"Allegro:BaseUrl", "https://allegro.pl.allegrosandbox.pl/"},
                                    {"Allegro:ClientId", "03Sample3dj"},
                                    {"Allegro:ClientSecret", "sampleClientSecret"},
                                    {"Allegro:DeviceCode", "1234567890"},
                                    {"Allegro:RefreshToken", "sampleRefreshToken"},
                                    {"Allegro:AccessToken", "sampleAccessToken"},
                                    {"Allegro:VerificationURIComplete", "https://allegro.pl.allegrosandbox.pl/uzytkownik/bezpieczenstwo/skojarz-aplikacje?code=xxxxxxx"},
                                    {"Allegro:AccessTokenExpiredDate", "18.10.2024 02:01:42"},
                                                                  };
            IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();
            _settingsService = new SettingsService(configuration);
        }
        #endregion
    }
}
