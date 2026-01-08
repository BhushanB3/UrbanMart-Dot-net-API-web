using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UrbanMart.Services.AuthAPI.Data;
using UrbanMart.Services.AuthAPI.Models;
using UrbanMart.Services.AuthAPI.Models.DTO;
using UrbanMart.Services.AuthAPI.Services;
using UrbanMart.Services.AuthAPI.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDBContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDBContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

ApplyMigration();

app.MapControllers();

app.Run();


void ApplyMigration()
{
    using(var scope = app.Services.CreateScope())
    {
        var _db = scope.ServiceProvider.GetService<ApplicationDBContext>();
        if (_db.Database.GetPendingMigrations().Any())
        {
            _db.Database.Migrate();
        }
    }
}