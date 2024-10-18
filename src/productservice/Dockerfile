# Stage 1: Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src  

# Restore dependencies
COPY ["productservice/productservice.csproj", "productservice/"]
RUN dotnet restore "productservice/productservice.csproj"

# Build
COPY productservice/. ./productservice
WORKDIR /src/productservice
RUN dotnet build "productservice.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish  
RUN dotnet publish "productservice.csproj" -c Release -o /app/publish

# Stage 3: Run Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app  
ENV ASPNETCORE_HTTP_PORTS=7099
EXPOSE 7099
COPY --from=publish /app/publish .  
ENTRYPOINT ["dotnet", "productservice.dll"]