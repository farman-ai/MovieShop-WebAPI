FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY MovieShop.ApplicationCore/MovieShop.ApplicationCore.csproj MovieShop.ApplicationCore/
COPY MovieShop.Infrastructure/MovieShop.Infrastructure.csproj MovieShop.Infrastructure/
COPY MovieShop.WebAPI/MovieShop.WebAPI.csproj MovieShop.WebAPI/
RUN dotnet restore MovieShop.WebAPI/MovieShop.WebAPI.csproj

COPY . .
RUN dotnet publish MovieShop.WebAPI/MovieShop.WebAPI.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ConnectionStrings__MovieShopDbConnection="Data Source=/data/movieshop.db"

EXPOSE 8080
VOLUME ["/data"]

ENTRYPOINT ["dotnet", "MovieShop.WebAPI.dll"]
