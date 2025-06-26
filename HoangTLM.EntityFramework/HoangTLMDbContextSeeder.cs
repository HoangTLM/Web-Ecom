using System;
using System.Linq;
using HoangTLM.Entities.CoreEntities;
using Microsoft.AspNetCore.Identity;

namespace HoangTLM.EntityFramework
{
    public static class HoangTLMDbContextSeeder
    {
        public static void SeedData(HoangTLMDbContext context)
        {
            // Product_Uom
            if (!context.Product_Uoms.Any())
            {
                context.Product_Uoms.AddRange(new[]
                {
                    new Product_Uom { Id = Guid.NewGuid(), Name = "Cái", Description = "Đơn vị cái", ConversionRate = 1 },
                    new Product_Uom { Id = Guid.NewGuid(), Name = "Hộp", Description = "Đơn vị hộp", ConversionRate = 10 },
                    new Product_Uom { Id = Guid.NewGuid(), Name = "Thùng", Description = "Đơn vị thùng", ConversionRate = 100 }
                });
            }

            // Loyalty_Tier
            if (!context.Loyalty_Tiers.Any())
            {
                context.Loyalty_Tiers.AddRange(new[]
                {
                    new Loyalty_Tier { Id = Guid.NewGuid(), Name = "Silver", MinPoints = 0, Description = "Thành viên bạc", Benefits = "Tích điểm cơ bản" },
                    new Loyalty_Tier { Id = Guid.NewGuid(), Name = "Gold", MinPoints = 1000, Description = "Thành viên vàng", Benefits = "Tích điểm + ưu đãi vàng" },
                    new Loyalty_Tier { Id = Guid.NewGuid(), Name = "Platinum", MinPoints = 5000, Description = "Thành viên bạch kim", Benefits = "Tích điểm + ưu đãi bạch kim" }
                });
            }

            // Product_Product
            if (!context.Product_Products.Any())
            {
                context.Product_Products.AddRange(new[]
                {
                    new Product_Product { Id = Guid.NewGuid(), Name = "Sản phẩm A", Price = 100000, Stock = 100, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new Product_Product { Id = Guid.NewGuid(), Name = "Sản phẩm B", Price = 200000, Stock = 50, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
                });
            }

            // Product_Product (thêm 10 sản phẩm)
            if (context.Product_Products.Count() < 10)
            {
                for (int i = 1; i <= 10; i++)
                {
                    if (!context.Product_Products.Any(p => p.Name == $"Sản phẩm {i}"))
                    {
                        context.Product_Products.Add(new Product_Product
                        {
                            Id = Guid.NewGuid(),
                            Name = $"Sản phẩm {i}",
                            Price = 100000 * i,
                            Stock = 100 + i * 10,
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        });
                    }
                }
            }

            // Customer_Customer
            if (!context.Customer_Customers.Any())
            {
                context.Customer_Customers.AddRange(new[]
                {
                    new Customer_Customer { Id = Guid.NewGuid(), FullName = "Nguyễn Văn A", Email = "a@example.com", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new Customer_Customer { Id = Guid.NewGuid(), FullName = "Trần Thị B", Email = "b@example.com", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
                });
            }

            // Customer_Customer (thêm 5 user)
            if (context.Customer_Customers.Count() < 5)
            {
                for (int i = 1; i <= 5; i++)
                {
                    if (!context.Customer_Customers.Any(c => c.Email == $"user{i}@example.com"))
                    {
                        context.Customer_Customers.Add(new Customer_Customer
                        {
                            Id = Guid.NewGuid(),
                            FullName = $"User {i}",
                            Email = $"user{i}@example.com",
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        });
                    }
                }
            }

            // Seed user mặc định: admin và guest
            if (!context.Customer_Customers.Any(c => c.Email == "admin@system.local"))
            {
                context.Customer_Customers.Add(new Customer_Customer
                {
                    Id = Guid.NewGuid(),
                    FullName = "Admin",
                    Email = "admin@system.local",
                    // Có thể thêm trường PasswordHash nếu có, ở đây chỉ seed thông tin cơ bản
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                });
            }
            if (!context.Customer_Customers.Any(c => c.Email == "guest@system.local"))
            {
                context.Customer_Customers.Add(new Customer_Customer
                {
                    Id = Guid.NewGuid(),
                    FullName = "Guest",
                    Email = "guest@system.local",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                });
            }

            context.SaveChanges();
        }
    }
} 