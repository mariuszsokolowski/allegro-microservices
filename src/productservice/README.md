# 📦 orderservice
The microservice is responsible for managing orders with Allegro's API.

## :mechanical_leg: Installation
### :pushpin: Prerequisites

- **.NET SDK**: `.NET 8.*` [Download .NET](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

### 🔧 Usage
1. Update appsetting.json (**BaseUrl**) keys if authservice is changed:
```cmd
cd src\orderservice
notepad .\appsettings.json
```

2. Run the application
### Run manually:
```cmd
cd src\orderservice
dotnet run
```
### 🐳 Run with Docker:
```cmd
cd src\orderservice
docker build -t orderservice .
docker run -d -p 7155:7155 -e ASPNETCORE_URLS="http://*:7155" orderservice orderservice
```
