namespace DataLayer;
using BusinessLayer;
using Microsoft.EntityFrameworkCore;

public class GamingDbContext : DbContext
{
    public GamingDbContext() : base()
    {

    }

    public GamingDbContext(DbContextOptions options)
    : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-L8OO2I0;Database=LibraryDb;Trusted_Connection=True;TrustServerCertificate=True;");
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Game> Games { get; set; }

    public DbSet<GameGenre> GameGenres { get; set; }

    public DbSet<User> Users { get; set; }
}

