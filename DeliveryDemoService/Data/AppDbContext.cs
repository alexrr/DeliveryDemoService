using System;
using System.Linq;
using DeliveryDemoService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DeliveryDemoService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<ParcelPoint> ParcelPoints { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().Property(order => order.Details)
                .HasConversion(
                    v => ConvertToString(v),
                    v => ConvertToCollection(v));
            var valueComparer = new ValueComparer<string[]>(
                (c1, c2) => c1 != null && c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c);
            modelBuilder
                .Entity<Order>()
                .Property(order => order.Details)
                .Metadata
                .SetValueComparer(valueComparer);

            modelBuilder.Entity<Order>().Property(order => order.Status)
                .HasConversion(
                    v => (int)v,
                    v => (OrderStatus)v);

            base.OnModelCreating(modelBuilder);
        }

        private static string ConvertToString(string[] strings)
        {
            var ns = strings.Select(s => s.Replace(";", "\\0x3B"));
            return string.Join(';', ns);
        }

        private static string[] ConvertToCollection(string s)
        {
            return s.Split(';').Select(s1 => s1.Replace("\\0x3B", ";")).ToArray();
        }
    }
}