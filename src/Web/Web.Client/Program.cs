using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Web.Client;
using Web.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var productsApiUrl = builder.Configuration["Services:ProductsApi"];
if (productsApiUrl == null)
{
    throw new InvalidOperationException("Falta la URL 'Services:ProductsApi' en appsettings.json.");
}
builder.Services.AddHttpClient<ProductsApiClient>(client =>
{
    client.BaseAddress = new Uri(productsApiUrl);
});

var customersApiUrl = builder.Configuration["Services:CustomersApi"];
if (customersApiUrl == null)
{
    throw new InvalidOperationException("Falta la URL 'Services:CustomersApi' en appsettings.json.");
}
builder.Services.AddHttpClient<CustomersApiClient>(client =>
{
    client.BaseAddress = new Uri(customersApiUrl);
});

var ordersApiUrl = builder.Configuration["Services:OrdersApi"];
if (ordersApiUrl == null)
{
    throw new InvalidOperationException("Falta la URL 'Services:OrdersApi' en appsettings.json.");
}
builder.Services.AddHttpClient<OrdersApiClient>(client =>
{
    client.BaseAddress = new Uri(ordersApiUrl);
});

await builder.Build().RunAsync();
