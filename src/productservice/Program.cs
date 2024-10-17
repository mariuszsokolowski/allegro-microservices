using Newtonsoft.Json.Linq;
using System.Net;

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

app.UseHttpsRedirection();

app.MapGet("/get", async (IHttpClientFactory httpClientFactory, IConfiguration config, int categoryId) =>
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

    var response = await client.GetAsync($"https://api.allegro.pl.allegrosandbox.pl/sale/categories/{categoryId}/product-parameters");
    var contents = await response.Content.ReadAsStringAsync();

    if (!response.IsSuccessStatusCode)
    {
        string errorMessage = "Failed to fetch products forms from Allegro API.";
        errorMessage += response.StatusCode == HttpStatusCode.UnprocessableEntity ? contents : "";
        return Results.Problem(errorMessage);
    }


    if (contents.Count()<1)
        return Results.NoContent();

    return Results.Json(contents);

})
.WithName("Get")
.WithDescription("Get products from allegro API")
.WithOpenApi();
app.Run();
