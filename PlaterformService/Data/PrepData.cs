using Microsoft.EntityFrameworkCore;
using PlatefromService.Data;
using PlatefromService.Models;

namespace PlaterformService.Data
{
    public class PrepData
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeetData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            };

        }

        private static void SeetData(AppDbContext context, bool isProd)
        {
            if (isProd)
            {
                Console.WriteLine("Attempting to aplly migrations...");
                try
                {
                    context.Database.Migrate();
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"Could not aplly migrations...{ex.Message}");
                }
            }

            if (!context.Plateforms.Any())
            {
                Console.WriteLine("------- Seeding Data ------");

                context.Plateforms.AddRange(
                    new Plateform() { Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
                    new Plateform() { Name = "Sql Server Exptess", Publisher = "Microsoft", Cost = "Free" },
                    new Plateform() { Name = "Kubernates", Publisher = "Cloud Native Computing Foundation", Cost = "Free" }
                    );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("------- We already have Data ------");
            }
        }
    }
}
