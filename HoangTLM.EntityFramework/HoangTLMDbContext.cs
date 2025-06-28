using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using HoangTLM.Entities.CoreEntities;

namespace HoangTLM.EntityFramework
{
    public class HoangTLMDbContext : IdentityDbContext<IdentityUser>
    {
        private readonly IConfiguration? _configuration;

        // DbSet cho các entity
        public DbSet<Customer_Customer> Customer_Customers { get; set; }
        public DbSet<Order_Order> Order_Orders { get; set; }
        public DbSet<Order_OrderItem> Order_OrderItems { get; set; }
        public DbSet<Product_Product> Product_Products { get; set; }
        public DbSet<Product_ProductUom> Product_ProductUoms { get; set; }
        public DbSet<Product_Uom> Product_Uoms { get; set; }
        public DbSet<Point_CustomerPoint> Point_CustomerPoints { get; set; }
        public DbSet<Point_PointTransaction> Point_PointTransactions { get; set; }
        public DbSet<Loyalty_Tier> Loyalty_Tiers { get; set; }
        public DbSet<Loyalty_TierHistory> Loyalty_TierHistories { get; set; }
        public DbSet<Voucher_Voucher> Voucher_Vouchers { get; set; }
        public DbSet<Voucher_CustomerVoucher> Voucher_CustomerVouchers { get; set; }
        public DbSet<System_ApiLog> System_ApiLogs { get; set; }
        public DbSet<System_ActionLog> System_ActionLogs { get; set; }
        public DbSet<System_EventLog> System_EventLogs { get; set; }
        public DbSet<Order_Cart> Order_Carts { get; set; }
        public DbSet<Order_CartItem> Order_CartItems { get; set; }
        public DbSet<Customer_Favorite> Customer_Favorites { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public HoangTLMDbContext(DbContextOptions<HoangTLMDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Cấu hình các entity và relationship sẽ được thêm ở đây
            // Identity tables đã được cấu hình tự động

            modelBuilder.Entity<Order_CartItem>()
                .Property(x => x.UnitPrice).HasPrecision(18, 2);
            modelBuilder.Entity<Order_CartItem>()
                .Property(x => x.TotalPrice).HasPrecision(18, 2);

            modelBuilder.Entity<Order_Order>()
                .Property(x => x.TotalAmount).HasPrecision(18, 2);

            modelBuilder.Entity<Order_OrderItem>()
                .Property(x => x.UnitPrice).HasPrecision(18, 2);
            modelBuilder.Entity<Order_OrderItem>()
                .Property(x => x.TotalPrice).HasPrecision(18, 2);

            modelBuilder.Entity<Product_Product>()
                .Property(x => x.Price).HasPrecision(18, 2);

            modelBuilder.Entity<Product_ProductUom>()
                .Property(x => x.Price).HasPrecision(18, 2);

            modelBuilder.Entity<Product_Uom>()
                .Property(x => x.ConversionRate).HasPrecision(18, 4);

            modelBuilder.Entity<Voucher_Voucher>()
                .Property(x => x.DiscountAmount).HasPrecision(18, 2);
            modelBuilder.Entity<Voucher_Voucher>()
                .Property(x => x.DiscountPercent).HasPrecision(5, 2);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Đọc connection string từ appsettings.json
                var connectionString = GetConnectionString();
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        private string GetConnectionString()
        {
            // Tạo configuration builder để đọc appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{GetEnvironment()}.json", optional: true, reloadOnChange: true)
                .Build();

            return configuration.GetConnectionString("DefaultConnection") 
                   ?? "Server=(localdb)\\mssqllocaldb;Database=HoangTLMDb;Trusted_Connection=true;MultipleActiveResultSets=true";
        }

        private string GetEnvironment()
        {
            return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
        }

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

                // Customer_Customer
                if (!context.Customer_Customers.Any())
                {
                    context.Customer_Customers.AddRange(new[]
                    {
                        new Customer_Customer { Id = Guid.NewGuid(), FullName = "Nguyễn Văn A", Email = "a@example.com", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                        new Customer_Customer { Id = Guid.NewGuid(), FullName = "Trần Thị B", Email = "b@example.com", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
                    });
                }

                context.SaveChanges();
            }
        }
    }
}
