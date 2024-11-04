using Microsoft.AspNetCore.Mvc.Testing;

namespace authservice.test
{
    public class authserviceTests : IClassFixture<WebApplicationFactory<Program>>
    {
        
        [Fact]
        public async Task verification_endpoint_should_returns_authorization_message()
        {

            // Act
            var response = await _client.GetAsync("/verification");
            var responseBody = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.Contains("Go to page and authorize device code", responseBody);
        }

        #region Arrange
        private readonly HttpClient _client;

        public authserviceTests(WebApplicationFactory<Program> factory)
        {
            _client=factory.CreateClient();
        }
        #endregion
    }
}
