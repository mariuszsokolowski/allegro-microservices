namespace authservice.Interfaces
{
    internal interface IAllegroService
    {
        Task GetDeviceCode(ISettingsService settingService);
        Task<string> SetAccessTokenByRefreshToken(ISettingsService settingService);
        Task<string> GetAccessToken(ISettingsService settingService);   
    }
}
