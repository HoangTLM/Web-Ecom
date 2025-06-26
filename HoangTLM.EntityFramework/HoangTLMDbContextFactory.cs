using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace HoangTLM.EntityFramework
{
    public class HoangTLMDbContextFactory : IDesignTimeDbContextFactory<HoangTLMDbContext>
    {
        public HoangTLMDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<HoangTLMDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new HoangTLMDbContext(optionsBuilder.Options);
        }
    }
} 