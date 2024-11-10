namespace authservice.Extensions
{
    internal static class ConfigurationExtensions
    {
        public static string GetValueOrDefault(this IConfiguration configuration, string key)
        {
            return configuration.GetValue<string>(key) ?? "";
        }
    }
}
