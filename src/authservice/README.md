# :lock: authservice
The microservice is responsible for managing authentication and access control when integrating with Allegro's API.

## :mechanical_leg: Installation
### :pushpin: Prerequisites

- **.NET SDK**: .net 8 [Download .NET](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

### 🔧 Usage
Update appsetting.json (**BaseUrl, ClientId, ClientSecret**) keys:
```cmd
cd src\authservice
notepad .\appsettings.json
```

Run app:
```cmd
cd src\authservice
dotnet run
```