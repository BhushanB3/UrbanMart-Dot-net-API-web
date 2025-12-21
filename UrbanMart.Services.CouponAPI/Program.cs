using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UrbanMart.Services.CouponAPI;
using UrbanMart.Services.CouponAPI.Data;
using UrbanMart.Services.CouponAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//this call our constructor from where data is passed to DbContext which creates connections per HTTP Request
builder.Services.AddDbContext<ApplicationDBContext>(configuration =>
{
    configuration.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly)
);

//IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
//builder.Services.AddSingleton(mapper);
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.CustomSchemaIds(type => type.FullName.Replace("+", "."));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

ApplyPendingMigration();

app.MapControllers();

app.Run();

void ApplyPendingMigration()
{
    using(var scope = app.Services.CreateScope())
    {
        var _db = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
        if (_db.Database.GetPendingMigrations().Any())
        {
            _db.Database.Migrate();
        }
    }
}

/*
 🔧 Internal Flow (Simple):

1. Application start

2. Program.cs me DbContext register hota hai

3. EF Core options banata hai

4. Controller me jab DbContext inject hota hai → DI engine constructor call karta hai

5. ApplicationDBContext(options) call hota hai

6. : base(options) EF Core ko pass hota hai

7. EF Core connection open karta hai

8. Migrations, queries sab is configuration se hoti hain
 */