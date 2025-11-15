using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.SqlServer;
using Entities;

namespace DataAccessLayer
{
    public class AppDBContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Projects\\Homework\\C#\\Lab 3.1\\Low-tier_critic\\Data Base\\DB_Low_tier_critic.mdf\";Integrated Security=True");
        }

        private static string SerializePlatforms(List<EnumPlatforms> platforms)
        {
            if (platforms == null || platforms.Count == 0)
                return string.Empty;

            return string.Join(',', platforms.Select(p => (int)p));
        }
        private static List<EnumPlatforms> DeserializePlatforms(string platformsString)
        {
            if (string.IsNullOrWhiteSpace(platformsString))
                return new List<EnumPlatforms>();

            var result = new List<EnumPlatforms>();
            var parts = platformsString.Split(',', StringSplitOptions.RemoveEmptyEntries);

            foreach (var part in parts)
            {
                if (int.TryParse(part, out int value) &&
                    Enum.IsDefined(typeof(EnumPlatforms), (EnumPlatforms)value))
                {
                    result.Add((EnumPlatforms)value);
                }
            }

            return result;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Game>()
                .Property(g => g.Platforms)
                .HasConversion(
                    v => SerializePlatforms(v),

                    v => DeserializePlatforms(v)
                );

            modelBuilder.Entity<Game>()
                .HasMany(g => g.Reviews)
                .WithOne()
                .HasForeignKey("GameId");
        }
    }
}
