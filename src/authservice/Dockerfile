# Stage 1: Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src  

# Restore dependencies
COPY ["authservice/authservice.csproj", "authservice/"]
RUN dotnet restore "authservice/authservice.csproj"

# Build
COPY authservice/. ./authservice
WORKDIR /src/authservice
RUN dotnet build "authservice.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish  
RUN dotnet publish "authservice.csproj" -c Release -o /app/publish

# Stage 3: Run Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app  
ENV ASPNETCORE_HTTP_PORTS=7283
EXPOSE 7283
COPY --from=publish /app/publish .  
ENTRYPOINT ["dotnet", "authservice.dll"]