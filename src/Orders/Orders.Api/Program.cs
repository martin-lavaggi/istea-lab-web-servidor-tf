using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Orders.Api.Middlewares;
using Orders.Api.Services;
using Orders.Application.Mappings;
using Orders.Application.Validations;
using Orders.Domain.Models.Aggregates;
using Orders.Domain.Repositories;
using Orders.Infrastructure.Data;
using Orders.Infrastructure.Repositories;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("logs/orders.log")
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Repository
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// AutoMapper escanea los profiles tanto del proyecto Api (DTOs de entrada)
// como del proyecto Application (DTOs de salida).
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(Program).Assembly, typeof(OrderProfile).Assembly);
});

// FluentValidation
builder.Services.AddScoped<IValidator<Order>, OrderValidator>();

// Clientes HTTP a los otros microservicios
builder.Services.AddHttpClient<CustomersApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:CustomersApi"]!);
});
builder.Services.AddHttpClient<ProductsApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:ProductsApi"]!);
});

// Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
