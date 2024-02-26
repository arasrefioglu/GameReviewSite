﻿using GameReviewSite.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GameReviewSite.DAL
{
    public class GameDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=AREFIOGLU;Database=GameReviewSite;integrated security=true;TrustServerCertificate=true;");
            //IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            //optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefConn"));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameGenre> GameGenres { get; set; }
        public DbSet<GameWebsite> GameWebsites { get; set; }
        public DbSet<CurrencyType> CurrencyTypes { get; set; }
        public DbSet<GamePrice> GamePrices { get; set; }
        public DbSet<UserRating> UserRatings { get; set; }
        public DbSet<GameComment> GameComments { get; set; }




    }
}
