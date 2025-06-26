using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HoangTLM.Auth;
using HoangTLM.Core.Repository;
using HoangTLM.EntityFramework;
using System;

namespace HoangTLM.Host
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHoangTLMAuthenticationAndAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            // Register JWT authentication from HoangTLM.Auth
            services.AddHoangTLMJwtAuthentication(
                issuer: "ExampleIssuer",
                audience: "ExampleAudience",
                secretKey: "SuperSecretKey123456!"
            );

            services.AddAuthorization();

            services.AddDbContext<HoangTLMDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<HoangTLMDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
    }
} 