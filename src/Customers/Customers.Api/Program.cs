using Customers.Api.Middlewares;
using Customers.Application.Mappings;
using Customers.Application.Validations;
using Customers.Domain.Models.Entities;
using Customers.Domain.Repositories;
using Customers.Infrastructure.Data;
using Customers.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("logs/customers.log")
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Repository
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

// AutoMapper escanea los profiles tanto del proyecto Api (DTOs de entrada)
// como del proyecto Application (DTOs de salida).
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(Program).Assembly, typeof(CustomerProfile).Assembly);
});

// FluentValidation
builder.Services.AddScoped<IValidator<Customer>, CustomerValidator>();

// Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS para que el cliente Blazor pueda llamar a esta API desde el navegador.
// Los orígenes permitidos vienen de appsettings.json.
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
if (allowedOrigins == null)
{
    allowedOrigins = new string[0];
}

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.WithOrigins(allowedOrigins).AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.MapControllers();

app.Run();
