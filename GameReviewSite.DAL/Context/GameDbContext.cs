using GameReviewSite.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace GameReviewSite.DAL.Context
{
    public class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=AREFIOGLU;Database=GameReviewSite;integrated security=true;TrustServerCertificate=true;");
            IConfigurationRoot configuration = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory()) 
                            .AddJsonFile("appsettings.json") 
                            .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefConn"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("GameReviewSite.Entities"));

        }



        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameGenre> GameGenres { get; set; }
        public DbSet<UserRating> UserRatings { get; set; }
        public DbSet<GameComment> GameComments { get; set; }
    }



}


