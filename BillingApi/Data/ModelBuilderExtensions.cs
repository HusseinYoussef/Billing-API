using System;
using BillingApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BillingApi.Data
{
    public static class ModelBuilderExtensions
    {
        public static void seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().HasData(
                new Item()
                {
                    Id = 1,
                    Name = "T-Shirt",
                    Manufacturer = "USA",
                    Price = 200,
                    Discount = 10
                },
                new Item()
                {
                    Id = 2,
                    Name = "Pants",
                    Manufacturer = "CA",
                    Price = 150,
                    Discount = 5
                },
                new Item()
                {
                    Id = 3,
                    Name = "Hat",
                    Manufacturer = "China",
                    Price = 50,
                    Discount = 0
                },
                new Item()
                {
                    Id = 4,
                    Name = "Shoes",
                    Manufacturer = "UK",
                    Price = 100,
                    Discount = 10
                },
                new Item()
                {
                    Id = 5,
                    Name = "Suit",
                    Manufacturer = "USA",
                    Price = 1000,
                    Discount = 15
                },
                new Item()
                {
                    Id = 6,
                    Name = "Jacket",
                    Manufacturer = "USA",
                    Price = 300,
                    Discount = 10
                },
                new Item()
                {
                    Id = 7,
                    Name = "Dress",
                    Manufacturer = "USA",
                    Price = 500,
                    Discount = 15
                }

            );
        }
    }
}