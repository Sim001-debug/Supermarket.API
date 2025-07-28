using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Supermarket.API.Domain.Repositories;
using Supermarket.API.Domain.Services;
using Supermarket.API.Models;
using Supermarket.API.Persistence.Contexts;
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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("supermarket-api"));

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    SeedDatabase(context);
}

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
    if (!context.Category.Any())
    {
        context.Category.AddRange(
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