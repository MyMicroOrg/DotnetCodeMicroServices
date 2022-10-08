using CommandService.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandService.Data
{
   public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions opt) : base(opt)
        {

        }

        public DbSet<Platform> Platfroms { get; set; }
        public DbSet<Command> Commands { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
            .Entity<Platform>()
            .HasMany(p => p.Commands)
            .WithOne(p => p.PlatForm)
            .HasForeignKey(p => p.PlatformId);

            modelBuilder
            .Entity<Command>()
            .HasOne(p => p.PlatForm)
            .WithMany(p => p.Commands)
            .HasForeignKey(p => p.PlatformId);
        }
    }
}