using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using orderservice.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapPost("/get", async (IHttpClientFactory httpClientFactory, IConfiguration config, [FromBody] OrderRangeDTO request) =>
{
    var client = httpClientFactory.CreateClient();
    var authServiceUrl = config["AuthService:BaseUrl"];

    var authResponse = await client.GetAsync($"{authServiceUrl}auth");

    if (!authResponse.IsSuccessStatusCode)
    {
        return Results.Problem("Failed to get access token from AuthService.");
    }

    var accessToken = await authResponse.Content.ReadAsStringAsync();

    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
    client.DefaultRequestHeaders.Add("Accept", "application/vnd.allegro.public.v1+json");

    string fromDate = Convert.ToDateTime(request.FromDate).ToString("yyyy-MM-ddTHH:mm:ssZ");
    string toDate = Convert.ToDateTime(request.ToDate).ToString("yyyy-MM-ddTHH:mm:ssZ");

    var response = await client.GetAsync($"https://api.allegro.pl.allegrosandbox.pl/order/checkout-forms?lineItems.boughtAt.gte={fromDate}&lineItems.boughtAt.lte={toDate}");

    if (!response.IsSuccessStatusCode)
    {
        return Results.Problem("Failed to fetch checkout forms from Allegro API.");
    }

    var contents = await response.Content.ReadAsStringAsync();
    var json = JObject.Parse(contents);
    var checkoutForms = json["checkoutForms"];

    if (checkoutForms.Count() < 1)
        return Results.NoContent();

    return Results.Ok(checkoutForms);

})
.WithName("Get")
.WithDescription("Get orders from allegro API")
.WithOpenApi();

app.Run();

