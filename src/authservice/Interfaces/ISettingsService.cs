namespace authservice.Interfaces
{
    public interface ISettingsService
    {
        /// <summary>
        /// Get ClientId object from appsettings.json
        /// </summary>
        /// <returns></returns>
        string GetClientId();
        /// <summary>
        /// Get ClientSecret object from appsettings.json
        /// </summary>
        /// <returns></returns>
        string GetClientSecret();
        /// <summary>
        /// Get BaseUrl object from appsettings.json
        /// </summary>
        /// <returns></returns>
        string GetBaseUrl();
        /// <summary>
        /// Get DeviceCode object from appsettings.json
        /// </summary>
        /// <returns></returns>
        string GetDeviceCode();
        /// <summary>
        /// Get AccessToken object from appsettings.json
        /// </summary>
        /// <returns></returns>
        string GetAccessToken();
        /// <summary>
        /// Get RefreshToken object from appsettings.json
        /// </summary>
        /// <returns></returns>
        string GetRefreshToken();
        /// <summary>
        /// Get VerificationURIComplete object from appsettings.json
        /// </summary>
        /// <returns></returns>
        string GetVerificationURIComplete();

        /// <summary>
        /// Set VerificationURIComplete object in appsettings
        /// </summary>
        void SetVerificationURIComplete(string verificationURIComplete);
        /// <summary>
        /// Set DeviceCode object in appsettings.json
        /// </summary>
        void SetDeviceCode(string deviceCode);
        /// <summary>
        /// Set AccessToken object in appsettings.json
        /// </summary>
        /// <param name="accessToken"></param>
        void SetAccessToken(string accessToken);
        /// <summary>
        /// Set RefreshToken object in appsettings.json
        /// </summary>
        /// <param name="accessToken"></param>
        void SetRefreshToken(string refreshToken);
        /// <summary>
        /// Set AccessToken object exiried date in appsettings.json
        /// </summary>
        /// <param name="accessToken"></param>
        void SetAccessTokenExpiredDate(DateTime accessTokenExpiredDate);

    }
}
