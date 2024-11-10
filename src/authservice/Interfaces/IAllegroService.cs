using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("authservice.test")]
//accessibility for  MOQ
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace authservice.Interfaces
{
    internal interface IAllegroService
    {
        Task GetDeviceCode(ISettingsService settingService);
        Task<string> SetAccessTokenByRefreshToken(ISettingsService settingService);
        Task<string> GetAccessToken(ISettingsService settingService);   
    }
}
