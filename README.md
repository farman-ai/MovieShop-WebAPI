# MovieShop Web API

Standalone ASP.NET Core Web API version of the MovieShop project.

## Projects

- `MovieShop.WebAPI` - API host and controllers
- `MovieShop.ApplicationCore` - entities, contracts, and models
- `MovieShop.Infrastructure` - EF Core data access, repositories, migrations, and services

## Useful Endpoints

- `GET /`
- `GET /openapi/v1.json`
- `GET /api/movies`
- `GET /api/movies/{id}`
- `GET /api/movies/genre/{genreId}`
- `POST /api/movies/{id}/purchase`
- `GET /api/genres`
- `GET /api/cast/{id}`
- `POST /api/account/register`
- `POST /api/account/login`
- `GET /api/users/{userId}/purchases`
- `POST /api/users/{userId}/reviews`
- `GET /api/admin/top-movies`

## Run

```bash
dotnet run --project MovieShop.WebAPI/MovieShop.WebAPI.csproj
```
