# Stage 1: Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src  

# Restore dependencies
COPY ["orderservice.csproj", "./"]
RUN dotnet restore "orderservice.csproj"

# Build
COPY . ./orderservice
WORKDIR /src/orderservice
RUN dotnet build "orderservice.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish  
RUN dotnet publish "orderservice.csproj" -c Release -o /app/publish

# Stage 3: Run Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app  
ENV ASPNETCORE_HTTP_PORTS=7155
EXPOSE 7155
COPY --from=publish /app/publish .  
ENTRYPOINT ["dotnet", "orderservice.dll"]