using Microsoft.EntityFrameworkCore; // Ensure this is included
using Supermarket.API.Data;
using Supermarket.API.Domain.Repositories;
using Supermarket.API.Models;
using Supermarket.API.Persistence.Repositories;
using Supermarket.API.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
                .AddJsonOptions(x =>
                {
                    x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                    x.JsonSerializerOptions.WriteIndented = true;
                });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adding dependency injection
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// register Mediator
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

// Fix: Ensure the required package is installed and the correct namespace is used
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("supermarket-api-in-memory"));

//builder.Services.AddAutoMapper();
// Specify the assembly explicitly to resolve the ambiguity
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

// seeding DATABASE
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    SeedDatabase(context); // <- you define this function
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void SeedDatabase(AppDbContext context)
{
    if (!context.Categories.Any())
    {
        context.Categories.AddRange(
            new Category { Id = 1, Name = "Fruits" },
            new Category { Id = 2, Name = "Vegetables" },
            new Category { Id = 3, Name = "Dairy" }
        );
        context.SaveChanges();
    }
    if (!context.Products.Any())
    {
        context.Products.AddRange(
            new Products { Id = 1, Name = "Apple", quantityInPackage = 10, unitOfMeasurement = EUnitOfMeasurement.Unity, CategoryId = 1 },
            new Products { Id = 2, Name = "Carrot", quantityInPackage = 5, unitOfMeasurement = EUnitOfMeasurement.Liter, CategoryId = 2 },
            new Products { Id = 3, Name = "Milk", quantityInPackage = 1, unitOfMeasurement = EUnitOfMeasurement.Kilogram, CategoryId = 3 }
        );
        context.SaveChanges();
    }
}