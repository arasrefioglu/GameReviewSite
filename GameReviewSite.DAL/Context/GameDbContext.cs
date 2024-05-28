using GameReviewSite.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace GameReviewSite.DAL.Context
{
    public class GameDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
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

            base.OnModelCreating(modelBuilder);


            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("GameReviewSite.Entities"));

            var hasher = new PasswordHasher<User>();
            var adminUser = new User
            {
                Id = Guid.NewGuid(),
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                IsAdmin = true
            };
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Q1w2e3r4.");

            modelBuilder.Entity<User>().HasData(adminUser);

           
            modelBuilder.Entity<GameGenre>().HasData(
                new GameGenre { Id = 1, Name = "Action-Adventure", CreatedDate = DateTime.Now },
                new GameGenre { Id = 2, Name = "Soulslike", CreatedDate = DateTime.Now },
                new GameGenre { Id = 3, Name = "RPG", CreatedDate = DateTime.Now },
                new GameGenre { Id = 4, Name = "Sport", CreatedDate = DateTime.Now },
                new GameGenre { Id = 5, Name = "Racing", CreatedDate = DateTime.Now },
                new GameGenre { Id = 6, Name = "FPS", CreatedDate = DateTime.Now },
                new GameGenre { Id = 7, Name = "Platform", CreatedDate = DateTime.Now }
            );

        }



        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameGenre> GameGenres { get; set; }
        public DbSet<UserRating> UserRatings { get; set; }
        public DbSet<GameComment> GameComments { get; set; }
    }



}


