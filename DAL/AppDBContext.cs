using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DataAccessLayer
{
    public class AppDBContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=games.db");
        }

        // Опционально: настройка отношений между сущностями
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка отношений: одна игра — много отзывов
            modelBuilder.Entity<Game>()
                .HasMany(g => g.Reviews)
                .WithOne()
                .HasForeignKey("GameId");

            // Опционально: настройка значений по умолчанию
            modelBuilder.Entity<Game>()
                .Property(g => g.Name)
                .HasDefaultValue("Unknown");
            modelBuilder.Entity<Game>()
                .Property(g => g.Developer)
                .HasDefaultValue("Unknown");
            modelBuilder.Entity<Game>()
                .Property(g => g.Description)
                .HasDefaultValue("Empty");
        }
    }
}
