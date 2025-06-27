using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace HoangTLM.Auth
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddHoangTLMJwtAuthentication(this IServiceCollection services, string issuer, string audience, string secretKey)
        {
            var key = Encoding.UTF8.GetBytes(secretKey);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
                
                // Cấu hình để tránh redirect và trả về 401 thay vì 302
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = async context =>
                    {
                        // Tắt automatic challenge để tránh redirect
                        context.HandleResponse();
                        
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        var result = System.Text.Json.JsonSerializer.Serialize(new { message = "Unauthorized" });
                        await context.Response.WriteAsync(result);
                    }
                };
            });
            return services;
        }
    }
}
