# Laboratorio de Aplicaciones Web Servidor — Trabajo Final

Solución de microservicios en ASP.NET Core 8 siguiendo Clean Architecture + DDD,
con Entity Framework Core sobre SQL Server.

## Microservicios
- **Products** — CRUD de productos (nombre, descripción, precio, stock).
- **Customers** — CRUD de clientes (nombre, email, dirección, fecha de registro).
- **Orders** — Crear órdenes a partir de un cliente y productos. Calcula total, descuenta stock vía HTTP a Products.

## Stack
- ASP.NET Core 8 Web API + Swagger
- Entity Framework Core 8 (Code First + Migrations) sobre SQL Server
- Clean Architecture (Api / Application / Domain / Infrastructure)
- AutoMapper, FluentValidation, Serilog
- Comunicación HTTP entre microservicios
