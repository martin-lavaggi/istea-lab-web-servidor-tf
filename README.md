# Trabajo Final — Laboratorio de Aplicaciones Web Servidor

Tres microservicios ASP.NET Core 8 (Products, Customers, Orders) con Clean
Architecture, EF Core Code First sobre SQL Server, AutoMapper, FluentValidation,
Serilog, Swagger y middleware global de excepciones. Orders se comunica por HTTP
con Products y Customers para crear órdenes y descontar stock.

## Estructura

```
src/
  Products/{Domain,Application,Infrastructure,Api}
  Customers/{Domain,Application,Infrastructure,Api}
  Orders/{Domain,Application,Infrastructure,Api}
```

Cada microservicio tiene su propia base: `ProductsDB`, `CustomersDB`, `OrdersDB`.

## Setup

Requiere .NET 8 SDK, SQL Server escuchando en `localhost,1433` con usuario `sa`
y contraseña `LabIstea2026!`, y `dotnet-ef`. Vía Docker:

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=LabIstea2026!" \
  -p 1433:1433 --name sqlserver \
  -d mcr.microsoft.com/mssql/server:2022-latest
```

Crear las bases con EF Migrations:

```bash
cd src/Products/Products.Api  && dotnet ef database update
cd ../../Customers/Customers.Api && dotnet ef database update
cd ../../Orders/Orders.Api    && dotnet ef database update --project ../Orders.Infrastructure --startup-project .
```

## Ejecutar

En tres terminales:

```bash
cd src/Products/Products.Api  && dotnet run --launch-profile http   # 5141
cd src/Customers/Customers.Api && dotnet run --launch-profile http  # 5150
cd src/Orders/Orders.Api      && dotnet run --launch-profile http   # 5032
```

Swagger en `http://localhost:<puerto>/swagger`. El flujo de prueba es: crear un
customer y uno o más productos, después `POST /api/order` con `customerId` y
los `items`.
