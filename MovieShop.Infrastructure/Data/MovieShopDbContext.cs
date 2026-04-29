using Microsoft.EntityFrameworkCore;
using MovieShop.ApplicationCore.Entities;

namespace MovieShop.Infrastructure.Data;

public class MovieShopDbContext : DbContext
{
    public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options) : base(options)
    {
    }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<MovieGenre> MovieGenres { get; set; }
    public DbSet<Cast> Casts { get; set; }
    public DbSet<MovieCast> MovieCasts { get; set; }
    public DbSet<Trailer> Trailers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<Favorite> Favorites { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>()
            .Property(m => m.Budget)
            .HasPrecision(18, 4);

        modelBuilder.Entity<Movie>()
            .Property(m => m.Revenue)
            .HasPrecision(18, 4);

        modelBuilder.Entity<Purchase>()
            .Property(p => p.TotalPrice)
            .HasPrecision(5, 2);

        modelBuilder.Entity<Review>()
            .Property(r => r.Rating)
            .HasPrecision(3, 2);

        modelBuilder.Entity<MovieGenre>()
            .HasKey(mg => new { mg.MovieId, mg.GenreId });

        modelBuilder.Entity<MovieCast>()
            .HasKey(mc => new { mc.MovieId, mc.CastId });

        modelBuilder.Entity<Favorite>()
            .HasKey(f => new { f.UserId, f.MovieId });

        modelBuilder.Entity<Review>()
            .HasKey(r => new { r.UserId, r.MovieId });

        modelBuilder.Entity<Purchase>()
            .HasKey(p => new { p.UserId, p.MovieId });

        modelBuilder.Entity<UserRole>()
            .HasKey(ur => new { ur.UserId, ur.RoleId });

        modelBuilder.Entity<Role>()
            .Property(r => r.Name)
            .HasMaxLength(20)
            .IsRequired();

        modelBuilder.Entity<Genre>()
            .Property(g => g.Name)
            .HasMaxLength(24)
            .IsRequired();

        modelBuilder.Entity<Cast>()
            .Property(c => c.Name)
            .HasMaxLength(128)
            .IsRequired();

        modelBuilder.Entity<MovieCast>()
            .Property(mc => mc.Character)
            .HasMaxLength(450)
            .IsRequired();

        modelBuilder.Entity<Trailer>()
            .Property(t => t.Name)
            .HasMaxLength(2084)
            .IsRequired();

        modelBuilder.Entity<Trailer>()
            .Property(t => t.TrailerUrl)
            .HasMaxLength(2084)
            .IsRequired();

        modelBuilder.Entity<Movie>()
            .Property(m => m.Title)
            .HasMaxLength(256)
            .IsRequired();

        modelBuilder.Entity<Movie>()
            .Property(m => m.Tagline)
            .HasMaxLength(512);

        modelBuilder.Entity<Movie>()
            .Property(m => m.PosterUrl)
            .HasMaxLength(2084);

        modelBuilder.Entity<Movie>()
            .Property(m => m.BackdropUrl)
            .HasMaxLength(2084);

        modelBuilder.Entity<Movie>()
            .Property(m => m.ImdbUrl)
            .HasMaxLength(2084);

        modelBuilder.Entity<Movie>()
            .Property(m => m.TmdbUrl)
            .HasMaxLength(2084);

        modelBuilder.Entity<Movie>()
            .Property(m => m.OriginalLanguage)
            .HasMaxLength(64);

        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .HasMaxLength(256)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(u => u.FirstName)
            .HasMaxLength(128)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(u => u.LastName)
            .HasMaxLength(128);

        modelBuilder.Entity<User>()
            .Property(u => u.PhoneNumber)
            .HasMaxLength(16);

        modelBuilder.Entity<User>()
            .Property(u => u.HashedPassword)
            .HasMaxLength(1024)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(u => u.Salt)
            .HasMaxLength(1024)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(u => u.ProfilePictureUrl)
            .HasMaxLength(2048);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}