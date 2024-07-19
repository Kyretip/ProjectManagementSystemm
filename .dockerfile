# Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# .sln dosyasını kopyala ve restore et
COPY *.sln .
COPY Business/Business.csproj Business/
COPY Core/Core.csproj Core/
COPY DataAccess/DataAccess.csproj DataAccess/
COPY Entities/Entities.csproj Entities/
COPY WebAPI/WebAPI.csproj WebAPI/
RUN dotnet restore

# Tüm projeleri build et
COPY . .
WORKDIR /app/WebAPI
RUN dotnet build -c Release -o /app/build

# Web API projeyi yayımla
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Web API projeyi çalıştır
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAPI.dll"]

