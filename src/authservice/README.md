# :lock: authservice
The microservice is responsible for managing authentication and access control when integrating with Allegro's API.

## :mechanical_leg: Installation
### :pushpin: Prerequisites

- **.NET SDK**: `.NET 8.*` [Download .NET](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

### 🔧 Usage
1. Update appsetting.json (**BaseUrl, ClientId, ClientSecret**) keys:
```cmd
cd src\authservice
notepad .\appsettings.json
```

2. Run the application:
```cmd
cd src\authservice
dotnet run
```
3. Authorize the device code on the Allegro authorization page:
- To make an HTTP GET request to the /verification endpoint, use the following curl command:

```cmd
curl  https://localhost:7283/verification
```

You will receive a URL similar to: `https://allegro.pl.allegrosandbox.pl/uzytkownik/bezpieczenstwo/skojarz-aplikacje?code=****`

4. Open the URL in your browser and authorize the device code 
 
5. After authorization, retrieve the AccessToken:

-Send a GET request to the /auth endpoint
```cmd
curl  https://localhost:7283/auth
```

You will receive the  **AccessToken**