using authservice.Infrastructures;
using authservice.Interfaces;
using authservice.Services;
using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ISettingsService, SettingsService>();
builder.Services.AddTransient<IAllegroService, AllegroService>();
builder.Services.AddHttpClient<IAllegroService, AllegroService>();

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();   

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/verification", async (ISettingsService settingService, IAllegroService allegroService) =>
{
    await allegroService.GetDeviceCode(settingService);
    await settingService.SetAccessTokenExpiredDate(null);
    return $"Go to page and authorize device code. Then go to endpoint /auth {settingService.GetVerificationURIComplete()}";
})
.WithName("Verification")
.WithDescription("Get DeviceCode using ClientId and UserSecret from appsettings.json")
.WithOpenApi();

app.MapGet("/auth", async (ISettingsService settingService, IAllegroService allegroService) =>
{
    if (!String.IsNullOrEmpty(settingService.GetAccessTokenExpiredDate().ToString()) && settingService.GetAccessTokenExpiredDate() < DateTime.Now.AddMinutes(15))
    {
        await allegroService.SetAccessTokenByRefreshToken(settingService);
    }
    else if (String.IsNullOrEmpty(settingService.GetAccessTokenExpiredDate().ToString()))
        await allegroService.GetAccessToken(settingService);
    return settingService.GetAccessToken();
})
.WithName("Authorization")
.WithDescription("Get DeviceCode using ClientId and UserSecret from appsettings.json");


app.Run();
//For testing
public partial class Program { }

