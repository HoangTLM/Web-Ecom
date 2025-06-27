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
            // Add Identity first
            services.AddDbContext<HoangTLMDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<HoangTLMDbContext>()
                .AddDefaultTokenProviders();

            // Register JWT authentication from HoangTLM.Auth using configuration values
            services.AddHoangTLMJwtAuthentication(
                issuer: configuration["JwtSettings:Issuer"] ?? "HoangTLM",
                audience: configuration["JwtSettings:Audience"] ?? "HoangTLMUsers",
                secretKey: configuration["JwtSettings:SecretKey"] ?? "HoangTLM_Web_Api_Key_For_Learning_2025"
            );

            services.AddAuthorization();

            // Register Repository with HoangTLMDbContext
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<DbContext, HoangTLMDbContext>();

            return services;
        }
    }
} 