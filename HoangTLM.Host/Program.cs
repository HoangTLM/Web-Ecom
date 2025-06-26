using HoangTLM.Auth;
using HoangTLM.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using HoangTLM.Host;
using Serilog.Extensions.Logging.File;

var builder = WebApplication.CreateBuilder(args);

// Add file logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddFile("logs/app-events.txt", minimumLevel: LogLevel.Information);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Nhập JWT token vào đây (Bearer <token>)"
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddDbContext<HoangTLMDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Đăng ký controller từ cả Host và Application
builder.Services.AddControllers()
    .AddApplicationPart(Assembly.Load("HoangTLM.Application"));

// Only call the new extension method:
builder.Services.AddHoangTLMAuthenticationAndAuthorization(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Map controller route
app.MapControllers();

// Tự động migrate và seed data khi khởi động
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<HoangTLMDbContext>();
    dbContext.Database.Migrate(); // Đảm bảo migrate xong
    HoangTLMDbContextSeeder.SeedData(dbContext); // Seed dữ liệu mẫu nếu chưa có

    // Seed Identity users
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    // Seed admin user
    if (await userManager.FindByEmailAsync("admin@system.local") == null)
    {
        var admin = new IdentityUser
        {
            UserName = "admin",
            Email = "admin@system.local",
            EmailConfirmed = true
        };
        await userManager.CreateAsync(admin, "1q2w3E*"); // Đặt password mặc định
    }
    // Seed guest user
    if (await userManager.FindByEmailAsync("guest@system.local") == null)
    {
        var guest = new IdentityUser
        {
            UserName = "guest",
            Email = "guest@system.local",
            EmailConfirmed = true
        };
        await userManager.CreateAsync(guest, "1qaz2wsx");
    }
}

app.Run();
