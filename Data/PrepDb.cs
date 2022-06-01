using System;
using System.Linq;
using CarService.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CarService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
            if (!context.Cars.Any())
            {
                Console.WriteLine("Seeding data");

                context.Cars.AddRange(
                    new Car() { Make = "VW", Mileage = 2312312 },
                    new Car() { Make = "VW", Mileage = 2312312 },
                    new Car() { Make = "VW", Mileage = 2312312 }
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> Already have data.");
            }
        }
    }
}