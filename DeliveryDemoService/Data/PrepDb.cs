using System;
using System.Linq;
using DeliveryDemoService.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DeliveryDemoService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            }
        }

        private static void SeedData(AppDbContext context, bool isProd)
        {
            //if (isProd)
            {
                Console.WriteLine("--> Attempting to apply migrations...");
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not run migrations: {ex.Message}");
                }
            }

            if (!context.Orders.Any())
            {
                context.Orders.AddRange(
                    new Order()
                    {
                        Details = new string[]{ "blue socks", "black box", "red pen" }, Value = 1011,
                        ParcelPointId = "1920-001", CustomerPhone = "+7910-001-02-03", CustomerFullName = "Alex Trump"
                    },
                    new Order()
                    {
                        Details = new string[] { "yellow submarine", "samrtphone", "gold plate" },
                        Value = 2917, ParcelPointId = "2350-102", CustomerPhone = "+7631-100-20-30",
                        CustomerFullName = "Joe Biden"
                    }
                );

                context.SaveChanges();
            }

            if (!context.ParcelPoints.Any())
            {
                context.ParcelPoints.AddRange(
                    new ParcelPoint()
                    {
                        Id = "1920-001",
                        Address = "very precise address",
                        Status = true
                    },
                    new ParcelPoint()
                    {
                        Id = "2350-102",
                        Address = "very precise address2",
                        Status = true
                    },
                    new ParcelPoint()
                    {
                        Id = "2350-404",
                        Address = "very precise address3",
                        Status = false
                    }

                );
                context.SaveChanges();
            }
        }
    }
}