using Microsoft.EntityFrameworkCore;
using MovieShop.ApplicationCore.Contracts.Repository;
using MovieShop.ApplicationCore.Contracts.Services;
using MovieShop.Infrastructure.Data;
using MovieShop.Infrastructure.Repository;
using MovieShop.Infrastructure.Services;
using MovieShop.WebAPI.Filters;
using MovieShop.WebAPI.Middleware;
using Serilog;
using Serilog.Formatting.Json;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .WriteTo.Logger(config => config
        .Filter.ByIncludingOnly(logEvent =>
            logEvent.Properties.TryGetValue("LogType", out var value) &&
            value.ToString() == "\"Exception\"")
        .WriteTo.File(new JsonFormatter(), "Logs/exceptions-.json", rollingInterval: RollingInterval.Day))
    .WriteTo.Logger(config => config
        .Filter.ByIncludingOnly(logEvent =>
            logEvent.Properties.TryGetValue("LogType", out var value) &&
            value.ToString() == "\"CreateMovieRequest\"")
        .WriteTo.File(new JsonFormatter(), "Logs/create-movie-requests-.json", rollingInterval: RollingInterval.Day))
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(Log.Logger);
builder.Services.AddScoped<LogCreateMovieRequestFilter>();

builder.Services.AddDbContext<MovieShopDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MovieShopDbConnection")));

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
builder.Services.AddScoped<IAdminService, AdminService>();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
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

try
{
    app.Run();
}
finally
{
    Log.CloseAndFlush();
}
