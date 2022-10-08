using Microsoft.EntityFrameworkCore;
using PlatefromService.Models;

namespace PlatefromService.Data
{

    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Plateform> Plateforms { get; set; }
    }

}