using System;
using BillingApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BillingApi.Data
{
    public class BillAppDbContext : DbContext
    {
        public BillAppDbContext(DbContextOptions<BillAppDbContext> options):base(options)
        {}

        public DbSet<Item> Items {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.seed();

            // Add Name Uniqueness
            modelBuilder.Entity<Item>()
                        .HasIndex(item => item.Name)
                        .IsUnique();
        }
    }
}