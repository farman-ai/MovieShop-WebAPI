using Microsoft.EntityFrameworkCore;
using MovieShop.ApplicationCore.Contracts.Repository;
using MovieShop.ApplicationCore.Contracts.Services;
using MovieShop.ApplicationCore.Entities;
using MovieShop.Infrastructure.Data;
using MovieShop.Infrastructure.Repository;
using MovieShop.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<MovieShopDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("MovieShopDbConnection")));

builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<ICastRepository, CastRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<ICastService, CastService>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
else
{
    app.UseExceptionHandler();
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => Results.Ok(new
{
    Name = "MovieShop Web API",
    Version = "1.0",
    OpenApi = "/openapi/v1.json"
}));

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MovieShopDbContext>();
    await dbContext.Database.EnsureCreatedAsync();
    await SeedDevelopmentData(dbContext);
}

app.Run();

static async Task SeedDevelopmentData(MovieShopDbContext dbContext)
{
    if (await dbContext.Movies.AnyAsync())
    {
        return;
    }

    var action = new Genre { Name = "Action" };
    var drama = new Genre { Name = "Drama" };
    var comedy = new Genre { Name = "Comedy" };

    var movies = new[]
    {
        new Movie
        {
            Title = "The Matrix",
            Overview = "A hacker discovers the reality he knows is a simulation.",
            Tagline = "Welcome to the real world.",
            Runtime = 136,
            Budget = 63000000,
            Revenue = 467222728,
            PosterUrl = "https://image.tmdb.org/t/p/w342/f89U3ADr1oiB1s9GkdPOEpXUk5H.jpg",
            BackdropUrl = "https://image.tmdb.org/t/p/w1280/fNG7i7RqMErkcqhohV2a6cV1Ehy.jpg",
            ImdbUrl = "https://www.imdb.com/title/tt0133093/",
            TmdbUrl = "https://www.themoviedb.org/movie/603",
            OriginalLanguage = "en",
            ReleaseDate = new DateTime(1999, 3, 31),
            MovieGenres = new List<MovieGenre> { new() { Genre = action } }
        },
        new Movie
        {
            Title = "Forrest Gump",
            Overview = "A kind-hearted man witnesses and influences several defining historical events.",
            Tagline = "Life is like a box of chocolates.",
            Runtime = 142,
            Budget = 55000000,
            Revenue = 677387716,
            PosterUrl = "https://image.tmdb.org/t/p/w342/arw2vcBveWOVZr6pxd9XTd1TdQa.jpg",
            BackdropUrl = "https://image.tmdb.org/t/p/w1280/qdIMHd4sEfJSckfVJfKQvisL02a.jpg",
            ImdbUrl = "https://www.imdb.com/title/tt0109830/",
            TmdbUrl = "https://www.themoviedb.org/movie/13",
            OriginalLanguage = "en",
            ReleaseDate = new DateTime(1994, 7, 6),
            MovieGenres = new List<MovieGenre> { new() { Genre = drama } }
        },
        new Movie
        {
            Title = "Back to the Future",
            Overview = "A teenager is accidentally sent thirty years into the past.",
            Tagline = "He's the only kid ever to get into trouble before he was born.",
            Runtime = 116,
            Budget = 19000000,
            Revenue = 381109762,
            PosterUrl = "https://image.tmdb.org/t/p/w342/fNOH9f1aA7XRTzl1sAOx9iF553Q.jpg",
            BackdropUrl = "https://image.tmdb.org/t/p/w1280/7lyBcpYB0Qt8gYhXYaEZUNlNQAv.jpg",
            ImdbUrl = "https://www.imdb.com/title/tt0088763/",
            TmdbUrl = "https://www.themoviedb.org/movie/105",
            OriginalLanguage = "en",
            ReleaseDate = new DateTime(1985, 7, 3),
            MovieGenres = new List<MovieGenre> { new() { Genre = comedy } }
        }
    };

    await dbContext.Movies.AddRangeAsync(movies);
    await dbContext.SaveChangesAsync();
}
