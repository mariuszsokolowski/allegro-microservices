# 📦 productservice
The microservice is responsible for products with Allegro's API.

## :mechanical_leg: Installation
### :pushpin: Prerequisites

- **.NET SDK**: `.NET 8.*` [Download .NET](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

### 🔧 Usage
1. Update appsetting.json (**BaseUrl**) keys if authservice is changed:
```cmd
cd src\productservice
notepad .\appsettings.json
```

2. Run the application
### Run manually:
```cmd
cd src\productservice
dotnet run
```
### 🐳 Run with Docker:
```cmd
cd src\productservice
docker build -t productservice .
docker run -d -p 7099:7099 -e ASPNETCORE_URLS="http://*:7155" orderservice orderservice
```
