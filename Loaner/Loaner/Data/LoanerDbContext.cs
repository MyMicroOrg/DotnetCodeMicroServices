using Loaner.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Loaner.Data
{
    public class LoanerDbContext : DbContext
    {
        public LoanerDbContext(DbContextOptions<LoanerDbContext> options) : base(options) { }

        public DbSet<Customer> customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerMap).Assembly);
        }
    }
}
