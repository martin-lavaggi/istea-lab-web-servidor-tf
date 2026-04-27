using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Products.Application.Mappings;
using Products.Application.Validations;
using Products.Domain.Models.Entities;
using Products.Domain.Repositories;
using Products.Infrastructure.Data;
using Products.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Repository
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// AutoMapper (escanea Application y Api)
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(Program).Assembly, typeof(ProductProfile).Assembly);
});

// FluentValidation
builder.Services.AddScoped<IValidator<Product>, ProductValidator>();

// Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
