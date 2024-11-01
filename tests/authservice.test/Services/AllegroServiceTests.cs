using authservice.Interfaces;
using authservice.Services;
using Moq;
using Moq.Protected;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace authservice.test.Services
{
    public class AllegroServiceTests
    {

        [Fact]
        public async Task get_acess_token_should_return_correct_value()
        {
            // Arrange
            var responseJson = new JObject
            {
                { "access_token", "testAccessToken" },
                { "refresh_token", "testRefreshToken" },
                { "expires_in", "3600" }
            };

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseJson.ToString())
                });

            // Act
            var accessToken = await _allegroService.GetAccessToken(_mockSettingsService.Object);

            // Assert
            Assert.Equal("testAccessToken", accessToken);
            _mockSettingsService.Verify(s => s.SetAccessToken("testAccessToken"), Times.Once);
            _mockSettingsService.Verify(s => s.SetRefreshToken("testRefreshToken"), Times.Once);
            _mockSettingsService.Verify(s => s.SetAccessTokenExpiredDate(It.IsAny<DateTime>()), Times.Once);
        }

        [Fact]
        public async Task get_device_code_should_set_device_code_and_verification_uri()
        {
            // Arrange
            var responseJson = new JObject
            {
                { "device_code", "testDeviceCode" },
                { "verification_uri_complete", "https://test.api/verify" }
            };

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseJson.ToString())
                });

            // Act
            await _allegroService.GetDeviceCode(_mockSettingsService.Object);

            // Assert
            _mockSettingsService.Verify(s => s.SetDeviceCode("testDeviceCode"), Times.Once);
            _mockSettingsService.Verify(s => s.SetVerificationURIComplete("https://test.api/verify"), Times.Once);
        }

        [Fact]
        public async Task set_access_token_by_refres_token_should_return_new_access_token_correct_value()
        {

            // Arrange
            var responseJson = new JObject
            {
                { "access_token", "newAccessToken" },
                { "refresh_token", "newRefreshToken" },
                { "expires_in", "3600" }
            };

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseJson.ToString())
                });

            // Act
            var accessToken = await _allegroService.SetAccessTokenByRefreshToken(_mockSettingsService.Object);

            // Assert
            Assert.Equal("newAccessToken", accessToken);
            _mockSettingsService.Verify(s => s.SetAccessToken("newAccessToken"), Times.Once);
            _mockSettingsService.Verify(s => s.SetRefreshToken("newRefreshToken"), Times.Once);
            _mockSettingsService.Verify(s => s.SetAccessTokenExpiredDate(It.IsAny<DateTime>()), Times.Once);
        }

        #region Arrange
        private readonly Mock<ISettingsService> _mockSettingsService;
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private readonly IAllegroService _allegroService;

        public AllegroServiceTests()
        {
            _mockSettingsService = new Mock<ISettingsService>();
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            var httpClient = new HttpClient(_mockHttpMessageHandler.Object);
            _allegroService = new AllegroService(httpClient);

            _mockSettingsService.Setup(s => s.GetClientId()).Returns("testClientId");
            _mockSettingsService.Setup(s => s.GetClientSecret()).Returns("testClientSecret");
            _mockSettingsService.Setup(s => s.GetBaseUrl()).Returns("https://test.api/");
            _mockSettingsService.Setup(s => s.GetDeviceCode()).Returns("testDeviceCode");
        }
        #endregion
    }
}
